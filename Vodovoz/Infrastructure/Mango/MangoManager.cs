﻿using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using ClientMangoService;
using ClientMangoService.Commands;
using Gtk;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Services;
using QS.Utilities;
using Vodovoz.Core.DataService;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Contacts;
using Vodovoz.Domain.Employees;
using Vodovoz.Infrastructure.Services;
using Vodovoz.Repositories.Client;
using Vodovoz.ViewModels.Mango;

namespace Vodovoz.Infrastructure.Mango
{
	public class MangoManager : PropertyChangedBase, IDisposable
	{
		private readonly Gtk.Action toolbarIcon;
		private readonly IUnitOfWorkFactory unitOfWorkFactory;
		private readonly IEmployeeService employeeService;
		private readonly IUserService userService;
		private readonly INavigationManager navigation;
		private ConnectionState connectionState;
		private uint extension;
		private MangoNotificationClient notificationClient;
		private readonly CancellationTokenSource notificationCancellation = new CancellationTokenSource();
		private NotificationMessage LastMessage;
		private IPage CurrentPage;
		private uint timer;
		private MangoController mangoController;

		public MangoManager(Gtk.Action toolbarIcon,
			IUnitOfWorkFactory unitOfWorkFactory,
			IEmployeeService employeeService,
			IUserService userService,
			INavigationManager navigation,
			BaseParametersProvider parametrs)
		{
			this.toolbarIcon = toolbarIcon;
			this.unitOfWorkFactory = unitOfWorkFactory ?? throw new ArgumentNullException(nameof(unitOfWorkFactory));
			this.employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
			this.userService = userService ?? throw new ArgumentNullException(nameof(userService));
			this.navigation = navigation ?? throw new ArgumentNullException(nameof(navigation));
 			this.mangoController = new MangoController(parametrs.VpbxApiKey, parametrs.VpbxApiSalt);

			timer = GLib.Timeout.Add (1000, new GLib.TimeoutHandler(HandleTimeoutHandler));
			toolbarIcon.Activated += ToolbarIcon_Activated;
		}

		public ConnectionState ConnectionState {
			#region Current State
			get => connectionState; private set {
				connectionState = value;
				var iconName = $"phone-{value.ToString().ToLower()}";
				toolbarIcon.StockId = iconName;
				if(ConnectionState == ConnectionState.Connected || ConnectionState == ConnectionState.Disconnected)
					toolbarIcon.ShortLabel = extension.ToString();
				else
					toolbarIcon.ShortLabel = "Mango";
				GtkHelper.WaitRedraw();
			}
		}

		public DateTime? StageBegin => LastMessage?.Timestamp.ToDateTime();
		public TimeSpan? StageDuration => DateTime.Now - StageBegin;

		public string CallerName => LastMessage?.CallFrom.Name;
		//List<object> FoundByPhoneItems;
		public string CallerNumber => LastMessage?.CallFrom.Number;


		#endregion

		#region Методы

		public void Connect()
		{
			using(var uow = unitOfWorkFactory.CreateWithoutRoot("MangoManager Connect")) {
				var employee = employeeService.GetEmployeeForUser(uow, userService.CurrentUserId);
				if(employee?.InnerPhone == null) {
					ConnectionState = ConnectionState.Disable;
					return;
				}

				extension = employee.InnerPhone.Value;
				ConnectionState = ConnectionState.Disconnected;
				notificationClient = new MangoNotificationClient(extension, notificationCancellation.Token);
				notificationClient.ChanalStateChanged+= NotificationClient_ChanalStateChanged;
				ConnectionState = notificationClient.IsNotificationActive ? ConnectionState.Connected : ConnectionState.Disconnected;
				notificationClient.IncomeCall += NotificationClientOnIncomeCall;
			}
		}

		#endregion
		#region Обработка событий

		private void NotificationClientOnIncomeCall(object sender, IncomeCallEventArgs e)
		{
			LastMessage = e.Message;
			Application.Invoke((s, arg) => HandleMessage(LastMessage));
		}

		void NotificationClient_ChanalStateChanged(object sender, ConnectionStateEventArgs e)
		{
			Gtk.Application.Invoke(delegate {
				ConnectionState = notificationClient.IsNotificationActive ? ConnectionState.Connected : ConnectionState.Disconnected;
			});
		}

		void ToolbarIcon_Activated(object sender, EventArgs e)
		{
			if(LastMessage != null && CurrentPage == null)
				HandleMessage(LastMessage);
			//FIXME Здесь еще открываем диалог исходящего вызова, если текущего диалога не происходит.
		}

		void CurrentPage_PageClosed(object sender, PageClosedEventArgs e)
		{
			CurrentPage = null;
		}

		#endregion
		#region Private

		List<Counterparty> clients = null;
		public List<Counterparty> Clients { get => clients; private set => clients = value; }
		Employee employee = null;
		public Employee Employee { get => employee; private set => employee = value; }
		List<DeliveryPoint> deliveryPoints = null;
		public List<DeliveryPoint> DeliveryPoints { get => deliveryPoints;private set => deliveryPoints = value; }

		private void FoundByPhoneItemsConfigure()
		{
			var _list= PhoneRepository.GetObjectByPhone(CallerNumber, unitOfWorkFactory.CreateWithoutRoot()) as ArrayList;
			if(_list != null)
			foreach(var item in _list) {
				if(item.GetType() == typeof(Counterparty)) {
						if(clients == null)
							clients = new List<Counterparty>();
						clients.Add(item as Counterparty);

					}
				else if(item.GetType() == typeof(Employee) && employee != null) {
						employee = item as Employee;
					}
				else if(item.GetType() == typeof(DeliveryPoint)) {
						if(deliveryPoints == null)
							deliveryPoints = new List<DeliveryPoint>();
						deliveryPoints.Add(item as DeliveryPoint);
					}
			}
		}

		bool HandleTimeoutHandler()
		{
			if(LastMessage != null)
				OnPropertyChanged(nameof(StageDuration));
			return true;
		}

		private void HandleMessage(NotificationMessage message)
		{
			if(CurrentPage != null) {
				navigation.ForceClosePage(CurrentPage);
			}

			if(message.State == CallState.Appeared) {
				//FoundByPhoneItemsConfigure();
				CurrentPage = navigation.OpenViewModel<IncomingCallViewModel, MangoManager>(null, this);
			}

			if(message.State == CallState.Connected) {
				if(message.CallFrom.Type == CallerType.Internal) {
					CurrentPage = navigation.OpenViewModel<InternalCallViewModel, MangoManager>(null,this);
				 }
				else
				{
					if(clients != null)
						CurrentPage = navigation.OpenViewModel<CounterpartyTalkViewModel, MangoManager, IEnumerable<Counterparty>>(null, this, clients);
					else
						CurrentPage = navigation.OpenViewModel<UnknowTalkViewModel, Phone>(null, new Phone() { Number = CallerNumber });
				}
			}

			if(CurrentPage != null)
				CurrentPage.PageClosed += CurrentPage_PageClosed;

			if(message.State == CallState.Disconnected) {
				LastMessage = null;
			}
		}

		#endregion
		#region MangoController_Methods

		public void HangUp()
		{
			//mangoController.HangUp(LastMessage.CallId);
			mangoController.HangUp("100500");

		}

		public IEnumerable<ClientMangoService.DTO.Users.User> GetAllVPBXEmploies()
		{
			return mangoController.GetAllVPBXEmploies();
		}


		#endregion
		public void Dispose()
		{
			notificationCancellation.Cancel();
			notificationClient.Dispose();
			GLib.Source.Remove(timer);
		}

	}

	public enum ConnectionState
	{
		Connected,
		Disable,
		Disconnected,
		Ring,
		Talk
	}
}
