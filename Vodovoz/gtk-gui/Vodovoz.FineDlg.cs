
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class FineDlg
	{
		private global::Gtk.VBox vbox4;

		private global::Gtk.HBox hbox5;

		private global::Gtk.Button buttonSave;

		private global::Gtk.Button btnCancel;

		private global::Gtk.Table tableWriteoff;

		private global::Gtk.Button btnShowUndelivery;

		private global::Gtk.HBox hbox3;

		private global::Gamma.Widgets.yEnumComboBox enumFineType;

		private global::Gtk.Label labelOverspending;

		private global::Gamma.GtkWidgets.ySpinButton yspinLiters;

		private global::Gtk.Label labelRequestRouteList;

		private global::Gtk.HBox hbox4;

		private global::Gamma.GtkWidgets.yEntry yentryFineReasonString;

		private global::Gtk.Button buttonGetReasonFromTemplate;

		private global::Gtk.HBox hbox6;

		private global::Gamma.GtkWidgets.ySpinButton yspinMoney;

		private global::QSProjectsLib.CurrencyLabel currencylabel1;

		private global::Gtk.Button buttonDivideAtAll;

		private global::Gtk.HBox hbox7;

		private global::Gamma.GtkWidgets.yLabel ylabelDate;

		private global::Gamma.Widgets.yDatePicker ydatepicker;

		private global::Gtk.Label label1;

		private global::Gtk.Label label3;

		private global::Gtk.Label label4;

		private global::Gtk.Label label5;

		private global::Gtk.Label label6;

		private global::Gtk.Label label7;

		private global::QS.Widgets.GtkUI.RepresentationEntry yentryreferenceRouteList;

		private global::Gamma.GtkWidgets.yLabel ylabelAuthor;

		private global::Vodovoz.FineItemsView fineitemsview1;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.FineDlg
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.FineDlg";
			// Container child Vodovoz.FineDlg.Gtk.Container+ContainerChild
			this.vbox4 = new global::Gtk.VBox();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			this.vbox4.BorderWidth = ((uint)(6));
			// Container child vbox4.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString("Сохранить");
			global::Gtk.Image w1 = new global::Gtk.Image();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-save", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w1;
			this.hbox5.Add(this.buttonSave);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.buttonSave]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.btnCancel = new global::Gtk.Button();
			this.btnCancel.CanFocus = true;
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.UseUnderline = true;
			this.btnCancel.Label = global::Mono.Unix.Catalog.GetString("Отменить");
			global::Gtk.Image w3 = new global::Gtk.Image();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.btnCancel.Image = w3;
			this.hbox5.Add(this.btnCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.btnCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox4.Add(this.hbox5);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.hbox5]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.tableWriteoff = new global::Gtk.Table(((uint)(4)), ((uint)(6)), false);
			this.tableWriteoff.Name = "tableWriteoff";
			this.tableWriteoff.RowSpacing = ((uint)(6));
			this.tableWriteoff.ColumnSpacing = ((uint)(6));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.btnShowUndelivery = new global::Gtk.Button();
			this.btnShowUndelivery.CanFocus = true;
			this.btnShowUndelivery.Name = "btnShowUndelivery";
			this.btnShowUndelivery.UseUnderline = true;
			this.btnShowUndelivery.Label = global::Mono.Unix.Catalog.GetString("Перейти в недовоз");
			this.tableWriteoff.Add(this.btnShowUndelivery);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.btnShowUndelivery]));
			w6.TopAttach = ((uint)(3));
			w6.BottomAttach = ((uint)(4));
			w6.LeftAttach = ((uint)(4));
			w6.RightAttach = ((uint)(5));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.enumFineType = new global::Gamma.Widgets.yEnumComboBox();
			this.enumFineType.Name = "enumFineType";
			this.enumFineType.ShowSpecialStateAll = false;
			this.enumFineType.ShowSpecialStateNot = false;
			this.enumFineType.UseShortTitle = false;
			this.enumFineType.DefaultFirst = false;
			this.hbox3.Add(this.enumFineType);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.enumFineType]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.labelOverspending = new global::Gtk.Label();
			this.labelOverspending.Name = "labelOverspending";
			this.labelOverspending.LabelProp = global::Mono.Unix.Catalog.GetString("Перерасход (л.):");
			this.hbox3.Add(this.labelOverspending);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.labelOverspending]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.yspinLiters = new global::Gamma.GtkWidgets.ySpinButton(0D, 100000D, 1D);
			this.yspinLiters.CanFocus = true;
			this.yspinLiters.Name = "yspinLiters";
			this.yspinLiters.Adjustment.PageIncrement = 10D;
			this.yspinLiters.ClimbRate = 1D;
			this.yspinLiters.Digits = ((uint)(2));
			this.yspinLiters.Numeric = true;
			this.yspinLiters.ValueAsDecimal = 0m;
			this.yspinLiters.ValueAsInt = 0;
			this.hbox3.Add(this.yspinLiters);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.yspinLiters]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox3.Gtk.Box+BoxChild
			this.labelRequestRouteList = new global::Gtk.Label();
			this.labelRequestRouteList.Name = "labelRequestRouteList";
			this.labelRequestRouteList.LabelProp = global::Mono.Unix.Catalog.GetString("<span color=\"red\">Необходимо выбрать маршрутный лист</span>");
			this.labelRequestRouteList.UseMarkup = true;
			this.hbox3.Add(this.labelRequestRouteList);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.labelRequestRouteList]));
			w10.Position = 3;
			w10.Expand = false;
			w10.Fill = false;
			this.tableWriteoff.Add(this.hbox3);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.hbox3]));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(4));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.yentryFineReasonString = new global::Gamma.GtkWidgets.yEntry();
			this.yentryFineReasonString.CanFocus = true;
			this.yentryFineReasonString.Name = "yentryFineReasonString";
			this.yentryFineReasonString.IsEditable = true;
			this.yentryFineReasonString.InvisibleChar = '●';
			this.hbox4.Add(this.yentryFineReasonString);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.yentryFineReasonString]));
			w12.Position = 0;
			// Container child hbox4.Gtk.Box+BoxChild
			this.buttonGetReasonFromTemplate = new global::Gtk.Button();
			this.buttonGetReasonFromTemplate.CanFocus = true;
			this.buttonGetReasonFromTemplate.Name = "buttonGetReasonFromTemplate";
			this.buttonGetReasonFromTemplate.UseUnderline = true;
			this.buttonGetReasonFromTemplate.Label = global::Mono.Unix.Catalog.GetString("Заполнить из шаблона");
			this.hbox4.Add(this.buttonGetReasonFromTemplate);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.buttonGetReasonFromTemplate]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			this.tableWriteoff.Add(this.hbox4);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.hbox4]));
			w14.TopAttach = ((uint)(3));
			w14.BottomAttach = ((uint)(4));
			w14.LeftAttach = ((uint)(1));
			w14.RightAttach = ((uint)(4));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.hbox6 = new global::Gtk.HBox();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.yspinMoney = new global::Gamma.GtkWidgets.ySpinButton(0D, 100000D, 1D);
			this.yspinMoney.CanFocus = true;
			this.yspinMoney.Name = "yspinMoney";
			this.yspinMoney.Adjustment.PageIncrement = 100D;
			this.yspinMoney.ClimbRate = 1D;
			this.yspinMoney.Digits = ((uint)(2));
			this.yspinMoney.Numeric = true;
			this.yspinMoney.ValueAsDecimal = 0m;
			this.yspinMoney.ValueAsInt = 0;
			this.hbox6.Add(this.yspinMoney);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.yspinMoney]));
			w15.Position = 0;
			w15.Expand = false;
			w15.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.currencylabel1 = new global::QSProjectsLib.CurrencyLabel();
			this.currencylabel1.Name = "currencylabel1";
			this.currencylabel1.LabelProp = "currencylabel1";
			this.hbox6.Add(this.currencylabel1);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.currencylabel1]));
			w16.Position = 1;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hbox6.Gtk.Box+BoxChild
			this.buttonDivideAtAll = new global::Gtk.Button();
			this.buttonDivideAtAll.CanFocus = true;
			this.buttonDivideAtAll.Name = "buttonDivideAtAll";
			this.buttonDivideAtAll.UseUnderline = true;
			this.buttonDivideAtAll.Label = global::Mono.Unix.Catalog.GetString("Разбить на всех");
			this.hbox6.Add(this.buttonDivideAtAll);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.buttonDivideAtAll]));
			w17.Position = 2;
			w17.Expand = false;
			w17.Fill = false;
			this.tableWriteoff.Add(this.hbox6);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.hbox6]));
			w18.TopAttach = ((uint)(2));
			w18.BottomAttach = ((uint)(3));
			w18.LeftAttach = ((uint)(3));
			w18.RightAttach = ((uint)(4));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.hbox7 = new global::Gtk.HBox();
			this.hbox7.Name = "hbox7";
			this.hbox7.Spacing = 6;
			// Container child hbox7.Gtk.Box+BoxChild
			this.ylabelDate = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelDate.Name = "ylabelDate";
			this.ylabelDate.LabelProp = global::Mono.Unix.Catalog.GetString("ylabel1");
			this.hbox7.Add(this.ylabelDate);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.ylabelDate]));
			w19.Position = 0;
			w19.Expand = false;
			w19.Fill = false;
			// Container child hbox7.Gtk.Box+BoxChild
			this.ydatepicker = new global::Gamma.Widgets.yDatePicker();
			this.ydatepicker.Events = ((global::Gdk.EventMask)(256));
			this.ydatepicker.Name = "ydatepicker";
			this.ydatepicker.WithTime = false;
			this.ydatepicker.Date = new global::System.DateTime(0);
			this.ydatepicker.IsEditable = false;
			this.ydatepicker.AutoSeparation = false;
			this.hbox7.Add(this.ydatepicker);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox7[this.ydatepicker]));
			w20.Position = 1;
			this.tableWriteoff.Add(this.hbox7);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.hbox7]));
			w21.TopAttach = ((uint)(2));
			w21.BottomAttach = ((uint)(3));
			w21.LeftAttach = ((uint)(1));
			w21.RightAttach = ((uint)(2));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Дата:");
			this.tableWriteoff.Add(this.label1);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label1]));
			w22.TopAttach = ((uint)(2));
			w22.BottomAttach = ((uint)(3));
			w22.XOptions = ((global::Gtk.AttachOptions)(4));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Причина штрафа:");
			this.tableWriteoff.Add(this.label3);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label3]));
			w23.TopAttach = ((uint)(3));
			w23.BottomAttach = ((uint)(4));
			w23.XOptions = ((global::Gtk.AttachOptions)(4));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.Xalign = 1F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Сумма штрафа:");
			this.tableWriteoff.Add(this.label4);
			global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label4]));
			w24.TopAttach = ((uint)(2));
			w24.BottomAttach = ((uint)(3));
			w24.LeftAttach = ((uint)(2));
			w24.RightAttach = ((uint)(3));
			w24.XOptions = ((global::Gtk.AttachOptions)(4));
			w24.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.Xalign = 1F;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("№ МЛ");
			this.tableWriteoff.Add(this.label5);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label5]));
			w25.TopAttach = ((uint)(1));
			w25.BottomAttach = ((uint)(2));
			w25.XOptions = ((global::Gtk.AttachOptions)(4));
			w25.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label6 = new global::Gtk.Label();
			this.label6.Name = "label6";
			this.label6.Xalign = 1F;
			this.label6.LabelProp = global::Mono.Unix.Catalog.GetString("Тип:");
			this.tableWriteoff.Add(this.label6);
			global::Gtk.Table.TableChild w26 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label6]));
			w26.XOptions = ((global::Gtk.AttachOptions)(4));
			w26.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label();
			this.label7.Name = "label7";
			this.label7.Xalign = 1F;
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString("Штраф выдал:");
			this.tableWriteoff.Add(this.label7);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label7]));
			w27.LeftAttach = ((uint)(4));
			w27.RightAttach = ((uint)(5));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.yentryreferenceRouteList = new global::QS.Widgets.GtkUI.RepresentationEntry();
			this.yentryreferenceRouteList.Events = ((global::Gdk.EventMask)(256));
			this.yentryreferenceRouteList.Name = "yentryreferenceRouteList";
			this.tableWriteoff.Add(this.yentryreferenceRouteList);
			global::Gtk.Table.TableChild w28 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.yentryreferenceRouteList]));
			w28.TopAttach = ((uint)(1));
			w28.BottomAttach = ((uint)(2));
			w28.LeftAttach = ((uint)(1));
			w28.RightAttach = ((uint)(4));
			w28.XOptions = ((global::Gtk.AttachOptions)(4));
			w28.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.ylabelAuthor = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelAuthor.Name = "ylabelAuthor";
			this.ylabelAuthor.LabelProp = global::Mono.Unix.Catalog.GetString("author");
			this.tableWriteoff.Add(this.ylabelAuthor);
			global::Gtk.Table.TableChild w29 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.ylabelAuthor]));
			w29.LeftAttach = ((uint)(5));
			w29.RightAttach = ((uint)(6));
			w29.XOptions = ((global::Gtk.AttachOptions)(4));
			w29.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox4.Add(this.tableWriteoff);
			global::Gtk.Box.BoxChild w30 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.tableWriteoff]));
			w30.Position = 1;
			w30.Expand = false;
			w30.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.fineitemsview1 = new global::Vodovoz.FineItemsView();
			this.fineitemsview1.Events = ((global::Gdk.EventMask)(256));
			this.fineitemsview1.Name = "fineitemsview1";
			this.fineitemsview1.IsFuelOverspending = false;
			this.vbox4.Add(this.fineitemsview1);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.fineitemsview1]));
			w31.Position = 2;
			this.Add(this.vbox4);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.btnShowUndelivery.Hide();
			this.Hide();
			this.yentryreferenceRouteList.ChangedByUser += new global::System.EventHandler(this.OnYentryreferenceRouteListChangedByUser);
			this.buttonDivideAtAll.Clicked += new global::System.EventHandler(this.OnButtonDivideAtAllClicked);
			this.buttonGetReasonFromTemplate.Clicked += new global::System.EventHandler(this.OnButtonGetReasonFromTemplateClicked);
			this.enumFineType.ChangedByUser += new global::System.EventHandler(this.OnEnumFineTypeChangedByUser);
			this.yspinLiters.ValueChanged += new global::System.EventHandler(this.OnYspinLitersValueChanged);
			this.btnShowUndelivery.Clicked += new global::System.EventHandler(this.OnBtnShowUndeliveryClicked);
		}
	}
}
