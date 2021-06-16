
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Views.Reports
{
	public partial class DeliveryAnalyticsReportView
	{
		private global::Gamma.GtkWidgets.yTable ytable1;

		private global::QS.Widgets.GtkUI.DatePicker dayOfWeek;

		private global::QS.Widgets.GtkUI.DatePicker deliveryDate;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry districtEntry;

		private global::Gamma.GtkWidgets.yButton exportBtn;

		private global::Gamma.GtkWidgets.yTreeView treeviewGeographic;

		private global::Gamma.GtkWidgets.yTreeView treeviewPartCity;

		private global::Gamma.GtkWidgets.yTreeView treeviewWave;

		private global::Gamma.GtkWidgets.yLabel ylabel1;

		private global::Gamma.GtkWidgets.yLabel ylabel2;

		private global::Gamma.GtkWidgets.yLabel ylabel3;

		private global::Gamma.GtkWidgets.yLabel ylabel4;

		private global::Gamma.GtkWidgets.yLabel ylabel5;

		private global::Gamma.GtkWidgets.yLabel ylabel6;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Views.Reports.DeliveryAnalyticsReportView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Views.Reports.DeliveryAnalyticsReportView";
			// Container child Vodovoz.Views.Reports.DeliveryAnalyticsReportView.Gtk.Container+ContainerChild
			this.ytable1 = new global::Gamma.GtkWidgets.yTable();
			this.ytable1.Name = "ytable1";
			this.ytable1.NRows = ((uint)(7));
			this.ytable1.NColumns = ((uint)(2));
			this.ytable1.RowSpacing = ((uint)(6));
			this.ytable1.ColumnSpacing = ((uint)(6));
			// Container child ytable1.Gtk.Table+TableChild
			this.dayOfWeek = new global::QS.Widgets.GtkUI.DatePicker();
			this.dayOfWeek.Events = ((global::Gdk.EventMask)(256));
			this.dayOfWeek.Name = "dayOfWeek";
			this.dayOfWeek.WithTime = false;
			this.dayOfWeek.HideCalendarButton = false;
			this.dayOfWeek.Date = new global::System.DateTime(0);
			this.dayOfWeek.IsEditable = true;
			this.dayOfWeek.AutoSeparation = false;
			this.ytable1.Add(this.dayOfWeek);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.ytable1[this.dayOfWeek]));
			w1.TopAttach = ((uint)(2));
			w1.BottomAttach = ((uint)(3));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.deliveryDate = new global::QS.Widgets.GtkUI.DatePicker();
			this.deliveryDate.Events = ((global::Gdk.EventMask)(256));
			this.deliveryDate.Name = "deliveryDate";
			this.deliveryDate.WithTime = false;
			this.deliveryDate.HideCalendarButton = false;
			this.deliveryDate.Date = new global::System.DateTime(0);
			this.deliveryDate.IsEditable = true;
			this.deliveryDate.AutoSeparation = false;
			this.ytable1.Add(this.deliveryDate);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.ytable1[this.deliveryDate]));
			w2.TopAttach = ((uint)(3));
			w2.BottomAttach = ((uint)(4));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.districtEntry = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.districtEntry.Events = ((global::Gdk.EventMask)(256));
			this.districtEntry.Name = "districtEntry";
			this.districtEntry.CanEditReference = true;
			this.ytable1.Add(this.districtEntry);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.ytable1[this.districtEntry]));
			w3.LeftAttach = ((uint)(1));
			w3.RightAttach = ((uint)(2));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.exportBtn = new global::Gamma.GtkWidgets.yButton();
			this.exportBtn.CanFocus = true;
			this.exportBtn.Name = "exportBtn";
			this.exportBtn.UseUnderline = true;
			this.exportBtn.Label = global::Mono.Unix.Catalog.GetString("Загрузить отчёт");
			this.ytable1.Add(this.exportBtn);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.ytable1[this.exportBtn]));
			w4.TopAttach = ((uint)(6));
			w4.BottomAttach = ((uint)(7));
			w4.RightAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(0));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.treeviewGeographic = new global::Gamma.GtkWidgets.yTreeView();
			this.treeviewGeographic.CanFocus = true;
			this.treeviewGeographic.Name = "treeviewGeographic";
			this.ytable1.Add(this.treeviewGeographic);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.ytable1[this.treeviewGeographic]));
			w5.TopAttach = ((uint)(5));
			w5.BottomAttach = ((uint)(6));
			w5.LeftAttach = ((uint)(1));
			w5.RightAttach = ((uint)(2));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.treeviewPartCity = new global::Gamma.GtkWidgets.yTreeView();
			this.treeviewPartCity.CanFocus = true;
			this.treeviewPartCity.Name = "treeviewPartCIty";
			this.ytable1.Add(this.treeviewPartCity);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.ytable1[this.treeviewPartCity]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(2));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.treeviewWave = new global::Gamma.GtkWidgets.yTreeView();
			this.treeviewWave.CanFocus = true;
			this.treeviewWave.Name = "treeviewWave";
			this.ytable1.Add(this.treeviewWave);
			global::Gtk.Table.TableChild w7 = ((global::Gtk.Table.TableChild)(this.ytable1[this.treeviewWave]));
			w7.TopAttach = ((uint)(4));
			w7.BottomAttach = ((uint)(5));
			w7.LeftAttach = ((uint)(1));
			w7.RightAttach = ((uint)(2));
			w7.XOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.ylabel1 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel1.Name = "ylabel1";
			this.ylabel1.Xalign = 1F;
			this.ylabel1.LabelProp = global::Mono.Unix.Catalog.GetString("Район:");
			this.ytable1.Add(this.ylabel1);
			global::Gtk.Table.TableChild w8 = ((global::Gtk.Table.TableChild)(this.ytable1[this.ylabel1]));
			w8.XOptions = ((global::Gtk.AttachOptions)(4));
			w8.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.ylabel2 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel2.Name = "ylabel2";
			this.ylabel2.Xalign = 1F;
			this.ylabel2.LabelProp = global::Mono.Unix.Catalog.GetString("Часть:");
			this.ytable1.Add(this.ylabel2);
			global::Gtk.Table.TableChild w9 = ((global::Gtk.Table.TableChild)(this.ytable1[this.ylabel2]));
			w9.TopAttach = ((uint)(1));
			w9.BottomAttach = ((uint)(2));
			w9.XOptions = ((global::Gtk.AttachOptions)(4));
			w9.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.ylabel3 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel3.Name = "ylabel3";
			this.ylabel3.Xalign = 1F;
			this.ylabel3.LabelProp = global::Mono.Unix.Catalog.GetString("День недели:");
			this.ytable1.Add(this.ylabel3);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.ytable1[this.ylabel3]));
			w10.TopAttach = ((uint)(2));
			w10.BottomAttach = ((uint)(3));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.ylabel4 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel4.Name = "ylabel4";
			this.ylabel4.Xalign = 1F;
			this.ylabel4.LabelProp = global::Mono.Unix.Catalog.GetString("Дата доставки:");
			this.ytable1.Add(this.ylabel4);
			global::Gtk.Table.TableChild w11 = ((global::Gtk.Table.TableChild)(this.ytable1[this.ylabel4]));
			w11.TopAttach = ((uint)(3));
			w11.BottomAttach = ((uint)(4));
			w11.XOptions = ((global::Gtk.AttachOptions)(4));
			w11.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.ylabel5 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel5.Name = "ylabel5";
			this.ylabel5.Xalign = 1F;
			this.ylabel5.LabelProp = global::Mono.Unix.Catalog.GetString("Волна:");
			this.ytable1.Add(this.ylabel5);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.ytable1[this.ylabel5]));
			w12.TopAttach = ((uint)(4));
			w12.BottomAttach = ((uint)(5));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child ytable1.Gtk.Table+TableChild
			this.ylabel6 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel6.Name = "ylabel6";
			this.ylabel6.Xalign = 1F;
			this.ylabel6.LabelProp = global::Mono.Unix.Catalog.GetString("Юг/Север:");
			this.ytable1.Add(this.ylabel6);
			global::Gtk.Table.TableChild w13 = ((global::Gtk.Table.TableChild)(this.ytable1[this.ylabel6]));
			w13.TopAttach = ((uint)(5));
			w13.BottomAttach = ((uint)(6));
			w13.XOptions = ((global::Gtk.AttachOptions)(4));
			w13.YOptions = ((global::Gtk.AttachOptions)(4));
			this.Add(this.ytable1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.exportBtn.Clicked += new global::System.EventHandler(this.OnExportBtnClicked);
		}
	}
}
