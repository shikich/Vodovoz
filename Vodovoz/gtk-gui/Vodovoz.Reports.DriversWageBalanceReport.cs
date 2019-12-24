﻿
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Reports
{
	public partial class DriversWageBalanceReport
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox1;

		private global::Gamma.GtkWidgets.yLabel ylabel1;

		private global::QS.Widgets.GtkUI.DatePicker ydateBalanceBefore;

		private global::Gtk.Table table1;

		private global::Gtk.Button buttonSelectAll;

		private global::Gtk.Button buttonSelectWage;

		private global::Gtk.Button buttonUnselectAll;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTreeView ytreeviewDrivers;

		private global::Gtk.Label label1;

		private global::QS.Widgets.GtkUI.DatePicker ydateDateSolary;

		private global::Gtk.Button buttonCreateReport;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Reports.DriversWageBalanceReport
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Reports.DriversWageBalanceReport";
			// Container child Vodovoz.Reports.DriversWageBalanceReport.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.ylabel1 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel1.Name = "ylabel1";
			this.ylabel1.LabelProp = global::Mono.Unix.Catalog.GetString("Дата:");
			this.hbox1.Add(this.ylabel1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.ylabel1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.ydateBalanceBefore = new global::QS.Widgets.GtkUI.DatePicker();
			this.ydateBalanceBefore.Events = ((global::Gdk.EventMask)(256));
			this.ydateBalanceBefore.Name = "ydateBalanceBefore";
			this.ydateBalanceBefore.WithTime = false;
			this.ydateBalanceBefore.Date = new global::System.DateTime(0);
			this.ydateBalanceBefore.IsEditable = true;
			this.ydateBalanceBefore.AutoSeparation = false;
			this.hbox1.Add(this.ydateBalanceBefore);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.ydateBalanceBefore]));
			w2.Position = 1;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(4)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.buttonSelectAll = new global::Gtk.Button();
			this.buttonSelectAll.CanFocus = true;
			this.buttonSelectAll.Name = "buttonSelectAll";
			this.buttonSelectAll.UseUnderline = true;
			this.buttonSelectAll.Label = global::Mono.Unix.Catalog.GetString("Выбрать всех");
			this.table1.Add(this.buttonSelectAll);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.buttonSelectAll]));
			w4.TopAttach = ((uint)(2));
			w4.BottomAttach = ((uint)(3));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.buttonSelectWage = new global::Gtk.Button();
			this.buttonSelectWage.CanFocus = true;
			this.buttonSelectWage.Name = "buttonSelectWage";
			this.buttonSelectWage.UseUnderline = true;
			this.buttonSelectWage.Label = global::Mono.Unix.Catalog.GetString("Проставить ЗП");
			this.table1.Add(this.buttonSelectWage);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.buttonSelectWage]));
			w5.TopAttach = ((uint)(3));
			w5.BottomAttach = ((uint)(4));
			w5.LeftAttach = ((uint)(1));
			w5.RightAttach = ((uint)(2));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.buttonUnselectAll = new global::Gtk.Button();
			this.buttonUnselectAll.CanFocus = true;
			this.buttonUnselectAll.Name = "buttonUnselectAll";
			this.buttonUnselectAll.UseUnderline = true;
			this.buttonUnselectAll.Label = global::Mono.Unix.Catalog.GetString("Снять выделение");
			this.table1.Add(this.buttonUnselectAll);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.buttonUnselectAll]));
			w6.TopAttach = ((uint)(2));
			w6.BottomAttach = ((uint)(3));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.HscrollbarPolicy = ((global::Gtk.PolicyType)(2));
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytreeviewDrivers = new global::Gamma.GtkWidgets.yTreeView();
			this.ytreeviewDrivers.CanFocus = true;
			this.ytreeviewDrivers.Name = "ytreeviewDrivers";
			this.GtkScrolledWindow.Add(this.ytreeviewDrivers);
			this.table1.Add(this.GtkScrolledWindow);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.RightAttach = ((uint)(2));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Водители:");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w9.RightAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ydateDateSolary = new global::QS.Widgets.GtkUI.DatePicker();
			this.ydateDateSolary.Events = ((global::Gdk.EventMask)(256));
			this.ydateDateSolary.Name = "ydateDateSolary";
			this.ydateDateSolary.WithTime = false;
			this.ydateDateSolary.Date = new global::System.DateTime(0);
			this.ydateDateSolary.IsEditable = true;
			this.ydateDateSolary.AutoSeparation = false;
			this.table1.Add(this.ydateDateSolary);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.ydateDateSolary]));
			w10.TopAttach = ((uint)(3));
			w10.BottomAttach = ((uint)(4));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox1.Add(this.table1);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.table1]));
			w11.Position = 1;
			// Container child vbox1.Gtk.Box+BoxChild
			this.buttonCreateReport = new global::Gtk.Button();
			this.buttonCreateReport.CanFocus = true;
			this.buttonCreateReport.Name = "buttonCreateReport";
			this.buttonCreateReport.UseUnderline = true;
			this.buttonCreateReport.Label = global::Mono.Unix.Catalog.GetString("Сформировать отчет");
			this.vbox1.Add(this.buttonCreateReport);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.buttonCreateReport]));
			w12.PackType = ((global::Gtk.PackType)(1));
			w12.Position = 2;
			w12.Expand = false;
			w12.Fill = false;
			this.Add(this.vbox1);
			if((this.Child != null)) {
				this.Child.ShowAll();
			}
			this.Hide();
			this.buttonUnselectAll.Clicked += new global::System.EventHandler(this.OnButtonUnselectAllClicked);
			this.buttonSelectWage.Clicked += new global::System.EventHandler(this.OnButtonSelectWageClicked);
			this.buttonSelectAll.Clicked += new global::System.EventHandler(this.OnButtonSelectAllClicked);
			this.buttonCreateReport.Clicked += new global::System.EventHandler(this.OnButtonCreateReportClicked);
		}
	}
}
