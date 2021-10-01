
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ReportsParameters.Logistic
{
	public partial class MileageReport
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Label label1;

		private global::QS.Widgets.GtkUI.DateRangePicker dateperiodpicker;

		private global::Gtk.HBox hbox7;

		private global::Gtk.Label label4;

		private global::Gamma.GtkWidgets.yCheckButton ycheckbutton1;

		private global::Gtk.HBox hbox3;

		private global::Gtk.Label label2;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entityviewmodelentryCar;

		private global::Gtk.HBox hbox5;

		private global::Gtk.Label label3;

		private global::QS.Views.Control.EntityEntry driverEntry;

		private global::Gtk.HBox hbox8;

		private global::Gtk.Label label5;

		private global::QS.Widgets.ValidatedEntry validatedentryDifference;

		private global::Gtk.HBox hbox9;

		private global::Gtk.Label label6;

		private global::Gamma.GtkWidgets.yCheckButton checkAnyStatus;

		private global::Gtk.Button buttonCreateReport;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ReportsParameters.Logistic.MileageReport
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ReportsParameters.Logistic.MileageReport";
			// Container child Vodovoz.ReportsParameters.Logistic.MileageReport.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 0F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Период:");
			this.hbox1.Add(this.label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.label1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.dateperiodpicker = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.dateperiodpicker.Events = ((global::Gdk.EventMask)(256));
			this.dateperiodpicker.Name = "dateperiodpicker";
			this.dateperiodpicker.StartDate = new global::System.DateTime(0);
			this.dateperiodpicker.EndDate = new global::System.DateTime(0);
			this.hbox1.Add(this.dateperiodpicker);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.dateperiodpicker]));
			w2.Position = 1;
			this.vbox2.Add(this.hbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox7 = new global::Gtk.HBox();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Только наши автомобили:");
			this.hbox7.Add(this.label4);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.label4]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.ycheckbutton1 = new global::Gamma.GtkWidgets.yCheckButton();
			this.ycheckbutton1.CanFocus = true;
			this.ycheckbutton1.Name = "ycheckbutton1";
			this.ycheckbutton1.Label = global::Mono.Unix.Catalog.GetString(" ");
			this.ycheckbutton1.DrawIndicator = true;
			this.ycheckbutton1.UseUnderline = true;
			this.hbox7.Add(this.ycheckbutton1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.ycheckbutton1]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			this.vbox2.Add(this.hbox7);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox7]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Автомобиль:");
			this.hbox3.Add(this.label2);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.label2]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.entityviewmodelentryCar = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entityviewmodelentryCar.Events = ((global::Gdk.EventMask)(256));
			this.entityviewmodelentryCar.Name = "entityviewmodelentryCar";
			this.entityviewmodelentryCar.CanEditReference = true;
			this.hbox3.Add(this.entityviewmodelentryCar);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.entityviewmodelentryCar]));
			w8.Position = 1;
			this.vbox2.Add(this.hbox3);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox3]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Водитель:");
			this.hbox5.Add(this.label3);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.label3]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.driverEntry = new global::QS.Views.Control.EntityEntry();
			this.driverEntry.Events = ((global::Gdk.EventMask)(256));
			this.driverEntry.Name = "driverEntry";
			this.hbox5.Add(this.driverEntry);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.driverEntry]));
			w11.Position = 1;
			this.vbox2.Add(this.hbox5);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox5]));
			w12.Position = 3;
			w12.Expand = false;
			w12.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox8 = new global::Gtk.HBox();
			this.hbox8.Name = "hbox8";
			this.hbox8.Spacing = 6;
			// Container child hbox8.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Расхождение км ≥");
			this.hbox8.Add(this.label5);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.label5]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Container child hbox8.Gtk.Box+BoxChild
			this.validatedentryDifference = new global::QS.Widgets.ValidatedEntry();
			this.validatedentryDifference.CanFocus = true;
			this.validatedentryDifference.Name = "validatedentryDifference";
			this.validatedentryDifference.IsEditable = true;
			this.validatedentryDifference.InvisibleChar = '•';
			this.hbox8.Add(this.validatedentryDifference);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox8[this.validatedentryDifference]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
			this.vbox2.Add(this.hbox8);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox8]));
			w15.Position = 4;
			w15.Expand = false;
			w15.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbox9 = new global::Gtk.HBox();
			this.hbox9.Name = "hbox9";
			this.hbox9.Spacing = 6;
			// Container child hbox9.Gtk.Box+BoxChild
			this.label6 = new global::Gtk.Label();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString("Отображать МЛ в любом статусе:");
			this.hbox9.Add(this.label6);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox9[this.label6]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hbox9.Gtk.Box+BoxChild
			this.checkAnyStatus = new global::Gamma.GtkWidgets.yCheckButton();
			this.checkAnyStatus.CanFocus = true;
			this.checkAnyStatus.Name = "checkAnyStatus";
			this.checkAnyStatus.Label = "";
			this.checkAnyStatus.DrawIndicator = true;
			this.checkAnyStatus.UseUnderline = true;
			this.hbox9.Add(this.checkAnyStatus);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox9[this.checkAnyStatus]));
			w17.Position = 1;
			w17.Expand = false;
			w17.Fill = false;
			this.vbox2.Add(this.hbox9);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbox9]));
			w18.Position = 5;
			w18.Expand = false;
			w18.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.buttonCreateReport = new global::Gtk.Button();
			this.buttonCreateReport.Sensitive = false;
			this.buttonCreateReport.CanFocus = true;
			this.buttonCreateReport.Name = "buttonCreateReport";
			this.buttonCreateReport.UseUnderline = true;
			this.buttonCreateReport.Label = global::Mono.Unix.Catalog.GetString("Сформировать отчет");
			this.vbox2.Add(this.buttonCreateReport);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.buttonCreateReport]));
			w19.Position = 6;
			w19.Expand = false;
			w19.Fill = false;
			this.Add(this.vbox2);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.dateperiodpicker.PeriodChanged += new global::System.EventHandler(this.OnDateperiodpickerPeriodChanged);
			this.buttonCreateReport.Clicked += new global::System.EventHandler(this.OnButtonCreateReportClicked);
		}
	}
}
