
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Dialogs.Fuel
{
	public partial class FuelWriteoffDocumentView
	{
		private global::Gtk.VBox vboxDialog;

		private global::Gtk.HBox hboxDialogButtons;

		private global::Gamma.GtkWidgets.yButton buttonSave;

		private global::Gamma.GtkWidgets.yButton buttonCancel;

		private global::Gtk.HBox hbox5;

		private global::Gamma.GtkWidgets.yButton buttonPrint;

		private global::Gtk.Table table1;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryEmployee;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryExpenseCategory;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTextView ytextviewReason;

		private global::Gtk.ScrolledWindow GtkScrolledWindow1;

		private global::Gamma.GtkWidgets.yTreeView ytreeviewItems;

		private global::Gtk.HBox hbox4;

		private global::Gamma.GtkWidgets.yButton ybuttonAddItem;

		private global::Gamma.GtkWidgets.yButton ybuttonDeleteItem;

		private global::Gtk.Label labelCashier;

		private global::Gtk.Label labelCashSubdivision;

		private global::Gtk.Label labelDate;

		private global::Gtk.Label labelEmployee;

		private global::Gtk.Label labelExpenseCategory;

		private global::Gtk.Label labelReason;

		private global::Gtk.VBox vbox5;

		private global::Vodovoz.Dialogs.Fuel.FuelBalanceView fuelbalanceview;

		private global::Gamma.Widgets.ySpecComboBox ycomboboxCashSubdivision;

		private global::Gamma.Widgets.yDatePicker ydatepickerDate;

		private global::Gamma.GtkWidgets.yLabel ylabelCashierValue;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Dialogs.Fuel.FuelWriteoffDocumentView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Dialogs.Fuel.FuelWriteoffDocumentView";
			// Container child Vodovoz.Dialogs.Fuel.FuelWriteoffDocumentView.Gtk.Container+ContainerChild
			this.vboxDialog = new global::Gtk.VBox();
			this.vboxDialog.Name = "vboxDialog";
			this.vboxDialog.Spacing = 6;
			// Container child vboxDialog.Gtk.Box+BoxChild
			this.hboxDialogButtons = new global::Gtk.HBox();
			this.hboxDialogButtons.Name = "hboxDialogButtons";
			this.hboxDialogButtons.Spacing = 6;
			// Container child hboxDialogButtons.Gtk.Box+BoxChild
			this.buttonSave = new global::Gamma.GtkWidgets.yButton();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString("Сохранить");
			global::Gtk.Image w1 = new global::Gtk.Image();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-save", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w1;
			this.hboxDialogButtons.Add(this.buttonSave);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hboxDialogButtons[this.buttonSave]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hboxDialogButtons.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gamma.GtkWidgets.yButton();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString("Отменить");
			global::Gtk.Image w3 = new global::Gtk.Image();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w3;
			this.hboxDialogButtons.Add(this.buttonCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hboxDialogButtons[this.buttonCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hboxDialogButtons.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.buttonPrint = new global::Gamma.GtkWidgets.yButton();
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
			this.hboxDialogButtons.Add(this.hbox5);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hboxDialogButtons[this.hbox5]));
			w7.PackType = ((global::Gtk.PackType)(1));
			w7.Position = 3;
			this.vboxDialog.Add(this.hboxDialogButtons);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vboxDialog[this.hboxDialogButtons]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vboxDialog.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(7)), ((uint)(5)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.entryEmployee = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryEmployee.Events = ((global::Gdk.EventMask)(256));
			this.entryEmployee.Name = "entryEmployee";
			this.entryEmployee.CanEditReference = false;
			this.table1.Add(this.entryEmployee);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.entryEmployee]));
			w9.TopAttach = ((uint)(2));
			w9.BottomAttach = ((uint)(3));
			w9.LeftAttach = ((uint)(1));
			w9.RightAttach = ((uint)(4));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryExpenseCategory = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryExpenseCategory.Events = ((global::Gdk.EventMask)(256));
			this.entryExpenseCategory.Name = "entryExpenseCategory";
			this.entryExpenseCategory.CanEditReference = false;
			this.table1.Add(this.entryExpenseCategory);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.entryExpenseCategory]));
			w10.TopAttach = ((uint)(1));
			w10.BottomAttach = ((uint)(2));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(4));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytextviewReason = new global::Gamma.GtkWidgets.yTextView();
			this.ytextviewReason.CanFocus = true;
			this.ytextviewReason.Name = "ytextviewReason";
			this.GtkScrolledWindow.Add(this.ytextviewReason);
			this.table1.Add(this.GtkScrolledWindow);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow]));
			w12.TopAttach = ((uint)(4));
			w12.BottomAttach = ((uint)(5));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(0));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindow1 = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow1.Name = "GtkScrolledWindow1";
			this.GtkScrolledWindow1.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow1.Gtk.Container+ContainerChild
			this.ytreeviewItems = new global::Gamma.GtkWidgets.yTreeView();
			this.ytreeviewItems.CanFocus = true;
			this.ytreeviewItems.Name = "ytreeviewItems";
			this.GtkScrolledWindow1.Add(this.ytreeviewItems);
			this.table1.Add(this.GtkScrolledWindow1);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindow1]));
			w14.TopAttach = ((uint)(5));
			w14.BottomAttach = ((uint)(6));
			w14.RightAttach = ((uint)(4));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.ybuttonAddItem = new global::Gamma.GtkWidgets.yButton();
			this.ybuttonAddItem.CanFocus = true;
			this.ybuttonAddItem.Name = "ybuttonAddItem";
			this.ybuttonAddItem.UseUnderline = true;
			this.ybuttonAddItem.Label = global::Mono.Unix.Catalog.GetString("Добавить");
			global::Gtk.Image w15 = new global::Gtk.Image();
			w15.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-add", global::Gtk.IconSize.Menu);
			this.ybuttonAddItem.Image = w15;
			this.hbox4.Add(this.ybuttonAddItem);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.ybuttonAddItem]));
			w16.Position = 0;
			w16.Expand = false;
			w16.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.ybuttonDeleteItem = new global::Gamma.GtkWidgets.yButton();
			this.ybuttonDeleteItem.CanFocus = true;
			this.ybuttonDeleteItem.Name = "ybuttonDeleteItem";
			this.ybuttonDeleteItem.UseUnderline = true;
			this.ybuttonDeleteItem.Label = global::Mono.Unix.Catalog.GetString("Удалить");
			global::Gtk.Image w17 = new global::Gtk.Image();
			w17.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-delete", global::Gtk.IconSize.Menu);
			this.ybuttonDeleteItem.Image = w17;
			this.hbox4.Add(this.ybuttonDeleteItem);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.ybuttonDeleteItem]));
			w18.Position = 1;
			w18.Expand = false;
			w18.Fill = false;
			this.table1.Add(this.hbox4);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox4]));
			w19.TopAttach = ((uint)(6));
			w19.BottomAttach = ((uint)(7));
			w19.RightAttach = ((uint)(4));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCashier = new global::Gtk.Label();
			this.labelCashier.Name = "labelCashier";
			this.labelCashier.Xalign = 1F;
			this.labelCashier.LabelProp = global::Mono.Unix.Catalog.GetString("Кассир:");
			this.labelCashier.Justify = ((global::Gtk.Justification)(1));
			this.table1.Add(this.labelCashier);
			global::Gtk.Table.TableChild w20 = ((global::Gtk.Table.TableChild)(this.table1[this.labelCashier]));
			w20.LeftAttach = ((uint)(2));
			w20.RightAttach = ((uint)(3));
			w20.XOptions = ((global::Gtk.AttachOptions)(4));
			w20.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelCashSubdivision = new global::Gtk.Label();
			this.labelCashSubdivision.Name = "labelCashSubdivision";
			this.labelCashSubdivision.Xalign = 1F;
			this.labelCashSubdivision.LabelProp = global::Mono.Unix.Catalog.GetString("Касса:");
			this.labelCashSubdivision.Justify = ((global::Gtk.Justification)(1));
			this.table1.Add(this.labelCashSubdivision);
			global::Gtk.Table.TableChild w21 = ((global::Gtk.Table.TableChild)(this.table1[this.labelCashSubdivision]));
			w21.TopAttach = ((uint)(3));
			w21.BottomAttach = ((uint)(4));
			w21.XOptions = ((global::Gtk.AttachOptions)(4));
			w21.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelDate = new global::Gtk.Label();
			this.labelDate.Name = "labelDate";
			this.labelDate.Xalign = 1F;
			this.labelDate.LabelProp = global::Mono.Unix.Catalog.GetString("Дата:");
			this.labelDate.Justify = ((global::Gtk.Justification)(1));
			this.table1.Add(this.labelDate);
			global::Gtk.Table.TableChild w22 = ((global::Gtk.Table.TableChild)(this.table1[this.labelDate]));
			w22.XOptions = ((global::Gtk.AttachOptions)(4));
			w22.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelEmployee = new global::Gtk.Label();
			this.labelEmployee.Name = "labelEmployee";
			this.labelEmployee.Xalign = 1F;
			this.labelEmployee.LabelProp = global::Mono.Unix.Catalog.GetString("Кому:");
			this.labelEmployee.Justify = ((global::Gtk.Justification)(1));
			this.table1.Add(this.labelEmployee);
			global::Gtk.Table.TableChild w23 = ((global::Gtk.Table.TableChild)(this.table1[this.labelEmployee]));
			w23.TopAttach = ((uint)(2));
			w23.BottomAttach = ((uint)(3));
			w23.XOptions = ((global::Gtk.AttachOptions)(4));
			w23.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelExpenseCategory = new global::Gtk.Label();
			this.labelExpenseCategory.Name = "labelExpenseCategory";
			this.labelExpenseCategory.Xalign = 1F;
			this.labelExpenseCategory.LabelProp = global::Mono.Unix.Catalog.GetString("Тип списания:");
			this.labelExpenseCategory.Justify = ((global::Gtk.Justification)(1));
			this.table1.Add(this.labelExpenseCategory);
			global::Gtk.Table.TableChild w24 = ((global::Gtk.Table.TableChild)(this.table1[this.labelExpenseCategory]));
			w24.TopAttach = ((uint)(1));
			w24.BottomAttach = ((uint)(2));
			w24.XOptions = ((global::Gtk.AttachOptions)(4));
			w24.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.labelReason = new global::Gtk.Label();
			this.labelReason.Name = "labelReason";
			this.labelReason.Xalign = 1F;
			this.labelReason.LabelProp = global::Mono.Unix.Catalog.GetString("Основание:");
			this.labelReason.Justify = ((global::Gtk.Justification)(1));
			this.table1.Add(this.labelReason);
			global::Gtk.Table.TableChild w25 = ((global::Gtk.Table.TableChild)(this.table1[this.labelReason]));
			w25.TopAttach = ((uint)(4));
			w25.BottomAttach = ((uint)(5));
			w25.XOptions = ((global::Gtk.AttachOptions)(4));
			w25.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.vbox5 = new global::Gtk.VBox();
			this.vbox5.Name = "vbox5";
			this.vbox5.Spacing = 6;
			// Container child vbox5.Gtk.Box+BoxChild
			this.fuelbalanceview = new global::Vodovoz.Dialogs.Fuel.FuelBalanceView();
			this.fuelbalanceview.Events = ((global::Gdk.EventMask)(256));
			this.fuelbalanceview.Name = "fuelbalanceview";
			this.vbox5.Add(this.fuelbalanceview);
			global::Gtk.Box.BoxChild w26 = ((global::Gtk.Box.BoxChild)(this.vbox5[this.fuelbalanceview]));
			w26.Position = 0;
			w26.Expand = false;
			w26.Fill = false;
			this.table1.Add(this.vbox5);
			global::Gtk.Table.TableChild w27 = ((global::Gtk.Table.TableChild)(this.table1[this.vbox5]));
			w27.BottomAttach = ((uint)(6));
			w27.LeftAttach = ((uint)(4));
			w27.RightAttach = ((uint)(5));
			w27.XOptions = ((global::Gtk.AttachOptions)(4));
			w27.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ycomboboxCashSubdivision = new global::Gamma.Widgets.ySpecComboBox();
			this.ycomboboxCashSubdivision.Name = "ycomboboxCashSubdivision";
			this.ycomboboxCashSubdivision.AddIfNotExist = false;
			this.ycomboboxCashSubdivision.DefaultFirst = false;
			this.ycomboboxCashSubdivision.ShowSpecialStateAll = false;
			this.ycomboboxCashSubdivision.ShowSpecialStateNot = false;
			this.table1.Add(this.ycomboboxCashSubdivision);
			global::Gtk.Table.TableChild w28 = ((global::Gtk.Table.TableChild)(this.table1[this.ycomboboxCashSubdivision]));
			w28.TopAttach = ((uint)(3));
			w28.BottomAttach = ((uint)(4));
			w28.LeftAttach = ((uint)(1));
			w28.RightAttach = ((uint)(4));
			w28.XOptions = ((global::Gtk.AttachOptions)(4));
			w28.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ydatepickerDate = new global::Gamma.Widgets.yDatePicker();
			this.ydatepickerDate.Events = ((global::Gdk.EventMask)(256));
			this.ydatepickerDate.Name = "ydatepickerDate";
			this.ydatepickerDate.WithTime = false;
			this.ydatepickerDate.Date = new global::System.DateTime(0);
			this.ydatepickerDate.IsEditable = true;
			this.ydatepickerDate.AutoSeparation = false;
			this.table1.Add(this.ydatepickerDate);
			global::Gtk.Table.TableChild w29 = ((global::Gtk.Table.TableChild)(this.table1[this.ydatepickerDate]));
			w29.LeftAttach = ((uint)(1));
			w29.RightAttach = ((uint)(2));
			w29.XOptions = ((global::Gtk.AttachOptions)(4));
			w29.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelCashierValue = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelCashierValue.Name = "ylabelCashierValue";
			this.ylabelCashierValue.Xalign = 0F;
			this.ylabelCashierValue.LabelProp = global::Mono.Unix.Catalog.GetString("кассир");
			this.table1.Add(this.ylabelCashierValue);
			global::Gtk.Table.TableChild w30 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelCashierValue]));
			w30.LeftAttach = ((uint)(3));
			w30.RightAttach = ((uint)(4));
			w30.XOptions = ((global::Gtk.AttachOptions)(4));
			w30.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vboxDialog.Add(this.table1);
			global::Gtk.Box.BoxChild w31 = ((global::Gtk.Box.BoxChild)(this.vboxDialog[this.table1]));
			w31.Position = 1;
			this.Add(this.vboxDialog);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
