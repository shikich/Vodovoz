
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class ReadyForReceptionView
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox1;

		private global::Vodovoz.ReadyForReceptionFilter readyforreceptionfilter1;

		private global::Gtk.Button buttonRefresh;

		private global::QSWidgetLib.SearchEntity searchentity1;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::QSOrmProject.RepresentationTreeView tableReadyForReception;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Button buttonOpen;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ReadyForReceptionView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ReadyForReceptionView";
			// Container child Vodovoz.ReadyForReceptionView.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.readyforreceptionfilter1 = new global::Vodovoz.ReadyForReceptionFilter();
			this.readyforreceptionfilter1.Events = ((global::Gdk.EventMask)(256));
			this.readyforreceptionfilter1.Name = "readyforreceptionfilter1";
			this.hbox1.Add(this.readyforreceptionfilter1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.readyforreceptionfilter1]));
			w1.Position = 0;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonRefresh = new global::Gtk.Button();
			this.buttonRefresh.CanFocus = true;
			this.buttonRefresh.Name = "buttonRefresh";
			this.buttonRefresh.UseUnderline = true;
			this.buttonRefresh.Label = global::Mono.Unix.Catalog.GetString("Обновить");
			this.hbox1.Add(this.buttonRefresh);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonRefresh]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.searchentity1 = new global::QSWidgetLib.SearchEntity();
			this.searchentity1.Events = ((global::Gdk.EventMask)(256));
			this.searchentity1.Name = "searchentity1";
			this.vbox1.Add(this.searchentity1);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.searchentity1]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.tableReadyForReception = new global::QSOrmProject.RepresentationTreeView();
			this.tableReadyForReception.CanFocus = true;
			this.tableReadyForReception.Name = "tableReadyForReception";
			this.GtkScrolledWindow.Add(this.tableReadyForReception);
			this.vbox1.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
			w6.Position = 2;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.buttonOpen = new global::Gtk.Button();
			this.buttonOpen.Sensitive = false;
			this.buttonOpen.CanFocus = true;
			this.buttonOpen.Name = "buttonOpen";
			this.buttonOpen.UseUnderline = true;
			this.buttonOpen.Label = global::Mono.Unix.Catalog.GetString("Разгрузить машину");
			global::Gtk.Image w7 = new global::Gtk.Image();
			w7.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-ok", global::Gtk.IconSize.Menu);
			this.buttonOpen.Image = w7;
			this.hbox4.Add(this.buttonOpen);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.buttonOpen]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			this.vbox1.Add(this.hbox4);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox4]));
			w9.Position = 3;
			w9.Expand = false;
			w9.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.buttonRefresh.Clicked += new global::System.EventHandler(this.OnButtonRefreshClicked);
			this.searchentity1.TextChanged += new global::System.EventHandler(this.OnSearchentity1TextChanged);
			this.tableReadyForReception.RowActivated += new global::Gtk.RowActivatedHandler(this.OnTableReadyForReceptionRowActivated);
			this.buttonOpen.Clicked += new global::System.EventHandler(this.OnButtonOpenClicked);
		}
	}
}
