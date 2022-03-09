
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class RouteListCreateItemsView
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox1;

		private global::Gamma.GtkWidgets.yLabel ylabelOrders;

		private global::QS.Widgets.EnumMenuButton enumbuttonAddOrder;

		private global::Gtk.Button buttonDelete;

		private global::Gtk.Button buttonOpenOrder;

		private global::Gtk.Label labelSum;

		private global::Gtk.Label lblVolume;

		private global::Gtk.Label lblWeight;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTreeView ytreeviewItems;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.RouteListCreateItemsView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.RouteListCreateItemsView";
			// Container child Vodovoz.RouteListCreateItemsView.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.ylabelOrders = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelOrders.Name = "ylabelOrders";
			this.ylabelOrders.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Заказы:</b>");
			this.ylabelOrders.UseMarkup = true;
			this.hbox1.Add(this.ylabelOrders);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.ylabelOrders]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.enumbuttonAddOrder = new global::QS.Widgets.EnumMenuButton();
			this.enumbuttonAddOrder.CanFocus = true;
			this.enumbuttonAddOrder.Name = "enumbuttonAddOrder";
			this.enumbuttonAddOrder.UseUnderline = true;
			this.enumbuttonAddOrder.UseMarkup = false;
			this.enumbuttonAddOrder.LabelXAlign = 0F;
			this.enumbuttonAddOrder.Label = global::Mono.Unix.Catalog.GetString("Добавить");
			global::Gtk.Image w2 = new global::Gtk.Image();
			w2.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-add", global::Gtk.IconSize.Menu);
			this.enumbuttonAddOrder.Image = w2;
			this.hbox1.Add(this.enumbuttonAddOrder);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.enumbuttonAddOrder]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonDelete = new global::Gtk.Button();
			this.buttonDelete.Sensitive = false;
			this.buttonDelete.CanFocus = true;
			this.buttonDelete.Name = "buttonDelete";
			this.buttonDelete.UseUnderline = true;
			this.buttonDelete.Label = global::Mono.Unix.Catalog.GetString("Удалить");
			global::Gtk.Image w4 = new global::Gtk.Image();
			w4.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-delete", global::Gtk.IconSize.Menu);
			this.buttonDelete.Image = w4;
			this.hbox1.Add(this.buttonDelete);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonDelete]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.buttonOpenOrder = new global::Gtk.Button();
			this.buttonOpenOrder.Sensitive = false;
			this.buttonOpenOrder.CanFocus = true;
			this.buttonOpenOrder.Name = "buttonOpenOrder";
			this.buttonOpenOrder.UseUnderline = true;
			this.buttonOpenOrder.Label = global::Mono.Unix.Catalog.GetString("Открыть заказ");
			this.hbox1.Add(this.buttonOpenOrder);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.buttonOpenOrder]));
			w6.Position = 3;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.labelSum = new global::Gtk.Label();
			this.labelSum.Name = "labelSum";
			this.labelSum.Xalign = 1F;
			this.labelSum.LabelProp = "";
			this.hbox1.Add(this.labelSum);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.labelSum]));
			w7.PackType = ((global::Gtk.PackType)(1));
			w7.Position = 4;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.lblVolume = new global::Gtk.Label();
			this.lblVolume.Name = "lblVolume";
			this.lblVolume.Xalign = 0F;
			this.lblVolume.LabelProp = "";
			this.lblVolume.UseMarkup = true;
			this.hbox1.Add(this.lblVolume);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.lblVolume]));
			w8.PackType = ((global::Gtk.PackType)(1));
			w8.Position = 5;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.lblWeight = new global::Gtk.Label();
			this.lblWeight.Name = "lblWeight";
			this.lblWeight.Xalign = 0F;
			this.lblWeight.LabelProp = "";
			this.lblWeight.UseMarkup = true;
			this.hbox1.Add(this.lblWeight);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.lblWeight]));
			w9.PackType = ((global::Gtk.PackType)(1));
			w9.Position = 6;
			w9.Expand = false;
			w9.Fill = false;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytreeviewItems = new global::Gamma.GtkWidgets.yTreeView();
			this.ytreeviewItems.CanFocus = true;
			this.ytreeviewItems.Name = "ytreeviewItems";
			this.ytreeviewItems.Reorderable = true;
			this.GtkScrolledWindow.Add(this.ytreeviewItems);
			this.vbox1.Add(this.GtkScrolledWindow);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.GtkScrolledWindow]));
			w12.Position = 1;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.enumbuttonAddOrder.EnumItemClicked += new global::System.EventHandler<QS.Widgets.EnumItemClickedEventArgs>(this.OnEnumbuttonAddOrderEnumItemClicked);
			this.buttonDelete.Clicked += new global::System.EventHandler(this.OnButtonDeleteClicked);
			this.buttonOpenOrder.Clicked += new global::System.EventHandler(this.OnButtonOpenOrderClicked);
			this.ytreeviewItems.RowActivated += new global::Gtk.RowActivatedHandler(this.OnYtreeviewItemsRowActivated);
		}
	}
}
