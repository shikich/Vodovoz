
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Filters.GtkViews
{
	public partial class ComplaintKindJournalFilterView
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.Table table1;

		private global::Gtk.Label labelComplaintObject;

		private global::Gamma.Widgets.ySpecComboBox yspeccomboboxComplaintObject;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Filters.GtkViews.ComplaintKindJournalFilterView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Filters.GtkViews.ComplaintKindJournalFilterView";
			// Container child Vodovoz.Filters.GtkViews.ComplaintKindJournalFilterView.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(2)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.labelComplaintObject = new global::Gtk.Label();
			this.labelComplaintObject.Name = "labelComplaintObject";
			this.labelComplaintObject.Xalign = 1F;
			this.labelComplaintObject.LabelProp = global::Mono.Unix.Catalog.GetString("Объект рекламации:");
			this.table1.Add(this.labelComplaintObject);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.labelComplaintObject]));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yspeccomboboxComplaintObject = new global::Gamma.Widgets.ySpecComboBox();
			this.yspeccomboboxComplaintObject.Name = "yspeccomboboxComplaintObject";
			this.yspeccomboboxComplaintObject.AddIfNotExist = false;
			this.yspeccomboboxComplaintObject.DefaultFirst = false;
			this.yspeccomboboxComplaintObject.ShowSpecialStateAll = false;
			this.yspeccomboboxComplaintObject.ShowSpecialStateNot = false;
			this.table1.Add(this.yspeccomboboxComplaintObject);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.yspeccomboboxComplaintObject]));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox2.Add(this.table1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.table1]));
			w3.Position = 0;
			this.Add(this.vbox2);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
