
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class SelfDeliveryDocumentDlg
	{
		private global::Gtk.VBox vbxMain;

		private global::Gtk.HBox hbox5;

		private global::Gtk.Button buttonSave;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.Button buttonPrint;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Table tableWriteoff;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTextView ytextviewCommnet;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gamma.GtkWidgets.yTextView ytextviewOrderInfo;

		private global::Gtk.Label label1;

		private global::Gtk.Label label2;

		private global::Gtk.Label label3;

		private global::Gtk.Label label4;

		private global::Gtk.Label label5;

		private global::QS.Widgets.GtkUI.RepresentationEntry yentryrefOrder;

		private global::Gamma.Widgets.yEntryReference yentryrefWarehouse;

		private global::Gamma.GtkWidgets.yLabel ylabelDate;

		private global::Gtk.HBox hbxGoods;

		private global::Vodovoz.SelfDeliveryDocumentItemsView selfdeliverydocumentitemsview1;

		private global::Gtk.VBox vbox2;

		private global::Gtk.HBox hbxTareToReturn;

		private global::Gtk.Label lblTareToReturn;

		private global::Gamma.GtkWidgets.ySpinButton spnTareToReturn;

		private global::Gamma.GtkWidgets.yLabel lblTareReturnedBefore;

		private global::Gtk.VBox vBoxOtherGoods;

		private global::Gtk.Label lblOtherGoods;

		private global::Gtk.ScrolledWindow GtkScrolledWindow4;

		private global::Gamma.GtkWidgets.yTreeView yTreeOtherGoods;

		private global::Gtk.HBox hbox6;

		private global::Gtk.Button btnAddOtherGoods;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.SelfDeliveryDocumentDlg
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.SelfDeliveryDocumentDlg";
			// Container child Vodovoz.SelfDeliveryDocumentDlg.Gtk.Container+ContainerChild
			this.vbxMain = new global::Gtk.VBox();
			this.vbxMain.Name = "vbxMain";
			this.vbxMain.Spacing = 6;
			this.vbxMain.BorderWidth = ((uint)(6));
			// Container child vbxMain.Gtk.Box+BoxChild
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
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString("Отменить");
			global::Gtk.Image w3 = new global::Gtk.Image();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w3;
			this.hbox5.Add(this.buttonCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.buttonCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.buttonPrint = new global::Gtk.Button();
			this.buttonPrint.Sensitive = false;
			this.buttonPrint.CanFocus = true;
			this.buttonPrint.Name = "buttonPrint";
			this.buttonPrint.UseUnderline = true;
			this.buttonPrint.Label = global::Mono.Unix.Catalog.GetString("Печать");
			global::Gtk.Image w5 = new global::Gtk.Image();
			w5.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-print", global::Gtk.IconSize.Menu);
			this.buttonPrint.Image = w5;
			this.hbox5.Add(this.buttonPrint);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.buttonPrint]));
			w6.PackType = ((global::Gtk.PackType)(1));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			this.vbxMain.Add(this.hbox5);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbxMain[this.hbox5]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbxMain.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.tableWriteoff = new global::Gtk.Table(((uint)(3)), ((uint)(4)), false);
			this.tableWriteoff.Name = "tableWriteoff";
			this.tableWriteoff.RowSpacing = ((uint)(6));
			this.tableWriteoff.ColumnSpacing = ((uint)(6));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytextviewCommnet = new global::Gamma.GtkWidgets.yTextView();
			this.ytextviewCommnet.CanFocus = true;
			this.ytextviewCommnet.Name = "ytextviewCommnet";
			this.GtkScrolledWindow.Add(this.ytextviewCommnet);
			this.tableWriteoff.Add(this.GtkScrolledWindow);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.GtkScrolledWindow]));
			w9.TopAttach = ((uint)(2));
			w9.BottomAttach = ((uint)(3));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(4));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.VscrollbarPolicy = ((global::Gtk.PolicyType)(2));
			this.GtkScrolledWindow1.HscrollbarPolicy = ((global::Gtk.PolicyType)(2));
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.ytextviewOrderInfo = new global::Gamma.GtkWidgets.yTextView();
			this.ytextviewOrderInfo.CanFocus = true;
			this.ytextviewOrderInfo.Name = "ytextviewOrderInfo";
			this.ytextviewOrderInfo.Editable = false;
			this.GtkScrolledWindow1.Add(this.ytextviewOrderInfo);
			this.tableWriteoff.Add(this.GtkScrolledWindow1);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.GtkScrolledWindow1]));
			w11.TopAttach = ((uint)(1));
			w11.BottomAttach = ((uint)(2));
			w11.LeftAttach = ((uint)(3));
			w11.RightAttach = ((uint)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Дата:");
			this.tableWriteoff.Add(this.label1);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label1]));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.Xalign = 1F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Заказ:");
			this.tableWriteoff.Add(this.label2);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label2]));
			w13.LeftAttach = ((uint)(2));
			w13.RightAttach = ((uint)(3));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Информация:");
			this.tableWriteoff.Add(this.label3);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label3]));
			w14.TopAttach = ((uint)(1));
			w14.BottomAttach = ((uint)(2));
			w14.LeftAttach = ((uint)(2));
			w14.RightAttach = ((uint)(3));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.Xalign = 1F;
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Склад:");
			this.tableWriteoff.Add(this.label4);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label4]));
			w15.TopAttach = ((uint)(1));
			w15.BottomAttach = ((uint)(2));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.Xalign = 1F;
			this.label5.Yalign = 0F;
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("Комментарий:");
			this.tableWriteoff.Add(this.label5);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.label5]));
			w16.TopAttach = ((uint)(2));
			w16.BottomAttach = ((uint)(3));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.yentryrefOrder = new global::QS.Widgets.GtkUI.RepresentationEntry();
			this.yentryrefOrder.Events = ((global::Gdk.EventMask)(256));
			this.yentryrefOrder.Name = "yentryrefOrder";
			this.tableWriteoff.Add(this.yentryrefOrder);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.yentryrefOrder]));
			w17.LeftAttach = ((uint)(3));
			w17.RightAttach = ((uint)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.yentryrefWarehouse = new global::Gamma.Widgets.yEntryReference();
			this.yentryrefWarehouse.Events = ((global::Gdk.EventMask)(256));
			this.yentryrefWarehouse.Name = "yentryrefWarehouse";
			this.tableWriteoff.Add(this.yentryrefWarehouse);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.yentryrefWarehouse]));
			w18.TopAttach = ((uint)(1));
			w18.BottomAttach = ((uint)(2));
			w18.LeftAttach = ((uint)(1));
			w18.RightAttach = ((uint)(2));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(0));
			// Container child tableWriteoff.Gtk.Table+TableChild
			this.ylabelDate = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelDate.Name = "ylabelDate";
			this.ylabelDate.LabelProp = global::Mono.Unix.Catalog.GetString("ylabel1");
			this.tableWriteoff.Add(this.ylabelDate);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.tableWriteoff[this.ylabelDate]));
			w19.LeftAttach = ((uint)(1));
			w19.RightAttach = ((uint)(2));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			this.hbox2.Add(this.tableWriteoff);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.tableWriteoff]));
			w20.Position = 0;
			this.vbxMain.Add(this.hbox2);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vbxMain[this.hbox2]));
			w21.Position = 1;
			w21.Expand = false;
			w21.Fill = false;
			// Container child vbxMain.Gtk.Box+BoxChild
			this.hbxGoods = new global::Gtk.HBox();
			this.hbxGoods.Name = "hbxGoods";
			this.hbxGoods.Spacing = 6;
			// Container child hbxGoods.Gtk.Box+BoxChild
			this.selfdeliverydocumentitemsview1 = new global::Vodovoz.SelfDeliveryDocumentItemsView();
			this.selfdeliverydocumentitemsview1.Events = ((global::Gdk.EventMask)(256));
			this.selfdeliverydocumentitemsview1.Name = "selfdeliverydocumentitemsview1";
			this.hbxGoods.Add(this.selfdeliverydocumentitemsview1);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.hbxGoods[this.selfdeliverydocumentitemsview1]));
			w22.Position = 0;
			// Container child hbxGoods.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.hbxTareToReturn = new global::Gtk.HBox();
			this.hbxTareToReturn.Name = "hbxTareToReturn";
			this.hbxTareToReturn.Spacing = 6;
			// Container child hbxTareToReturn.Gtk.Box+BoxChild
			this.lblTareToReturn = new global::Gtk.Label();
			this.lblTareToReturn.Name = "lblTareToReturn";
			this.lblTareToReturn.Xalign = 0F;
			this.lblTareToReturn.LabelProp = global::Mono.Unix.Catalog.GetString("Тара на возврат:");
			this.hbxTareToReturn.Add(this.lblTareToReturn);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.hbxTareToReturn[this.lblTareToReturn]));
			w23.Position = 0;
			w23.Expand = false;
			w23.Fill = false;
			// Container child hbxTareToReturn.Gtk.Box+BoxChild
			this.spnTareToReturn = new global::Gamma.GtkWidgets.ySpinButton(0D, 100D, 1D);
			this.spnTareToReturn.CanFocus = true;
			this.spnTareToReturn.Name = "spnTareToReturn";
			this.spnTareToReturn.Adjustment.PageIncrement = 10D;
			this.spnTareToReturn.ClimbRate = 1D;
			this.spnTareToReturn.Numeric = true;
			this.spnTareToReturn.ValueAsDecimal = 0m;
			this.spnTareToReturn.ValueAsInt = 0;
			this.hbxTareToReturn.Add(this.spnTareToReturn);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.hbxTareToReturn[this.spnTareToReturn]));
			w24.Position = 1;
			this.vbox2.Add(this.hbxTareToReturn);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.hbxTareToReturn]));
			w25.Position = 0;
			w25.Expand = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.lblTareReturnedBefore = new global::Gamma.GtkWidgets.yLabel();
			this.lblTareReturnedBefore.Name = "lblTareReturnedBefore";
			this.lblTareReturnedBefore.Xalign = 0F;
			this.lblTareReturnedBefore.Yalign = 0F;
			this.lblTareReturnedBefore.UseMarkup = true;
			this.lblTareReturnedBefore.WidthChars = 0;
			this.lblTareReturnedBefore.MaxWidthChars = 0;
			this.vbox2.Add(this.lblTareReturnedBefore);
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.lblTareReturnedBefore]));
			w26.Position = 1;
			w26.Expand = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.vBoxOtherGoods = new global::Gtk.VBox();
			this.vBoxOtherGoods.Name = "vBoxOtherGoods";
			this.vBoxOtherGoods.Spacing = 6;
			// Container child vBoxOtherGoods.Gtk.Box+BoxChild
			this.lblOtherGoods = new global::Gtk.Label();
			this.lblOtherGoods.Name = "lblOtherGoods";
			this.lblOtherGoods.Xalign = 0F;
			this.lblOtherGoods.LabelProp = global::Mono.Unix.Catalog.GetString("ТМЦ на возврат:");
			this.vBoxOtherGoods.Add(this.lblOtherGoods);
			global::Gtk.Box.BoxChild w27 = ((global::Gtk.Box.BoxChild)(this.vBoxOtherGoods[this.lblOtherGoods]));
			w27.Position = 0;
			w27.Expand = false;
			w27.Fill = false;
			// Container child vBoxOtherGoods.Gtk.Box+BoxChild
			this.GtkScrolledWindow4 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow4.Name = "GtkScrolledWindow4";
			this.GtkScrolledWindow4.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow4.Gtk.Container+ContainerChild
			this.yTreeOtherGoods = new global::Gamma.GtkWidgets.yTreeView();
			this.yTreeOtherGoods.CanFocus = true;
			this.yTreeOtherGoods.Name = "yTreeOtherGoods";
			this.GtkScrolledWindow4.Add(this.yTreeOtherGoods);
			this.vBoxOtherGoods.Add(this.GtkScrolledWindow4);
			global::Gtk.Box.BoxChild w29 = ((global::Gtk.Box.BoxChild)(this.vBoxOtherGoods[this.GtkScrolledWindow4]));
			w29.Position = 1;
			// Container child vBoxOtherGoods.Gtk.Box+BoxChild
			this.hbox6 = new global::Gtk.HBox();
			this.hbox6.Name = "hbox6";
			this.hbox6.Spacing = 6;
			// Container child hbox6.Gtk.Box+BoxChild
			this.btnAddOtherGoods = new global::Gtk.Button();
			this.btnAddOtherGoods.CanFocus = true;
			this.btnAddOtherGoods.Name = "btnAddOtherGoods";
			this.btnAddOtherGoods.UseUnderline = true;
			this.btnAddOtherGoods.Label = global::Mono.Unix.Catalog.GetString("ТМЦ");
			global::Gtk.Image w30 = new global::Gtk.Image();
			w30.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-add", global::Gtk.IconSize.Menu);
			this.btnAddOtherGoods.Image = w30;
			this.hbox6.Add(this.btnAddOtherGoods);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.hbox6[this.btnAddOtherGoods]));
			w31.Position = 0;
			w31.Expand = false;
			w31.Fill = false;
			this.vBoxOtherGoods.Add(this.hbox6);
			global::Gtk.Box.BoxChild w32 = ((global::Gtk.Box.BoxChild)(this.vBoxOtherGoods[this.hbox6]));
			w32.Position = 2;
			w32.Expand = false;
			w32.Fill = false;
			this.vbox2.Add(this.vBoxOtherGoods);
			global::Gtk.Box.BoxChild w33 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.vBoxOtherGoods]));
			w33.Position = 2;
			this.hbxGoods.Add(this.vbox2);
			global::Gtk.Box.BoxChild w34 = ((global::Gtk.Box.BoxChild)(this.hbxGoods[this.vbox2]));
			w34.Position = 1;
			this.vbxMain.Add(this.hbxGoods);
			global::Gtk.Box.BoxChild w35 = ((global::Gtk.Box.BoxChild)(this.vbxMain[this.hbxGoods]));
			w35.Position = 2;
			this.Add(this.vbxMain);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.yentryrefWarehouse.ChangedByUser += new global::System.EventHandler(this.OnYentryrefWarehouseChangedByUser);
			this.yentryrefOrder.ChangedByUser += new global::System.EventHandler(this.OnYentryrefOrderChangedByUser);
			this.btnAddOtherGoods.Clicked += new global::System.EventHandler(this.OnBtnAddOtherGoodsClicked);
		}
	}
}
