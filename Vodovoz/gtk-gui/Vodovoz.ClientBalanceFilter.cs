
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class ClientBalanceFilter
	{
		private global::Gtk.Table table1;

		private global::Gtk.CheckButton checkIncludeSold;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryClient;

		private global::QSOrmProject.EntryReference entryreferenceNomenclature;

		private global::QS.Widgets.GtkUI.RepresentationEntry entryreferencePoint;

		private global::Gtk.Label label1;

		private global::Gtk.Label label2;

		private global::Gtk.Label label3;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ClientBalanceFilter
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ClientBalanceFilter";
			// Container child Vodovoz.ClientBalanceFilter.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table(((uint)(2)), ((uint)(4)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.checkIncludeSold = new global::Gtk.CheckButton();
			this.checkIncludeSold.CanFocus = true;
			this.checkIncludeSold.Name = "checkIncludeSold";
			this.checkIncludeSold.Label = global::Mono.Unix.Catalog.GetString("Включая проданное");
			this.checkIncludeSold.DrawIndicator = true;
			this.checkIncludeSold.UseUnderline = true;
			this.table1.Add(this.checkIncludeSold);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.checkIncludeSold]));
			w1.TopAttach = ((uint)(1));
			w1.BottomAttach = ((uint)(2));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryClient = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryClient.Events = ((global::Gdk.EventMask)(256));
			this.entryClient.Name = "entryClient";
			this.entryClient.CanEditReference = false;
			this.table1.Add(this.entryClient);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.entryClient]));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryreferenceNomenclature = new global::QSOrmProject.EntryReference();
			this.entryreferenceNomenclature.Events = ((global::Gdk.EventMask)(256));
			this.entryreferenceNomenclature.Name = "entryreferenceNomenclature";
			this.table1.Add(this.entryreferenceNomenclature);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.entryreferenceNomenclature]));
			w3.TopAttach = ((uint)(1));
			w3.BottomAttach = ((uint)(2));
			w3.LeftAttach = ((uint)(3));
			w3.RightAttach = ((uint)(4));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryreferencePoint = new global::QS.Widgets.GtkUI.RepresentationEntry();
			this.entryreferencePoint.Sensitive = false;
			this.entryreferencePoint.Events = ((global::Gdk.EventMask)(256));
			this.entryreferencePoint.Name = "entryreferencePoint";
			this.table1.Add(this.entryreferencePoint);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.entryreferencePoint]));
			w4.LeftAttach = ((uint)(3));
			w4.RightAttach = ((uint)(4));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Контрагент:");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Точка доставки:");
			this.table1.Add(this.label2);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.label2]));
			w6.LeftAttach = ((uint)(2));
			w6.RightAttach = ((uint)(3));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Номенклатура:");
			this.table1.Add(this.label3);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.table1[this.label3]));
			w7.TopAttach = ((uint)(1));
			w7.BottomAttach = ((uint)(2));
			w7.LeftAttach = ((uint)(2));
			w7.RightAttach = ((uint)(3));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			w7.YOptions = ((global::Gtk.AttachOptions)(4));
			this.Add(this.table1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.entryreferencePoint.Changed += new global::System.EventHandler(this.OnEntryreferencePointChanged);
			this.entryreferenceNomenclature.ChangedByUser += new global::System.EventHandler(this.OnEntryreferenceNomenclatureChangedByUser);
			this.entryClient.Changed += new global::System.EventHandler(this.OnEntryClientChanged);
			this.checkIncludeSold.Toggled += new global::System.EventHandler(this.OnCheckIncludeSoldToggled);
		}
	}
}
