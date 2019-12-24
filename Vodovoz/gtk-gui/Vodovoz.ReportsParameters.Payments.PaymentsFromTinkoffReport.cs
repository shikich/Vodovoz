﻿
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ReportsParameters.Payments
{
	public partial class PaymentsFromTinkoffReport
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.Table table1;

		private global::Gtk.HBox hbox2;

		private global::Gamma.GtkWidgets.yLabel lblShop;

		private global::Gamma.Widgets.ySpecComboBox ySCmbShop;

		private global::QS.Widgets.GtkUI.DatePicker pkrStartDate;

		private global::Gtk.RadioButton rbtnCustomPeriod;

		private global::Gtk.RadioButton rbtnLast3Days;

		private global::Gtk.RadioButton rbtnYesterday;

		private global::Gtk.Button buttonRun;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ReportsParameters.Payments.PaymentsFromTinkoffReport
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ReportsParameters.Payments.PaymentsFromTinkoffReport";
			// Container child Vodovoz.ReportsParameters.Payments.PaymentsFromTinkoffReport.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(4)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			this.table1.BorderWidth = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.lblShop = new global::Gamma.GtkWidgets.yLabel();
			this.lblShop.Name = "lblShop";
			this.lblShop.Xalign = 1F;
			this.lblShop.LabelProp = global::Mono.Unix.Catalog.GetString("Магазин:");
			this.hbox2.Add(this.lblShop);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.lblShop]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.ySCmbShop = new global::Gamma.Widgets.ySpecComboBox();
			this.ySCmbShop.Name = "ySCmbShop";
			this.ySCmbShop.AddIfNotExist = false;
			this.ySCmbShop.DefaultFirst = false;
			this.ySCmbShop.ShowSpecialStateAll = true;
			this.ySCmbShop.ShowSpecialStateNot = false;
			this.hbox2.Add(this.ySCmbShop);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.ySCmbShop]));
			w2.Position = 1;
			this.table1.Add(this.hbox2);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox2]));
			w3.RightAttach = ((uint)(2));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.pkrStartDate = new global::QS.Widgets.GtkUI.DatePicker();
			this.pkrStartDate.Events = ((global::Gdk.EventMask)(256));
			this.pkrStartDate.Name = "pkrStartDate";
			this.pkrStartDate.WithTime = false;
			this.pkrStartDate.Date = new global::System.DateTime(0);
			this.pkrStartDate.IsEditable = false;
			this.pkrStartDate.AutoSeparation = false;
			this.table1.Add(this.pkrStartDate);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.pkrStartDate]));
			w4.TopAttach = ((uint)(3));
			w4.BottomAttach = ((uint)(4));
			w4.LeftAttach = ((uint)(1));
			w4.RightAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.rbtnCustomPeriod = new global::Gtk.RadioButton(global::Mono.Unix.Catalog.GetString("Начиная с даты"));
			this.rbtnCustomPeriod.CanFocus = true;
			this.rbtnCustomPeriod.Name = "rbtnCustomPeriod";
			this.rbtnCustomPeriod.Active = true;
			this.rbtnCustomPeriod.DrawIndicator = true;
			this.rbtnCustomPeriod.UseUnderline = true;
			this.rbtnCustomPeriod.Group = new global::GLib.SList(global::System.IntPtr.Zero);
			this.table1.Add(this.rbtnCustomPeriod);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.rbtnCustomPeriod]));
			w5.TopAttach = ((uint)(3));
			w5.BottomAttach = ((uint)(4));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.rbtnLast3Days = new global::Gtk.RadioButton(global::Mono.Unix.Catalog.GetString("За последние 3 дня"));
			this.rbtnLast3Days.CanFocus = true;
			this.rbtnLast3Days.Name = "rbtnLast3Days";
			this.rbtnLast3Days.DrawIndicator = true;
			this.rbtnLast3Days.UseUnderline = true;
			this.rbtnLast3Days.Group = this.rbtnCustomPeriod.Group;
			this.table1.Add(this.rbtnLast3Days);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.rbtnLast3Days]));
			w6.TopAttach = ((uint)(2));
			w6.BottomAttach = ((uint)(3));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.rbtnYesterday = new global::Gtk.RadioButton(global::Mono.Unix.Catalog.GetString("За вчера"));
			this.rbtnYesterday.CanFocus = true;
			this.rbtnYesterday.Name = "rbtnYesterday";
			this.rbtnYesterday.DrawIndicator = true;
			this.rbtnYesterday.UseUnderline = true;
			this.rbtnYesterday.Group = this.rbtnCustomPeriod.Group;
			this.table1.Add(this.rbtnYesterday);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.rbtnYesterday]));
			w7.TopAttach = ((uint)(1));
			w7.BottomAttach = ((uint)(2));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox2.Add(this.table1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.table1]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.buttonRun = new global::Gtk.Button();
			this.buttonRun.CanFocus = true;
			this.buttonRun.Name = "buttonRun";
			this.buttonRun.UseUnderline = true;
			this.buttonRun.Label = global::Mono.Unix.Catalog.GetString("Сформировать отчет");
			this.vbox2.Add(this.buttonRun);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.buttonRun]));
			w9.PackType = ((global::Gtk.PackType)(1));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			this.Add(this.vbox2);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.buttonRun.Clicked += new global::System.EventHandler(this.OnButtonRunClicked);
		}
	}
}
