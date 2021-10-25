using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using Autofac;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Orders;
using QS.Commands;
using QS.Navigation;
using QS.Project.Journal;
using QS.ViewModels.Control.EEVM;
using Vodovoz.Core.DataService;
using Vodovoz.Domain.WageCalculation.CalculationServices.RouteList;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Undeliveries;
using Vodovoz.EntityRepositories.WageCalculation;
using Vodovoz.FilterViewModels.Employees;
using Vodovoz.Infrastructure.Services;
using Vodovoz.Journals.JournalViewModels.Employees;
using Vodovoz.Parameters;
using Vodovoz.ViewModels.Employees;
using Vodovoz.ViewModels.Journals.Filters.Orders;
using Vodovoz.ViewModels.Journals.JournalViewModels.Orders;

namespace Vodovoz.ViewModels.Logistic
{
	public class RouteListAnalysisViewModel : EntityTabViewModelBase<RouteList>
	{
		private readonly WageParameterService _wageParameterService =
			new WageParameterService(new WageCalculationRepository(), new BaseParametersProvider(new ParametersProvider()));

		#region Constructor

		public RouteListAnalysisViewModel(
			IEntityUoWBuilder uowBuilder,
			IUnitOfWorkFactory unitOfWorkFactory, 
			ICommonServices commonServices,
			IDeliveryShiftRepository deliveryShiftRepository,
			IUndeliveredOrdersRepository undeliveredOrdersRepository,
			ILifetimeScope scope,
			INavigationManager navigationManager)
			: base (uowBuilder, unitOfWorkFactory, commonServices, navigationManager, scope)
		{
			UndeliveredOrdersRepository =
				undeliveredOrdersRepository ?? throw new ArgumentNullException(nameof(undeliveredOrdersRepository));

			if(deliveryShiftRepository == null)
			{
				throw new ArgumentNullException(nameof(deliveryShiftRepository));
			}

			DeliveryShifts = deliveryShiftRepository.ActiveShifts(UoW);
			Entity.ObservableAddresses.PropertyOfElementChanged += ObservableAddressesOnPropertyOfElementChanged;
			
			CurrentEmployee = Scope.Resolve<IEmployeeService>().GetEmployeeForUser(UoW, CurrentUser.Id);
			
			if(CurrentEmployee == null) {
				AbortOpening("Ваш пользователь не привязан к действующему сотруднику, вы не можете открыть " +
				             "диалог разбора МЛ, так как некого указывать в качестве логиста.", "Невозможно открыть разбор МЛ");
			}
			
			//LogisticanSelectorFactory = _employeeJournalFactory.CreateWorkingOfficeEmployeeAutocompleteSelectorFactory();
			//DriverSelectorFactory = _employeeJournalFactory.CreateWorkingDriverEmployeeAutocompleteSelectorFactory();
			//ForwarderSelectorFactory = _employeeJournalFactory.CreateWorkingForwarderEmployeeAutocompleteSelectorFactory();

			TabName = $"Диалог разбора {Entity.Title}";
		}
		
		#endregion
		
		#region Properties

		public IEntityEntryViewModel LogisticanSelectorFactory { get; }
		public IEntityEntryViewModel DriverSelectorFactory { get; }
		public IEntityEntryViewModel ForwarderSelectorFactory { get; }
		public IUndeliveredOrdersRepository UndeliveredOrdersRepository { get; }

		public readonly IList<DeliveryShift> DeliveryShifts;
		
		public Employee CurrentEmployee { get; }

		public RouteListItem SelectedItem { get; set; }

		#endregion

		public Action UpdateTreeAddresses;
		
		private void ObservableAddressesOnPropertyOfElementChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName == nameof(LateArrivalReason))
				SelectedItem.LateArrivalReasonAuthor = CurrentEmployee;
		}

		#region Commands

		private DelegateCommand openOrderCommand;
		public DelegateCommand OpenOrderCommand => openOrderCommand ?? (openOrderCommand = new DelegateCommand(
			() => {
				var dlg = new OrderDlg(SelectedItem.Order) {
					HasChanges = false
				};

				dlg.SetDlgToReadOnly();
				TabParent.AddSlaveTab(this, dlg);

			}, () => SelectedItem != null
		));
		
		private DelegateCommand openUndeliveredOrderCommand;
		public DelegateCommand OpenUndeliveredOrderCommand => 
			openUndeliveredOrderCommand ?? (openUndeliveredOrderCommand = new DelegateCommand(
				() =>
				{
					/*var undeliveredOrdersFilter = new UndeliveredOrdersFilterViewModel(
						_orderSelectorFactory,
						_employeeJournalFactory,
						_counterpartyJournalFactory,
						_deliveryPointJournalFactory,
						_subdivisionJournalFactory)
					{
						HidenByDefault = true,
						RestrictOldOrder = SelectedItem.Order,
						RestrictOldOrderStartDate = SelectedItem.Order.DeliveryDate,
						RestrictOldOrderEndDate = SelectedItem.Order.DeliveryDate
					};

					var dlg = new UndeliveredOrdersJournalViewModel(
						undeliveredOrdersFilter,
						UnitOfWorkFactory,
						_commonServices,
						_gtkDialogsOpener,
						employeeService,
						_undeliveredOrdersJournalOpener,
						UndeliveredOrdersRepository);

					dlg.TabClosed += (s,e) => UpdateTreeAddresses?.Invoke();
					
					TabParent.AddTab(dlg, this);*/
					var filterParams = new UndeliveredOrdersFilterViewModelParameters(new CustomUndeliveredOrdersFilterParameters(
						new Action<UndeliveredOrdersFilterViewModel>[]
						{
							x => x.HidenByDefault = true,
							x => x.RestrictOldOrder = SelectedItem.Order,
							x => x.RestrictOldOrderStartDate = SelectedItem.Order.DeliveryDate,
							x => x.RestrictOldOrderEndDate = SelectedItem.Order.DeliveryDate
						},
						true));
					var page = NavigationManager.OpenViewModel<UndeliveredOrdersJournalViewModel, UndeliveredOrdersFilterViewModelParameters>(
						this, filterParams, OpenPageOptions.IgnoreHash);
					page.ViewModel.TabClosed += (s, args) => UpdateTreeAddresses?.Invoke();
				}, 
				() => SelectedItem != null
			)
		);
		
		private DelegateCommand createFineCommand;
		public DelegateCommand CreateFineCommand => createFineCommand ?? (createFineCommand = new DelegateCommand(
			() => {
				
				/*var fineViewModel = new FineViewModel(
					EntityUoWBuilder.ForCreate(),
					QS.DomainModel.UoW.UnitOfWorkFactory.GetDefaultFactory,
					undeliveryViewOpener,
					employeeService,
					_employeeJournalFactory.CreateEmployeeAutocompleteSelectorFactory(),
					CommonServices
				);

				fineViewModel.RouteList = SelectedItem.RouteList;

				var undeliveredOrder = GetUndeliveredOrder();

				if (undeliveredOrder != null)
				{
					fineViewModel.UndeliveredOrder = undeliveredOrder;
				}

				if(SelectedItem.CalculateTimeLateArrival() != null)
					fineViewModel.FineReasonString = $"Опоздание по заказу №{SelectedItem.Order.Id} от {SelectedItem.Order.DeliveryDate:d}";

				fineViewModel.EntitySaved += (sender, args) =>
				{
					SelectedItem.AddFine(args.Entity as Fine);
					UpdateTreeAddresses?.Invoke();
				}; 
				
				TabParent.AddSlaveTab(this, fineViewModel);*/
				var page = NavigationManager.OpenViewModel<FineViewModel, IEntityUoWBuilder>(
					this, EntityUoWBuilder.ForCreate(), OpenPageOptions.AsSlave);
				
				page.ViewModel.RouteList = SelectedItem.RouteList;

				var undeliveredOrder = GetUndeliveredOrder();

				if (undeliveredOrder != null)
				{
					page.ViewModel.UndeliveredOrder = undeliveredOrder;
				}

				if(SelectedItem.CalculateTimeLateArrival() != null)
				{
					page.ViewModel.FineReasonString =
						$"Опоздание по заказу №{SelectedItem.Order.Id} от {SelectedItem.Order.DeliveryDate:d}";
				}

				page.ViewModel.EntitySaved += (sender, args) =>
				{
					SelectedItem.AddFine(args.Entity as Fine);
					UpdateTreeAddresses?.Invoke();
				};
			}, () => SelectedItem != null
		));
		
		private DelegateCommand attachFineCommand;
		public DelegateCommand AttachFineCommand => attachFineCommand ?? (attachFineCommand = new DelegateCommand(
			() =>
			{
				/*var fineFilter = new FineFilterViewModel();
				fineFilter.ExcludedIds = SelectedItem.Fines.Select(x => x.Id).ToArray();
				var fineJournalViewModel = new FinesJournalViewModel(
					fineFilter,
					undeliveryViewOpener,
					employeeService,
					_employeeJournalFactory.CreateEmployeeAutocompleteSelectorFactory(),
					UnitOfWorkFactory,
					CommonServices
				);
				fineJournalViewModel.SelectionMode = JournalSelectionMode.Single;
				fineJournalViewModel.OnEntitySelectedResult +=
					(sender, e) =>
					{
						var selectedNode = e.SelectedNodes.FirstOrDefault();
						
						if (selectedNode == null)
							return;
						
						var fine = UoW.GetById<Fine>(selectedNode.Id);

						var undeliveredOrder = GetUndeliveredOrder();
						if (undeliveredOrder != null)
						{
							fine.UndeliveredOrder = undeliveredOrder;
							UoW.Save(fine);
						}

						SelectedItem.AddFine(fine);
						UpdateTreeAddresses?.Invoke();
					};
				TabParent.AddSlaveTab(this, fineJournalViewModel);*/
				var filterParams = new Action<FineFilterViewModel>[]
				{
					x => x.ExcludedIds = SelectedItem.Fines.Select(fine => fine.Id).ToArray()
				};
				var page = NavigationManager.OpenViewModel<FinesJournalViewModel, Action<FineFilterViewModel>[]>(
					this, filterParams, OpenPageOptions.AsSlave);
				
				page.ViewModel.SelectionMode = JournalSelectionMode.Single;
				page.ViewModel.OnEntitySelectedResult +=
					(sender, e) =>
					{
						var selectedNode = e.SelectedNodes.FirstOrDefault();
						
						if (selectedNode == null)
							return;
						
						var fine = UoW.GetById<Fine>(selectedNode.Id);

						var undeliveredOrder = GetUndeliveredOrder();
						if (undeliveredOrder != null)
						{
							fine.UndeliveredOrder = undeliveredOrder;
							UoW.Save(fine);
						}

						SelectedItem.AddFine(fine);
						UpdateTreeAddresses?.Invoke();
					};
			}, () => SelectedItem != null
		));
		
		private DelegateCommand detachAllFinesCommand;
		public DelegateCommand DetachAllFinesCommand => 
			detachAllFinesCommand ?? (detachAllFinesCommand = new DelegateCommand(
			() =>
			{
				SelectedItem.RemoveAllFines();
				UpdateTreeAddresses?.Invoke();
			},
			() => SelectedItem != null
		));
		
		private DelegateCommand createDetachFineCommand;

		public DelegateCommand DetachFineCommand => 
			createDetachFineCommand ?? (createDetachFineCommand = new DelegateCommand(
			() =>
			{
				/*var fineFilter = new FineFilterViewModel();
				fineFilter.FindFinesWithIds = SelectedItem.Fines.Select(x => x.Id).ToArray();
				var fineJournalViewModel = new FinesJournalViewModel(
					fineFilter,
					undeliveryViewOpener,
					employeeService,
					_employeeJournalFactory.CreateEmployeeAutocompleteSelectorFactory(),
					UnitOfWorkFactory,
					CommonServices
				);
				fineJournalViewModel.SelectionMode = JournalSelectionMode.Single;
				fineJournalViewModel.OnEntitySelectedResult +=
					(sender, e) =>
					{
						var selectedNode = e.SelectedNodes.FirstOrDefault();
						
						if (selectedNode == null)
							return;
						
						var fine = UoW.GetById<Fine>(selectedNode.Id);

						SelectedItem.RemoveFine(fine);
						UpdateTreeAddresses?.Invoke();
					};
				TabParent.AddSlaveTab(this, fineJournalViewModel);*/
				var filterParams = new Action<FineFilterViewModel>[]
				{
					x => x.FindFinesWithIds = SelectedItem.Fines.Select(fine => fine.Id).ToArray()
				};
				var page = NavigationManager.OpenViewModel<FinesJournalViewModel, Action<FineFilterViewModel>[]>(
					this, filterParams, OpenPageOptions.AsSlave);
				
				page.ViewModel.SelectionMode = JournalSelectionMode.Single;
				page.ViewModel.OnEntitySelectedResult +=
					(sender, e) =>
					{
						var selectedNode = e.SelectedNodes.FirstOrDefault();
						
						if (selectedNode == null)
							return;
						
						var fine = UoW.GetById<Fine>(selectedNode.Id);

						SelectedItem.RemoveFine(fine);
						UpdateTreeAddresses?.Invoke();
					};
			}, () => SelectedItem != null
		));
		 
		#endregion

		public string UpdateBottlesSummaryInfo()
		{
			string bottles = null;
			int completedBottles = Entity.Addresses.Where(x => x != null && x.Status == RouteListItemStatus.Completed)
												   .Sum(x => x.Order.Total19LBottlesToDeliver);
			int canceledBottles = Entity.Addresses.Where(
				x => x != null && (x.Status == RouteListItemStatus.Canceled
				                   || x.Status == RouteListItemStatus.Overdue
				                   || x.Status == RouteListItemStatus.Transfered)
			).Sum(x => x.Order.Total19LBottlesToDeliver);
			int enrouteBottles = Entity.Addresses.Where(x => x != null && x.Status == RouteListItemStatus.EnRoute)
												 .Sum(x => x.Order.Total19LBottlesToDeliver);
			bottles = "<b>Всего 19л. бутылей в МЛ:</b>\n";
			bottles += $"Выполнено: <b>{completedBottles}</b>\n";
			bottles += $" Отменено: <b>{canceledBottles}</b>\n";
			bottles += $" Осталось: <b>{enrouteBottles}</b>\n";
			
			return bottles;
		}

		private UndeliveredOrder GetUndeliveredOrder() =>
			UndeliveredOrdersRepository.GetListOfUndeliveriesForOrder(UoW, SelectedItem.Order.Id).SingleOrDefault();

		public void SetLogisticianCommentAuthor()
		{
			if(!string.IsNullOrEmpty(Entity.LogisticiansComment))
				Entity.LogisticiansCommentAuthor = CurrentEmployee;
		}

		public void CalculateWages() =>
			Entity.CalculateWages(_wageParameterService);
	}
}
