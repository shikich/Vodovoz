
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Filters.GtkViews
{
	public partial class DebtorsFilterView
	{
		private global::Gtk.HBox hbox2;

		private global::Gtk.Table table1;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entityVMEntryDeliveryPoint;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryreferenceClient;

		private global::Gtk.HBox hboxBottleDebtCount;

		private global::Gamma.Widgets.yValidatedEntry yvalidatedentryDebtFrom;

		private global::Gtk.Label label6;

		private global::Gamma.Widgets.yValidatedEntry yvalidatedentryDebtTo;

		private global::Gtk.Label label1;

		private global::Gtk.Label label2;

		private global::Gtk.Label label4;

		private global::Gtk.Label label5;

		private global::Gamma.Widgets.yEnumComboBox yenumcomboboxOPF;

		private global::Gtk.VSeparator vseparator6;

		private global::Gtk.Table table2;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entityviewmodelentryNomenclature;

		private global::Gtk.HBox hbox5;

		private global::QS.Widgets.GtkUI.DateRangePicker ydateperiodpickerLastOrder;

		private global::Gamma.GtkWidgets.yCheckButton ycheckbuttonHideActive;

		private global::Gtk.HBox hboxBottlesCount;

		private global::Gamma.Widgets.yValidatedEntry yvalidatedentryBottlesFrom;

		private global::Gtk.Label label8;

		private global::Gamma.Widgets.yValidatedEntry yvalidatedentryBottlesTo;

		private global::Gtk.Label label10;

		private global::Gtk.Label label3;

		private global::Gtk.Label label7;

		private global::Gtk.Label label9;

		private global::Gamma.GtkWidgets.yCheckButton ycheckbuttonHideOneOrder;

		private global::Gamma.Widgets.ySpecComboBox ycomboboxReason;

		private global::Gtk.VSeparator vseparator1;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Filters.GtkViews.DebtorsFilterView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Filters.GtkViews.DebtorsFilterView";
			// Container child Vodovoz.Filters.GtkViews.DebtorsFilterView.Gtk.Container+ContainerChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(4)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.entityVMEntryDeliveryPoint = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entityVMEntryDeliveryPoint.Events = ((global::Gdk.EventMask)(256));
			this.entityVMEntryDeliveryPoint.Name = "entityVMEntryDeliveryPoint";
			this.entityVMEntryDeliveryPoint.CanEditReference = false;
			this.table1.Add(this.entityVMEntryDeliveryPoint);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.entityVMEntryDeliveryPoint]));
			w1.TopAttach = ((uint)(1));
			w1.BottomAttach = ((uint)(2));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryreferenceClient = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryreferenceClient.Events = ((global::Gdk.EventMask)(256));
			this.entryreferenceClient.Name = "entryreferenceClient";
			this.entryreferenceClient.CanEditReference = false;
			this.table1.Add(this.entryreferenceClient);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.entryreferenceClient]));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hboxBottleDebtCount = new global::Gtk.HBox();
			this.hboxBottleDebtCount.Name = "hboxBottleDebtCount";
			this.hboxBottleDebtCount.Spacing = 6;
			// Container child hboxBottleDebtCount.Gtk.Box+BoxChild
			this.yvalidatedentryDebtFrom = new global::Gamma.Widgets.yValidatedEntry();
			this.yvalidatedentryDebtFrom.CanFocus = true;
			this.yvalidatedentryDebtFrom.Name = "yvalidatedentryDebtFrom";
			this.yvalidatedentryDebtFrom.IsEditable = true;
			this.yvalidatedentryDebtFrom.InvisibleChar = '•';
			this.hboxBottleDebtCount.Add(this.yvalidatedentryDebtFrom);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hboxBottleDebtCount[this.yvalidatedentryDebtFrom]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hboxBottleDebtCount.Gtk.Box+BoxChild
			this.label6 = new global::Gtk.Label();
			this.label6.Name = "label6";
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString("до");
			this.hboxBottleDebtCount.Add(this.label6);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hboxBottleDebtCount[this.label6]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hboxBottleDebtCount.Gtk.Box+BoxChild
			this.yvalidatedentryDebtTo = new global::Gamma.Widgets.yValidatedEntry();
			this.yvalidatedentryDebtTo.CanFocus = true;
			this.yvalidatedentryDebtTo.Name = "yvalidatedentryDebtTo";
			this.yvalidatedentryDebtTo.IsEditable = true;
			this.yvalidatedentryDebtTo.InvisibleChar = '•';
			this.hboxBottleDebtCount.Add(this.yvalidatedentryDebtTo);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hboxBottleDebtCount[this.yvalidatedentryDebtTo]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			this.table1.Add(this.hboxBottleDebtCount);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.hboxBottleDebtCount]));
			w6.TopAttach = ((uint)(3));
			w6.BottomAttach = ((uint)(4));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Контрагент:");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("ОПФ");
			this.table1.Add(this.label2);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.label2]));
			w8.TopAttach = ((uint)(2));
			w8.BottomAttach = ((uint)(3));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Адрес :");
			this.table1.Add(this.label4);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.label4]));
			w9.TopAttach = ((uint)(1));
			w9.BottomAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Долг по таре от");
			this.table1.Add(this.label5);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.label5]));
			w10.TopAttach = ((uint)(3));
			w10.BottomAttach = ((uint)(4));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yenumcomboboxOPF = new global::Gamma.Widgets.yEnumComboBox();
			this.yenumcomboboxOPF.Name = "yenumcomboboxOPF";
			this.yenumcomboboxOPF.ShowSpecialStateAll = true;
			this.yenumcomboboxOPF.ShowSpecialStateNot = false;
			this.yenumcomboboxOPF.UseShortTitle = false;
			this.yenumcomboboxOPF.DefaultFirst = false;
			this.table1.Add(this.yenumcomboboxOPF);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.yenumcomboboxOPF]));
			w11.TopAttach = ((uint)(2));
			w11.BottomAttach = ((uint)(3));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			this.hbox2.Add(this.table1);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.table1]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.vseparator6 = new global::Gtk.VSeparator();
			this.vseparator6.Name = "vseparator6";
			this.hbox2.Add(this.vseparator6);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.vseparator6]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.table2 = new global::Gtk.Table(((uint)(4)), ((uint)(3)), false);
			this.table2.Name = "table2";
			this.table2.RowSpacing = ((uint)(6));
			this.table2.ColumnSpacing = ((uint)(6));
			// Container child table2.Gtk.Table+TableChild
			this.entityviewmodelentryNomenclature = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entityviewmodelentryNomenclature.Events = ((global::Gdk.EventMask)(256));
			this.entityviewmodelentryNomenclature.Name = "entityviewmodelentryNomenclature";
			this.entityviewmodelentryNomenclature.CanEditReference = false;
			this.table2.Add(this.entityviewmodelentryNomenclature);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table2[this.entityviewmodelentryNomenclature]));
			w14.TopAttach = ((uint)(2));
			w14.BottomAttach = ((uint)(3));
			w14.LeftAttach = ((uint)(1));
			w14.RightAttach = ((uint)(2));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.ydateperiodpickerLastOrder = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.ydateperiodpickerLastOrder.Events = ((global::Gdk.EventMask)(256));
			this.ydateperiodpickerLastOrder.Name = "ydateperiodpickerLastOrder";
			this.ydateperiodpickerLastOrder.StartDate = new global::System.DateTime(0);
			this.ydateperiodpickerLastOrder.EndDate = new global::System.DateTime(0);
			this.hbox5.Add(this.ydateperiodpickerLastOrder);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.ydateperiodpickerLastOrder]));
			w15.Position = 0;
			// Container child hbox5.Gtk.Box+BoxChild
			this.ycheckbuttonHideActive = new global::Gamma.GtkWidgets.yCheckButton();
			this.ycheckbuttonHideActive.CanFocus = true;
			this.ycheckbuttonHideActive.Name = "ycheckbuttonHideActive";
			this.ycheckbuttonHideActive.Label = global::Mono.Unix.Catalog.GetString("Скрыть активных");
			this.ycheckbuttonHideActive.DrawIndicator = true;
			this.ycheckbuttonHideActive.UseUnderline = true;
			this.hbox5.Add(this.ycheckbuttonHideActive);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.ycheckbuttonHideActive]));
			w16.Position = 1;
			w16.Expand = false;
			w16.Fill = false;
			this.table2.Add(this.hbox5);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table2[this.hbox5]));
			w17.LeftAttach = ((uint)(1));
			w17.RightAttach = ((uint)(2));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.hboxBottlesCount = new global::Gtk.HBox();
			this.hboxBottlesCount.Name = "hboxBottlesCount";
			this.hboxBottlesCount.Spacing = 6;
			// Container child hboxBottlesCount.Gtk.Box+BoxChild
			this.yvalidatedentryBottlesFrom = new global::Gamma.Widgets.yValidatedEntry();
			this.yvalidatedentryBottlesFrom.CanFocus = true;
			this.yvalidatedentryBottlesFrom.Name = "yvalidatedentryBottlesFrom";
			this.yvalidatedentryBottlesFrom.IsEditable = true;
			this.yvalidatedentryBottlesFrom.InvisibleChar = '•';
			this.hboxBottlesCount.Add(this.yvalidatedentryBottlesFrom);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hboxBottlesCount[this.yvalidatedentryBottlesFrom]));
			w18.Position = 0;
			w18.Expand = false;
			w18.Fill = false;
			// Container child hboxBottlesCount.Gtk.Box+BoxChild
			this.label8 = new global::Gtk.Label();
			this.label8.Name = "label8";
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString("до");
			this.hboxBottlesCount.Add(this.label8);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hboxBottlesCount[this.label8]));
			w19.Position = 1;
			w19.Expand = false;
			w19.Fill = false;
			// Container child hboxBottlesCount.Gtk.Box+BoxChild
			this.yvalidatedentryBottlesTo = new global::Gamma.Widgets.yValidatedEntry();
			this.yvalidatedentryBottlesTo.CanFocus = true;
			this.yvalidatedentryBottlesTo.Name = "yvalidatedentryBottlesTo";
			this.yvalidatedentryBottlesTo.IsEditable = true;
			this.yvalidatedentryBottlesTo.InvisibleChar = '•';
			this.hboxBottlesCount.Add(this.yvalidatedentryBottlesTo);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hboxBottlesCount[this.yvalidatedentryBottlesTo]));
			w20.Position = 2;
			w20.Expand = false;
			w20.Fill = false;
			this.table2.Add(this.hboxBottlesCount);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table2[this.hboxBottlesCount]));
			w21.TopAttach = ((uint)(3));
			w21.BottomAttach = ((uint)(4));
			w21.LeftAttach = ((uint)(1));
			w21.RightAttach = ((uint)(2));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label10 = new global::Gtk.Label();
			this.label10.Name = "label10";
			this.label10.LabelProp = global::Mono.Unix.Catalog.GetString("ТМЦ в посл. заказе:");
			this.table2.Add(this.label10);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table2[this.label10]));
			w22.TopAttach = ((uint)(2));
			w22.BottomAttach = ((uint)(3));
			w22.XOptions = ((global::Gtk.AttachOptions)(4));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Последн. заказ :");
			this.table2.Add(this.label3);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table2[this.label3]));
			w23.XOptions = ((global::Gtk.AttachOptions)(4));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label();
			this.label7.Name = "label7";
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString("Бутылей От");
			this.table2.Add(this.label7);
			global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.table2[this.label7]));
			w24.TopAttach = ((uint)(3));
			w24.BottomAttach = ((uint)(4));
			w24.XOptions = ((global::Gtk.AttachOptions)(4));
			w24.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.label9 = new global::Gtk.Label();
			this.label9.Name = "label9";
			this.label9.LabelProp = global::Mono.Unix.Catalog.GetString("Основание скидки:");
			this.table2.Add(this.label9);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table2[this.label9]));
			w25.TopAttach = ((uint)(1));
			w25.BottomAttach = ((uint)(2));
			w25.XOptions = ((global::Gtk.AttachOptions)(4));
			w25.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.ycheckbuttonHideOneOrder = new global::Gamma.GtkWidgets.yCheckButton();
			this.ycheckbuttonHideOneOrder.CanFocus = true;
			this.ycheckbuttonHideOneOrder.Name = "ycheckbuttonHideOneOrder";
			this.ycheckbuttonHideOneOrder.Label = global::Mono.Unix.Catalog.GetString("Скрыть клиентов с одним заказом");
			this.ycheckbuttonHideOneOrder.DrawIndicator = true;
			this.ycheckbuttonHideOneOrder.UseUnderline = true;
			this.table2.Add(this.ycheckbuttonHideOneOrder);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.table2[this.ycheckbuttonHideOneOrder]));
			w26.LeftAttach = ((uint)(2));
			w26.RightAttach = ((uint)(3));
			w26.XOptions = ((global::Gtk.AttachOptions)(4));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table2.Gtk.Table+TableChild
			this.ycomboboxReason = new global::Gamma.Widgets.ySpecComboBox();
			this.ycomboboxReason.Name = "ycomboboxReason";
			this.ycomboboxReason.AddIfNotExist = false;
			this.ycomboboxReason.DefaultFirst = false;
			this.ycomboboxReason.ShowSpecialStateAll = false;
			this.ycomboboxReason.ShowSpecialStateNot = true;
			this.table2.Add(this.ycomboboxReason);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.table2[this.ycomboboxReason]));
			w27.TopAttach = ((uint)(1));
			w27.BottomAttach = ((uint)(2));
			w27.LeftAttach = ((uint)(1));
			w27.RightAttach = ((uint)(2));
			w27.XOptions = ((global::Gtk.AttachOptions)(4));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			this.hbox2.Add(this.table2);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.table2]));
			w28.Position = 2;
			w28.Expand = false;
			w28.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.vseparator1 = new global::Gtk.VSeparator();
			this.vseparator1.Name = "vseparator1";
			this.hbox2.Add(this.vseparator1);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.vseparator1]));
			w29.PackType = ((global::Gtk.PackType)(1));
			w29.Position = 3;
			w29.Expand = false;
			w29.Fill = false;
			this.Add(this.hbox2);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.entryreferenceClient.ChangedByUser += new global::System.EventHandler(this.OnEntryreferenceClientChanged);
		}
	}
}
