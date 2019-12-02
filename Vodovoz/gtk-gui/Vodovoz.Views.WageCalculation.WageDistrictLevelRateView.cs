
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Views.WageCalculation
{
	public partial class WageDistrictLevelRateView
	{
		private global::Gtk.VBox vbxMain;

		private global::Gtk.HBox hbxControls;

		private global::Gamma.GtkWidgets.yButton btnFillRates;

		private global::Gamma.GtkWidgets.yButton ybuttonAddParameter;

		private global::Gamma.GtkWidgets.yButton ybuttonRemoveParameter;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTreeView treeViewWageRates;

		private global::Gtk.HSeparator hseparator1;

		private global::Vodovoz.Core.WidgetContainerView widgetcontainerview;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Views.WageCalculation.WageDistrictLevelRateView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Views.WageCalculation.WageDistrictLevelRateView";
			// Container child Vodovoz.Views.WageCalculation.WageDistrictLevelRateView.Gtk.Container+ContainerChild
			this.vbxMain = new global::Gtk.VBox();
			this.vbxMain.Name = "vbxMain";
			this.vbxMain.Spacing = 6;
			// Container child vbxMain.Gtk.Box+BoxChild
			this.hbxControls = new global::Gtk.HBox();
			this.hbxControls.Name = "hbxControls";
			this.hbxControls.Spacing = 6;
			// Container child hbxControls.Gtk.Box+BoxChild
			this.btnFillRates = new global::Gamma.GtkWidgets.yButton();
			this.btnFillRates.CanFocus = true;
			this.btnFillRates.Name = "btnFillRates";
			this.btnFillRates.UseUnderline = true;
			this.btnFillRates.Label = global::Mono.Unix.Catalog.GetString("Сгенерировать ставки");
			this.hbxControls.Add(this.btnFillRates);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbxControls[this.btnFillRates]));
			w1.Position = 2;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbxControls.Gtk.Box+BoxChild
			this.ybuttonAddParameter = new global::Gamma.GtkWidgets.yButton();
			this.ybuttonAddParameter.CanFocus = true;
			this.ybuttonAddParameter.Name = "ybuttonAddParameter";
			this.ybuttonAddParameter.UseUnderline = true;
			this.ybuttonAddParameter.Label = global::Mono.Unix.Catalog.GetString("Добавить");
			global::Gtk.Image w2 = new global::Gtk.Image();
			w2.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-add", global::Gtk.IconSize.Menu);
			this.ybuttonAddParameter.Image = w2;
			this.hbxControls.Add(this.ybuttonAddParameter);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbxControls[this.ybuttonAddParameter]));
			w3.Position = 3;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbxControls.Gtk.Box+BoxChild
			this.ybuttonRemoveParameter = new global::Gamma.GtkWidgets.yButton();
			this.ybuttonRemoveParameter.CanFocus = true;
			this.ybuttonRemoveParameter.Name = "ybuttonRemoveParameter";
			this.ybuttonRemoveParameter.UseUnderline = true;
			this.ybuttonRemoveParameter.Label = global::Mono.Unix.Catalog.GetString("Удалить");
			global::Gtk.Image w4 = new global::Gtk.Image();
			w4.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-delete", global::Gtk.IconSize.Menu);
			this.ybuttonRemoveParameter.Image = w4;
			this.hbxControls.Add(this.ybuttonRemoveParameter);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbxControls[this.ybuttonRemoveParameter]));
			w5.Position = 4;
			w5.Expand = false;
			w5.Fill = false;
			this.vbxMain.Add(this.hbxControls);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbxMain[this.hbxControls]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbxMain.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.treeViewWageRates = new global::Gamma.GtkWidgets.yTreeView();
			this.treeViewWageRates.CanFocus = true;
			this.treeViewWageRates.Name = "treeViewWageRates";
			this.GtkScrolledWindow.Add(this.treeViewWageRates);
			this.vbxMain.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbxMain[this.GtkScrolledWindow]));
			w8.Position = 1;
			// Container child vbxMain.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator();
			this.hseparator1.Name = "hseparator1";
			this.vbxMain.Add(this.hseparator1);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbxMain[this.hseparator1]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbxMain.Gtk.Box+BoxChild
			this.widgetcontainerview = new global::Vodovoz.Core.WidgetContainerView();
			this.widgetcontainerview.Events = ((global::Gdk.EventMask)(256));
			this.widgetcontainerview.Name = "widgetcontainerview";
			this.vbxMain.Add(this.widgetcontainerview);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbxMain[this.widgetcontainerview]));
			w10.Position = 3;
			w10.Expand = false;
			w10.Fill = false;
			this.Add(this.vbxMain);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.ybuttonAddParameter.Clicked += new global::System.EventHandler(this.OnYbuttonAddParameterClicked);
			this.ybuttonRemoveParameter.Clicked += new global::System.EventHandler(this.OnYbuttonRemoveParameterClicked);
			this.treeViewWageRates.RowActivated += new global::Gtk.RowActivatedHandler(this.OnTreeViewWageRatesRowActivated);
		}
	}
}
