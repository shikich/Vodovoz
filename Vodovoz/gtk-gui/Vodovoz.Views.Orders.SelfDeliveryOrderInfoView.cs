
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Views.Orders
{
	public partial class SelfDeliveryOrderInfoView
	{
		private global::Gtk.VBox vboxMain;

		private global::Gtk.ScrolledWindow scrolledWindowMain;

		private global::Gtk.HBox hbox1;

		private global::Vodovoz.ViewWidgets.Orders.OrderInfoExpandedPanelView orderInfoExpandedPanelView;

		private global::Gtk.VSeparator vseparator1;

		private global::Gtk.HBox hboxOrderItems;

		private global::Gtk.HBox hboxJournals;

		private global::Gtk.HSeparator hseparator1;

		private global::Gtk.HBox hboxSumAndTrifle;

		private global::Gamma.GtkWidgets.yLabel ylblTotal;

		private global::Gamma.GtkWidgets.yLabel ylblOrderSum;

		private global::Gamma.GtkWidgets.yLabel ylblTrifle;

		private global::Gamma.Widgets.yValidatedEntry entryTrifle;

		private global::Gtk.HSeparator hseparator2;

		private global::Gtk.HBox hboxManageBtns;

		private global::Gamma.GtkWidgets.yButton ybtnAccept;

		private global::Gamma.GtkWidgets.yButton ybtnWaitingForPayment;

		private global::Gamma.GtkWidgets.yButton ybtnSendingMaster;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Views.Orders.SelfDeliveryOrderInfoView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Views.Orders.SelfDeliveryOrderInfoView";
			// Container child Vodovoz.Views.Orders.SelfDeliveryOrderInfoView.Gtk.Container+ContainerChild
			this.vboxMain = new global::Gtk.VBox();
			this.vboxMain.Name = "vboxMain";
			this.vboxMain.Spacing = 6;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.scrolledWindowMain = new global::Gtk.ScrolledWindow();
			this.scrolledWindowMain.CanFocus = true;
			this.scrolledWindowMain.Name = "scrolledWindowMain";
			this.scrolledWindowMain.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child scrolledWindowMain.Gtk.Container+ContainerChild
			global::Gtk.Viewport w1 = new global::Gtk.Viewport();
			w1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child GtkViewport.Gtk.Container+ContainerChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.orderInfoExpandedPanelView = new global::Vodovoz.ViewWidgets.Orders.OrderInfoExpandedPanelView();
			this.orderInfoExpandedPanelView.Events = ((global::Gdk.EventMask)(256));
			this.orderInfoExpandedPanelView.Name = "orderInfoExpandedPanelView";
			this.hbox1.Add(this.orderInfoExpandedPanelView);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.orderInfoExpandedPanelView]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vseparator1 = new global::Gtk.VSeparator();
			this.vseparator1.Name = "vseparator1";
			this.hbox1.Add(this.vseparator1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vseparator1]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.hboxOrderItems = new global::Gtk.HBox();
			this.hboxOrderItems.Name = "hboxOrderItems";
			this.hboxOrderItems.Spacing = 6;
			this.hbox1.Add(this.hboxOrderItems);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.hboxOrderItems]));
			w4.Position = 2;
			// Container child hbox1.Gtk.Box+BoxChild
			this.hboxJournals = new global::Gtk.HBox();
			this.hboxJournals.Name = "hboxJournals";
			this.hboxJournals.Spacing = 6;
			this.hbox1.Add(this.hboxJournals);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.hboxJournals]));
			w5.Position = 3;
			w1.Add(this.hbox1);
			this.scrolledWindowMain.Add(w1);
			this.vboxMain.Add(this.scrolledWindowMain);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.scrolledWindowMain]));
			w8.Position = 0;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator();
			this.hseparator1.Name = "hseparator1";
			this.vboxMain.Add(this.hseparator1);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.hseparator1]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.hboxSumAndTrifle = new global::Gtk.HBox();
			this.hboxSumAndTrifle.Name = "hboxSumAndTrifle";
			this.hboxSumAndTrifle.Spacing = 6;
			// Container child hboxSumAndTrifle.Gtk.Box+BoxChild
			this.ylblTotal = new global::Gamma.GtkWidgets.yLabel();
			this.ylblTotal.Name = "ylblTotal";
			this.ylblTotal.LabelProp = global::Mono.Unix.Catalog.GetString("Итого:");
			this.hboxSumAndTrifle.Add(this.ylblTotal);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hboxSumAndTrifle[this.ylblTotal]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hboxSumAndTrifle.Gtk.Box+BoxChild
			this.ylblOrderSum = new global::Gamma.GtkWidgets.yLabel();
			this.ylblOrderSum.Name = "ylblOrderSum";
			this.ylblOrderSum.LabelProp = global::Mono.Unix.Catalog.GetString("сумма");
			this.hboxSumAndTrifle.Add(this.ylblOrderSum);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hboxSumAndTrifle[this.ylblOrderSum]));
			w11.Position = 1;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hboxSumAndTrifle.Gtk.Box+BoxChild
			this.ylblTrifle = new global::Gamma.GtkWidgets.yLabel();
			this.ylblTrifle.Name = "ylblTrifle";
			this.ylblTrifle.LabelProp = global::Mono.Unix.Catalog.GetString("Сдача с:");
			this.hboxSumAndTrifle.Add(this.ylblTrifle);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hboxSumAndTrifle[this.ylblTrifle]));
			w12.Position = 2;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hboxSumAndTrifle.Gtk.Box+BoxChild
			this.entryTrifle = new global::Gamma.Widgets.yValidatedEntry();
			this.entryTrifle.CanFocus = true;
			this.entryTrifle.Name = "entryTrifle";
			this.entryTrifle.IsEditable = true;
			this.entryTrifle.InvisibleChar = '•';
			this.hboxSumAndTrifle.Add(this.entryTrifle);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hboxSumAndTrifle[this.entryTrifle]));
			w13.Position = 3;
			w13.Expand = false;
			w13.Fill = false;
			this.vboxMain.Add(this.hboxSumAndTrifle);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.hboxSumAndTrifle]));
			w14.Position = 2;
			w14.Expand = false;
			w14.Fill = false;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.hseparator2 = new global::Gtk.HSeparator();
			this.hseparator2.Name = "hseparator2";
			this.vboxMain.Add(this.hseparator2);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.hseparator2]));
			w15.Position = 3;
			w15.Expand = false;
			w15.Fill = false;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.hboxManageBtns = new global::Gtk.HBox();
			this.hboxManageBtns.Name = "hboxManageBtns";
			this.hboxManageBtns.Spacing = 6;
			// Container child hboxManageBtns.Gtk.Box+BoxChild
			this.ybtnAccept = new global::Gamma.GtkWidgets.yButton();
			this.ybtnAccept.CanFocus = true;
			this.ybtnAccept.Name = "ybtnAccept";
			this.ybtnAccept.UseUnderline = true;
			this.ybtnAccept.Label = global::Mono.Unix.Catalog.GetString("Подтвердить");
			this.hboxManageBtns.Add(this.ybtnAccept);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hboxManageBtns[this.ybtnAccept]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hboxManageBtns.Gtk.Box+BoxChild
			this.ybtnWaitingForPayment = new global::Gamma.GtkWidgets.yButton();
			this.ybtnWaitingForPayment.CanFocus = true;
			this.ybtnWaitingForPayment.Name = "ybtnWaitingForPayment";
			this.ybtnWaitingForPayment.UseUnderline = true;
			this.ybtnWaitingForPayment.Label = global::Mono.Unix.Catalog.GetString("Ожидание оплаты");
			this.hboxManageBtns.Add(this.ybtnWaitingForPayment);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hboxManageBtns[this.ybtnWaitingForPayment]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			// Container child hboxManageBtns.Gtk.Box+BoxChild
			this.ybtnSendingMaster = new global::Gamma.GtkWidgets.yButton();
			this.ybtnSendingMaster.CanFocus = true;
			this.ybtnSendingMaster.Name = "ybtnSendingMaster";
			this.ybtnSendingMaster.UseUnderline = true;
			this.ybtnSendingMaster.Label = global::Mono.Unix.Catalog.GetString("Выезд мастера");
			this.hboxManageBtns.Add(this.ybtnSendingMaster);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hboxManageBtns[this.ybtnSendingMaster]));
			w18.Position = 2;
			w18.Expand = false;
			w18.Fill = false;
			this.vboxMain.Add(this.hboxManageBtns);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.hboxManageBtns]));
			w19.Position = 4;
			w19.Expand = false;
			w19.Fill = false;
			this.Add(this.vboxMain);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.hboxJournals.Hide();
			this.Hide();
		}
	}
}
