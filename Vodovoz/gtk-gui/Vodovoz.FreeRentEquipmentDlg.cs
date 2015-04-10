
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class FreeRentEquipmentDlg
	{
		private global::Gtk.VBox vbox3;
		
		private global::Gtk.HBox hbox4;
		
		private global::Gtk.Button buttonSave;
		
		private global::Gtk.Button buttonCancel;
		
		private global::Gtk.DataBindings.DataTable datatable1;
		
		private global::Gtk.Label label10;
		
		private global::Gtk.Label label11;
		
		private global::Gtk.Label label7;
		
		private global::Gtk.Label label8;
		
		private global::Gtk.Label labelDeposit;
		
		private global::Gtk.Label labelMinWaterAmount;
		
		private global::Gtk.DataBindings.DataEntryReference referenceEquipment;
		
		private global::Gtk.DataBindings.DataEntryReference referenceFreeRentPackage;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Vodovoz.FreeRentEquipmentDlg
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Vodovoz.FreeRentEquipmentDlg";
			// Container child Vodovoz.FreeRentEquipmentDlg.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox ();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			this.vbox3.BorderWidth = ((uint)(6));
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
			this.datatable1 = new global::Gtk.DataBindings.DataTable (((uint)(4)), ((uint)(2)), false);
			this.datatable1.Name = "datatable1";
			this.datatable1.RowSpacing = ((uint)(6));
			this.datatable1.ColumnSpacing = ((uint)(6));
			this.datatable1.InheritedDataSource = false;
			this.datatable1.InheritedBoundaryDataSource = false;
			this.datatable1.InheritedDataSource = false;
			this.datatable1.InheritedBoundaryDataSource = false;
			// Container child datatable1.Gtk.Table+TableChild
			this.label10 = new global::Gtk.Label ();
			this.label10.Name = "label10";
			this.label10.Xalign = 1F;
			this.label10.LabelProp = global::Mono.Unix.Catalog.GetString ("Минимальное\nкол-во бутылей:");
			this.label10.Justify = ((global::Gtk.Justification)(1));
			this.datatable1.Add (this.label10);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.label10]));
			w6.TopAttach = ((uint)(2));
			w6.BottomAttach = ((uint)(3));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.label11 = new global::Gtk.Label ();
			this.label11.Name = "label11";
			this.label11.Xalign = 1F;
			this.label11.LabelProp = global::Mono.Unix.Catalog.GetString ("Пакет бесплатной\nаренды:");
			this.label11.Justify = ((global::Gtk.Justification)(1));
			this.datatable1.Add (this.label11);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.label11]));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.label7 = new global::Gtk.Label ();
			this.label7.Name = "label7";
			this.label7.Xalign = 1F;
			this.label7.LabelProp = global::Mono.Unix.Catalog.GetString ("Оборудование:");
			this.datatable1.Add (this.label7);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.label7]));
			w8.TopAttach = ((uint)(1));
			w8.BottomAttach = ((uint)(2));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.label8 = new global::Gtk.Label ();
			this.label8.Name = "label8";
			this.label8.Xalign = 1F;
			this.label8.LabelProp = global::Mono.Unix.Catalog.GetString ("Сумма залога:");
			this.datatable1.Add (this.label8);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.label8]));
			w9.TopAttach = ((uint)(3));
			w9.BottomAttach = ((uint)(4));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.labelDeposit = new global::Gtk.Label ();
			this.labelDeposit.Name = "labelDeposit";
			this.labelDeposit.Xalign = 0F;
			this.labelDeposit.LabelProp = global::Mono.Unix.Catalog.GetString ("%Deposit% рублей");
			this.datatable1.Add (this.labelDeposit);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.labelDeposit]));
			w10.TopAttach = ((uint)(3));
			w10.BottomAttach = ((uint)(4));
			w10.LeftAttach = ((uint)(1));
			w10.RightAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.labelMinWaterAmount = new global::Gtk.Label ();
			this.labelMinWaterAmount.Name = "labelMinWaterAmount";
			this.labelMinWaterAmount.Xalign = 0F;
			this.labelMinWaterAmount.LabelProp = global::Mono.Unix.Catalog.GetString ("%minWaterAmount% бутылей");
			this.datatable1.Add (this.labelMinWaterAmount);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.labelMinWaterAmount]));
			w11.TopAttach = ((uint)(2));
			w11.BottomAttach = ((uint)(3));
			w11.LeftAttach = ((uint)(1));
			w11.RightAttach = ((uint)(2));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.referenceEquipment = new global::Gtk.DataBindings.DataEntryReference ();
			this.referenceEquipment.Events = ((global::Gdk.EventMask)(256));
			this.referenceEquipment.Name = "referenceEquipment";
			this.referenceEquipment.DisplayFields = new string[] {
				"NomenclatureName"
			};
			this.referenceEquipment.DisplayFormatString = "{0}";
			this.referenceEquipment.InheritedDataSource = true;
			this.referenceEquipment.Mappings = "Equipment";
			this.referenceEquipment.InheritedBoundaryDataSource = false;
			this.referenceEquipment.CursorPointsEveryType = false;
			this.datatable1.Add (this.referenceEquipment);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.referenceEquipment]));
			w12.TopAttach = ((uint)(1));
			w12.BottomAttach = ((uint)(2));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child datatable1.Gtk.Table+TableChild
			this.referenceFreeRentPackage = new global::Gtk.DataBindings.DataEntryReference ();
			this.referenceFreeRentPackage.Events = ((global::Gdk.EventMask)(256));
			this.referenceFreeRentPackage.Name = "referenceFreeRentPackage";
			this.referenceFreeRentPackage.DisplayFields = new string[] {
				"Name"
			};
			this.referenceFreeRentPackage.DisplayFormatString = "{0}";
			this.referenceFreeRentPackage.InheritedDataSource = true;
			this.referenceFreeRentPackage.Mappings = "FreeRentPackage";
			this.referenceFreeRentPackage.ColumnMappings = "";
			this.referenceFreeRentPackage.InheritedBoundaryDataSource = false;
			this.referenceFreeRentPackage.CursorPointsEveryType = false;
			this.datatable1.Add (this.referenceFreeRentPackage);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.datatable1 [this.referenceFreeRentPackage]));
			w13.LeftAttach = ((uint)(1));
			w13.RightAttach = ((uint)(2));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox3.Add (this.datatable1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox3 [this.datatable1]));
			w14.Position = 1;
			w14.Expand = false;
			w14.Fill = false;
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
