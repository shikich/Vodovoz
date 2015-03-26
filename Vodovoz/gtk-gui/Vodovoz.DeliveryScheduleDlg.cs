
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class DeliveryScheduleDlg
	{
		private global::Gtk.VBox vbox3;
		
		private global::Gtk.HBox hbox4;
		
		private global::Gtk.Button buttonSave;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.DataBindings.DataTable datatable1;
		
		private global::Gtk.DataBindings.DataEntry entrySerialNumber;
		
		private global::Gtk.HBox hbox1;
		
		private global::QSOrmProject.DataTimeEntry entryFrom;
		
		private global::Gtk.Label label1;
		
		private global::QSOrmProject.DataTimeEntry entryTo;
		
		private global::Gtk.Label label10;
		
		private global::Gtk.Label label11;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Vodovoz.DeliveryScheduleDlg
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Vodovoz.DeliveryScheduleDlg";
			// Container child Vodovoz.DeliveryScheduleDlg.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox ();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.buttonSave = new global::Gtk.Button ();
			this.buttonSave.CanFocus = true;
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.UseUnderline = true;
			this.buttonSave.Label = global::Mono.Unix.Catalog.GetString ("Сохранить");
			global::Gtk.Image w1 = new global::Gtk.Image ();
			w1.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-save", global::Gtk.IconSize.Menu);
			this.buttonSave.Image = w1;
			this.hbox4.Add (this.buttonSave);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.buttonSave]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.buttonCancel = new global::Gtk.Button ();
			this.buttonCancel.CanFocus = true;
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.UseUnderline = true;
			this.buttonCancel.Label = global::Mono.Unix.Catalog.GetString ("Отменить");
			global::Gtk.Image w3 = new global::Gtk.Image ();
			w3.Pixbuf = global::Stetic.IconLoader.LoadIcon (this, "gtk-revert-to-saved", global::Gtk.IconSize.Menu);
			this.buttonCancel.Image = w3;
			this.hbox4.Add (this.buttonCancel);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox4 [this.buttonCancel]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.vbox3.Add (this.hbox4);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.hbox4]));
			w5.Position = 0;
			w5.Expand = false;
			w5.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.datatable1 = new global::Gtk.DataBindings.DataTable (((uint)(2)), ((uint)(2)), false);
			this.datatable1.Name = "datatable1";
			this.datatable1.RowSpacing = ((uint)(6));
			this.datatable1.ColumnSpacing = ((uint)(6));
			this.datatable1.InheritedDataSource = false;
			this.datatable1.InheritedBoundaryDataSource = false;
			this.datatable1.InheritedDataSource = false;
			this.datatable1.InheritedBoundaryDataSource = false;
			// Container child datatable1.Gtk.Table+TableChild
			this.entrySerialNumber = new global::Gtk.DataBindings.DataEntry ();
			this.entrySerialNumber.CanFocus = true;
			this.entrySerialNumber.Name = "entrySerialNumber";
			this.entrySerialNumber.IsEditable = true;
			this.entrySerialNumber.InvisibleChar = '●';
			this.entrySerialNumber.InheritedDataSource = true;
			this.entrySerialNumber.Mappings = "Name";
			this.entrySerialNumber.InheritedBoundaryDataSource = false;
			this.entrySerialNumber.InheritedDataSource = true;
			this.entrySerialNumber.Mappings = "Name";
			this.entrySerialNumber.InheritedBoundaryDataSource = false;
			this.datatable1.Add (this.entrySerialNumber);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.entrySerialNumber]));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.hbox1 = new global::Gtk.HBox ();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.entryFrom = new global::QSOrmProject.DataTimeEntry ();
			this.entryFrom.CanFocus = true;
			this.entryFrom.Name = "entryFrom";
			this.entryFrom.IsEditable = true;
			this.entryFrom.InvisibleChar = '●';
			this.entryFrom.ShowSeconds = false;
			this.entryFrom.AutocompleteStep = 30;
			this.entryFrom.Time = new global::System.DateTime (0);
			this.entryFrom.InheritedDataSource = true;
			this.entryFrom.Mappings = "From";
			this.entryFrom.InheritedBoundaryDataSource = false;
			this.hbox1.Add (this.entryFrom);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.entryFrom]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label ();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString ("До:");
			this.hbox1.Add (this.label1);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.label1]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.entryTo = new global::QSOrmProject.DataTimeEntry ();
			this.entryTo.CanFocus = true;
			this.entryTo.Name = "entryTo";
			this.entryTo.IsEditable = true;
			this.entryTo.InvisibleChar = '●';
			this.entryTo.ShowSeconds = false;
			this.entryTo.AutocompleteStep = 30;
			this.entryTo.Time = new global::System.DateTime (0);
			this.entryTo.InheritedDataSource = true;
			this.entryTo.Mappings = "To";
			this.entryTo.InheritedBoundaryDataSource = false;
			this.hbox1.Add (this.entryTo);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox1 [this.entryTo]));
			w9.Position = 2;
			w9.Expand = false;
			w9.Fill = false;
			this.datatable1.Add (this.hbox1);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.hbox1]));
			w10.TopAttach = ((uint)(1));
			w10.BottomAttach = ((uint)(2));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.label10 = new global::Gtk.Label ();
			this.label10.Name = "label10";
			this.label10.Xalign = 1F;
			this.label10.LabelProp = global::Mono.Unix.Catalog.GetString ("C:");
			this.label10.Justify = ((global::Gtk.Justification)(1));
			this.datatable1.Add (this.label10);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.label10]));
			w11.TopAttach = ((uint)(1));
			w11.BottomAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.label11 = new global::Gtk.Label ();
			this.label11.Name = "label11";
			this.label11.Xalign = 1F;
			this.label11.LabelProp = global::Mono.Unix.Catalog.GetString ("Название:");
			this.datatable1.Add (this.label11);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.label11]));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox3.Add (this.datatable1);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.datatable1]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			this.Add (this.vbox3);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
			this.buttonSave.Clicked += new global::System.EventHandler (this.OnButtonSaveClicked);
			this.buttonCancel.Clicked += new global::System.EventHandler (this.OnButtonCancelClicked);
		}
	}
}
