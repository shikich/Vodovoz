
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ViewWidgets.JournalActions
{
	public partial class PaymentsJournalActionsView
	{
		private global::Gamma.GtkWidgets.yHBox yhboxBtns;

		private global::Gamma.GtkWidgets.yHBox hboxDefaultBtns;

		private global::Gtk.VSeparator vseparator1;

		private global::Gamma.GtkWidgets.yButton btnCompleteAllocation;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ViewWidgets.JournalActions.PaymentsJournalActionsView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ViewWidgets.JournalActions.PaymentsJournalActionsView";
			// Container child Vodovoz.ViewWidgets.JournalActions.PaymentsJournalActionsView.Gtk.Container+ContainerChild
			this.yhboxBtns = new global::Gamma.GtkWidgets.yHBox();
			this.yhboxBtns.Name = "yhboxBtns";
			this.yhboxBtns.Spacing = 6;
			// Container child yhboxBtns.Gtk.Box+BoxChild
			this.hboxDefaultBtns = new global::Gamma.GtkWidgets.yHBox();
			this.hboxDefaultBtns.Name = "hboxDefaultBtns";
			this.hboxDefaultBtns.Spacing = 6;
			this.yhboxBtns.Add(this.hboxDefaultBtns);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.yhboxBtns[this.hboxDefaultBtns]));
			w1.Position = 0;
			// Container child yhboxBtns.Gtk.Box+BoxChild
			this.vseparator1 = new global::Gtk.VSeparator();
			this.vseparator1.Name = "vseparator1";
			this.yhboxBtns.Add(this.vseparator1);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.yhboxBtns[this.vseparator1]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child yhboxBtns.Gtk.Box+BoxChild
			this.btnCompleteAllocation = new global::Gamma.GtkWidgets.yButton();
			this.btnCompleteAllocation.CanFocus = true;
			this.btnCompleteAllocation.Name = "btnCompleteAllocation";
			this.btnCompleteAllocation.UseUnderline = true;
			this.btnCompleteAllocation.Label = global::Mono.Unix.Catalog.GetString("Завершить распределение");
			this.yhboxBtns.Add(this.btnCompleteAllocation);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.yhboxBtns[this.btnCompleteAllocation]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			this.Add(this.yhboxBtns);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
