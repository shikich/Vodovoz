
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Panel
{
	public partial class PanelViewContainer
	{
		private global::Gtk.Frame frame1;
		
		private global::Gtk.Alignment GtkAlignment1;
		
		private global::Gtk.ToggleButton buttonPin;

		protected virtual void Build ()
		{
			global::Stetic.Gui.Initialize (this);
			// Widget Vodovoz.Panel.PanelViewContainer
			global::Stetic.BinContainer.Attach (this);
			this.Name = "Vodovoz.Panel.PanelViewContainer";
			// Container child Vodovoz.Panel.PanelViewContainer.Gtk.Container+ContainerChild
			this.frame1 = new global::Gtk.Frame ();
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(2));
			this.frame1.LabelXalign = 1F;
			this.frame1.BorderWidth = ((uint)(6));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment1 = new global::Gtk.Alignment (0F, 0F, 1F, 1F);
			this.GtkAlignment1.Name = "GtkAlignment1";
			this.GtkAlignment1.LeftPadding = ((uint)(12));
			this.GtkAlignment1.RightPadding = ((uint)(5));
			this.frame1.Add (this.GtkAlignment1);
			this.buttonPin = new global::Gtk.ToggleButton ();
			this.buttonPin.Name = "buttonPin";
			this.buttonPin.FocusOnClick = false;
			global::Gtk.Image w2 = new global::Gtk.Image ();
			w2.Pixbuf = global::Gdk.Pixbuf.LoadFromResource ("Vodovoz.icons.buttons.pin-up.png");
			this.buttonPin.Image = w2;
			this.frame1.LabelWidget = this.buttonPin;
			this.Add (this.frame1);
			if ((this.Child != null)) {
				this.Child.ShowAll ();
			}
			this.Hide ();
		}
	}
}
