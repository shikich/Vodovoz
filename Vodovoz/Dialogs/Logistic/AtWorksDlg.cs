﻿using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using Gdk;
using Gtk;
using QS.Dialog;
using QS.Dialog.Gtk;
using QS.Dialog.GtkUI;
using QS.DomainModel.UoW;
using QS.Project.Domain;
using QS.Project.Journal;
using QS.Project.Services;
using QS.Services;
using QS.Tdi;
using QSOrmProject;
using Vodovoz.Core.DataService;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Service.BaseParametersServices;
using Vodovoz.EntityRepositories;
using Vodovoz.EntityRepositories.Employees;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Sale;
using Vodovoz.EntityRepositories.Stock;
using Vodovoz.EntityRepositories.Store;
using Vodovoz.EntityRepositories.WageCalculation;
using Vodovoz.Factories;
using Vodovoz.Filters.ViewModels;
using Vodovoz.JournalViewModels;
using Vodovoz.Parameters;
using Vodovoz.Services;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Infrastructure.Services;
using Vodovoz.ViewModels.Journals.FilterViewModels.Employees;
using Vodovoz.ViewModels.Journals.JournalFactories;
using Vodovoz.ViewModels.Journals.JournalSelectors;
using Vodovoz.ViewModels.TempAdapters;
using Vodovoz.ViewModels.ViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Logistic;
using VodovozInfrastructure.Endpoints;

namespace Vodovoz.Dialogs.Logistic
{
	public partial class AtWorksDlg : TdiTabBase, ITdiDialog, ISingleUoWDialog
	{
		private static readonly BaseParametersProvider _baseParametersProvider = new BaseParametersProvider(new ParametersProvider());
		
		private readonly IEmployeeJournalFactory _employeeJournalFactory;
		private readonly DriverApiUserRegisterEndpoint _driverApiRegistrationEndpoint;
		private readonly IAuthorizationService _authorizationService = new AuthorizationServiceFactory().CreateNewAuthorizationService();
		private readonly IEmployeeWageParametersFactory _employeeWageParametersFactory = new EmployeeWageParametersFactory();
		private readonly ISubdivisionJournalFactory _subdivisionJournalFactory = new SubdivisionJournalFactory();
		private readonly IEmployeePostsJournalFactory _employeePostsJournalFactory = new EmployeePostsJournalFactory();
		private readonly ICashDistributionCommonOrganisationProvider _cashDistributionCommonOrganisationProvider =
			new CashDistributionCommonOrganisationProvider(new OrganizationParametersProvider(new ParametersProvider()));
		private readonly ISubdivisionService _subdivisionService = SubdivisionParametersProvider.Instance;
		private readonly IEmailServiceSettingAdapter _emailServiceSettingAdapter = new EmailServiceSettingAdapter();
		private readonly IWageCalculationRepository _wageCalculationRepository  = new WageCalculationRepository();
		private readonly IEmployeeRepository _employeeRepository = new EmployeeRepository();
		private readonly IValidationContextFactory _validationContextFactory = new ValidationContextFactory();
		private readonly IPhonesViewModelFactory _phonesViewModelFactory = new PhonesViewModelFactory(new PhoneRepository());
		private readonly IUserRepository _userRepository = new UserRepository();
		private readonly ICarRepository _carRepository = new CarRepository();
		private readonly IGeographicGroupRepository _geographicGroupRepository = new GeographicGroupRepository();
		private readonly IScheduleRestrictionRepository _scheduleRestrictionRepository = new ScheduleRestrictionRepository();
		private readonly IWarehouseRepository _warehouseRepository = new WarehouseRepository();
        private readonly IRouteListRepository _routeListRepository = new RouteListRepository(new StockRepository(), _baseParametersProvider);
        private readonly IAttachmentsViewModelFactory _attachmentsViewModelFactory = new AttachmentsViewModelFactory();
        private readonly EmployeeFilterViewModel _forwarderFilter;

		public AtWorksDlg(
			IDefaultDeliveryDayScheduleSettings defaultDeliveryDayScheduleSettings,
			IEmployeeJournalFactory employeeJournalFactory,
			DriverApiUserRegisterEndpoint driverApiUserRegisterEndpoint)
		{
			if(defaultDeliveryDayScheduleSettings == null)
			{
				throw new ArgumentNullException(nameof(defaultDeliveryDayScheduleSettings));
			}

			_employeeJournalFactory = employeeJournalFactory ?? throw new ArgumentNullException(nameof(employeeJournalFactory));
			_driverApiRegistrationEndpoint = driverApiUserRegisterEndpoint ?? throw new ArgumentNullException(nameof(driverApiUserRegisterEndpoint));
			this.Build();

			var colorWhite = new Color(0xff, 0xff, 0xff);
			var colorLightRed = new Color(0xff, 0x66, 0x66);
			ytreeviewAtWorkDrivers.ColumnsConfig = FluentColumnsConfig<AtWorkDriver>.Create()
				.AddColumn("Приоритет")
					.AddNumericRenderer(x => x.PriorityAtDay)
					.Editing(new Gtk.Adjustment(6, 1, 10, 1, 1, 1))
				.AddColumn("Статус")
					.AddTextRenderer(x => x.Status.GetEnumTitle())
				.AddColumn("Причина")
					.AddTextRenderer(x => x.Reason)
						.AddSetter((cell, driver) => cell.Editable = driver.Status == AtWorkDriver.DriverStatus.NotWorking)
				.AddColumn("Водитель")
					.AddTextRenderer(x => x.Employee.ShortName)
				.AddColumn("Скор.")
					.AddTextRenderer(x => x.Employee.DriverSpeed.ToString("P0"))
				.AddColumn("График работы")
					.AddComboRenderer(x => x.DaySchedule)
					.SetDisplayFunc(x => x.Name)
					.FillItems(UoW.GetAll<DeliveryDaySchedule>().ToList())
					.Editing()
				.AddColumn("Оконч. работы")
					.AddTextRenderer(x => x.EndOfDayText).Editable()
				.AddColumn("Экспедитор")
					.AddComboRenderer(x => x.WithForwarder)
					.SetDisplayFunc(x => x.Employee.ShortName).Editing().Tag(Columns.Forwarder)
				.AddColumn("Автомобиль")
					.AddPixbufRenderer(x => x.Car != null && x.Car.IsCompanyCar ? vodovozCarIcon : null)
					.AddTextRenderer(x => x.Car != null ? x.Car.RegistrationNumber : "нет")
				.AddColumn("База")
					.AddComboRenderer(x => x.GeographicGroup)
					.SetDisplayFunc(x => x.Name)
					.FillItems(_geographicGroupRepository.GeographicGroupsWithCoordinates(UoW))
					.AddSetter(
						(c, n) => {
							c.Editable = true;
							c.BackgroundGdk = n.GeographicGroup == null
								? colorLightRed
								: colorWhite;
						}
					)
				.AddColumn("Грузоп.")
					.AddTextRenderer(x => x.Car != null ? x.Car.MaxWeight.ToString("D") : null)
				.AddColumn("Районы доставки")
					.AddTextRenderer(x => string.Join(", ", x.DistrictsPriorities.Select(d => d.District.DistrictName)))
				.AddColumn("")
				.AddColumn("Комментарий")
					.AddTextRenderer(x => x.Comment)
						.Editable(true)
				.RowCells().AddSetter<CellRendererText>((c, n) => c.Foreground = n.Status == AtWorkDriver.DriverStatus.NotWorking? "gray": "black")
				.Finish();

			ytreeviewAtWorkDrivers.Selection.Mode = Gtk.SelectionMode.Multiple;
			ytreeviewAtWorkDrivers.Selection.Changed += YtreeviewDrivers_Selection_Changed;

			ytreeviewOnDayForwarders.ColumnsConfig = FluentColumnsConfig<AtWorkForwarder>.Create()
				.AddColumn("Экспедитор").AddTextRenderer(x => x.Employee.ShortName)
				.AddColumn("Едет с водителем").AddTextRenderer(x => RenderForwaderWithDriver(x))
				.Finish();
			ytreeviewOnDayForwarders.Selection.Mode = Gtk.SelectionMode.Multiple;
			ytreeviewOnDayForwarders.Selection.Changed += YtreeviewForwarders_Selection_Changed;
			
			ydateAtWorks.Date = DateTime.Today;
			
			int currentUserId = ServicesConfig.CommonServices.UserService.CurrentUserId;
			canReturnDriver = ServicesConfig.CommonServices.PermissionService.ValidateUserPresetPermission("can_return_driver_to_work", currentUserId);

			this.defaultDeliveryDaySchedule =
				UoW.GetById<DeliveryDaySchedule>(defaultDeliveryDayScheduleSettings.GetDefaultDeliveryDayScheduleId());

			_forwarderFilter = new EmployeeFilterViewModel();
			_forwarderFilter.SetAndRefilterAtOnce(
				x => x.RestrictCategory = EmployeeCategory.forwarder,
				x => x.CanChangeStatus = true,
				x => x.Status = EmployeeStatus.IsWorking);
		}
		
		private static readonly NLog.Logger logger = NLog.LogManager.GetCurrentClassLogger();
		
		private readonly Gdk.Pixbuf vodovozCarIcon = Pixbuf.LoadFromResource("Vodovoz.icons.buttons.vodovoz-logo.png");

		private IList<AtWorkDriver> driversAtDay;
		private IList<AtWorkForwarder> forwardersAtDay;
		private HashSet<AtWorkDriver> driversWithCommentChanged = new HashSet<AtWorkDriver>();
		private GenericObservableList<AtWorkDriver> observableDriversAtDay;
		private GenericObservableList<AtWorkForwarder> observableForwardersAtDay;
		private readonly bool canReturnDriver;
		private readonly DeliveryDaySchedule defaultDeliveryDaySchedule;

		public IUnitOfWork UoW { get; } = UnitOfWorkFactory.CreateWithoutRoot();
		public bool HasChanges => UoW.HasChanges;
		public event EventHandler<EntitySavedEventArgs> EntitySaved;
		
		private DateTime DialogAtDate => ydateAtWorks.Date;

		#region Properties

		private IList<AtWorkDriver> DriversAtDay {
			set {
				driversAtDay = value;
				observableDriversAtDay = new GenericObservableList<AtWorkDriver>(driversAtDay);
				ytreeviewAtWorkDrivers.SetItemsSource(observableDriversAtDay);
				observableDriversAtDay.PropertyOfElementChanged += (sender, args) => driversWithCommentChanged.Add(sender as AtWorkDriver);
			}
			get => driversAtDay;
		}

		private IList<AtWorkForwarder> ForwardersAtDay {
			set {
				forwardersAtDay = value;
				if(observableForwardersAtDay != null)
					observableForwardersAtDay.ListChanged -= ObservableForwardersAtDay_ListChanged;
				observableForwardersAtDay = new GenericObservableList<AtWorkForwarder>(forwardersAtDay);
				observableForwardersAtDay.ListChanged += ObservableForwardersAtDay_ListChanged;
				ytreeviewOnDayForwarders.SetItemsSource(observableForwardersAtDay);
				ObservableForwardersAtDay_ListChanged(null);
			}
			get => forwardersAtDay;
		}

		public override string TabName {
			get => $"Работают {ydateAtWorks.Date:d}";
			protected set => throw new InvalidOperationException("Установка протеворечит логике работы.");
		}

		#endregion
		

		#region Events

		#region Buttons
		protected void OnButtonSaveChangesClicked(object sender, EventArgs e)
		{
			Save();
		}
		protected void OnButtonCancelChangesClicked(object sender, EventArgs e)
		{
			UoW.Session.Clear();
			FillDialogAtDay();
		}
		
		protected void OnButtonAddWorkingDriversClicked(object sender, EventArgs e)
		{
			var workDriversAtDay = _employeeRepository.GetWorkingDriversAtDay(UoW, DialogAtDate);

			if(workDriversAtDay.Count > 0) {
				foreach(var driver in workDriversAtDay) {
					if(driversAtDay.Any(x => x.Employee.Id == driver.Id)) {
						logger.Warn($"Водитель {driver.ShortName} уже добавлен. Пропускаем...");
						continue;
					}

					var car = _carRepository.GetCarByDriver(UoW, driver);
					var daySchedule = GetDriverWorkDaySchedule(driver);

					var atwork = new AtWorkDriver(driver, DialogAtDate, car, daySchedule);
					GetDefaultForwarder(driver, atwork);

					observableDriversAtDay.Add(atwork);
				}
			}
			DriversAtDay = driversAtDay.OrderBy(x => x.Employee.ShortName).ToList();
		}
		
		protected void OnButtonAddDriverClicked(object sender, EventArgs e)
		{
			var selectDrivers = _employeeJournalFactory.CreateWorkingDriverEmployeeJournal();
			selectDrivers.SelectionMode = JournalSelectionMode.Multiple;
			selectDrivers.TabName = "Водители";
			
			selectDrivers.OnEntitySelectedResult += SelectDrivers_OnEntitySelectedResult;
			TabParent.AddSlaveTab(this, selectDrivers);
		}

		protected void OnButtonRemoveDriverClicked(object sender, EventArgs e)
		{
			var toDel = ytreeviewAtWorkDrivers.GetSelectedObjects<AtWorkDriver>();
			
			foreach(var driver in toDel)
			{
				if (driver.Id > 0)
				{
					ChangeButtonAddRemove(driver.Status == AtWorkDriver.DriverStatus.IsWorking);
					if (driver.Status == AtWorkDriver.DriverStatus.NotWorking)
					{
						if (canReturnDriver)
						{
							driver.Status = AtWorkDriver.DriverStatus.IsWorking;
						}
					}
					else
					{
						driver.Status = AtWorkDriver.DriverStatus.NotWorking;
						driver.AuthorRemovedDriver = _employeeRepository.GetEmployeeForCurrentUser(UoW);
						driver.RemovedDate = DateTime.Now;
					}
				}
				observableDriversAtDay.OnPropertyChanged(nameof(driver.Status));
			}
		}

		protected void OnButtonClearDriverScreenClicked(object sender, EventArgs e)
		{
			if (MessageDialogHelper.RunQuestionWithTitleDialog("ВНИМАНИЕ!!!",
				$"Список работающих и снятых водителей на дату: { ydateAtWorks.Date.ToShortDateString()} будет очищен\n\n" +
				"Вы действительно хотите продолжить?"))
			{
				DriversAtDay.ToList().ForEach(x => UoW.Delete(x));
				observableDriversAtDay.Clear();
				SetButtonClearDriverScreenSensitive();
			}
		}
		
		protected void OnButtonDriverSelectAutoClicked(object sender, EventArgs e)
		{
			var driver = ytreeviewAtWorkDrivers.GetSelectedObjects<AtWorkDriver>().FirstOrDefault();

			if(driver == null)
			{
				MessageDialogHelper.RunWarningDialog("Не выбран водитель!");
				return;
			}
			
			var filter = new CarJournalFilterViewModel();
			filter.SetAndRefilterAtOnce(
				x => x.RestrictedCarTypesOfUse = Car.GetCompanyHavingsTypes(),
				x => x.IncludeArchive = false);
			var journal = new CarJournalViewModel(filter, UnitOfWorkFactory.GetDefaultFactory, ServicesConfig.CommonServices);
			journal.SelectionMode = JournalSelectionMode.Single;
			journal.OnEntitySelectedResult += (o, args) =>
			{
				var car = UoW.GetById<Car>(args.SelectedNodes.First().Id);
				driversAtDay.Where(x => x.Car != null && x.Car.Id == car.Id).ToList().ForEach(x => x.Car = null);
				driver.Car = car;
			};
			TabParent.AddSlaveTab(this, journal);
		}
		
		protected void OnButtonAppointForwardersClicked(object sender, EventArgs e)
		{
			var toAdd = new List<AtWorkForwarder>();
			foreach(var forwarder in ForwardersAtDay.Where(f => DriversAtDay.All(d => d.WithForwarder != f))) {
				var defaulDriver = DriversAtDay.FirstOrDefault(d => d.WithForwarder == null && d.Employee.DefaultForwarder?.Id == forwarder.Employee.Id);
				if(defaulDriver != null)
					defaulDriver.WithForwarder = forwarder;
				else
					toAdd.Add(forwarder);
			}

			if(toAdd.Count == 0)
				return;

			var orders = _scheduleRestrictionRepository.OrdersCountByDistrict(UoW, DialogAtDate, 12);
			var districtsBottles = orders.GroupBy(x => x.DistrictId).ToDictionary(x => x.Key, x => x.Sum(o => o.WaterCount));

			foreach(var forwarder in toAdd) {
				var driversToAdd = DriversAtDay.Where(x => x.WithForwarder == null && x.Car != null && x.Car.TypeOfUse != CarTypeOfUse.CompanyLargus).ToList();

				if(driversToAdd.Count == 0) {
					logger.Warn("Не осталось водителей для добавленя экспедиторов.");
					break;
				}

				int ManOnDistrict(int districtId) => driversAtDay
					.Where(dr =>
						dr.Car != null
						&& dr.Car.TypeOfUse != CarTypeOfUse.CompanyLargus
						&& dr.DistrictsPriorities.Any(dd2 => dd2.District.Id == districtId)
					)
					.Sum(dr => dr.WithForwarder == null ? 1 : 2);

				var driversToAddWithWithActivePrioritySets =
					driversToAdd.Where(x => x.Employee.DriverDistrictPrioritySets.Any(p => p.IsActive));

				var driver = driversToAddWithWithActivePrioritySets
					.OrderByDescending(x => districtsBottles
						.Where(db => x.Employee.DriverDistrictPrioritySets
							.First(s => s.IsActive).DriverDistrictPriorities
							.Any(dd => dd.District.Id == db.Key))
						.Max(db => (double)db.Value / ManOnDistrict(db.Key)))
					.FirstOrDefault();

				if(driver != null) {
					driver.WithForwarder = forwarder;
				}
			}

			MessageDialogHelper.RunInfoDialog("Готово.");
		}

		protected void OnButtonOpenCarClicked(object sender, EventArgs e)
		{
			var selected = ytreeviewAtWorkDrivers.GetSelectedObjects<AtWorkDriver>().First();

			TabParent.OpenTab(
				DialogHelper.GenerateDialogHashName<Car>(selected.Car.Id),
				() => new CarViewModel(EntityUoWBuilder.ForOpen(selected.Car.Id),
				UnitOfWorkFactory.GetDefaultFactory,
				ServicesConfig.CommonServices,
				new EmployeeJournalFactory(),
				new AttachmentsViewModelFactory(),
				new CarRepository())
			);
		}
		
		protected void OnButtonEditDistrictsClicked(object sender, EventArgs e)
		{
			districtpriorityview1.Visible = !districtpriorityview1.Visible;
		}

		protected void OnButtonOpenDriverClicked(object sender, EventArgs e)
		{
			var selected = ytreeviewAtWorkDrivers.GetSelectedObjects<AtWorkDriver>();
			
			foreach(var one in selected) 
			{
				var employeeUow = UnitOfWorkFactory.CreateForRoot<Employee>(one.Employee.Id);

				var employeeViewModel = new EmployeeViewModel(
					_authorizationService,
					_employeeWageParametersFactory,
					_employeeJournalFactory,
					_subdivisionJournalFactory,
					_employeePostsJournalFactory,
					_cashDistributionCommonOrganisationProvider,
					_subdivisionService,
					_emailServiceSettingAdapter,
					_wageCalculationRepository,
					_employeeRepository,
					employeeUow,
					ServicesConfig.CommonServices,
					_validationContextFactory,
					_phonesViewModelFactory,
					_warehouseRepository,
					_routeListRepository,
					_driverApiRegistrationEndpoint,
					CurrentUserSettings.Settings,
					_userRepository,
					_baseParametersProvider,
					_attachmentsViewModelFactory);
				
				TabParent.OpenTab(
					DialogHelper.GenerateDialogHashName<Employee>(one.Employee.Id),
					() => employeeViewModel
				);
			}
		}
		
		protected void OnButtonAddForwarderClicked(object sender, EventArgs e)
		{
			var forwardersJournal = _employeeJournalFactory.CreateEmployeesJournal(_forwarderFilter);
			forwardersJournal.SelectionMode = JournalSelectionMode.Multiple;
			forwardersJournal.OnEntitySelectedResult += OnForwardersSelected;
			TabParent.AddSlaveTab(this, forwardersJournal);
		}
		
		protected void OnButtonRemoveForwarderClicked(object sender, EventArgs e)
		{
			var toDel = ytreeviewOnDayForwarders.GetSelectedObjects<AtWorkForwarder>();
			foreach(var forwarder in toDel) {
				if(forwarder.Id > 0)
					UoW.Delete(forwarder);
				observableForwardersAtDay.Remove(forwarder);
			}
		}
		#endregion

		#region YTreeView

		void YtreeviewDrivers_Selection_Changed(object sender, EventArgs e)
		{
			buttonRemoveDriver.Sensitive = buttonDriverSelectAuto.Sensitive
				= buttonOpenDriver.Sensitive = ytreeviewAtWorkDrivers.Selection.CountSelectedRows() > 0;

			if(ytreeviewAtWorkDrivers.Selection.CountSelectedRows() != 1 && districtpriorityview1.Visible)
				districtpriorityview1.Visible = false;

			buttonOpenCar.Sensitive = false;

			if(ytreeviewAtWorkDrivers.Selection.CountSelectedRows() == 1) {
				var selected = ytreeviewAtWorkDrivers.GetSelectedObjects<AtWorkDriver>();
				districtpriorityview1.ListParent = selected[0];
				districtpriorityview1.Districts = selected[0].ObservableDistrictsPriorities;
				buttonOpenCar.Sensitive = selected[0].Car != null;
				ChangeButtonAddRemove(selected[0].Status == AtWorkDriver.DriverStatus.NotWorking);
			}
			districtpriorityview1.Sensitive = ytreeviewAtWorkDrivers.Selection.CountSelectedRows() == 1;
		}
		
		void YtreeviewForwarders_Selection_Changed(object sender, EventArgs e)
		{
			buttonRemoveForwarder.Sensitive = ytreeviewOnDayForwarders.Selection.CountSelectedRows() > 0;
		}

		void ObservableForwardersAtDay_ListChanged(object aList)
		{
			var renderer = ytreeviewAtWorkDrivers.ColumnsConfig.GetRendererMappingByTagGeneric<ComboRendererMapping<AtWorkDriver, AtWorkForwarder>>(Columns.Forwarder).First();
			renderer.FillItems(ForwardersAtDay, "без экспедитора");
		}
		#endregion
		
		protected void OnYdateAtWorksDateChanged(object sender, EventArgs e)
		{
			FillDialogAtDay();
			OnTabNameChanged();
			SetButtonClearDriverScreenSensitive();
		}

		void SelectDrivers_OnEntitySelectedResult(object sender, JournalSelectedNodesEventArgs e)
		{
			var addDrivers = e.SelectedNodes;
			logger.Info("Получаем авто для водителей...");
			var onlyNew = addDrivers.Where(x => driversAtDay.All(y => y.Employee.Id != x.Id)).ToList();
			var allCars = _carRepository.GetCarsByDrivers(UoW, onlyNew.Select(x => x.Id).ToArray());

			foreach(var driver in addDrivers) {
				var drv = UoW.GetById<Employee>(driver.Id);

				if(driversAtDay.Any(x => x.Employee.Id == driver.Id)) {
					logger.Warn($"Водитель {drv.ShortName} уже добавлен. Пропускаем...");
					continue;
				}

				var daySchedule = GetDriverWorkDaySchedule(drv);
				var atwork = new AtWorkDriver(drv, DialogAtDate, allCars.FirstOrDefault(x => x.Driver.Id == driver.Id), daySchedule);

				GetDefaultForwarder(drv, atwork);

				driversAtDay.Add(atwork);
			}
			DriversAtDay = driversAtDay.OrderBy(x => x.Employee.ShortName).ToList();
			logger.Info("Ок");
			SetButtonClearDriverScreenSensitive();
		}

		protected void OnHideForwadersToggled(object o, Gtk.ToggledArgs args)
		{
			vboxForwarders.Visible = hideForwaders.ArrowDirection == Gtk.ArrowType.Down;
		}

		private void OnForwardersSelected(object sender, JournalSelectedNodesEventArgs e)
		{
			var loaded = UoW.GetById<Employee>(e.SelectedNodes.Select(x => x.Id));
			foreach(var forwarder in loaded)
			{
				if(forwardersAtDay.Any(x => x.Employee.Id == forwarder.Id))
				{
					logger.Warn($"Экспедитор {forwarder.ShortName} пропущен так как уже присутствует в списке.");
					continue;
				}
				forwardersAtDay.Add(new AtWorkForwarder(forwarder, DialogAtDate));
			}
			ForwardersAtDay = forwardersAtDay.OrderBy(x => x.Employee.ShortName).ToList();
		}

		#endregion

		#region Fuctions
		private void SetButtonClearDriverScreenSensitive()
		{
			if (ydateAtWorks.Date < DateTime.Now.Date || !driversAtDay.Any())
			{
				buttonClearDriverScreen.Sensitive = false;
			}
			else
			{
				buttonClearDriverScreen.Sensitive = true;
			}
		}


		private void ChangeButtonAddRemove(bool needRemove)
		{
			if (!canReturnDriver)
			{
				return;
			}
			
			if (needRemove)
			{
				buttonRemoveDriver.Label = "Вернуть водителя";
				buttonRemoveDriver.Image = new Gtk.Image(){Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-add", global::Gtk.IconSize.Menu)};
			}
			else
			{
				buttonRemoveDriver.Label = "Снять водителя";
				buttonRemoveDriver.Image = new Gtk.Image(){Pixbuf = Stetic.IconLoader.LoadIcon(this, "gtk-remove", global::Gtk.IconSize.Menu)};
			}
		}

		public void SaveAndClose() { throw new NotImplementedException(); }

		private DeliveryDaySchedule GetDriverWorkDaySchedule(Employee driver)
		{
			var driverWorkSchedule = driver
				.ObservableDriverWorkScheduleSets.SingleOrDefault(x => x.IsActive)
				?.ObservableDriverWorkSchedules.SingleOrDefault(x => (int)x.WeekDay == (int)DialogAtDate.DayOfWeek);

			return driverWorkSchedule == null 
				? defaultDeliveryDaySchedule
				: driverWorkSchedule.DaySchedule;
		}

		private void GetDefaultForwarder(Employee driver, AtWorkDriver atwork)
		{
			if(driver.DefaultForwarder != null) {
				var forwarder = ForwardersAtDay.FirstOrDefault(x => x.Employee.Id == driver.DefaultForwarder.Id);

				if(forwarder == null) {
					if(MessageDialogHelper.RunQuestionDialog($"Водитель {driver.ShortName} обычно ездит с экспедитором {driver.DefaultForwarder.ShortName}. Он отсутствует в списке экспедиторов. Добавить его в список?")) {
						forwarder = new AtWorkForwarder(driver.DefaultForwarder, DialogAtDate);
						observableForwardersAtDay.Add(forwarder);
					}
				}

				if(forwarder != null && DriversAtDay.All(x => x.WithForwarder != forwarder)) {
					atwork.WithForwarder = forwarder;
				}
			}
		}
		
		public bool Save()
		{
			// В случае, если вкладка сохраняется, а в списке есть Снятые водители, сделать проверку, что у каждого из них заполнена причина.
			var NotWorkingDrivers = DriversAtDay.ToList()
				.Where(driver => driver.Status == AtWorkDriver.DriverStatus.NotWorking);
			
			if (NotWorkingDrivers.Count() != 0)
				foreach (var atWorkDriver in NotWorkingDrivers)
				{
					if (!String.IsNullOrEmpty(atWorkDriver.Reason)) continue;
					MessageDialogHelper.RunWarningDialog("Не у всех снятых водителей указаны причины!");
					return false;
				}
			
			var currentEmployee = _employeeRepository.GetEmployeeForCurrentUser(UoW);
			// Сохранение изменившихся за этот раз авторов и дат комментариев
			foreach (var atWorkDriver in driversWithCommentChanged)
			{
				atWorkDriver.CommentLastEditedAuthor = currentEmployee;
				atWorkDriver.CommentLastEditedDate = DateTime.Now;
			}
			driversWithCommentChanged.Clear();
			ForwardersAtDay.ToList().ForEach(x => UoW.Save(x));
			DriversAtDay.ToList().ForEach(x => UoW.Save(x));
			UoW.Commit();
			FillDialogAtDay();
			return true;
		}
		
		string RenderForwaderWithDriver(AtWorkForwarder atWork)
		{
			return string.Join(", ", driversAtDay.Where(x => x.WithForwarder == atWork).Select(x => x.Employee.ShortName));
		}

		void FillDialogAtDay()
		{
			UoW.Session.Clear();

			logger.Info("Загружаем экспедиторов на {0:d}...", DialogAtDate);
			ForwardersAtDay = new EntityRepositories.Logistic.AtWorkRepository().GetForwardersAtDay(UoW, DialogAtDate);

			logger.Info("Загружаем водителей на {0:d}...", DialogAtDate);
			DriversAtDay = new EntityRepositories.Logistic.AtWorkRepository().GetDriversAtDay(UoW, DialogAtDate);

			logger.Info("Ок");

			CheckAndCorrectDistrictPriorities();
			SetButtonClearDriverScreenSensitive();
		}

		//Если дата диалога >= даты активации набора районов и есть хотя бы один район у водителя, который не принадлежит активному набору районов
		private void CheckAndCorrectDistrictPriorities() {
			var activeDistrictsSet = UoW.Session.QueryOver<DistrictsSet>().Where(x => x.Status == DistrictsSetStatus.Active).SingleOrDefault();
			if(activeDistrictsSet == null) {
				throw new ArgumentNullException(nameof(activeDistrictsSet), @"Не найдена активная версия районов");
			}
			if(activeDistrictsSet.DateActivated == null) {
				throw new ArgumentNullException(nameof(activeDistrictsSet), @"У активной версии районов не проставлена дата активации");
			}
			if(DialogAtDate.Date >= activeDistrictsSet.DateActivated.Value.Date) {
				var outDatedpriorities = DriversAtDay.SelectMany(x => x.DistrictsPriorities.Where(d => d.District.DistrictsSet.Id != activeDistrictsSet.Id)).ToList();
				if(!outDatedpriorities.Any()) 
					return;
				
				int deletedCount = 0;
				foreach (var priority in outDatedpriorities) {
					var newDistrict = activeDistrictsSet.ObservableDistricts.FirstOrDefault(x => x.CopyOf == priority.District);
					if(newDistrict == null) {
						priority.Driver.ObservableDistrictsPriorities.Remove(priority);
						UoW.Delete(priority);
						deletedCount++;
					}
					else {
						priority.District = newDistrict;
						UoW.Save(priority);
					}
				}
				MessageDialogHelper.RunInfoDialog($"Были найдены и исправлены устаревшие приоритеты районов.\nУдалено приоритетов, ссылающихся на несуществующий район: {deletedCount}");
				ytreeviewAtWorkDrivers.YTreeModel.EmitModelChanged();
			}
		}
		#endregion
		
		public override void Destroy()
		{
			UoW?.Dispose();
			base.Destroy();
		}
		
		
		enum Columns
		{
			Forwarder
		}
			   
	}
}
