﻿
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class RouteListsFilter
	{
		private global::Gtk.Table table1;

		private global::QS.Widgets.GtkUI.DateRangePicker dateperiodOrders;

		private global::Gamma.Widgets.yEnumComboBox enumcomboStatus;

		private global::Gtk.Label label1;

		private global::Gtk.Label label2;

		private global::Gtk.Label label3;

		private global::Gtk.Label label4;

		private global::Gtk.Label label5;

		private global::Gamma.Widgets.yEntryReference yentryreferenceShift;

		private global::Gamma.Widgets.yEnumComboBox yEnumCmbTransport;

		private global::Gamma.Widgets.ySpecComboBox ySpecCmbGeographicGroup;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.RouteListsFilter
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.RouteListsFilter";
			// Container child Vodovoz.RouteListsFilter.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table(((uint)(2)), ((uint)(6)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.dateperiodOrders = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.dateperiodOrders.Events = ((global::Gdk.EventMask)(256));
			this.dateperiodOrders.Name = "dateperiodOrders";
			this.dateperiodOrders.StartDate = new global::System.DateTime(0);
			this.dateperiodOrders.EndDate = new global::System.DateTime(0);
			this.table1.Add(this.dateperiodOrders);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.dateperiodOrders]));
			w1.TopAttach = ((uint)(1));
			w1.BottomAttach = ((uint)(2));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.enumcomboStatus = new global::Gamma.Widgets.yEnumComboBox();
			this.enumcomboStatus.Name = "enumcomboStatus";
			this.enumcomboStatus.ShowSpecialStateAll = true;
			this.enumcomboStatus.ShowSpecialStateNot = false;
			this.enumcomboStatus.UseShortTitle = false;
			this.enumcomboStatus.DefaultFirst = false;
			this.table1.Add(this.enumcomboStatus);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.enumcomboStatus]));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Статус:");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.Xalign = 1F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Смена:");
			this.table1.Add(this.label2);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.label2]));
			w4.TopAttach = ((uint)(1));
			w4.BottomAttach = ((uint)(2));
			w4.LeftAttach = ((uint)(2));
			w4.RightAttach = ((uint)(3));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Район города:");
			this.table1.Add(this.label3);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.label3]));
			w5.LeftAttach = ((uint)(2));
			w5.RightAttach = ((uint)(3));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.Xalign = 1F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Период:");
			this.table1.Add(this.label4);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.label4]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.Xalign = 1F;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Тип ТС:");
			this.table1.Add(this.label5);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.label5]));
			w7.TopAttach = ((uint)(1));
			w7.BottomAttach = ((uint)(2));
			w7.LeftAttach = ((uint)(4));
			w7.RightAttach = ((uint)(5));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yentryreferenceShift = new global::Gamma.Widgets.yEntryReference();
			this.yentryreferenceShift.Events = ((global::Gdk.EventMask)(256));
			this.yentryreferenceShift.Name = "yentryreferenceShift";
			this.table1.Add(this.yentryreferenceShift);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.yentryreferenceShift]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.LeftAttach = ((uint)(3));
			w8.RightAttach = ((uint)(4));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yEnumCmbTransport = new global::Gamma.Widgets.yEnumComboBox();
			this.yEnumCmbTransport.Name = "yEnumCmbTransport";
			this.yEnumCmbTransport.ShowSpecialStateAll = true;
			this.yEnumCmbTransport.ShowSpecialStateNot = false;
			this.yEnumCmbTransport.UseShortTitle = false;
			this.yEnumCmbTransport.DefaultFirst = false;
			this.table1.Add(this.yEnumCmbTransport);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.yEnumCmbTransport]));
			w9.TopAttach = ((uint)(1));
			w9.BottomAttach = ((uint)(2));
			w9.LeftAttach = ((uint)(5));
			w9.RightAttach = ((uint)(6));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ySpecCmbGeographicGroup = new global::Gamma.Widgets.ySpecComboBox();
			this.ySpecCmbGeographicGroup.Name = "ySpecCmbGeographicGroup";
			this.ySpecCmbGeographicGroup.AddIfNotExist = false;
			this.ySpecCmbGeographicGroup.DefaultFirst = false;
			this.ySpecCmbGeographicGroup.ShowSpecialStateAll = true;
			this.ySpecCmbGeographicGroup.ShowSpecialStateNot = false;
			this.table1.Add(this.ySpecCmbGeographicGroup);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.ySpecCmbGeographicGroup]));
			w10.LeftAttach = ((uint)(3));
			w10.RightAttach = ((uint)(4));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			this.Add(this.table1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.ySpecCmbGeographicGroup.Changed += new global::System.EventHandler(this.OnYSpecCmbGeographicGroupChanged);
			this.yEnumCmbTransport.ChangedByUser += new global::System.EventHandler(this.OnYEnumCmbTransportChangedByUser);
			this.yentryreferenceShift.Changed += new global::System.EventHandler(this.OnYentryreferenceShiftChanged);
			this.enumcomboStatus.EnumItemSelected += new global::System.EventHandler<Gamma.Widgets.ItemSelectedEventArgs>(this.OnEnumcomboStatusEnumItemSelected);
			this.dateperiodOrders.PeriodChanged += new global::System.EventHandler(this.OnDateperiodOrdersPeriodChanged);
		}
	}
}
