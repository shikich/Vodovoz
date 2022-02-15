﻿using QS.Attachments.ViewModels.Widgets;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Services;
using QS.ViewModels;
using System;
using System.Linq;
using System.Threading;
using NLog;
using QS.Dialog.ViewModels;
using QS.Navigation;
using Vodovoz.Controllers;
using Vodovoz.Domain.Logistic.Cars;
using Vodovoz.Factories;
using Vodovoz.Services;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Factories;
using Vodovoz.ViewModels.TempAdapters;
using Vodovoz.ViewModels.Widgets.Cars;

namespace Vodovoz.ViewModels.ViewModels.Logistic
{
	public class CarViewModel : EntityTabViewModelBase<Car>
	{
		private readonly IRouteListsWageController _routeListsWageController;
		private static readonly Logger _logger = LogManager.GetCurrentClassLogger();
		private const string _canChangeBottlesFromAddressPermissionName = "can_change_cars_bottles_from_address";
		private bool _canChangeBottlesFromAddress;

		private AttachmentsViewModel _attachmentsViewModel;
		private string _driverInfoText;

		public CarViewModel(
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory unitOfWorkFactory,
			ICommonServices commonServices,
			IEmployeeJournalFactory employeeJournalFactory,
			IAttachmentsViewModelFactory attachmentsViewModelFactory,
			ICarModelJournalFactory carModelJournalFactory,
			ICarVersionsViewModelFactory carVersionsViewModelFactory,
			IRouteListsWageController routeListsWageController,
			IGeographicGroupParametersProvider geographicGroupParametersProvider,
			INavigationManager navigationManager)
			: base(uowBuilder, unitOfWorkFactory, commonServices, navigationManager)
		{
			if(navigationManager == null)
			{
				throw new ArgumentNullException(nameof(navigationManager));
			}
			_routeListsWageController = routeListsWageController ?? throw new ArgumentNullException(nameof(routeListsWageController));
			CarModelJournalFactory = carModelJournalFactory ?? throw new ArgumentNullException(nameof(carModelJournalFactory));

			TabName = "Автомобиль";

			EmployeeJournalFactory = employeeJournalFactory;
			AttachmentsViewModel = attachmentsViewModelFactory.CreateNewAttachmentsViewModel(Entity.ObservableAttachments);
			CarVersionsViewModel = carVersionsViewModelFactory?.CreateCarVersionsViewModel(Entity)
				?? throw new ArgumentNullException(nameof(carVersionsViewModelFactory));

			CanChangeBottlesFromAddress = commonServices.PermissionService.ValidateUserPresetPermission(
				_canChangeBottlesFromAddressPermissionName,
				commonServices.UserService.CurrentUserId
			);
			EastGeographicGroupId =
				(geographicGroupParametersProvider ?? throw new ArgumentNullException(nameof(geographicGroupParametersProvider)))
				.EastGeographicGroupId;

			CanEditCarModel = commonServices.CurrentPermissionService.ValidateEntityPermission(typeof(CarModel)).CanUpdate;
			CanChangeCarModel = Entity.Id == 0 || commonServices.CurrentPermissionService.ValidatePresetPermission("can_change_car_model");
			CanEditFuelCardNumber = commonServices.CurrentPermissionService.ValidatePresetPermission("can_change_fuel_card_number");
			ValidationContext.ServiceContainer.AddService(typeof(ICommonServices), commonServices);

			Entity.PropertyChanged += (sender, args) =>
			{
				if(args.PropertyName == nameof(Entity.Driver) && Entity.Driver != null)
				{
					OnDriverChanged();
				}
			};
			OnDriverChanged();
		}

		public string DriverInfoText
		{
			get => _driverInfoText;
			set => SetField(ref _driverInfoText, value);
		}

		public bool CanChangeBottlesFromAddress
		{
			get => _canChangeBottlesFromAddress;
			set => SetField(ref _canChangeBottlesFromAddress, value);
		}

		public AttachmentsViewModel AttachmentsViewModel
		{
			get => _attachmentsViewModel;
			set => SetField(ref _attachmentsViewModel, value);
		}

		public int EastGeographicGroupId { get; }
		public bool CanEditCarModel { get; }
		public bool CanChangeCarModel { get; }
		public bool CanEditFuelCardNumber { get; }
		public IEmployeeJournalFactory EmployeeJournalFactory { get; }
		public ICarModelJournalFactory CarModelJournalFactory { get; }
		public CarVersionsViewModel CarVersionsViewModel { get; }

		public override bool Save(bool close)
		{
			var routeLists = CarVersionsViewModel.GetAllAffectedRouteLists(UoW);
			if(!routeLists.Any())
			{
				return base.Save(close);
			}

			if(!CommonServices.InteractiveService.Question($"Будет пересчитана зарплата в {routeLists.Count} МЛ. Продолжить?"))
			{
				return false;
			}

			_logger.Info("Запущен пересчёт зарплаты в МЛ");

			IPage<ProgressWindowViewModel> progressWindow = null;
			var cts = new CancellationTokenSource();
			bool isProgressWindowClosed = false;

			void OnProgressWindowClosed(object sender, PageClosedEventArgs args)
			{
				cts.Cancel();
				isProgressWindowClosed = true;
			}

			try
			{
				progressWindow = NavigationManager.OpenViewModel<ProgressWindowViewModel>(this);
				progressWindow.PageClosed += OnProgressWindowClosed;
				var progressBarDisplayable = progressWindow.ViewModel.Progress;

				_routeListsWageController.ProgressBarDisplayable = progressBarDisplayable;
				_routeListsWageController.RecalculateRouteListsWage(UoW, routeLists, cts.Token);
				_logger.Info("Пересчёт зарплаты в МЛ завершён");

				progressBarDisplayable.Update("Сохранение...");

				foreach(var routeList in routeLists)
				{
					UoW.Save(routeList);
				}
				return base.Save(close);
			}
			catch(OperationCanceledException)
			{
				_logger.Debug("Пересчёт зарплаты в МЛ был отменён");
				return false;
			}
			finally
			{
				if(progressWindow != null)
				{
					progressWindow.PageClosed -= OnProgressWindowClosed;
					if(!isProgressWindowClosed)
					{
						NavigationManager.ForceClosePage(progressWindow);
					}
				}
			}
		}

		private void OnDriverChanged()
		{
			if(Entity.Driver == null)
			{
				return;
			}

			var docs = Entity.Driver.GetMainDocuments();
			if(docs.Any())
			{
				DriverInfoText =
					$"\tПаспорт: {docs.First().PassportSeria} № {docs.First().PassportNumber}\n" +
					$"\tАдрес регистрации: {Entity.Driver.AddressRegistration}";
			}
			else
			{
				DriverInfoText = "Главный документ отсутствует";
			}
		}
	}
}
