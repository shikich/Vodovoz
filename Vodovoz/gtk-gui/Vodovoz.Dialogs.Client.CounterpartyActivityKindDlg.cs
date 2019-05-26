
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Dialogs.Client
{
	public partial class CounterpartyActivityKindDlg
	{
		private global::Gtk.VBox datavbox1;

		private global::Gtk.HBox hbox2;

		private global::Gtk.Button buttonSave;

		private global::Gtk.Button buttonCancel;

		private global::Gtk.HSeparator hseparator1;

		private global::Gtk.Table datatable1;

		private global::Gamma.GtkWidgets.yEntry entName;

		private global::Gtk.Label lblName;

		private global::Gtk.Frame frmSubstrings;

		private global::Gtk.Alignment GtkAlignment2;

		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gamma.GtkWidgets.yTextView txtSubstrings;

		private global::Gtk.Label lblSubstrings;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Dialogs.Client.CounterpartyActivityKindDlg
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Dialogs.Client.CounterpartyActivityKindDlg";
			// Container child Vodovoz.Dialogs.Client.CounterpartyActivityKindDlg.Gtk.Container+ContainerChild
			this.datavbox1 = new global::Gtk.VBox();
			this.datavbox1.Name = "datavbox1";
			this.datavbox1.Spacing = 6;
			// Container child datavbox1.Gtk.Box+BoxChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString("Сохранить");
			global::Gtk.Image w1 = new global::Gtk.Image();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-save", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w1;
			this.hbox2.Add(this.buttonSave);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.buttonSave]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString("Отменить");
			global::Gtk.Image w3 = new global::Gtk.Image();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon(this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w3;
			this.hbox2.Add(this.buttonCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.buttonCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.datavbox1.Add(this.hbox2);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.datavbox1[this.hbox2]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child datavbox1.Gtk.Box+BoxChild
			this.hseparator1 = new global::Gtk.HSeparator();
			this.hseparator1.Name = "hseparator1";
			this.datavbox1.Add(this.hseparator1);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.datavbox1[this.hseparator1]));
			w6.Position = 1;
			w6.Expand = false;
			w6.Fill = false;
			// Container child datavbox1.Gtk.Box+BoxChild
			this.datatable1 = new global::Gtk.Table(((uint)(1)), ((uint)(2)), false);
			this.datatable1.Name = "datatable1";
			this.datatable1.RowSpacing = ((uint)(6));
			this.datatable1.ColumnSpacing = ((uint)(6));
			// Container child datatable1.Gtk.Table+TableChild
			this.entName = new global::Gamma.GtkWidgets.yEntry();
			this.entName.CanFocus = true;
			this.entName.Name = "entName";
			this.entName.IsEditable = true;
			this.entName.InvisibleChar = '●';
			this.datatable1.Add(this.entName);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.datatable1[this.entName]));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.lblName = new global::Gtk.Label();
			this.lblName.Name = "lblName";
			this.lblName.Xalign = 1F;
			this.lblName.LabelProp = global::Mono.Unix.Catalog.GetString("Название:");
			this.datatable1.Add(this.lblName);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.datatable1[this.lblName]));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			this.datavbox1.Add(this.datatable1);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.datavbox1[this.datatable1]));
			w9.Position = 2;
			w9.Expand = false;
			// Container child datavbox1.Gtk.Box+BoxChild
			this.frmSubstrings = new global::Gtk.Frame();
			this.frmSubstrings.Name = "frmSubstrings";
			this.frmSubstrings.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frmSubstrings.Gtk.Container+ContainerChild
			this.GtkAlignment2 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
			this.GtkAlignment2.Name = "GtkAlignment2";
			this.GtkAlignment2.LeftPadding = ((uint)(12));
			// Container child GtkAlignment2.Gtk.Container+ContainerChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.HscrollbarPolicy = ((global::Gtk.PolicyType)(2));
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			this.txtSubstrings = new global::Gamma.GtkWidgets.yTextView();
			this.txtSubstrings.CanFocus = true;
			this.txtSubstrings.Name = "txtSubstrings";
			this.txtSubstrings.Justification = ((global::Gtk.Justification)(2));
			this.txtSubstrings.WrapMode = ((global::Gtk.WrapMode)(2));
			this.GtkScrolledWindow.Add(this.txtSubstrings);
			this.GtkAlignment2.Add(this.GtkScrolledWindow);
			this.frmSubstrings.Add(this.GtkAlignment2);
			this.lblSubstrings = new global::Gtk.Label();
			this.lblSubstrings.Name = "lblSubstrings";
			this.lblSubstrings.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Искомые подстроки:</b> (<span foreground=\'Red\'>Должны начинаться с новой строк" +
					"и</span>)");
			this.lblSubstrings.UseMarkup = true;
			this.frmSubstrings.LabelWidget = this.lblSubstrings;
			this.datavbox1.Add(this.frmSubstrings);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.datavbox1[this.frmSubstrings]));
			w13.Position = 3;
			this.Add(this.datavbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
