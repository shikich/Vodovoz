
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Views.Mango
{
	public partial class TestMangoView
	{
		private global::Gtk.HBox hbox2;

		private global::Gtk.Button button383;

		private global::Gtk.Button button384;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Views.Mango.TestMangoView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Views.Mango.TestMangoView";
			// Container child Vodovoz.Views.Mango.TestMangoView.Gtk.Container+ContainerChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.button383 = new global::Gtk.Button();
			this.button383.CanFocus = true;
			this.button383.Name = "button383";
			this.button383.UseUnderline = true;
			this.button383.Label = global::Mono.Unix.Catalog.GetString("GetAllVPBXEmploies");
			this.hbox2.Add(this.button383);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.button383]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox2.Gtk.Box+BoxChild
			this.button384 = new global::Gtk.Button();
			this.button384.CanFocus = true;
			this.button384.Name = "button384";
			this.button384.UseUnderline = true;
			this.button384.Label = global::Mono.Unix.Catalog.GetString("HangUp");
			this.hbox2.Add(this.button384);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.button384]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			this.Add(this.hbox2);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.button383.Clicked += new global::System.EventHandler(this.Clicked_GetAllVPBXEmploies);
			this.button384.Clicked += new global::System.EventHandler(this.Clicked_HangUp);
		}
	}
}
