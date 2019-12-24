﻿
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ReportsParameters.Sales
{
	public partial class OrderCreationDateReport
	{
		private global::Gtk.VBox vboxMainMastersReport;

		private global::Gtk.HBox hboxPeriod;

		private global::Gtk.Label labelPeriod;

		private global::QS.Widgets.GtkUI.DateRangePicker datePeriodPicker;

		private global::Gtk.HBox hboxDriver;

		private global::Gtk.Label lblEmployee;

		private global::QS.Widgets.GtkUI.RepresentationEntry yEntRefEmployee;

		private global::Gtk.Button buttonCreateReport;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ReportsParameters.Sales.OrderCreationDateReport
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ReportsParameters.Sales.OrderCreationDateReport";
			// Container child Vodovoz.ReportsParameters.Sales.OrderCreationDateReport.Gtk.Container+ContainerChild
			this.vboxMainMastersReport = new global::Gtk.VBox();
			this.vboxMainMastersReport.Name = "vboxMainMastersReport";
			this.vboxMainMastersReport.Spacing = 6;
			// Container child vboxMainMastersReport.Gtk.Box+BoxChild
			this.hboxPeriod = new global::Gtk.HBox();
			this.hboxPeriod.Name = "hboxPeriod";
			this.hboxPeriod.Spacing = 6;
			// Container child hboxPeriod.Gtk.Box+BoxChild
			this.labelPeriod = new global::Gtk.Label();
			this.labelPeriod.Name = "labelPeriod";
			this.labelPeriod.Xalign = 0F;
			this.labelPeriod.LabelProp = global::Mono.Unix.Catalog.GetString("Период:");
			this.hboxPeriod.Add(this.labelPeriod);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hboxPeriod[this.labelPeriod]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hboxPeriod.Gtk.Box+BoxChild
			this.datePeriodPicker = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.datePeriodPicker.Events = ((global::Gdk.EventMask)(256));
			this.datePeriodPicker.Name = "datePeriodPicker";
			this.datePeriodPicker.StartDate = new global::System.DateTime(0);
			this.datePeriodPicker.EndDate = new global::System.DateTime(0);
			this.hboxPeriod.Add(this.datePeriodPicker);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hboxPeriod[this.datePeriodPicker]));
			w2.Position = 1;
			this.vboxMainMastersReport.Add(this.hboxPeriod);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vboxMainMastersReport[this.hboxPeriod]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vboxMainMastersReport.Gtk.Box+BoxChild
			this.hboxDriver = new global::Gtk.HBox();
			this.hboxDriver.Name = "hboxDriver";
			this.hboxDriver.Spacing = 6;
			// Container child hboxDriver.Gtk.Box+BoxChild
			this.lblEmployee = new global::Gtk.Label();
			this.lblEmployee.Name = "lblEmployee";
			this.lblEmployee.LabelProp = global::Mono.Unix.Catalog.GetString("Сотрудник:");
			this.hboxDriver.Add(this.lblEmployee);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hboxDriver[this.lblEmployee]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hboxDriver.Gtk.Box+BoxChild
			this.yEntRefEmployee = new global::QS.Widgets.GtkUI.RepresentationEntry();
			this.yEntRefEmployee.Events = ((global::Gdk.EventMask)(256));
			this.yEntRefEmployee.Name = "yEntRefEmployee";
			this.hboxDriver.Add(this.yEntRefEmployee);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hboxDriver[this.yEntRefEmployee]));
			w5.Position = 1;
			this.vboxMainMastersReport.Add(this.hboxDriver);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vboxMainMastersReport[this.hboxDriver]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vboxMainMastersReport.Gtk.Box+BoxChild
			this.buttonCreateReport = new global::Gtk.Button();
			this.buttonCreateReport.Sensitive = false;
			this.buttonCreateReport.CanFocus = true;
			this.buttonCreateReport.Name = "buttonCreateReport";
			this.buttonCreateReport.UseUnderline = true;
			this.buttonCreateReport.Label = global::Mono.Unix.Catalog.GetString("Сформировать отчет");
			this.vboxMainMastersReport.Add(this.buttonCreateReport);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vboxMainMastersReport[this.buttonCreateReport]));
			w7.Position = 3;
			w7.Expand = false;
			w7.Fill = false;
			this.Add(this.vboxMainMastersReport);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.datePeriodPicker.PeriodChanged += new global::System.EventHandler(this.OnChangeReportParameters);
			this.yEntRefEmployee.Changed += new global::System.EventHandler(this.OnChangeReportParameters);
			this.buttonCreateReport.Clicked += new global::System.EventHandler(this.OnButtonCreateReportClicked);
		}
	}
}
