﻿
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ReportsParameters.Logistic
{
	public partial class FuelConsumptionReport
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Label label1;

		private global::QS.Widgets.GtkUI.DateRangePicker dateperiodpicker;

		private global::Vodovoz.ViewWidgets.GeographicGroupsToStringWidget geographicGroup;

		private global::Gtk.CheckButton chkDetailed;

		private global::Gtk.Button buttonCreateReport;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ReportsParameters.Logistic.FuelConsumptionReport
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ReportsParameters.Logistic.FuelConsumptionReport";
			// Container child Vodovoz.ReportsParameters.Logistic.FuelConsumptionReport.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Дата:");
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
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.geographicGroup = new global::Vodovoz.ViewWidgets.GeographicGroupsToStringWidget();
			this.geographicGroup.Events = ((global::Gdk.EventMask)(256));
			this.geographicGroup.Name = "geographicGroup";
			this.geographicGroup.Label = "";
			this.vbox1.Add(this.geographicGroup);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.geographicGroup]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.chkDetailed = new global::Gtk.CheckButton();
			this.chkDetailed.CanFocus = true;
			this.chkDetailed.Name = "chkDetailed";
			this.chkDetailed.Label = global::Mono.Unix.Catalog.GetString("Подробный отчёт");
			this.chkDetailed.DrawIndicator = true;
			this.chkDetailed.UseUnderline = true;
			this.vbox1.Add(this.chkDetailed);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.chkDetailed]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.buttonCreateReport = new global::Gtk.Button();
			this.buttonCreateReport.CanFocus = true;
			this.buttonCreateReport.Name = "buttonCreateReport";
			this.buttonCreateReport.UseUnderline = true;
			this.buttonCreateReport.Label = global::Mono.Unix.Catalog.GetString("Сформировать отчет");
			this.vbox1.Add(this.buttonCreateReport);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.buttonCreateReport]));
			w6.Position = 4;
			w6.Expand = false;
			w6.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.buttonCreateReport.Clicked += new global::System.EventHandler(this.OnButtonCreateReportClicked);
		}
	}
}
