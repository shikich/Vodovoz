
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Filters.GtkViews
{
	public partial class OrganizationCashTransferDocumentFilterView
	{
		private global::Gtk.HBox hbox1;

		private global::Gtk.Label Label1;

		private global::QS.Widgets.GtkUI.DateRangePicker dateperiodCashTransfer;

		private global::Gtk.Label label2;

		private global::QS.Views.Control.EntityEntry authorEntry;

		private global::Gtk.Label label3;

		private global::QS.Widgets.GtkUI.SpecialListComboBox speciallistCmbOrganisationsFrom;

		private global::Gtk.Label label4;

		private global::QS.Widgets.GtkUI.SpecialListComboBox speciallistCmbOrganisationsTo;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Filters.GtkViews.OrganizationCashTransferDocumentFilterView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Filters.GtkViews.OrganizationCashTransferDocumentFilterView";
			// Container child Vodovoz.Filters.GtkViews.OrganizationCashTransferDocumentFilterView.Gtk.Container+ContainerChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.Label1 = new global::Gtk.Label();
			this.Label1.Name = "Label1";
			this.Label1.LabelProp = global::Mono.Unix.Catalog.GetString("Период");
			this.hbox1.Add(this.Label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.Label1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.dateperiodCashTransfer = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.dateperiodCashTransfer.Events = ((global::Gdk.EventMask)(256));
			this.dateperiodCashTransfer.Name = "dateperiodCashTransfer";
			this.dateperiodCashTransfer.StartDate = new global::System.DateTime(0);
			this.dateperiodCashTransfer.EndDate = new global::System.DateTime(0);
			this.hbox1.Add(this.dateperiodCashTransfer);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.dateperiodCashTransfer]));
			w2.Position = 1;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Автор");
			this.hbox1.Add(this.label2);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.label2]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.authorEntry = new global::QS.Views.Control.EntityEntry();
			this.authorEntry.Events = ((global::Gdk.EventMask)(256));
			this.authorEntry.Name = "authorEntry";
			this.hbox1.Add(this.authorEntry);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.authorEntry]));
			w4.Position = 3;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Откуда");
			this.hbox1.Add(this.label3);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.label3]));
			w5.Position = 4;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.speciallistCmbOrganisationsFrom = new global::QS.Widgets.GtkUI.SpecialListComboBox();
			this.speciallistCmbOrganisationsFrom.Name = "speciallistCmbOrganisationsFrom";
			this.speciallistCmbOrganisationsFrom.AddIfNotExist = false;
			this.speciallistCmbOrganisationsFrom.DefaultFirst = false;
			this.speciallistCmbOrganisationsFrom.ShowSpecialStateAll = true;
			this.speciallistCmbOrganisationsFrom.ShowSpecialStateNot = false;
			this.hbox1.Add(this.speciallistCmbOrganisationsFrom);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.speciallistCmbOrganisationsFrom]));
			w6.Position = 5;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("Куда");
			this.hbox1.Add(this.label4);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.label4]));
			w7.Position = 6;
			w7.Expand = false;
			w7.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.speciallistCmbOrganisationsTo = new global::QS.Widgets.GtkUI.SpecialListComboBox();
			this.speciallistCmbOrganisationsTo.Name = "speciallistCmbOrganisationsTo";
			this.speciallistCmbOrganisationsTo.AddIfNotExist = false;
			this.speciallistCmbOrganisationsTo.DefaultFirst = false;
			this.speciallistCmbOrganisationsTo.ShowSpecialStateAll = true;
			this.speciallistCmbOrganisationsTo.ShowSpecialStateNot = false;
			this.hbox1.Add(this.speciallistCmbOrganisationsTo);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.speciallistCmbOrganisationsTo]));
			w8.Position = 7;
			w8.Expand = false;
			w8.Fill = false;
			this.Add(this.hbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
