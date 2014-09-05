
// This file has been generated by the GUI designer. Do not modify.

public partial class MainWindow
{
	private global::Gtk.UIManager UIManager;
	
	private global::Gtk.Action ActionServiceTikets;
	
	private global::Gtk.Action ActionOrganizations;
	
	private global::Gtk.Action Action;
	
	private global::Gtk.Action dialogAuthenticationAction;
	
	private global::Gtk.Action Action1;
	
	private global::Gtk.Action Action2;
	
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
	
	private global::Gtk.Action ActionNewOrder;
	
	private global::Gtk.Action ActionOrdersTable;
	
	private global::Gtk.VBox vbox1;
	
	private global::Gtk.MenuBar menubarMain;
	
	private global::Gtk.Toolbar toolbarMain;
	
	private global::Gtk.Toolbar toolbarSub;
	
	private global::QSTDI.TdiNotebook tdiMain;
	
	private global::Gtk.Statusbar statusbarMain;
	
	private global::Gtk.Label labelUser;
	
	private global::Gtk.Label labelStatus;

	protected virtual void Build ()
	{
		global::Stetic.Gui.Initialize (this);
		// Widget MainWindow
		this.UIManager = new global::Gtk.UIManager ();
		global::Gtk.ActionGroup w1 = new global::Gtk.ActionGroup ("services");
		this.ActionServiceTikets = new global::Gtk.Action ("ActionServiceTikets", global::Mono.Unix.Catalog.GetString ("Журнал заявок"), null, "table");
		this.ActionServiceTikets.ShortLabel = global::Mono.Unix.Catalog.GetString ("Журнал заявок");
		w1.Add (this.ActionServiceTikets, null);
		this.ActionOrganizations = new global::Gtk.Action ("ActionOrganizations", global::Mono.Unix.Catalog.GetString ("Организации"), null, null);
		this.ActionOrganizations.ShortLabel = global::Mono.Unix.Catalog.GetString ("Организации");
		w1.Add (this.ActionOrganizations, null);
		this.UIManager.InsertActionGroup (w1, 0);
		global::Gtk.ActionGroup w2 = new global::Gtk.ActionGroup ("Default");
		this.Action = new global::Gtk.Action ("Action", global::Mono.Unix.Catalog.GetString ("База"), null, null);
		this.Action.ShortLabel = global::Mono.Unix.Catalog.GetString ("База");
		w2.Add (this.Action, null);
		this.dialogAuthenticationAction = new global::Gtk.Action ("dialogAuthenticationAction", global::Mono.Unix.Catalog.GetString ("Изменить пароль"), null, "gtk-dialog-authentication");
		this.dialogAuthenticationAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Изменить пароль");
		w2.Add (this.dialogAuthenticationAction, null);
		this.Action1 = new global::Gtk.Action ("Action1", global::Mono.Unix.Catalog.GetString ("Справка"), null, null);
		this.Action1.ShortLabel = global::Mono.Unix.Catalog.GetString ("Справка");
		w2.Add (this.Action1, null);
		this.Action2 = new global::Gtk.Action ("Action2", global::Mono.Unix.Catalog.GetString ("Справочники"), null, null);
		this.Action2.ShortLabel = global::Mono.Unix.Catalog.GetString ("Справочники");
		w2.Add (this.Action2, null);
		this.aboutAction = new global::Gtk.Action ("aboutAction", global::Mono.Unix.Catalog.GetString ("_О программе"), null, "gtk-about");
		this.aboutAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("_О программе");
		w2.Add (this.aboutAction, null);
		this.UsersAction = new global::Gtk.Action ("UsersAction", global::Mono.Unix.Catalog.GetString ("Пользователи"), null, "users");
		this.UsersAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("Пользователи");
		w2.Add (this.UsersAction, null);
		this.quitAction = new global::Gtk.Action ("quitAction", global::Mono.Unix.Catalog.GetString ("В_ыход"), null, "gtk-quit");
		this.quitAction.ShortLabel = global::Mono.Unix.Catalog.GetString ("В_ыход");
		w2.Add (this.quitAction, null);
		this.ActionOrders = new global::Gtk.RadioAction ("ActionOrders", global::Mono.Unix.Catalog.GetString ("Заказы"), null, "order", 0);
		this.ActionOrders.Group = new global::GLib.SList (global::System.IntPtr.Zero);
		this.ActionOrders.ShortLabel = global::Mono.Unix.Catalog.GetString ("Заказы");
		w2.Add (this.ActionOrders, null);
		this.ActionServices = new global::Gtk.RadioAction ("ActionServices", global::Mono.Unix.Catalog.GetString ("Сервис"), null, "service", 0);
		this.ActionServices.Group = this.ActionOrders.Group;
		this.ActionServices.ShortLabel = global::Mono.Unix.Catalog.GetString ("Сервис");
		w2.Add (this.ActionServices, null);
		this.ActionLogistics = new global::Gtk.RadioAction ("ActionLogistics", global::Mono.Unix.Catalog.GetString ("Логистика"), null, "logistic", 0);
		this.ActionLogistics.Group = this.ActionOrders.Group;
		this.ActionLogistics.ShortLabel = global::Mono.Unix.Catalog.GetString ("Логистика");
		w2.Add (this.ActionLogistics, null);
		this.ActionStock = new global::Gtk.RadioAction ("ActionStock", global::Mono.Unix.Catalog.GetString ("Склад"), null, "stock", 0);
		this.ActionStock.Group = this.ActionOrders.Group;
		this.ActionStock.ShortLabel = global::Mono.Unix.Catalog.GetString ("Склад");
		w2.Add (this.ActionStock, null);
		this.ActionCash = new global::Gtk.RadioAction ("ActionCash", global::Mono.Unix.Catalog.GetString ("Касса"), null, "cash", 0);
		this.ActionCash.Group = this.ActionOrders.Group;
		this.ActionCash.ShortLabel = global::Mono.Unix.Catalog.GetString ("Касса");
		w2.Add (this.ActionCash, null);
		this.ActionAccounting = new global::Gtk.RadioAction ("ActionAccounting", global::Mono.Unix.Catalog.GetString ("Бухгалтерия"), null, "accounting", 0);
		this.ActionAccounting.Group = this.ActionOrders.Group;
		this.ActionAccounting.ShortLabel = global::Mono.Unix.Catalog.GetString ("Бухгалтерия");
		w2.Add (this.ActionAccounting, null);
		this.ActionReports = new global::Gtk.RadioAction ("ActionReports", global::Mono.Unix.Catalog.GetString ("Отчеты"), null, "report", 0);
		this.ActionReports.Group = this.ActionOrders.Group;
		this.ActionReports.ShortLabel = global::Mono.Unix.Catalog.GetString ("Отчеты");
		w2.Add (this.ActionReports, null);
		this.ActionArchive = new global::Gtk.RadioAction ("ActionArchive", global::Mono.Unix.Catalog.GetString ("Архив"), null, "archive", 0);
		this.ActionArchive.Group = this.ActionOrders.Group;
		this.ActionArchive.ShortLabel = global::Mono.Unix.Catalog.GetString ("Архив");
		w2.Add (this.ActionArchive, null);
		this.UIManager.InsertActionGroup (w2, 1);
		global::Gtk.ActionGroup w3 = new global::Gtk.ActionGroup ("orders");
		this.ActionNewOrder = new global::Gtk.Action ("ActionNewOrder", global::Mono.Unix.Catalog.GetString ("Новый заказ"), null, "gtk-new");
		this.ActionNewOrder.ShortLabel = global::Mono.Unix.Catalog.GetString ("Новый заказ");
		w3.Add (this.ActionNewOrder, null);
		this.ActionOrdersTable = new global::Gtk.Action ("ActionOrdersTable", global::Mono.Unix.Catalog.GetString ("Журнал заказов"), null, "table");
		this.ActionOrdersTable.ShortLabel = global::Mono.Unix.Catalog.GetString ("Журнал заказов");
		w3.Add (this.ActionOrdersTable, null);
		this.UIManager.InsertActionGroup (w3, 2);
		this.AddAccelGroup (this.UIManager.AccelGroup);
		this.Name = "MainWindow";
		this.Title = global::Mono.Unix.Catalog.GetString ("MainWindow");
		this.WindowPosition = ((global::Gtk.WindowPosition)(4));
		// Container child MainWindow.Gtk.Container+ContainerChild
		this.vbox1 = new global::Gtk.VBox ();
		this.vbox1.Name = "vbox1";
		this.vbox1.Spacing = 6;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><menubar name='menubarMain'><menu name='Action' action='Action'><menuitem name='dialogAuthenticationAction' action='dialogAuthenticationAction'/><menuitem name='UsersAction' action='UsersAction'/><separator/><menuitem name='quitAction' action='quitAction'/></menu><menu name='Action2' action='Action2'><menuitem name='ActionOrganizations' action='ActionOrganizations'/></menu><menu name='Action1' action='Action1'><menuitem name='aboutAction' action='aboutAction'/></menu></menubar></ui>");
		this.menubarMain = ((global::Gtk.MenuBar)(this.UIManager.GetWidget ("/menubarMain")));
		this.menubarMain.Name = "menubarMain";
		this.vbox1.Add (this.menubarMain);
		global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.menubarMain]));
		w4.Position = 0;
		w4.Expand = false;
		w4.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name='toolbarMain'><toolitem name='ActionOrders' action='ActionOrders'/><toolitem name='ActionServices' action='ActionServices'/><toolitem name='ActionLogistics' action='ActionLogistics'/><toolitem name='ActionStock' action='ActionStock'/><toolitem name='ActionCash' action='ActionCash'/><toolitem name='ActionAccounting' action='ActionAccounting'/><toolitem name='ActionReports' action='ActionReports'/><toolitem name='ActionArchive' action='ActionArchive'/></toolbar></ui>");
		this.toolbarMain = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbarMain")));
		this.toolbarMain.Name = "toolbarMain";
		this.toolbarMain.ShowArrow = false;
		this.toolbarMain.ToolbarStyle = ((global::Gtk.ToolbarStyle)(2));
		this.toolbarMain.IconSize = ((global::Gtk.IconSize)(6));
		this.vbox1.Add (this.toolbarMain);
		global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.toolbarMain]));
		w5.Position = 1;
		w5.Expand = false;
		w5.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.UIManager.AddUiFromString ("<ui><toolbar name='toolbarSub'/></ui>");
		this.toolbarSub = ((global::Gtk.Toolbar)(this.UIManager.GetWidget ("/toolbarSub")));
		this.toolbarSub.Name = "toolbarSub";
		this.toolbarSub.ShowArrow = false;
		this.toolbarSub.ToolbarStyle = ((global::Gtk.ToolbarStyle)(1));
		this.toolbarSub.IconSize = ((global::Gtk.IconSize)(2));
		this.vbox1.Add (this.toolbarSub);
		global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.toolbarSub]));
		w6.Position = 2;
		w6.Expand = false;
		w6.Fill = false;
		// Container child vbox1.Gtk.Box+BoxChild
		this.tdiMain = new global::QSTDI.TdiNotebook ();
		this.tdiMain.Name = "tdiMain";
		this.tdiMain.CurrentPage = 0;
		this.tdiMain.ShowTabs = false;
		this.vbox1.Add (this.tdiMain);
		global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.tdiMain]));
		w7.Position = 3;
		// Container child vbox1.Gtk.Box+BoxChild
		this.statusbarMain = new global::Gtk.Statusbar ();
		this.statusbarMain.Name = "statusbarMain";
		this.statusbarMain.Spacing = 6;
		// Container child statusbarMain.Gtk.Box+BoxChild
		this.labelUser = new global::Gtk.Label ();
		this.labelUser.Name = "labelUser";
		this.labelUser.LabelProp = global::Mono.Unix.Catalog.GetString ("label2");
		this.statusbarMain.Add (this.labelUser);
		global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.statusbarMain [this.labelUser]));
		w8.Position = 0;
		w8.Expand = false;
		w8.Fill = false;
		// Container child statusbarMain.Gtk.Box+BoxChild
		this.labelStatus = new global::Gtk.Label ();
		this.labelStatus.Name = "labelStatus";
		this.labelStatus.LabelProp = global::Mono.Unix.Catalog.GetString ("label1");
		this.statusbarMain.Add (this.labelStatus);
		global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.statusbarMain [this.labelStatus]));
		w9.Position = 2;
		w9.Expand = false;
		w9.Fill = false;
		this.vbox1.Add (this.statusbarMain);
		global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1 [this.statusbarMain]));
		w10.PackType = ((global::Gtk.PackType)(1));
		w10.Position = 4;
		w10.Expand = false;
		w10.Fill = false;
		this.Add (this.vbox1);
		if ((this.Child != null)) {
			this.Child.ShowAll ();
		}
		this.DefaultWidth = 739;
		this.DefaultHeight = 300;
		this.Show ();
		this.DeleteEvent += new global::Gtk.DeleteEventHandler (this.OnDeleteEvent);
		this.ActionOrganizations.Activated += new global::System.EventHandler (this.OnActionOrganizationsActivated);
		this.dialogAuthenticationAction.Activated += new global::System.EventHandler (this.OnDialogAuthenticationActionActivated);
		this.aboutAction.Activated += new global::System.EventHandler (this.OnAboutActionActivated);
		this.UsersAction.Activated += new global::System.EventHandler (this.OnAction3Activated);
		this.quitAction.Activated += new global::System.EventHandler (this.OnQuitActionActivated);
		this.ActionOrders.Toggled += new global::System.EventHandler (this.OnActionOrdersToggled);
		this.ActionServices.Toggled += new global::System.EventHandler (this.OnActionServicesToggled);
	}
}
