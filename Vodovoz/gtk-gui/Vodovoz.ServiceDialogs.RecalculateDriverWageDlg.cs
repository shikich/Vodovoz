
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ServiceDialogs
{
	public partial class RecalculateDriverWageDlg
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox1;

		private global::Gamma.GtkWidgets.yLabel ylabel1;

		private global::QS.Widgets.GtkUI.RepresentationEntry entryDriver;

		private global::Gtk.HBox hbox4;

		private global::Gamma.GtkWidgets.yLabel ylabel2;

		private global::Gamma.Widgets.yDatePicker datePickerFrom;

		private global::Gamma.GtkWidgets.yLabel ylabel3;

		private global::Gamma.Widgets.yDatePicker datePickerTo;

		private global::Gtk.HBox hbox5;

		private global::Gamma.GtkWidgets.yButton buttonRecalculate;

		private global::Gamma.GtkWidgets.yButton buttonRecalculateForwarder;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ServiceDialogs.RecalculateDriverWageDlg
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ServiceDialogs.RecalculateDriverWageDlg";
			// Container child Vodovoz.ServiceDialogs.RecalculateDriverWageDlg.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.ylabel1 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel1.Name = "ylabel1";
			this.ylabel1.LabelProp = global::Mono.Unix.Catalog.GetString("Водитель");
			this.hbox1.Add(this.ylabel1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.ylabel1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.entryDriver = new global::QS.Widgets.GtkUI.RepresentationEntry();
			this.entryDriver.Events = ((global::Gdk.EventMask)(256));
			this.entryDriver.Name = "entryDriver";
			this.hbox1.Add(this.entryDriver);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.entryDriver]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w3.Position = 0;
			w3.Expand = false;
			w3.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			this.hbox4.Spacing = 6;
			// Container child hbox4.Gtk.Box+BoxChild
			this.ylabel2 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel2.Name = "ylabel2";
			this.ylabel2.LabelProp = global::Mono.Unix.Catalog.GetString("Дата МЛ с:");
			this.hbox4.Add(this.ylabel2);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.ylabel2]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.datePickerFrom = new global::Gamma.Widgets.yDatePicker();
			this.datePickerFrom.Events = ((global::Gdk.EventMask)(256));
			this.datePickerFrom.Name = "datePickerFrom";
			this.datePickerFrom.WithTime = false;
			this.datePickerFrom.Date = new global::System.DateTime(0);
			this.datePickerFrom.IsEditable = false;
			this.datePickerFrom.AutoSeparation = false;
			this.hbox4.Add(this.datePickerFrom);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.datePickerFrom]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.ylabel3 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel3.Name = "ylabel3";
			this.ylabel3.LabelProp = global::Mono.Unix.Catalog.GetString("Дата МЛ с:");
			this.hbox4.Add(this.ylabel3);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.ylabel3]));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.datePickerTo = new global::Gamma.Widgets.yDatePicker();
			this.datePickerTo.Events = ((global::Gdk.EventMask)(256));
			this.datePickerTo.Name = "datePickerTo";
			this.datePickerTo.WithTime = false;
			this.datePickerTo.Date = new global::System.DateTime(0);
			this.datePickerTo.IsEditable = false;
			this.datePickerTo.AutoSeparation = false;
			this.hbox4.Add(this.datePickerTo);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.datePickerTo]));
			w7.Position = 3;
			w7.Expand = false;
			w7.Fill = false;
			this.vbox1.Add(this.hbox4);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox4]));
			w8.Position = 1;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox5 = new global::Gtk.HBox();
			this.hbox5.Name = "hbox5";
			this.hbox5.Spacing = 6;
			// Container child hbox5.Gtk.Box+BoxChild
			this.buttonRecalculate = new global::Gamma.GtkWidgets.yButton();
			this.buttonRecalculate.CanFocus = true;
			this.buttonRecalculate.Name = "buttonRecalculate";
			this.buttonRecalculate.UseUnderline = true;
			this.buttonRecalculate.Label = global::Mono.Unix.Catalog.GetString("Пересчитать ЗП водителей");
			this.hbox5.Add(this.buttonRecalculate);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.buttonRecalculate]));
			w9.Position = 0;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox5.Gtk.Box+BoxChild
			this.buttonRecalculateForwarder = new global::Gamma.GtkWidgets.yButton();
			this.buttonRecalculateForwarder.CanFocus = true;
			this.buttonRecalculateForwarder.Name = "buttonRecalculateForwarder";
			this.buttonRecalculateForwarder.UseUnderline = true;
			this.buttonRecalculateForwarder.Label = global::Mono.Unix.Catalog.GetString("Пересчитать ЗП экспедиторов");
			this.hbox5.Add(this.buttonRecalculateForwarder);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.hbox5[this.buttonRecalculateForwarder]));
			w10.Position = 1;
			w10.Expand = false;
			w10.Fill = false;
			this.vbox1.Add(this.hbox5);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox5]));
			w11.Position = 2;
			w11.Expand = false;
			w11.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
