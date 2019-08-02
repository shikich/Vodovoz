
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Views.Complaints
{
	public partial class CreateComplaintView
	{
		private global::Gtk.VBox vboxDialog;

		private global::Gtk.HBox hboxDialogButtons;

		private global::Gamma.GtkWidgets.yButton buttonSave;

		private global::Gamma.GtkWidgets.yButton buttonCancel;

		private global::Gtk.Table tableFields;

		private global::Gamma.Widgets.ySpecComboBox comboboxComplaintSource;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryCounterparty;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryOrder;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTextView ytextviewComplaintText;

		private global::Gtk.Label labelComplaintText;

		private global::Gtk.Label labelCounterparty;

		private global::Gtk.Label labelGuilty;

		private global::Gtk.Label labelName;

		private global::Gtk.Label labelName2;

		private global::Gtk.Label labelOrder;

		private global::Gtk.Label labelSource;

		private global::Gamma.GtkWidgets.yEntry yentryName;

		private global::Gamma.GtkWidgets.yEntry yentryPhone;

		private global::Gtk.HSeparator hseparator1;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Views.Complaints.CreateComplaintView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Views.Complaints.CreateComplaintView";
			// Container child Vodovoz.Views.Complaints.CreateComplaintView.Gtk.Container+ContainerChild
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
			this.vboxDialog.Add(this.hboxDialogButtons);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vboxDialog[this.hboxDialogButtons]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vboxDialog.Gtk.Box+BoxChild
			this.tableFields = new global::Gtk.Table(((uint)(12)), ((uint)(2)), false);
			this.tableFields.Name = "tableFields";
			this.tableFields.RowSpacing = ((uint)(6));
			this.tableFields.ColumnSpacing = ((uint)(6));
			// Container child tableFields.Gtk.Table+TableChild
			this.comboboxComplaintSource = new global::Gamma.Widgets.ySpecComboBox();
			this.comboboxComplaintSource.Name = "comboboxComplaintSource";
			this.comboboxComplaintSource.AddIfNotExist = false;
			this.comboboxComplaintSource.DefaultFirst = false;
			this.comboboxComplaintSource.ShowSpecialStateAll = false;
			this.comboboxComplaintSource.ShowSpecialStateNot = false;
			this.tableFields.Add(this.comboboxComplaintSource);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.tableFields[this.comboboxComplaintSource]));
			w6.TopAttach = ((uint)(10));
			w6.BottomAttach = ((uint)(11));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.entryCounterparty = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryCounterparty.Events = ((global::Gdk.EventMask)(256));
			this.entryCounterparty.Name = "entryCounterparty";
			this.entryCounterparty.CanEditReference = false;
			this.tableFields.Add(this.entryCounterparty);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.tableFields[this.entryCounterparty]));
			w7.TopAttach = ((uint)(1));
			w7.BottomAttach = ((uint)(2));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.entryOrder = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryOrder.Events = ((global::Gdk.EventMask)(256));
			this.entryOrder.Name = "entryOrder";
			this.entryOrder.CanEditReference = false;
			this.tableFields.Add(this.entryOrder);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.tableFields[this.entryOrder]));
			w8.TopAttach = ((uint)(7));
			w8.BottomAttach = ((uint)(8));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.ytextviewComplaintText = new global::Gamma.GtkWidgets.yTextView();
			this.ytextviewComplaintText.CanFocus = true;
			this.ytextviewComplaintText.Name = "ytextviewComplaintText";
			this.GtkScrolledWindow.Add(this.ytextviewComplaintText);
			this.tableFields.Add(this.GtkScrolledWindow);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.tableFields[this.GtkScrolledWindow]));
			w10.TopAttach = ((uint)(5));
			w10.BottomAttach = ((uint)(6));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			// Container child tableFields.Gtk.Table+TableChild
			this.labelComplaintText = new global::Gtk.Label();
			this.labelComplaintText.Name = "labelComplaintText";
			this.labelComplaintText.Xalign = 1F;
			this.labelComplaintText.LabelProp = global::Mono.Unix.Catalog.GetString("Проблема:");
			this.tableFields.Add(this.labelComplaintText);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.tableFields[this.labelComplaintText]));
			w11.TopAttach = ((uint)(5));
			w11.BottomAttach = ((uint)(6));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.labelCounterparty = new global::Gtk.Label();
			this.labelCounterparty.Name = "labelCounterparty";
			this.labelCounterparty.Xalign = 1F;
			this.labelCounterparty.LabelProp = global::Mono.Unix.Catalog.GetString("Клиент:");
			this.tableFields.Add(this.labelCounterparty);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.tableFields[this.labelCounterparty]));
			w12.TopAttach = ((uint)(1));
			w12.BottomAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.labelGuilty = new global::Gtk.Label();
			this.labelGuilty.Name = "labelGuilty";
			this.labelGuilty.Xalign = 1F;
			this.labelGuilty.LabelProp = global::Mono.Unix.Catalog.GetString("Виновники:");
			this.tableFields.Add(this.labelGuilty);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.tableFields[this.labelGuilty]));
			w13.TopAttach = ((uint)(11));
			w13.BottomAttach = ((uint)(12));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.labelName = new global::Gtk.Label();
			this.labelName.Name = "labelName";
			this.labelName.Xalign = 1F;
			this.labelName.LabelProp = global::Mono.Unix.Catalog.GetString("Имя:");
			this.tableFields.Add(this.labelName);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.tableFields[this.labelName]));
			w14.TopAttach = ((uint)(3));
			w14.BottomAttach = ((uint)(4));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.labelName2 = new global::Gtk.Label();
			this.labelName2.Name = "labelName2";
			this.labelName2.Xalign = 1F;
			this.labelName2.LabelProp = global::Mono.Unix.Catalog.GetString("Телефон:");
			this.tableFields.Add(this.labelName2);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.tableFields[this.labelName2]));
			w15.TopAttach = ((uint)(9));
			w15.BottomAttach = ((uint)(10));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.labelOrder = new global::Gtk.Label();
			this.labelOrder.Name = "labelOrder";
			this.labelOrder.Xalign = 1F;
			this.labelOrder.LabelProp = global::Mono.Unix.Catalog.GetString("Заказ:");
			this.tableFields.Add(this.labelOrder);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.tableFields[this.labelOrder]));
			w16.TopAttach = ((uint)(7));
			w16.BottomAttach = ((uint)(8));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.labelSource = new global::Gtk.Label();
			this.labelSource.Name = "labelSource";
			this.labelSource.Xalign = 1F;
			this.labelSource.LabelProp = global::Mono.Unix.Catalog.GetString("Источник:");
			this.tableFields.Add(this.labelSource);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.tableFields[this.labelSource]));
			w17.TopAttach = ((uint)(10));
			w17.BottomAttach = ((uint)(11));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.yentryName = new global::Gamma.GtkWidgets.yEntry();
			this.yentryName.CanFocus = true;
			this.yentryName.Name = "yentryName";
			this.yentryName.IsEditable = true;
			this.yentryName.InvisibleChar = '●';
			this.tableFields.Add(this.yentryName);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.tableFields[this.yentryName]));
			w18.TopAttach = ((uint)(3));
			w18.BottomAttach = ((uint)(4));
			w18.LeftAttach = ((uint)(1));
			w18.RightAttach = ((uint)(2));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child tableFields.Gtk.Table+TableChild
			this.yentryPhone = new global::Gamma.GtkWidgets.yEntry();
			this.yentryPhone.CanFocus = true;
			this.yentryPhone.Name = "yentryPhone";
			this.yentryPhone.IsEditable = true;
			this.yentryPhone.InvisibleChar = '●';
			this.tableFields.Add(this.yentryPhone);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.tableFields[this.yentryPhone]));
			w19.TopAttach = ((uint)(9));
			w19.BottomAttach = ((uint)(10));
			w19.LeftAttach = ((uint)(1));
			w19.RightAttach = ((uint)(2));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vboxDialog.Add(this.tableFields);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.vboxDialog[this.tableFields]));
			w20.Position = 1;
			// Container child vboxDialog.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator();
			this.hseparator1.Name = "hseparator1";
			this.vboxDialog.Add(this.hseparator1);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.vboxDialog[this.hseparator1]));
			w21.Position = 2;
			w21.Expand = false;
			w21.Fill = false;
			this.Add(this.vboxDialog);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
