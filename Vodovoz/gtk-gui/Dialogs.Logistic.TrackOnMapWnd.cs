
// This file has been generated by the GUI designer. Do not modify.
namespace Dialogs.Logistic
{
	public partial class TrackOnMapWnd
	{
		private global::Gtk.VBox vbox1;

		private global::Gtk.HBox hbox1;

		private global::Gtk.VBox vbox6;

		private global::Gtk.Label label1;

		private global::Gtk.RadioButton radioSmall;

		private global::Gtk.RadioButton radioNumbers;

		private global::Gtk.Label labelDistance;

		private global::Gtk.VBox vbox2;

		private global::Gtk.Button buttonRecalculateToBase;

		private global::Gtk.Button buttonFindGap;

		private global::Gtk.Button buttonRecountMileage;

		private global::Gtk.VBox vbox7;

		private global::Gtk.Label labelCutTrack;

		private global::Gtk.VBox vbox5;

		private global::Gtk.Button buttonLastAddress;

		private global::Gtk.Button buttonCutTrack;

		private global::Gtk.VBox vbox4;

		private global::Gamma.Widgets.yDatePicker ydatepickerStart;

		private global::Gamma.Widgets.yDatePicker ydatepickerEnd;

		private global::Gtk.VBox vbox3;

		private global::Gtk.Label label4;

		private global::Gtk.Label label2;

		private global::Gtk.Label label5;

		private global::GMap.NET.GtkSharp.GMapControl gmapWidget;

		private global::Gtk.Statusbar statusbar1;

		private global::Gamma.GtkWidgets.yLabel ylabelPoint;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Dialogs.Logistic.TrackOnMapWnd
			this.Name = "Dialogs.Logistic.TrackOnMapWnd";
			this.Title = global::Mono.Unix.Catalog.GetString("TrackOnMapWnd");
			this.WindowPosition = ((global::Gtk.WindowPosition)(4));
			// Container child Dialogs.Logistic.TrackOnMapWnd.Gtk.Container+ContainerChild
			this.vbox1 = new global::Gtk.VBox();
			this.vbox1.Name = "vbox1";
			this.vbox1.Spacing = 6;
			// Container child vbox1.Gtk.Box+BoxChild
			this.hbox1 = new global::Gtk.HBox();
			this.hbox1.Name = "hbox1";
			this.hbox1.Spacing = 6;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox6 = new global::Gtk.VBox();
			this.vbox6.Name = "vbox6";
			this.vbox6.Spacing = 6;
			// Container child vbox6.Gtk.Box+BoxChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Вид маркеров:");
			this.vbox6.Add(this.label1);
			global::Gtk.Box.BoxChild w1 = ((global::Gtk.Box.BoxChild)(this.vbox6[this.label1]));
			w1.Position = 0;
			w1.Expand = false;
			w1.Fill = false;
			// Container child vbox6.Gtk.Box+BoxChild
			this.radioSmall = new global::Gtk.RadioButton(global::Mono.Unix.Catalog.GetString("Маленькие"));
			this.radioSmall.CanFocus = true;
			this.radioSmall.Name = "radioSmall";
			this.radioSmall.DrawIndicator = true;
			this.radioSmall.UseUnderline = true;
			this.radioSmall.Group = new global::GLib.SList(global::System.IntPtr.Zero);
			this.vbox6.Add(this.radioSmall);
			global::Gtk.Box.BoxChild w2 = ((global::Gtk.Box.BoxChild)(this.vbox6[this.radioSmall]));
			w2.Position = 1;
			w2.Expand = false;
			w2.Fill = false;
			// Container child vbox6.Gtk.Box+BoxChild
			this.radioNumbers = new global::Gtk.RadioButton(global::Mono.Unix.Catalog.GetString("Пронумерованные"));
			this.radioNumbers.CanFocus = true;
			this.radioNumbers.Name = "radioNumbers";
			this.radioNumbers.DrawIndicator = true;
			this.radioNumbers.UseUnderline = true;
			this.radioNumbers.Group = this.radioSmall.Group;
			this.vbox6.Add(this.radioNumbers);
			global::Gtk.Box.BoxChild w3 = ((global::Gtk.Box.BoxChild)(this.vbox6[this.radioNumbers]));
			w3.Position = 2;
			w3.Expand = false;
			w3.Fill = false;
			this.hbox1.Add(this.vbox6);
			global::Gtk.Box.BoxChild w4 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox6]));
			w4.Position = 0;
			w4.Expand = false;
			w4.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.labelDistance = new global::Gtk.Label();
			this.labelDistance.Name = "labelDistance";
			this.labelDistance.LabelProp = global::Mono.Unix.Catalog.GetString("Расстояние");
			this.hbox1.Add(this.labelDistance);
			global::Gtk.Box.BoxChild w5 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.labelDistance]));
			w5.Position = 2;
			w5.Expand = false;
			w5.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 3;
			// Container child vbox2.Gtk.Box+BoxChild
			this.buttonRecalculateToBase = new global::Gtk.Button();
			this.buttonRecalculateToBase.CanFocus = true;
			this.buttonRecalculateToBase.Name = "buttonRecalculateToBase";
			this.buttonRecalculateToBase.UseUnderline = true;
			this.buttonRecalculateToBase.Label = global::Mono.Unix.Catalog.GetString("Пересчитать км до базы");
			this.vbox2.Add(this.buttonRecalculateToBase);
			global::Gtk.Box.BoxChild w6 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.buttonRecalculateToBase]));
			w6.Position = 0;
			w6.Expand = false;
			w6.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.buttonFindGap = new global::Gtk.Button();
			this.buttonFindGap.CanFocus = true;
			this.buttonFindGap.Name = "buttonFindGap";
			this.buttonFindGap.UseUnderline = true;
			this.buttonFindGap.Label = global::Mono.Unix.Catalog.GetString("Найти и пересчитать разрывы");
			this.vbox2.Add(this.buttonFindGap);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.buttonFindGap]));
			w7.Position = 1;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.buttonRecountMileage = new global::Gtk.Button();
			this.buttonRecountMileage.CanFocus = true;
			this.buttonRecountMileage.Name = "buttonRecountMileage";
			this.buttonRecountMileage.UseUnderline = true;
			this.buttonRecountMileage.Label = global::Mono.Unix.Catalog.GetString("Пересчитать километраж");
			this.vbox2.Add(this.buttonRecountMileage);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.buttonRecountMileage]));
			w8.Position = 2;
			w8.Expand = false;
			w8.Fill = false;
			this.hbox1.Add(this.vbox2);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox2]));
			w9.Position = 4;
			w9.Expand = false;
			w9.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox7 = new global::Gtk.VBox();
			this.vbox7.Name = "vbox7";
			this.vbox7.Spacing = 6;
			// Container child vbox7.Gtk.Box+BoxChild
			this.labelCutTrack = new global::Gtk.Label();
			this.labelCutTrack.Name = "labelCutTrack";
			this.labelCutTrack.LabelProp = global::Mono.Unix.Catalog.GetString("Обработанный\nтрек:");
			this.vbox7.Add(this.labelCutTrack);
			global::Gtk.Box.BoxChild w10 = ((global::Gtk.Box.BoxChild)(this.vbox7[this.labelCutTrack]));
			w10.Position = 0;
			w10.Expand = false;
			w10.Fill = false;
			this.hbox1.Add(this.vbox7);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox7]));
			w11.PackType = ((global::Gtk.PackType)(1));
			w11.Position = 6;
			w11.Expand = false;
			w11.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox5 = new global::Gtk.VBox();
			this.vbox5.Name = "vbox5";
			this.vbox5.Spacing = 6;
			// Container child vbox5.Gtk.Box+BoxChild
			this.buttonLastAddress = new global::Gtk.Button();
			this.buttonLastAddress.CanFocus = true;
			this.buttonLastAddress.Name = "buttonLastAddress";
			this.buttonLastAddress.UseUnderline = true;
			this.buttonLastAddress.Label = global::Mono.Unix.Catalog.GetString("Последний адрес");
			this.vbox5.Add(this.buttonLastAddress);
			global::Gtk.Box.BoxChild w12 = ((global::Gtk.Box.BoxChild)(this.vbox5[this.buttonLastAddress]));
			w12.Position = 0;
			w12.Expand = false;
			w12.Fill = false;
			// Container child vbox5.Gtk.Box+BoxChild
			this.buttonCutTrack = new global::Gtk.Button();
			this.buttonCutTrack.CanFocus = true;
			this.buttonCutTrack.Name = "buttonCutTrack";
			this.buttonCutTrack.UseUnderline = true;
			this.buttonCutTrack.Label = global::Mono.Unix.Catalog.GetString("Обрезать");
			this.vbox5.Add(this.buttonCutTrack);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.vbox5[this.buttonCutTrack]));
			w13.Position = 1;
			w13.Expand = false;
			w13.Fill = false;
			this.hbox1.Add(this.vbox5);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox5]));
			w14.PackType = ((global::Gtk.PackType)(1));
			w14.Position = 7;
			w14.Expand = false;
			w14.Fill = false;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox4 = new global::Gtk.VBox();
			this.vbox4.Name = "vbox4";
			this.vbox4.Spacing = 6;
			// Container child vbox4.Gtk.Box+BoxChild
			this.ydatepickerStart = new global::Gamma.Widgets.yDatePicker();
			this.ydatepickerStart.Events = ((global::Gdk.EventMask)(256));
			this.ydatepickerStart.Name = "ydatepickerStart";
			this.ydatepickerStart.WithTime = true;
			this.ydatepickerStart.Date = new global::System.DateTime(0);
			this.ydatepickerStart.IsEditable = true;
			this.ydatepickerStart.AutoSeparation = false;
			this.vbox4.Add(this.ydatepickerStart);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.ydatepickerStart]));
			w15.Position = 0;
			w15.Expand = false;
			w15.Fill = false;
			// Container child vbox4.Gtk.Box+BoxChild
			this.ydatepickerEnd = new global::Gamma.Widgets.yDatePicker();
			this.ydatepickerEnd.Events = ((global::Gdk.EventMask)(256));
			this.ydatepickerEnd.Name = "ydatepickerEnd";
			this.ydatepickerEnd.WithTime = true;
			this.ydatepickerEnd.Date = new global::System.DateTime(0);
			this.ydatepickerEnd.IsEditable = true;
			this.ydatepickerEnd.AutoSeparation = false;
			this.vbox4.Add(this.ydatepickerEnd);
			global::Gtk.Box.BoxChild w16 = ((global::Gtk.Box.BoxChild)(this.vbox4[this.ydatepickerEnd]));
			w16.Position = 1;
			w16.Expand = false;
			this.hbox1.Add(this.vbox4);
			global::Gtk.Box.BoxChild w17 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox4]));
			w17.PackType = ((global::Gtk.PackType)(1));
			w17.Position = 8;
			// Container child hbox1.Gtk.Box+BoxChild
			this.vbox3 = new global::Gtk.VBox();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.label4 = new global::Gtk.Label();
			this.label4.Name = "label4";
			this.label4.LabelProp = global::Mono.Unix.Catalog.GetString("обрезать до");
			this.vbox3.Add(this.label4);
			global::Gtk.Box.BoxChild w18 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.label4]));
			w18.Position = 0;
			w18.Expand = false;
			w18.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.label2 = new global::Gtk.Label();
			this.label2.Sensitive = false;
			this.label2.Name = "label2";
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("label1");
			this.vbox3.Add(this.label2);
			global::Gtk.Box.BoxChild w19 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.label2]));
			w19.Position = 1;
			w19.Expand = false;
			w19.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.label5 = new global::Gtk.Label();
			this.label5.Name = "label5";
			this.label5.LabelProp = global::Mono.Unix.Catalog.GetString("обрезать после");
			this.vbox3.Add(this.label5);
			global::Gtk.Box.BoxChild w20 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.label5]));
			w20.Position = 2;
			w20.Expand = false;
			w20.Fill = false;
			this.hbox1.Add(this.vbox3);
			global::Gtk.Box.BoxChild w21 = ((global::Gtk.Box.BoxChild)(this.hbox1[this.vbox3]));
			w21.PackType = ((global::Gtk.PackType)(1));
			w21.Position = 9;
			w21.Expand = false;
			w21.Fill = false;
			this.vbox1.Add(this.hbox1);
			global::Gtk.Box.BoxChild w22 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.hbox1]));
			w22.Position = 0;
			w22.Expand = false;
			w22.Fill = false;
			// Container child vbox1.Gtk.Box+BoxChild
			this.gmapWidget = new global::GMap.NET.GtkSharp.GMapControl();
			this.gmapWidget.Name = "gmapWidget";
			this.gmapWidget.MaxZoom = 0;
			this.gmapWidget.MinZoom = 0;
			this.gmapWidget.MouseWheelZoomEnabled = true;
			this.gmapWidget.ShowTileGridLines = false;
			this.gmapWidget.GrayScaleMode = false;
			this.gmapWidget.NegativeMode = false;
			this.gmapWidget.HasFrame = false;
			this.gmapWidget.Bearing = 0F;
			this.gmapWidget.Zoom = 0;
			this.gmapWidget.RoutesEnabled = true;
			this.gmapWidget.PolygonsEnabled = false;
			this.gmapWidget.MarkersEnabled = true;
			this.gmapWidget.CanDragMap = true;
			this.vbox1.Add(this.gmapWidget);
			global::Gtk.Box.BoxChild w23 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.gmapWidget]));
			w23.Position = 2;
			// Container child vbox1.Gtk.Box+BoxChild
			this.statusbar1 = new global::Gtk.Statusbar();
			this.statusbar1.Name = "statusbar1";
			this.statusbar1.Spacing = 6;
			// Container child statusbar1.Gtk.Box+BoxChild
			this.ylabelPoint = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelPoint.Name = "ylabelPoint";
			this.statusbar1.Add(this.ylabelPoint);
			global::Gtk.Box.BoxChild w24 = ((global::Gtk.Box.BoxChild)(this.statusbar1[this.ylabelPoint]));
			w24.Position = 2;
			w24.Expand = false;
			w24.Fill = false;
			this.vbox1.Add(this.statusbar1);
			global::Gtk.Box.BoxChild w25 = ((global::Gtk.Box.BoxChild)(this.vbox1[this.statusbar1]));
			w25.Position = 3;
			w25.Expand = false;
			w25.Fill = false;
			this.Add(this.vbox1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.DefaultWidth = 1090;
			this.DefaultHeight = 300;
			this.label2.Hide();
			this.Show();
			this.radioSmall.Clicked += new global::System.EventHandler(this.OnRadioSmallClicked);
			this.radioNumbers.Clicked += new global::System.EventHandler(this.OnRadioNumbersClicked);
			this.buttonRecalculateToBase.Clicked += new global::System.EventHandler(this.OnButtonRecalculateToBaseClicked);
			this.buttonFindGap.Clicked += new global::System.EventHandler(this.OnButtonFindGapClicked);
			this.buttonRecountMileage.Clicked += new global::System.EventHandler(this.OnButtonRecountMileageClicked);
			this.buttonLastAddress.Clicked += new global::System.EventHandler(this.OnButtonLastAddressClicked);
			this.buttonCutTrack.Clicked += new global::System.EventHandler(this.OnButtonCutTrackClicked);
		}
	}
}
