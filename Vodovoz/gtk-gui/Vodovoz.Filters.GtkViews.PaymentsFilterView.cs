
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Filters.GtkViews
{
	public partial class PaymentsFilterView
	{
		private global::Gtk.VBox vboxMain;

		private global::Gtk.HBox hboxMain;

		private global::Gtk.Label label28;

		private global::QS.Widgets.GtkUI.DateRangePicker dateRangeFilter;

		private global::Gamma.Widgets.yEnumComboBox yenumcomboPaymentState;

		private global::Gamma.GtkWidgets.yCheckButton ycheckbtnHideCompleted;

		private global::Gamma.GtkWidgets.yCheckButton chkIsManualCreate;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Filters.GtkViews.PaymentsFilterView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Filters.GtkViews.PaymentsFilterView";
			// Container child Vodovoz.Filters.GtkViews.PaymentsFilterView.Gtk.Container+ContainerChild
			this.vboxMain = new global::Gtk.VBox();
			this.vboxMain.Name = "vboxMain";
			this.vboxMain.Spacing = 6;
			// Container child vboxMain.Gtk.Box+BoxChild
			this.hboxMain = new global::Gtk.HBox();
			this.hboxMain.Name = "hboxMain";
			this.hboxMain.Spacing = 6;
			// Container child hboxMain.Gtk.Box+BoxChild
			this.label28 = new global::Gtk.Label();
			this.label28.Name = "label28";
			this.label28.LabelProp = global::Mono.Unix.Catalog.GetString("Дата:");
			this.hboxMain.Add(this.label28);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hboxMain[this.label28]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hboxMain.Gtk.Box+BoxChild
			this.dateRangeFilter = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.dateRangeFilter.Events = ((global::Gdk.EventMask)(256));
			this.dateRangeFilter.Name = "dateRangeFilter";
			this.dateRangeFilter.StartDate = new global::System.DateTime(0);
			this.dateRangeFilter.EndDate = new global::System.DateTime(0);
			this.hboxMain.Add(this.dateRangeFilter);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hboxMain[this.dateRangeFilter]));
			w2.Position = 1;
			w2.Expand = false;
			// Container child hboxMain.Gtk.Box+BoxChild
			this.yenumcomboPaymentState = new global::Gamma.Widgets.yEnumComboBox();
			this.yenumcomboPaymentState.Name = "yenumcomboPaymentState";
			this.yenumcomboPaymentState.ShowSpecialStateAll = true;
			this.yenumcomboPaymentState.ShowSpecialStateNot = false;
			this.yenumcomboPaymentState.UseShortTitle = false;
			this.yenumcomboPaymentState.DefaultFirst = false;
			this.hboxMain.Add(this.yenumcomboPaymentState);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hboxMain[this.yenumcomboPaymentState]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hboxMain.Gtk.Box+BoxChild
			this.ycheckbtnHideCompleted = new global::Gamma.GtkWidgets.yCheckButton();
			this.ycheckbtnHideCompleted.CanFocus = true;
			this.ycheckbtnHideCompleted.Name = "ycheckbtnHideCompleted";
			this.ycheckbtnHideCompleted.Label = global::Mono.Unix.Catalog.GetString("Скрыть завершенные");
			this.ycheckbtnHideCompleted.DrawIndicator = true;
			this.ycheckbtnHideCompleted.UseUnderline = true;
			this.hboxMain.Add(this.ycheckbtnHideCompleted);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hboxMain[this.ycheckbtnHideCompleted]));
			w4.Position = 3;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hboxMain.Gtk.Box+BoxChild
			this.chkIsManualCreate = new global::Gamma.GtkWidgets.yCheckButton();
			this.chkIsManualCreate.CanFocus = true;
			this.chkIsManualCreate.Name = "chkIsManualCreate";
			this.chkIsManualCreate.Label = global::Mono.Unix.Catalog.GetString("Созданы вручную");
			this.chkIsManualCreate.DrawIndicator = true;
			this.chkIsManualCreate.UseUnderline = true;
			this.hboxMain.Add(this.chkIsManualCreate);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hboxMain[this.chkIsManualCreate]));
			w5.Position = 4;
			w5.Expand = false;
			w5.Fill = false;
			this.vboxMain.Add(this.hboxMain);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vboxMain[this.hboxMain]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			this.Add(this.vboxMain);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
