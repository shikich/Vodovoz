
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Filters.GtkViews
{
	public partial class EmployeeFilterView
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox1;

		private global::Gtk.Label labelCategory;

		private global::Gamma.Widgets.yEnumComboBox enumcomboCategory;

		private global::Gamma.GtkWidgets.yLabel ylabelStatus;

		private global::Gamma.Widgets.yEnumComboBox yenumcomboStatus;

		private global::Gtk.Label lblRegistrationType;

		private global::Gamma.Widgets.yEnumComboBox registrationTypeCmb;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Alignment alignmentSubdivision;

		private global::Gtk.Label lblSubdivision;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry evmeSubdivision;

		private global::Gtk.Label lblDriverOf;

		private global::Gamma.Widgets.yEnumComboBox cmbDriverOf;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Label lblHiredDate;

		private global::QS.Widgets.GtkUI.DateRangePicker drpHiredDate;

		private global::Gtk.Label lblFirstDayOnWork;

		private global::QS.Widgets.GtkUI.DateRangePicker drpFirstDayOnWork;

		private global::Gtk.HBox hbox7;

		private global::Gtk.Label lblFiredDate;

		private global::QS.Widgets.GtkUI.DateRangePicker drpFiredDate;

		private global::Gtk.Alignment alignmentSettlement;

		private global::Gtk.Label lblSettlementDate;

		private global::QS.Widgets.GtkUI.DateRangePicker drpSettlementDate;

		private global::Gtk.HBox hbox10;

		private global::Gamma.GtkWidgets.yCheckButton ychkVisitingMaster;

		private global::Gamma.GtkWidgets.yCheckButton ychkDriverForOneDay;

		private global::Gamma.GtkWidgets.yCheckButton ychkChainStoreDriver;

		private global::Gamma.GtkWidgets.yCheckButton ychkRFcitizenship;

		private global::Gamma.GtkWidgets.yHBox hboxDriversAndTerminals;

		private global::Gamma.GtkWidgets.yLabel labelDriversAndTerminals;

		private global::Gamma.Widgets.yEnumComboBox comboDriverType;

		private global::Gamma.GtkWidgets.yCheckButton checkSortByPriority;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Filters.GtkViews.EmployeeFilterView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Filters.GtkViews.EmployeeFilterView";
			// Container child Vodovoz.Filters.GtkViews.EmployeeFilterView.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.labelCategory = new global::Gtk.Label();
			this.labelCategory.Name = "labelCategory";
			this.labelCategory.LabelProp = global::Mono.Unix.Catalog.GetString("Категория:");
			this.hbox1.Add(this.labelCategory);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.labelCategory]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.enumcomboCategory = new global::Gamma.Widgets.yEnumComboBox();
			this.enumcomboCategory.Name = "enumcomboCategory";
			this.enumcomboCategory.ShowSpecialStateAll = true;
			this.enumcomboCategory.ShowSpecialStateNot = false;
			this.enumcomboCategory.UseShortTitle = false;
			this.enumcomboCategory.DefaultFirst = false;
			this.hbox1.Add(this.enumcomboCategory);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.enumcomboCategory]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.ylabelStatus = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelStatus.Name = "ylabelStatus";
			this.ylabelStatus.LabelProp = global::Mono.Unix.Catalog.GetString("Статус:");
			this.hbox1.Add(this.ylabelStatus);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.ylabelStatus]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.yenumcomboStatus = new global::Gamma.Widgets.yEnumComboBox();
			this.yenumcomboStatus.Name = "yenumcomboStatus";
			this.yenumcomboStatus.ShowSpecialStateAll = true;
			this.yenumcomboStatus.ShowSpecialStateNot = false;
			this.yenumcomboStatus.UseShortTitle = false;
			this.yenumcomboStatus.DefaultFirst = false;
			this.hbox1.Add(this.yenumcomboStatus);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.yenumcomboStatus]));
			w4.Position = 3;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.lblRegistrationType = new global::Gtk.Label();
			this.lblRegistrationType.Name = "lblRegistrationType";
			this.lblRegistrationType.LabelProp = global::Mono.Unix.Catalog.GetString("Оформление:");
			this.hbox1.Add(this.lblRegistrationType);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.lblRegistrationType]));
			w5.Position = 4;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.registrationTypeCmb = new global::Gamma.Widgets.yEnumComboBox();
			this.registrationTypeCmb.Name = "registrationTypeCmb";
			this.registrationTypeCmb.ShowSpecialStateAll = false;
			this.registrationTypeCmb.ShowSpecialStateNot = false;
			this.registrationTypeCmb.UseShortTitle = false;
			this.registrationTypeCmb.DefaultFirst = false;
			this.hbox1.Add(this.registrationTypeCmb);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.registrationTypeCmb]));
			w6.Position = 5;
			w6.Expand = false;
			w6.Fill = false;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.alignmentSubdivision = new global::Gtk.Alignment(0.5F, 0.5F, 1F, 1F);
			this.alignmentSubdivision.Name = "alignmentSubdivision";
			this.alignmentSubdivision.LeftPadding = ((uint)(6));
			// Container child alignmentSubdivision.Gtk.Container+ContainerChild
			this.lblSubdivision = new global::Gtk.Label();
			this.lblSubdivision.Name = "lblSubdivision";
			this.lblSubdivision.LabelProp = global::Mono.Unix.Catalog.GetString("Подразделение:");
			this.alignmentSubdivision.Add(this.lblSubdivision);
			this.hbox2.Add(this.alignmentSubdivision);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.alignmentSubdivision]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.evmeSubdivision = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.evmeSubdivision.WidthRequest = 400;
			this.evmeSubdivision.Events = ((global::Gdk.EventMask)(256));
			this.evmeSubdivision.Name = "evmeSubdivision";
			this.evmeSubdivision.CanEditReference = false;
			this.hbox2.Add(this.evmeSubdivision);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.evmeSubdivision]));
			w10.Position = 1;
			w10.Expand = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.lblDriverOf = new global::Gtk.Label();
			this.lblDriverOf.Name = "lblDriverOf";
			this.lblDriverOf.Xalign = 1F;
			this.lblDriverOf.LabelProp = global::Mono.Unix.Catalog.GetString("Управляет а\\м:");
			this.hbox2.Add(this.lblDriverOf);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.lblDriverOf]));
			w11.Position = 2;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.cmbDriverOf = new global::Gamma.Widgets.yEnumComboBox();
			this.cmbDriverOf.Name = "cmbDriverOf";
			this.cmbDriverOf.ShowSpecialStateAll = true;
			this.cmbDriverOf.ShowSpecialStateNot = false;
			this.cmbDriverOf.UseShortTitle = false;
			this.cmbDriverOf.DefaultFirst = false;
			this.hbox2.Add(this.cmbDriverOf);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.cmbDriverOf]));
			w12.Position = 3;
			w12.Expand = false;
			w12.Fill = false;
			this.vbox1.Add(this.hbox2);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox2]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.lblHiredDate = new global::Gtk.Label();
			this.lblHiredDate.Name = "lblHiredDate";
			this.lblHiredDate.LabelProp = global::Mono.Unix.Catalog.GetString("Прием на работу:");
			this.hbox4.Add(this.lblHiredDate);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.lblHiredDate]));
			w14.Position = 0;
			w14.Expand = false;
			w14.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.drpHiredDate = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.drpHiredDate.WidthRequest = 200;
			this.drpHiredDate.Events = ((global::Gdk.EventMask)(256));
			this.drpHiredDate.Name = "drpHiredDate";
			this.drpHiredDate.StartDate = new global::System.DateTime(0);
			this.drpHiredDate.EndDate = new global::System.DateTime(0);
			this.hbox4.Add(this.drpHiredDate);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.drpHiredDate]));
			w15.Position = 1;
			w15.Expand = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.lblFirstDayOnWork = new global::Gtk.Label();
			this.lblFirstDayOnWork.Name = "lblFirstDayOnWork";
			this.lblFirstDayOnWork.LabelProp = global::Mono.Unix.Catalog.GetString("Первый рабочий день:");
			this.hbox4.Add(this.lblFirstDayOnWork);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.lblFirstDayOnWork]));
			w16.Position = 2;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.drpFirstDayOnWork = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.drpFirstDayOnWork.WidthRequest = 200;
			this.drpFirstDayOnWork.Events = ((global::Gdk.EventMask)(256));
			this.drpFirstDayOnWork.Name = "drpFirstDayOnWork";
			this.drpFirstDayOnWork.StartDate = new global::System.DateTime(0);
			this.drpFirstDayOnWork.EndDate = new global::System.DateTime(0);
			this.hbox4.Add(this.drpFirstDayOnWork);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.drpFirstDayOnWork]));
			w17.Position = 3;
			w17.Expand = false;
			this.vbox1.Add(this.hbox4);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox4]));
			w18.Position = 2;
			w18.Expand = false;
			w18.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox7 = new global::Gtk.HBox();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.lblFiredDate = new global::Gtk.Label();
			this.lblFiredDate.Name = "lblFiredDate";
			this.lblFiredDate.LabelProp = global::Mono.Unix.Catalog.GetString("Дата увольнения:");
			this.hbox7.Add(this.lblFiredDate);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.lblFiredDate]));
			w19.Position = 0;
			w19.Expand = false;
			w19.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.drpFiredDate = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.drpFiredDate.WidthRequest = 200;
			this.drpFiredDate.Events = ((global::Gdk.EventMask)(256));
			this.drpFiredDate.Name = "drpFiredDate";
			this.drpFiredDate.StartDate = new global::System.DateTime(0);
			this.drpFiredDate.EndDate = new global::System.DateTime(0);
			this.hbox7.Add(this.drpFiredDate);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.drpFiredDate]));
			w20.Position = 1;
			w20.Expand = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.alignmentSettlement = new global::Gtk.Alignment(0.5F, 0.5F, 1F, 1F);
			this.alignmentSettlement.Name = "alignmentSettlement";
			this.alignmentSettlement.LeftPadding = ((uint)(57));
			// Container child alignmentSettlement.Gtk.Container+ContainerChild
			this.lblSettlementDate = new global::Gtk.Label();
			this.lblSettlementDate.Name = "lblSettlementDate";
			this.lblSettlementDate.LabelProp = global::Mono.Unix.Catalog.GetString("Дата расчета:");
			this.alignmentSettlement.Add(this.lblSettlementDate);
			this.hbox7.Add(this.alignmentSettlement);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.alignmentSettlement]));
			w22.Position = 2;
			w22.Expand = false;
			w22.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.drpSettlementDate = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.drpSettlementDate.WidthRequest = 200;
			this.drpSettlementDate.Events = ((global::Gdk.EventMask)(256));
			this.drpSettlementDate.Name = "drpSettlementDate";
			this.drpSettlementDate.StartDate = new global::System.DateTime(0);
			this.drpSettlementDate.EndDate = new global::System.DateTime(0);
			this.hbox7.Add(this.drpSettlementDate);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.drpSettlementDate]));
			w23.Position = 3;
			w23.Expand = false;
			this.vbox1.Add(this.hbox7);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox7]));
			w24.Position = 3;
			w24.Expand = false;
			w24.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox10 = new global::Gtk.HBox();
			this.hbox10.Name = "hbox10";
			this.hbox10.Spacing = 6;
			// Container child hbox10.Gtk.Box+BoxChild
			this.ychkVisitingMaster = new global::Gamma.GtkWidgets.yCheckButton();
			this.ychkVisitingMaster.CanFocus = true;
			this.ychkVisitingMaster.Name = "ychkVisitingMaster";
			this.ychkVisitingMaster.Label = global::Mono.Unix.Catalog.GetString("Выездной мастер");
			this.ychkVisitingMaster.DrawIndicator = true;
			this.ychkVisitingMaster.UseUnderline = true;
			this.hbox10.Add(this.ychkVisitingMaster);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.hbox10[this.ychkVisitingMaster]));
			w25.Position = 0;
			w25.Expand = false;
			w25.Fill = false;
			// Container child hbox10.Gtk.Box+BoxChild
			this.ychkDriverForOneDay = new global::Gamma.GtkWidgets.yCheckButton();
			this.ychkDriverForOneDay.CanFocus = true;
			this.ychkDriverForOneDay.Name = "ychkDriverForOneDay";
			this.ychkDriverForOneDay.Label = global::Mono.Unix.Catalog.GetString("Разовый водитель");
			this.ychkDriverForOneDay.DrawIndicator = true;
			this.ychkDriverForOneDay.UseUnderline = true;
			this.hbox10.Add(this.ychkDriverForOneDay);
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.hbox10[this.ychkDriverForOneDay]));
			w26.Position = 1;
			w26.Expand = false;
			w26.Fill = false;
			// Container child hbox10.Gtk.Box+BoxChild
			this.ychkChainStoreDriver = new global::Gamma.GtkWidgets.yCheckButton();
			this.ychkChainStoreDriver.CanFocus = true;
			this.ychkChainStoreDriver.Name = "ychkChainStoreDriver";
			this.ychkChainStoreDriver.Label = global::Mono.Unix.Catalog.GetString("Водитель для сетей");
			this.ychkChainStoreDriver.DrawIndicator = true;
			this.ychkChainStoreDriver.UseUnderline = true;
			this.hbox10.Add(this.ychkChainStoreDriver);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.hbox10[this.ychkChainStoreDriver]));
			w27.Position = 2;
			w27.Expand = false;
			w27.Fill = false;
			// Container child hbox10.Gtk.Box+BoxChild
			this.ychkRFcitizenship = new global::Gamma.GtkWidgets.yCheckButton();
			this.ychkRFcitizenship.CanFocus = true;
			this.ychkRFcitizenship.Name = "ychkRFcitizenship";
			this.ychkRFcitizenship.Label = global::Mono.Unix.Catalog.GetString("Гражданство РФ");
			this.ychkRFcitizenship.DrawIndicator = true;
			this.ychkRFcitizenship.UseUnderline = true;
			this.hbox10.Add(this.ychkRFcitizenship);
			global::Gtk.Box.BoxChild w28 = ((global::Gtk.Box.BoxChild)(this.hbox10[this.ychkRFcitizenship]));
			w28.Position = 3;
			w28.Expand = false;
			w28.Fill = false;
			this.vbox1.Add(this.hbox10);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox10]));
			w29.Position = 4;
			w29.Expand = false;
			w29.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hboxDriversAndTerminals = new global::Gamma.GtkWidgets.yHBox();
			this.hboxDriversAndTerminals.Name = "hboxDriversAndTerminals";
			this.hboxDriversAndTerminals.Spacing = 6;
			// Container child hboxDriversAndTerminals.Gtk.Box+BoxChild
			this.labelDriversAndTerminals = new global::Gamma.GtkWidgets.yLabel();
			this.labelDriversAndTerminals.Name = "labelDriversAndTerminals";
			this.labelDriversAndTerminals.LabelProp = global::Mono.Unix.Catalog.GetString("Водители и терминалы:");
			this.hboxDriversAndTerminals.Add(this.labelDriversAndTerminals);
			global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.hboxDriversAndTerminals[this.labelDriversAndTerminals]));
			w30.Position = 0;
			w30.Expand = false;
			w30.Fill = false;
			// Container child hboxDriversAndTerminals.Gtk.Box+BoxChild
			this.comboDriverType = new global::Gamma.Widgets.yEnumComboBox();
			this.comboDriverType.Name = "comboDriverType";
			this.comboDriverType.ShowSpecialStateAll = true;
			this.comboDriverType.ShowSpecialStateNot = false;
			this.comboDriverType.UseShortTitle = false;
			this.comboDriverType.DefaultFirst = false;
			this.hboxDriversAndTerminals.Add(this.comboDriverType);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.hboxDriversAndTerminals[this.comboDriverType]));
			w31.Position = 1;
			w31.Expand = false;
			w31.Fill = false;
			// Container child hboxDriversAndTerminals.Gtk.Box+BoxChild
			this.checkSortByPriority = new global::Gamma.GtkWidgets.yCheckButton();
			this.checkSortByPriority.CanFocus = true;
			this.checkSortByPriority.Name = "checkSortByPriority";
			this.checkSortByPriority.Label = global::Mono.Unix.Catalog.GetString("Сортировать сотрудников по приоритету для выдачи терминалов");
			this.checkSortByPriority.DrawIndicator = true;
			this.checkSortByPriority.UseUnderline = true;
			this.hboxDriversAndTerminals.Add(this.checkSortByPriority);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.hboxDriversAndTerminals[this.checkSortByPriority]));
			w32.Position = 2;
			w32.Expand = false;
			w32.Fill = false;
			this.vbox1.Add(this.hboxDriversAndTerminals);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hboxDriversAndTerminals]));
			w33.Position = 5;
			w33.Expand = false;
			w33.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
