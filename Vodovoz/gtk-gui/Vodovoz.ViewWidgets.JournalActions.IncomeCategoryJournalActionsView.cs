
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ViewWidgets.JournalActions
{
	public partial class IncomeCategoryJournalActionsView
	{
		private global::Gamma.GtkWidgets.yHBox hboxMain;

		private global::Gamma.GtkWidgets.yHBox hboxDefaultBtns;

		private global::Gamma.GtkWidgets.yHBox yhboxCustomBtns;

		private global::Gtk.VSeparator vseparator1;

		private global::Gamma.GtkWidgets.yButton btnExportData;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ViewWidgets.JournalActions.IncomeCategoryJournalActionsView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ViewWidgets.JournalActions.IncomeCategoryJournalActionsView";
			// Container child Vodovoz.ViewWidgets.JournalActions.IncomeCategoryJournalActionsView.Gtk.Container+ContainerChild
			this.hboxMain = new global::Gamma.GtkWidgets.yHBox();
			this.hboxMain.Name = "hboxMain";
			this.hboxMain.Spacing = 6;
			// Container child hboxMain.Gtk.Box+BoxChild
			this.hboxDefaultBtns = new global::Gamma.GtkWidgets.yHBox();
			this.hboxDefaultBtns.Name = "hboxDefaultBtns";
			this.hboxDefaultBtns.Spacing = 6;
			this.hboxMain.Add(this.hboxDefaultBtns);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hboxMain[this.hboxDefaultBtns]));
			w1.Position = 0;
			// Container child hboxMain.Gtk.Box+BoxChild
			this.yhboxCustomBtns = new global::Gamma.GtkWidgets.yHBox();
			this.yhboxCustomBtns.Name = "yhboxCustomBtns";
			this.yhboxCustomBtns.Spacing = 6;
			// Container child yhboxCustomBtns.Gtk.Box+BoxChild
			this.vseparator1 = new global::Gtk.VSeparator();
			this.vseparator1.Name = "vseparator1";
			this.yhboxCustomBtns.Add(this.vseparator1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.yhboxCustomBtns[this.vseparator1]));
			w2.Position = 0;
			w2.Expand = false;
			w2.Fill = false;
			// Container child yhboxCustomBtns.Gtk.Box+BoxChild
			this.btnExportData = new global::Gamma.GtkWidgets.yButton();
			this.btnExportData.CanFocus = true;
			this.btnExportData.Name = "btnExportData";
			this.btnExportData.UseUnderline = true;
			this.btnExportData.Label = global::Mono.Unix.Catalog.GetString("Экспорт");
			this.yhboxCustomBtns.Add(this.btnExportData);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.yhboxCustomBtns[this.btnExportData]));
			w3.Position = 1;
			w3.Expand = false;
			w3.Fill = false;
			this.hboxMain.Add(this.yhboxCustomBtns);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hboxMain[this.yhboxCustomBtns]));
			w4.Position = 1;
			w4.Expand = false;
			w4.Fill = false;
			this.Add(this.hboxMain);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
