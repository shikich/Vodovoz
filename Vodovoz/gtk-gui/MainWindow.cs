
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	
	private global::Gtk.Action ActionBaseMenu;
	
	private global::Gtk.Action dialogAuthenticationAction;
	
	private global::Gtk.Action ActionAbout;
	
	private global::Gtk.Action aboutAction;
	
	private global::Gtk.Action UsersAction;
	
	private global::Gtk.Action quitAction;
	
	private global::Gtk.RadioAction ActionOrders;
	
	private global::Gtk.RadioAction ActionServices;
	
	private global::Gtk.RadioAction ActionLogistics;
	
	private global::Gtk.RadioAction ActionStock;
	
	private global::Gtk.RadioAction ActionCash;
	
	private global::Gtk.RadioAction ActionAccounting;
	
	private global::Gtk.RadioAction ActionReports;
	
	private global::Gtk.RadioAction ActionArchive;
	
	private global::Gtk.Action ActionOrg;
	
	private global::Gtk.Action ActionBanksMenu;
	
	private global::Gtk.Action ActionBanksRF;
	
	private global::Gtk.Action ActionOrgMenu;
	
	private global::Gtk.Action ActionEmploey;
	
	private global::Gtk.Action ActionNationality;
	
	private global::Gtk.Action ActionStatus;
	
	private global::Gtk.Action ActionEMailTypes;
	
	private global::Gtk.Action Action;
	
	private global::Gtk.Action ActionCounterpartyPost;
	
	private global::Gtk.Action ActionFreeRentPackage;
	
	private global::Gtk.Action ActionEquipment;
	
	private global::Gtk.Action ActionWarehouses;
	
	private global::Gtk.Action ActionCar;
	
	private global::Gtk.Action ActionColors;
	
	private global::Gtk.Action ActionUnits;
	
	private global::Gtk.Action ActionManufacturers;
	
	private global::Gtk.Action ActionEquipmentTypes;
	
	private global::Gtk.Action ActionNomenclature;
	
	private global::Gtk.Action ActionPhoneTypes;
	
	private global::Gtk.Action ActionTMC;
	
	private global::Gtk.Action ActionMenuLogistic;
	
	private global::Gtk.Action ActionCounterparty1;
	
	private global::Gtk.Action ActionCounterpartyHandbook;
	
	private global::Gtk.Action ActionSignificance;
	
	private global::Gtk.Action ActionPaidRentPackage;
	
	private global::Gtk.Action Action11;
	
	private global::Gtk.Action ActionDeliverySchedule;
	
	private global::Gtk.Action ActionLogisticsArea;
	
	private global::Gtk.Action ActionUpdateBanks;
	
	private global::Gtk.Action ActionProductSpecification;
	
	private global::Gtk.Action ActionCullingCategory;
	
	private global::Gtk.Action Action12;
	
	private global::Gtk.Action ActionCommentTemplates;
	
	private global::Gtk.Action ActionLoad1c;
	
	private global::Gtk.Action ActionRouteColumns;
	
	private global::Gtk.Action ActionFuelType;
	
	private global::Gtk.Action ActionDeliveryShift;
	
	private global::Gtk.Action ActionParameters;
	
	private global::Gtk.Action Action13;
	
	private global::Gtk.Action Action14;
	
	private global::Gtk.Action Action15;
	
	private global::Gtk.Action ActionDocTemplates;
	
	private global::Gtk.HBox hbox1;
	
	private global::Gtk.VBox vbox1;
	
	private global::Gtk.MenuBar menubarMain;
	
	private global::Gtk.Toolbar toolbarMain;
	
	private global::Gtk.Toolbar toolbarSub;
	
	private global::QSTDI.TdiNotebook tdiMain;
	
	private global::Gtk.Statusbar statusbarMain;
	
	private global::Gtk.Label labelUser;
	
	private global::Gtk.Label labelStatus;
	
	private global::Vodovoz.Panel.InfoPanel infopanel;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("Default");
		this.ActionBaseMenu = new global::Gtk.Action ("ActionBaseMenu", global::Mono.Unix.Catalog.GetString ("База"), null, null);
		this.ActionBaseMenu.ShortLabel = global::Mono.Unix.Catalog.GetString ("База");
		w1.Add (this.ActionBaseMenu, null);
		this.dialogAuthenticationAction = new global::Gtk.Action ("dialogAuthenticationAction", global::Mono.Unix.Catalog.GetString ("Изменить пароль"), null, "gtk-dialog-authentication");
		this.dialogAuthenticationAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Изменить пароль");
		w1.Add (this.dialogAuthenticationAction, null);
		this.ActionAbout = new global::Gtk.Action ("ActionAbout", global::Mono.Unix.Catalog.GetString ("Справка"), null, null);
		this.ActionAbout.ShortLabel = global::Mono.Unix.Catalog.GetString ("Справка");
		w1.Add (this.ActionAbout, null);
		this.aboutAction = new global::Gtk.Action ("aboutAction", global::Mono.Unix.Catalog.GetString ("_О программе"), null, "gtk-about");
		this.aboutAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_О программе");
		w1.Add (this.aboutAction, null);
		this.UsersAction = new global::Gtk.Action ("UsersAction", global::Mono.Unix.Catalog.GetString ("Пользователи"), null, "users");
		this.UsersAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Пользователи");
		w1.Add (this.UsersAction, null);
		this.quitAction = new global::Gtk.Action ("quitAction", global::Mono.Unix.Catalog.GetString ("В_ыход"), null, "gtk-quit");
		this.quitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("В_ыход");
		w1.Add (this.quitAction, null);
		this.ActionOrders = new global::Gtk.RadioAction ("ActionOrders", global::Mono.Unix.Catalog.GetString ("Заказы"), null, "order", 0);
		this.ActionOrders.Group = new global::GLib.SList (global::System.IntPtr.Zero);
		this.ActionOrders.ShortLabel = global::Mono.Unix.Catalog.GetString ("Заказы");
		w1.Add (this.ActionOrders, null);
		this.ActionServices = new global::Gtk.RadioAction ("ActionServices", global::Mono.Unix.Catalog.GetString ("Сервис"), null, "service", 0);
		this.ActionServices.Group = this.ActionOrders.Group;
		this.ActionServices.ShortLabel = global::Mono.Unix.Catalog.GetString ("Сервис");
		w1.Add (this.ActionServices, null);
		this.ActionLogistics = new global::Gtk.RadioAction ("ActionLogistics", global::Mono.Unix.Catalog.GetString ("Логистика"), null, "logistic", 0);
		this.ActionLogistics.Group = this.ActionServices.Group;
		this.ActionLogistics.ShortLabel = global::Mono.Unix.Catalog.GetString ("Логистика");
		w1.Add (this.ActionLogistics, null);
		this.ActionStock = new global::Gtk.RadioAction ("ActionStock", global::Mono.Unix.Catalog.GetString ("Склад"), null, "stock", 0);
		this.ActionStock.Group = this.ActionServices.Group;
		this.ActionStock.ShortLabel = global::Mono.Unix.Catalog.GetString ("Склад");
		w1.Add (this.ActionStock, null);
		this.ActionCash = new global::Gtk.RadioAction ("ActionCash", global::Mono.Unix.Catalog.GetString ("Касса"), null, "cash", 0);
		this.ActionCash.Group = this.ActionServices.Group;
		this.ActionCash.ShortLabel = global::Mono.Unix.Catalog.GetString ("Касса");
		w1.Add (this.ActionCash, null);
		this.ActionAccounting = new global::Gtk.RadioAction ("ActionAccounting", global::Mono.Unix.Catalog.GetString ("Бухгалтерия"), null, "accounting", 0);
		this.ActionAccounting.Group = this.ActionServices.Group;
		this.ActionAccounting.ShortLabel = global::Mono.Unix.Catalog.GetString ("Бухгалтерия");
		w1.Add (this.ActionAccounting, null);
		this.ActionReports = new global::Gtk.RadioAction ("ActionReports", global::Mono.Unix.Catalog.GetString ("Отчеты"), null, "report", 0);
		this.ActionReports.Group = this.ActionServices.Group;
		this.ActionReports.ShortLabel = global::Mono.Unix.Catalog.GetString ("Отчеты");
		w1.Add (this.ActionReports, null);
		this.ActionArchive = new global::Gtk.RadioAction ("ActionArchive", global::Mono.Unix.Catalog.GetString ("Архив"), null, "archive", 0);
		this.ActionArchive.Group = this.ActionServices.Group;
		this.ActionArchive.ShortLabel = global::Mono.Unix.Catalog.GetString ("Архив");
		w1.Add (this.ActionArchive, null);
		this.ActionOrg = new global::Gtk.Action ("ActionOrg", global::Mono.Unix.Catalog.GetString ("Организации"), null, null);
		this.ActionOrg.ShortLabel = global::Mono.Unix.Catalog.GetString ("Организации");
		w1.Add (this.ActionOrg, null);
		this.ActionBanksMenu = new global::Gtk.Action ("ActionBanksMenu", global::Mono.Unix.Catalog.GetString ("Банки"), null, null);
		this.ActionBanksMenu.ShortLabel = global::Mono.Unix.Catalog.GetString ("Банки");
		w1.Add (this.ActionBanksMenu, null);
		this.ActionBanksRF = new global::Gtk.Action ("ActionBanksRF", global::Mono.Unix.Catalog.GetString ("Банки РФ"), null, null);
		this.ActionBanksRF.ShortLabel = global::Mono.Unix.Catalog.GetString ("Банки РФ");
		w1.Add (this.ActionBanksRF, null);
		this.ActionOrgMenu = new global::Gtk.Action ("ActionOrgMenu", global::Mono.Unix.Catalog.GetString ("Наша организация"), null, null);
		this.ActionOrgMenu.ShortLabel = global::Mono.Unix.Catalog.GetString ("Наша организация");
		w1.Add (this.ActionOrgMenu, null);
		this.ActionEmploey = new global::Gtk.Action ("ActionEmploey", global::Mono.Unix.Catalog.GetString ("Сотрудники"), null, null);
		this.ActionEmploey.ShortLabel = global::Mono.Unix.Catalog.GetString ("Сотрудники");
		w1.Add (this.ActionEmploey, null);
		this.ActionNationality = new global::Gtk.Action ("ActionNationality", global::Mono.Unix.Catalog.GetString ("Национальность"), null, null);
		this.ActionNationality.ShortLabel = global::Mono.Unix.Catalog.GetString ("Национальность");
		w1.Add (this.ActionNationality, null);
		this.ActionStatus = new global::Gtk.Action ("ActionStatus", global::Mono.Unix.Catalog.GetString ("Статусы"), null, null);
		this.ActionStatus.ShortLabel = global::Mono.Unix.Catalog.GetString ("Статусы");
		w1.Add (this.ActionStatus, null);
		this.ActionEMailTypes = new global::Gtk.Action ("ActionEMailTypes", global::Mono.Unix.Catalog.GetString ("Типы e-mail адресов"), null, null);
		this.ActionEMailTypes.ShortLabel = global::Mono.Unix.Catalog.GetString ("Типы e-mail адресов");
		w1.Add (this.ActionEMailTypes, null);
		this.Action = new global::Gtk.Action ("Action", global::Mono.Unix.Catalog.GetString ("Справочники"), null, null);
		this.Action.ShortLabel = global::Mono.Unix.Catalog.GetString ("Справочники");
		w1.Add (this.Action, null);
		this.ActionCounterpartyPost = new global::Gtk.Action ("ActionCounterpartyPost", global::Mono.Unix.Catalog.GetString ("Должности сотрудников контрагента"), null, null);
		this.ActionCounterpartyPost.ShortLabel = global::Mono.Unix.Catalog.GetString ("Должности сотрудников контрагента");
		w1.Add (this.ActionCounterpartyPost, null);
		this.ActionFreeRentPackage = new global::Gtk.Action ("ActionFreeRentPackage", global::Mono.Unix.Catalog.GetString ("Пакеты бесплатной аренды"), null, null);
		this.ActionFreeRentPackage.ShortLabel = global::Mono.Unix.Catalog.GetString ("Пакеты бесплатной аренды");
		w1.Add (this.ActionFreeRentPackage, null);
		this.ActionEquipment = new global::Gtk.Action ("ActionEquipment", global::Mono.Unix.Catalog.GetString ("Оборудование"), null, null);
		this.ActionEquipment.ShortLabel = global::Mono.Unix.Catalog.GetString ("Оборудование");
		w1.Add (this.ActionEquipment, null);
		this.ActionWarehouses = new global::Gtk.Action ("ActionWarehouses", global::Mono.Unix.Catalog.GetString ("Склады"), null, null);
		this.ActionWarehouses.ShortLabel = global::Mono.Unix.Catalog.GetString ("Склады");
		w1.Add (this.ActionWarehouses, null);
		this.ActionCar = new global::Gtk.Action ("ActionCar", global::Mono.Unix.Catalog.GetString ("Автомобили"), null, null);
		this.ActionCar.ShortLabel = global::Mono.Unix.Catalog.GetString ("Автомобили");
		w1.Add (this.ActionCar, null);
		this.ActionColors = new global::Gtk.Action ("ActionColors", global::Mono.Unix.Catalog.GetString ("Цвета оборудования"), null, null);
		this.ActionColors.ShortLabel = global::Mono.Unix.Catalog.GetString ("Цвета");
		w1.Add (this.ActionColors, null);
		this.ActionUnits = new global::Gtk.Action ("ActionUnits", global::Mono.Unix.Catalog.GetString ("Единицы измерения"), null, null);
		this.ActionUnits.ShortLabel = global::Mono.Unix.Catalog.GetString ("Единицы измерения");
		w1.Add (this.ActionUnits, null);
		this.ActionManufacturers = new global::Gtk.Action ("ActionManufacturers", global::Mono.Unix.Catalog.GetString ("Производители оборудования"), null, null);
		this.ActionManufacturers.ShortLabel = global::Mono.Unix.Catalog.GetString ("Производители оборудования");
		w1.Add (this.ActionManufacturers, null);
		this.ActionEquipmentTypes = new global::Gtk.Action ("ActionEquipmentTypes", global::Mono.Unix.Catalog.GetString ("Типы оборудования"), null, null);
		this.ActionEquipmentTypes.ShortLabel = global::Mono.Unix.Catalog.GetString ("Типы оборудования");
		w1.Add (this.ActionEquipmentTypes, null);
		this.ActionNomenclature = new global::Gtk.Action ("ActionNomenclature", global::Mono.Unix.Catalog.GetString ("Номенклатура"), null, null);
		this.ActionNomenclature.ShortLabel = global::Mono.Unix.Catalog.GetString ("Номенклатура");
		w1.Add (this.ActionNomenclature, null);
		this.ActionPhoneTypes = new global::Gtk.Action ("ActionPhoneTypes", global::Mono.Unix.Catalog.GetString ("Типы телефонов"), null, null);
		this.ActionPhoneTypes.ShortLabel = global::Mono.Unix.Catalog.GetString ("Типы телефонов");
		w1.Add (this.ActionPhoneTypes, null);
		this.ActionTMC = new global::Gtk.Action ("ActionTMC", global::Mono.Unix.Catalog.GetString ("ТМЦ"), null, null);
		this.ActionTMC.ShortLabel = global::Mono.Unix.Catalog.GetString ("ТМЦ");
		w1.Add (this.ActionTMC, null);
		this.ActionMenuLogistic = new global::Gtk.Action ("ActionMenuLogistic", global::Mono.Unix.Catalog.GetString ("Логистика"), null, null);
		this.ActionMenuLogistic.ShortLabel = global::Mono.Unix.Catalog.GetString ("Значимость контрагента");
		w1.Add (this.ActionMenuLogistic, null);
		this.ActionCounterparty1 = new global::Gtk.Action ("ActionCounterparty1", global::Mono.Unix.Catalog.GetString ("Контрагенты"), null, null);
		this.ActionCounterparty1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Контрагенты");
		w1.Add (this.ActionCounterparty1, null);
		this.ActionCounterpartyHandbook = new global::Gtk.Action ("ActionCounterpartyHandbook", global::Mono.Unix.Catalog.GetString ("Контрагенты"), null, null);
		this.ActionCounterpartyHandbook.ShortLabel = global::Mono.Unix.Catalog.GetString ("Контрагенты");
		w1.Add (this.ActionCounterpartyHandbook, null);
		this.ActionSignificance = new global::Gtk.Action ("ActionSignificance", global::Mono.Unix.Catalog.GetString ("Значимость контрагента"), null, null);
		this.ActionSignificance.ShortLabel = global::Mono.Unix.Catalog.GetString ("Значимость контрагента");
		w1.Add (this.ActionSignificance, null);
		this.ActionPaidRentPackage = new global::Gtk.Action ("ActionPaidRentPackage", global::Mono.Unix.Catalog.GetString ("Условия платной аренды"), null, null);
		this.ActionPaidRentPackage.ShortLabel = global::Mono.Unix.Catalog.GetString ("Условия платной аренды");
		w1.Add (this.ActionPaidRentPackage, null);
		this.Action11 = new global::Gtk.Action ("Action11", global::Mono.Unix.Catalog.GetString ("Логистика"), null, null);
		this.Action11.ShortLabel = global::Mono.Unix.Catalog.GetString ("Логистика");
		w1.Add (this.Action11, null);
		this.ActionDeliverySchedule = new global::Gtk.Action ("ActionDeliverySchedule", global::Mono.Unix.Catalog.GetString ("Графики доставки"), null, null);
		this.ActionDeliverySchedule.ShortLabel = global::Mono.Unix.Catalog.GetString ("Графики доставки");
		w1.Add (this.ActionDeliverySchedule, null);
		this.ActionLogisticsArea = new global::Gtk.Action ("ActionLogisticsArea", global::Mono.Unix.Catalog.GetString ("Логистические районы"), null, null);
		this.ActionLogisticsArea.ShortLabel = global::Mono.Unix.Catalog.GetString ("Логистические районы");
		w1.Add (this.ActionLogisticsArea, null);
		this.ActionUpdateBanks = new global::Gtk.Action ("ActionUpdateBanks", global::Mono.Unix.Catalog.GetString ("Обновить банки с сайта РБК"), null, null);
		this.ActionUpdateBanks.ShortLabel = global::Mono.Unix.Catalog.GetString ("Обновить банки с сайта РБК");
		w1.Add (this.ActionUpdateBanks, null);
		this.ActionProductSpecification = new global::Gtk.Action ("ActionProductSpecification", global::Mono.Unix.Catalog.GetString ("Спецификация продукции"), null, null);
		this.ActionProductSpecification.ShortLabel = global::Mono.Unix.Catalog.GetString ("Спецификация продукции");
		w1.Add (this.ActionProductSpecification, null);
		this.ActionCullingCategory = new global::Gtk.Action ("ActionCullingCategory", global::Mono.Unix.Catalog.GetString ("Категории выбраковки"), null, null);
		this.ActionCullingCategory.ShortLabel = global::Mono.Unix.Catalog.GetString ("Категории выбраковки");
		w1.Add (this.ActionCullingCategory, null);
		this.Action12 = new global::Gtk.Action ("Action12", global::Mono.Unix.Catalog.GetString ("Помощники"), null, null);
		this.Action12.ShortLabel = global::Mono.Unix.Catalog.GetString ("Помощники");
		w1.Add (this.Action12, null);
		this.ActionCommentTemplates = new global::Gtk.Action ("ActionCommentTemplates", global::Mono.Unix.Catalog.GetString ("Шаблоны комментариев"), null, null);
		this.ActionCommentTemplates.ShortLabel = global::Mono.Unix.Catalog.GetString ("Шаблоны комментариев");
		w1.Add (this.ActionCommentTemplates, null);
		this.ActionLoad1c = new global::Gtk.Action ("ActionLoad1c", global::Mono.Unix.Catalog.GetString ("Загрузить из 1с 7.7"), null, null);
		this.ActionLoad1c.ShortLabel = global::Mono.Unix.Catalog.GetString ("Загрузить из 1с 7.7");
		w1.Add (this.ActionLoad1c, null);
		this.ActionRouteColumns = new global::Gtk.Action ("ActionRouteColumns", global::Mono.Unix.Catalog.GetString ("Колонки номенклатуры"), null, null);
		this.ActionRouteColumns.ShortLabel = global::Mono.Unix.Catalog.GetString ("Колонки номенклатуры");
		w1.Add (this.ActionRouteColumns, null);
		this.ActionFuelType = new global::Gtk.Action ("ActionFuelType", global::Mono.Unix.Catalog.GetString ("Виды топлива"), null, null);
		this.ActionFuelType.ShortLabel = global::Mono.Unix.Catalog.GetString ("Виды топлива");
		w1.Add (this.ActionFuelType, null);
		this.ActionDeliveryShift = new global::Gtk.Action ("ActionDeliveryShift", global::Mono.Unix.Catalog.GetString ("Смена доставки"), null, null);
		this.ActionDeliveryShift.ShortLabel = global::Mono.Unix.Catalog.GetString ("Смена доставки");
		w1.Add (this.ActionDeliveryShift, null);
		this.ActionParameters = new global::Gtk.Action ("ActionParameters", global::Mono.Unix.Catalog.GetString ("Параметры приложения"), null, "gtk-preferences");
		this.ActionParameters.ShortLabel = global::Mono.Unix.Catalog.GetString ("Параметры приложения");
		w1.Add (this.ActionParameters, null);
		this.Action13 = new global::Gtk.Action ("Action13", global::Mono.Unix.Catalog.GetString ("Касса"), null, null);
		this.Action13.ShortLabel = global::Mono.Unix.Catalog.GetString ("Касса");
		w1.Add (this.Action13, null);
		this.Action14 = new global::Gtk.Action ("Action14", global::Mono.Unix.Catalog.GetString ("Статьи доходов"), null, null);
		this.Action14.ShortLabel = global::Mono.Unix.Catalog.GetString ("Статьи доходов");
		w1.Add (this.Action14, null);
		this.Action15 = new global::Gtk.Action ("Action15", global::Mono.Unix.Catalog.GetString ("Статьи расходов"), null, null);
		this.Action15.ShortLabel = global::Mono.Unix.Catalog.GetString ("Статьи расходов");
		w1.Add (this.Action15, null);
		this.ActionDocTemplates = new global::Gtk.Action ("ActionDocTemplates", global::Mono.Unix.Catalog.GetString ("Шаблоны документов"), null, null);
		this.ActionDocTemplates.ShortLabel = global::Mono.Unix.Catalog.GetString ("Шаблоны документов");
		w1.Add (this.ActionDocTemplates, null);
		this.UIManager.InsertActionGroup (w1, 0);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(1));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.hbox1 = new global::Gtk.HBox ();
		this.hbox1.Name = "hbox1";
		this.hbox1.Spacing = 6;
		// Container child hbox1.Gtk.Box+BoxChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubarMain'><menu name='ActionBaseMenu' action='ActionBaseMenu'><menuitem name='dialogAuthenticationAction' action='dialogAuthenticationAction'/><menuitem name='UsersAction' action='UsersAction'/><separator/><menuitem name='ActionParameters' action='ActionParameters'/><separator/><menuitem name='quitAction' action='quitAction'/></menu><menu name='Action' action='Action'><menu name='ActionOrgMenu' action='ActionOrgMenu'><menuitem name='ActionOrg' action='ActionOrg'/><separator/><menuitem name='ActionEmploey' action='ActionEmploey'/><menuitem name='ActionNationality' action='ActionNationality'/><menuitem name='ActionWarehouses' action='ActionWarehouses'/><separator/><menuitem name='ActionPhoneTypes' action='ActionPhoneTypes'/><menuitem name='ActionEMailTypes' action='ActionEMailTypes'/></menu><menu name='ActionTMC' action='ActionTMC'><menuitem name='ActionNomenclature' action='ActionNomenclature'/><menuitem name='ActionUnits' action='ActionUnits'/><menuitem name='ActionEquipment' action='ActionEquipment'/><separator/><menuitem name='ActionEquipmentTypes' action='ActionEquipmentTypes'/><menuitem name='ActionManufacturers' action='ActionManufacturers'/><menuitem name='ActionColors' action='ActionColors'/><separator/><menuitem name='ActionProductSpecification' action='ActionProductSpecification'/><menuitem name='ActionCullingCategory' action='ActionCullingCategory'/><separator/><menuitem name='ActionFreeRentPackage' action='ActionFreeRentPackage'/><menuitem name='ActionPaidRentPackage' action='ActionPaidRentPackage'/></menu><menu name='ActionBanksMenu' action='ActionBanksMenu'><menuitem name='ActionBanksRF' action='ActionBanksRF'/><separator/><menuitem name='ActionUpdateBanks' action='ActionUpdateBanks'/></menu><menu name='Action13' action='Action13'><menuitem name='Action14' action='Action14'/><menuitem name='Action15' action='Action15'/></menu><menu name='ActionCounterparty1' action='ActionCounterparty1'><menuitem name='ActionCounterpartyHandbook' action='ActionCounterpartyHandbook'/><separator/><menuitem name='ActionSignificance' action='ActionSignificance'/><menuitem name='ActionStatus' action='ActionStatus'/><menuitem name='ActionCounterpartyPost' action='ActionCounterpartyPost'/><separator/><menuitem name='ActionDocTemplates' action='ActionDocTemplates'/><separator/><menuitem name='ActionLoad1c' action='ActionLoad1c'/></menu><menu name='ActionMenuLogistic' action='ActionMenuLogistic'><menuitem name='ActionDeliverySchedule' action='ActionDeliverySchedule'/><menuitem name='ActionLogisticsArea' action='ActionLogisticsArea'/><menuitem name='ActionDeliveryShift' action='ActionDeliveryShift'/><separator/><menuitem name='ActionCar' action='ActionCar'/><menuitem name='ActionFuelType' action='ActionFuelType'/><separator/><menuitem name='ActionRouteColumns' action='ActionRouteColumns'/></menu><menu name='Action12' action='Action12'><menuitem name='ActionCommentTemplates' action='ActionCommentTemplates'/></menu></menu><menu name='ActionAbout' action='ActionAbout'><menuitem name='aboutAction' action='aboutAction'/></menu></menubar></ui>");
		this.menubarMain = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubarMain")));
		this.menubarMain.Name = "menubarMain";
		this.vbox1.Add (this.menubarMain);
		global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubarMain]));
		w2.Position = 0;
		w2.Expand = false;
		w2.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name='toolbarMain'><toolitem name='ActionOrders' action='ActionOrders'/><toolitem name='ActionServices' action='ActionServices'/><toolitem name='ActionLogistics' action='ActionLogistics'/><toolitem name='ActionStock' action='ActionStock'/><toolitem name='ActionCash' action='ActionCash'/><toolitem name='ActionAccounting' action='ActionAccounting'/><toolitem name='ActionReports' action='ActionReports'/><toolitem name='ActionArchive' action='ActionArchive'/></toolbar></ui>");
		this.toolbarMain = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbarMain")));
		this.toolbarMain.Name = "toolbarMain";
		this.toolbarMain.ShowArrow = false;
		this.toolbarMain.ToolbarStyle = ((global::Gtk.ToolbarStyle)(2));
		this.toolbarMain.IconSize = ((global::Gtk.IconSize)(6));
		this.vbox1.Add (this.toolbarMain);
		global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.toolbarMain]));
		w3.Position = 1;
		w3.Expand = false;
		w3.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name='toolbarSub'/></ui>");
		this.toolbarSub = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbarSub")));
		this.toolbarSub.Name = "toolbarSub";
		this.toolbarSub.ShowArrow = false;
		this.toolbarSub.ToolbarStyle = ((global::Gtk.ToolbarStyle)(1));
		this.toolbarSub.IconSize = ((global::Gtk.IconSize)(2));
		this.vbox1.Add (this.toolbarSub);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.toolbarSub]));
		w4.Position = 2;
		w4.Expand = false;
		w4.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.tdiMain = new global::QSTDI.TdiNotebook ();
		this.tdiMain.Name = "tdiMain";
		this.tdiMain.CurrentPage = 0;
		this.tdiMain.ShowTabs = false;
		this.tdiMain.Scrollable = true;
		this.vbox1.Add (this.tdiMain);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.tdiMain]));
		w5.Position = 3;
		// Container child vbox1.Gtk.Box+BoxChild
		this.statusbarMain = new global::Gtk.Statusbar ();
		this.statusbarMain.Name = "statusbarMain";
		this.statusbarMain.Spacing = 6;
		// Container child statusbarMain.Gtk.Box+BoxChild
		this.labelUser = new global::Gtk.Label ();
		this.labelUser.Name = "labelUser";
		this.statusbarMain.Add (this.labelUser);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.statusbarMain [this.labelUser]));
		w6.Position = 0;
		w6.Expand = false;
		w6.Fill = false;
		// Container child statusbarMain.Gtk.Box+BoxChild
		this.labelStatus = new global::Gtk.Label ();
		this.labelStatus.Name = "labelStatus";
		this.statusbarMain.Add (this.labelStatus);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.statusbarMain [this.labelStatus]));
		w7.Position = 2;
		w7.Expand = false;
		w7.Fill = false;
		this.vbox1.Add (this.statusbarMain);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.statusbarMain]));
		w8.PackType = ((global::Gtk.PackType)(1));
		w8.Position = 4;
		w8.Expand = false;
		w8.Fill = false;
		this.hbox1.Add (this.vbox1);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.vbox1]));
		w9.Position = 0;
		// Container child hbox1.Gtk.Box+BoxChild
		this.infopanel = new global::Vodovoz.Panel.InfoPanel ();
		this.infopanel.Events = ((global::Gdk.EventMask)(256));
		this.infopanel.Name = "infopanel";
		this.hbox1.Add (this.infopanel);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.infopanel]));
		w10.Position = 1;
		w10.Expand = false;
		w10.Fill = false;
		this.Add (this.hbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 950;
		this.DefaultHeight = 530;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.dialogAuthenticationAction.Activated += new global::System.EventHandler (this.OnDialogAuthenticationActionActivated);
		this.aboutAction.Activated += new global::System.EventHandler (this.OnAboutActionActivated);
		this.UsersAction.Activated += new global::System.EventHandler (this.OnAction3Activated);
		this.quitAction.Activated += new global::System.EventHandler (this.OnQuitActionActivated);
		this.ActionOrders.Toggled += new global::System.EventHandler (this.OnActionOrdersToggled);
		this.ActionServices.Toggled += new global::System.EventHandler (this.OnActionServicesToggled);
		this.ActionLogistics.Toggled += new global::System.EventHandler (this.OnActionLogisticsToggled);
		this.ActionStock.Toggled += new global::System.EventHandler (this.OnActionStockToggled);
		this.ActionCash.Toggled += new global::System.EventHandler (this.OnActionCashToggled);
		this.ActionAccounting.Toggled += new global::System.EventHandler (this.OnActionAccountingToggled);
		this.ActionOrg.Activated += new global::System.EventHandler (this.OnActionOrganizationsActivated);
		this.ActionBanksRF.Activated += new global::System.EventHandler (this.OnActionBanksRFActivated);
		this.ActionEmploey.Activated += new global::System.EventHandler (this.OnActionEmploeyActivated);
		this.ActionNationality.Activated += new global::System.EventHandler (this.OnActionNationalityActivated);
		this.ActionStatus.Activated += new global::System.EventHandler (this.OnActionStatusActivated);
		this.ActionEMailTypes.Activated += new global::System.EventHandler (this.OnActionEMailTypesActivated);
		this.ActionCounterpartyPost.Activated += new global::System.EventHandler (this.OnActionCounterpartyPostActivated);
		this.ActionFreeRentPackage.Activated += new global::System.EventHandler (this.OnActionFreeRentPackageActivated);
		this.ActionEquipment.Activated += new global::System.EventHandler (this.OnActionEquipmentActivated);
		this.ActionWarehouses.Activated += new global::System.EventHandler (this.OnActionWarehousesActivated);
		this.ActionCar.Activated += new global::System.EventHandler (this.OnActionCarsActivated);
		this.ActionColors.Activated += new global::System.EventHandler (this.OnActionColorsActivated);
		this.ActionUnits.Activated += new global::System.EventHandler (this.OnActionUnitsActivated);
		this.ActionManufacturers.Activated += new global::System.EventHandler (this.OnActionManufacturersActivated);
		this.ActionEquipmentTypes.Activated += new global::System.EventHandler (this.OnActionEquipmentTypesActivated);
		this.ActionNomenclature.Activated += new global::System.EventHandler (this.OnActionNomenclatureActivated);
		this.ActionPhoneTypes.Activated += new global::System.EventHandler (this.OnActionPhoneTypesActivated);
		this.ActionCounterpartyHandbook.Activated += new global::System.EventHandler (this.OnActionCounterpartyHandbookActivated);
		this.ActionSignificance.Activated += new global::System.EventHandler (this.OnActionSignificanceActivated);
		this.ActionPaidRentPackage.Activated += new global::System.EventHandler (this.OnActionPaidRentPackageActivated);
		this.ActionDeliverySchedule.Activated += new global::System.EventHandler (this.OnActionDeliveryScheduleActivated);
		this.ActionLogisticsArea.Activated += new global::System.EventHandler (this.OnActionLogisticsAreaActivated);
		this.ActionUpdateBanks.Activated += new global::System.EventHandler (this.OnActionUpdateBanksActivated);
		this.ActionProductSpecification.Activated += new global::System.EventHandler (this.OnActionProductSpecificationActivated);
		this.ActionCullingCategory.Activated += new global::System.EventHandler (this.OnActionCullingCategoryActivated);
		this.ActionCommentTemplates.Activated += new global::System.EventHandler (this.OnActionCommentTemplatesActivated);
		this.ActionLoad1c.Activated += new global::System.EventHandler (this.OnActionLoad1cActivated);
		this.ActionRouteColumns.Activated += new global::System.EventHandler (this.OnActionRouteColumnsActivated);
		this.ActionFuelType.Activated += new global::System.EventHandler (this.OnActionFuelTypeActivated);
		this.ActionDeliveryShift.Activated += new global::System.EventHandler (this.OnActionDeliveryShiftActivated);
		this.ActionParameters.Activated += new global::System.EventHandler (this.OnActionParametersActivated);
		this.Action14.Activated += new global::System.EventHandler (this.OnAction14Activated);
		this.Action15.Activated += new global::System.EventHandler (this.OnAction15Activated);
		this.ActionDocTemplates.Activated += new global::System.EventHandler (this.OnActionDocTemplatesActivated);
		this.tdiMain.TabAdded += new global::System.EventHandler<QSTDI.TabAddedEventArgs> (this.OnTdiMainTabAdded);
		this.tdiMain.TabSwitched += new global::System.EventHandler<QSTDI.TabSwitchedEventArgs> (this.OnTdiMainTabSwitched);
		this.tdiMain.TabClosed += new global::System.EventHandler<QSTDI.TabClosedEventArgs> (this.OnTdiMainTabClosed);
	}
}
