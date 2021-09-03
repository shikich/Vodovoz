
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Views.Print
{
	public partial class DocumentsPrinterView
	{
		private global::Gtk.UIManager UIManager;

		private global::Gtk.HBox hboxMain;

		private global::Gtk.VBox vbox1;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTreeView ytreeviewDocuments;

		private global::Gtk.HBox hbox2;

		private global::Gamma.GtkWidgets.yButton ybtnPrintAll;

		private global::Gtk.Button buttonCancel;

		private global::fyiReporting.RdlGtkViewer.ReportViewer reportviewer;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Views.Print.DocumentsPrinterView
			Stetic.BinContainer w1 = global::Stetic.BinContainer.Attach(this);
			this.UIManager = new global::Gtk.UIManager();
			global::Gtk.ActionGroup w2 = new global::Gtk.ActionGroup("Default");
			this.UIManager.InsertActionGroup(w2, 0);
			this.Name = "Vodovoz.Views.Print.DocumentsPrinterView";
			// Container child Vodovoz.Views.Print.DocumentsPrinterView.Gtk.Container+ContainerChild
			this.hboxMain = new global::Gtk.HBox();
			this.hboxMain.Name = "hboxMain";
			this.hboxMain.Spacing = 6;
			// Container child hboxMain.Gtk.Box+BoxChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.HscrollbarPolicy = ((global::Gtk.PolicyType)(2));
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytreeviewDocuments = new global::Gamma.GtkWidgets.yTreeView();
			this.ytreeviewDocuments.CanFocus = true;
			this.ytreeviewDocuments.Name = "ytreeviewDocuments";
			this.GtkScrolledWindow.Add(this.ytreeviewDocuments);
			this.vbox1.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
			w4.Position = 0;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.ybtnPrintAll = new global::Gamma.GtkWidgets.yButton();
			this.ybtnPrintAll.CanFocus = true;
			this.ybtnPrintAll.Name = "ybtnPrintAll";
			this.ybtnPrintAll.UseUnderline = true;
			this.ybtnPrintAll.Label = global::Mono.Unix.Catalog.GetString("Печатать выбранные");
			global::Gtk.Image w5 = new global::Gtk.Image();
			w5.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-print", global::Gtk.IconSize.Menu);
			this.ybtnPrintAll.Image = w5;
			this.hbox2.Add(this.ybtnPrintAll);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.ybtnPrintAll]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString("Отмена");
			global::Gtk.Image w7 = new global::Gtk.Image();
			w7.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-cancel", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w7;
			this.hbox2.Add(this.buttonCancel);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.buttonCancel]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			this.vbox1.Add(this.hbox2);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox2]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			this.hboxMain.Add(this.vbox1);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hboxMain[this.vbox1]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hboxMain.Gtk.Box+BoxChild
			this.reportviewer = new global::fyiReporting.RdlGtkViewer.ReportViewer();
			this.reportviewer.WidthRequest = 0;
			this.reportviewer.HeightRequest = 0;
			this.reportviewer.Events = ((global::Gdk.EventMask)(256));
			this.reportviewer.Name = "reportviewer";
			this.reportviewer.ShowErrors = false;
			this.reportviewer.ShowParameters = false;
			this.hboxMain.Add(this.reportviewer);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hboxMain[this.reportviewer]));
			w11.Position = 1;
			this.Add(this.hboxMain);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			w1.SetUiManager(UIManager);
			this.Hide();
		}
	}
}
