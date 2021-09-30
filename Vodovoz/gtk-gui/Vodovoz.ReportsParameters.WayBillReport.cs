
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ReportsParameters
{
	public partial class WayBillReport
	{
		private global::Gtk.VBox vbox3;

		private global::Gtk.Table table1;

		private global::QS.Widgets.GtkUI.DatePicker datepicker;

		private global::QS.Views.Control.EntityEntry driverEntry;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryCar;

		private global::Gtk.HBox hbox4;

		private global::Gtk.Entry timeHourEntry;

		private global::Gtk.Label label1;

		private global::Gtk.Entry timeMinuteEntry;

		private global::Gtk.Label label3;

		private global::Gtk.Label label2;

		private global::Gamma.GtkWidgets.yLabel ylabel1;

		private global::Gamma.GtkWidgets.yLabel ylabel2;

		private global::Gamma.GtkWidgets.yLabel ylabel3;

		private global::Gamma.GtkWidgets.yButton ybuttonCreateReport;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ReportsParameters.WayBillReport
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ReportsParameters.WayBillReport";
			// Container child Vodovoz.ReportsParameters.WayBillReport.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(4)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.datepicker = new global::QS.Widgets.GtkUI.DatePicker();
			this.datepicker.Events = ((global::Gdk.EventMask)(256));
			this.datepicker.Name = "datepicker";
			this.datepicker.WithTime = false;
			this.datepicker.HideCalendarButton = true;
			this.datepicker.Date = new global::System.DateTime(0);
			this.datepicker.IsEditable = true;
			this.datepicker.AutoSeparation = false;
			this.table1.Add(this.datepicker);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.datepicker]));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.driverEntry = new global::QS.Views.Control.EntityEntry();
			this.driverEntry.Events = ((global::Gdk.EventMask)(256));
			this.driverEntry.Name = "driverEntry";
			this.table1.Add(this.driverEntry);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.driverEntry]));
			w2.TopAttach = ((uint)(1));
			w2.BottomAttach = ((uint)(2));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.entryCar = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryCar.Events = ((global::Gdk.EventMask)(256));
			this.entryCar.Name = "entryCar";
			this.entryCar.CanEditReference = false;
			this.table1.Add(this.entryCar);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.entryCar]));
			w3.TopAttach = ((uint)(2));
			w3.BottomAttach = ((uint)(3));
			w3.LeftAttach = ((uint)(1));
			w3.RightAttach = ((uint)(2));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox4 = new global::Gtk.HBox();
			this.hbox4.Name = "hbox4";
			// Container child hbox4.Gtk.Box+BoxChild
			this.timeHourEntry = new global::Gtk.Entry();
			this.timeHourEntry.WidthRequest = 30;
			this.timeHourEntry.CanFocus = true;
			this.timeHourEntry.Name = "timeHourEntry";
			this.timeHourEntry.IsEditable = true;
			this.timeHourEntry.MaxLength = 2;
			this.timeHourEntry.InvisibleChar = '•';
			this.hbox4.Add(this.timeHourEntry);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.timeHourEntry]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString(":");
			this.hbox4.Add(this.label1);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.label1]));
			w5.Position = 1;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.timeMinuteEntry = new global::Gtk.Entry();
			this.timeMinuteEntry.WidthRequest = 30;
			this.timeMinuteEntry.CanFocus = true;
			this.timeMinuteEntry.Name = "timeMinuteEntry";
			this.timeMinuteEntry.IsEditable = true;
			this.timeMinuteEntry.MaxLength = 2;
			this.timeMinuteEntry.InvisibleChar = '•';
			this.hbox4.Add(this.timeMinuteEntry);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.timeMinuteEntry]));
			w6.Position = 2;
			w6.Expand = false;
			w6.Fill = false;
			// Container child hbox4.Gtk.Box+BoxChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.hbox4.Add(this.label3);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.hbox4[this.label3]));
			w7.Position = 3;
			w7.Expand = false;
			w7.Fill = false;
			w7.Padding = ((uint)(179));
			this.table1.Add(this.hbox4);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox4]));
			w8.TopAttach = ((uint)(3));
			w8.BottomAttach = ((uint)(4));
			w8.LeftAttach = ((uint)(1));
			w8.RightAttach = ((uint)(2));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.Xalign = 1F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Дата:");
			this.table1.Add(this.label2);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.table1[this.label2]));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabel1 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel1.Name = "ylabel1";
			this.ylabel1.Xalign = 1F;
			this.ylabel1.LabelProp = global::Mono.Unix.Catalog.GetString("Водитель:");
			this.table1.Add(this.ylabel1);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabel1]));
			w10.TopAttach = ((uint)(1));
			w10.BottomAttach = ((uint)(2));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabel2 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel2.Name = "ylabel2";
			this.ylabel2.Xalign = 1F;
			this.ylabel2.LabelProp = global::Mono.Unix.Catalog.GetString("Автомобиль:");
			this.table1.Add(this.ylabel2);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabel2]));
			w11.TopAttach = ((uint)(2));
			w11.BottomAttach = ((uint)(3));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabel3 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel3.Name = "ylabel3";
			this.ylabel3.Xalign = 1F;
			this.ylabel3.LabelProp = global::Mono.Unix.Catalog.GetString("Выезд:");
			this.table1.Add(this.ylabel3);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabel3]));
			w12.TopAttach = ((uint)(3));
			w12.BottomAttach = ((uint)(4));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox3.Add(this.table1);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.table1]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.ybuttonCreateReport = new global::Gamma.GtkWidgets.yButton();
			this.ybuttonCreateReport.CanFocus = true;
			this.ybuttonCreateReport.Name = "ybuttonCreateReport";
			this.ybuttonCreateReport.UseUnderline = true;
			this.ybuttonCreateReport.Label = global::Mono.Unix.Catalog.GetString("Сформировать отчет");
			this.vbox3.Add(this.ybuttonCreateReport);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.ybuttonCreateReport]));
			w14.Position = 2;
			w14.Expand = false;
			w14.Fill = false;
			this.Add(this.vbox3);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.ybuttonCreateReport.Clicked += new global::System.EventHandler(this.OnButtonCreateRepotClicked);
		}
	}
}
