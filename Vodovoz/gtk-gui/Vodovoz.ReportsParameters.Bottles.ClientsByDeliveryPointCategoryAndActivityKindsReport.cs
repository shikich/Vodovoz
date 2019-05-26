
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.ReportsParameters.Bottles
{
	public partial class ClientsByDeliveryPointCategoryAndActivityKindsReport
	{
		private global::Gtk.VBox vbox2;

		private global::Gtk.Table table1;

		private global::QS.Widgets.GtkUI.DateRangePicker dtrngPeriod;

		private global::Gamma.Widgets.yEnumComboBox enumCmbPaymentType;

		private global::Gamma.GtkWidgets.yLabel lblDeliverypointCategory;

		private global::Gtk.Label lblPaymentType;

		private global::Gamma.GtkWidgets.yLabel lblPeriod;

		private global::Gamma.Widgets.ySpecComboBox specCmbDeliveryPointCategory;

		private global::Gtk.Frame frame1;

		private global::Gtk.Alignment GtkAlignment1;

		private global::Gtk.VBox vbox3;

		private global::Gamma.Widgets.ySpecComboBox specCmbSubstring;

		private global::Gamma.GtkWidgets.yEntry yEntSubstring;

		private global::Gtk.ScrolledWindow srlWinSubstrings;

		private global::Gamma.GtkWidgets.yTreeView yTreeSubstrings;

		private global::Gtk.Label GtkLabel2;

		private global::Gtk.Button buttonRun;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.ReportsParameters.Bottles.ClientsByDeliveryPointCategoryAndActivityKindsReport
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.ReportsParameters.Bottles.ClientsByDeliveryPointCategoryAndActivityKindsR" +
				"eport";
			// Container child Vodovoz.ReportsParameters.Bottles.ClientsByDeliveryPointCategoryAndActivityKindsReport.Gtk.Container+ContainerChild
			this.vbox2 = new global::Gtk.VBox();
			this.vbox2.Name = "vbox2";
			this.vbox2.Spacing = 6;
			// Container child vbox2.Gtk.Box+BoxChild
			this.table1 = new global::Gtk.Table(((uint)(3)), ((uint)(2)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			this.table1.BorderWidth = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.dtrngPeriod = new global::QS.Widgets.GtkUI.DateRangePicker();
			this.dtrngPeriod.Events = ((global::Gdk.EventMask)(256));
			this.dtrngPeriod.Name = "dtrngPeriod";
			this.dtrngPeriod.StartDate = new global::System.DateTime(0);
			this.dtrngPeriod.EndDate = new global::System.DateTime(0);
			this.table1.Add(this.dtrngPeriod);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.dtrngPeriod]));
			w1.LeftAttach = ((uint)(1));
			w1.RightAttach = ((uint)(2));
			w1.XOptions = ((global::Gtk.AttachOptions)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.enumCmbPaymentType = new global::Gamma.Widgets.yEnumComboBox();
			this.enumCmbPaymentType.Name = "enumCmbPaymentType";
			this.enumCmbPaymentType.ShowSpecialStateAll = true;
			this.enumCmbPaymentType.ShowSpecialStateNot = false;
			this.enumCmbPaymentType.UseShortTitle = false;
			this.enumCmbPaymentType.DefaultFirst = false;
			this.table1.Add(this.enumCmbPaymentType);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.enumCmbPaymentType]));
			w2.TopAttach = ((uint)(2));
			w2.BottomAttach = ((uint)(3));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.lblDeliverypointCategory = new global::Gamma.GtkWidgets.yLabel();
			this.lblDeliverypointCategory.Name = "lblDeliverypointCategory";
			this.lblDeliverypointCategory.Xalign = 1F;
			this.lblDeliverypointCategory.LabelProp = global::Mono.Unix.Catalog.GetString("Категория точки доставки:");
			this.table1.Add(this.lblDeliverypointCategory);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.lblDeliverypointCategory]));
			w3.TopAttach = ((uint)(1));
			w3.BottomAttach = ((uint)(2));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.lblPaymentType = new global::Gtk.Label();
			this.lblPaymentType.Name = "lblPaymentType";
			this.lblPaymentType.Xalign = 1F;
			this.lblPaymentType.LabelProp = global::Mono.Unix.Catalog.GetString("Тип оплаты:");
			this.table1.Add(this.lblPaymentType);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.lblPaymentType]));
			w4.TopAttach = ((uint)(2));
			w4.BottomAttach = ((uint)(3));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.lblPeriod = new global::Gamma.GtkWidgets.yLabel();
			this.lblPeriod.Name = "lblPeriod";
			this.lblPeriod.Xalign = 1F;
			this.lblPeriod.LabelProp = global::Mono.Unix.Catalog.GetString("Период:");
			this.table1.Add(this.lblPeriod);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.lblPeriod]));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.specCmbDeliveryPointCategory = new global::Gamma.Widgets.ySpecComboBox();
			this.specCmbDeliveryPointCategory.Name = "specCmbDeliveryPointCategory";
			this.specCmbDeliveryPointCategory.AddIfNotExist = false;
			this.specCmbDeliveryPointCategory.DefaultFirst = false;
			this.specCmbDeliveryPointCategory.ShowSpecialStateAll = true;
			this.specCmbDeliveryPointCategory.ShowSpecialStateNot = false;
			this.table1.Add(this.specCmbDeliveryPointCategory);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.specCmbDeliveryPointCategory]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(2));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			this.vbox2.Add(this.table1);
			global::Gtk.Box.BoxChild w7 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.table1]));
			w7.Position = 0;
			w7.Expand = false;
			w7.Fill = false;
			// Container child vbox2.Gtk.Box+BoxChild
			this.frame1 = new global::Gtk.Frame();
			this.frame1.Name = "frame1";
			this.frame1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child frame1.Gtk.Container+ContainerChild
			this.GtkAlignment1 = new global::Gtk.Alignment(0F, 0F, 1F, 1F);
			this.GtkAlignment1.Name = "GtkAlignment1";
			this.GtkAlignment1.LeftPadding = ((uint)(12));
			// Container child GtkAlignment1.Gtk.Container+ContainerChild
			this.vbox3 = new global::Gtk.VBox();
			this.vbox3.Name = "vbox3";
			this.vbox3.Spacing = 6;
			// Container child vbox3.Gtk.Box+BoxChild
			this.specCmbSubstring = new global::Gamma.Widgets.ySpecComboBox();
			this.specCmbSubstring.Name = "specCmbSubstring";
			this.specCmbSubstring.AddIfNotExist = false;
			this.specCmbSubstring.DefaultFirst = false;
			this.specCmbSubstring.ShowSpecialStateAll = false;
			this.specCmbSubstring.ShowSpecialStateNot = true;
			this.vbox3.Add(this.specCmbSubstring);
			global::Gtk.Box.BoxChild w8 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.specCmbSubstring]));
			w8.Position = 0;
			w8.Expand = false;
			w8.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.yEntSubstring = new global::Gamma.GtkWidgets.yEntry();
			this.yEntSubstring.CanFocus = true;
			this.yEntSubstring.Name = "yEntSubstring";
			this.yEntSubstring.IsEditable = true;
			this.yEntSubstring.InvisibleChar = '•';
			this.vbox3.Add(this.yEntSubstring);
			global::Gtk.Box.BoxChild w9 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.yEntSubstring]));
			w9.Position = 1;
			w9.Expand = false;
			w9.Fill = false;
			// Container child vbox3.Gtk.Box+BoxChild
			this.srlWinSubstrings = new global::Gtk.ScrolledWindow();
			this.srlWinSubstrings.Name = "srlWinSubstrings";
			this.srlWinSubstrings.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child srlWinSubstrings.Gtk.Container+ContainerChild
			this.yTreeSubstrings = new global::Gamma.GtkWidgets.yTreeView();
			this.yTreeSubstrings.CanFocus = true;
			this.yTreeSubstrings.Name = "yTreeSubstrings";
			this.srlWinSubstrings.Add(this.yTreeSubstrings);
			this.vbox3.Add(this.srlWinSubstrings);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.vbox3[this.srlWinSubstrings]));
			w11.Position = 2;
			this.GtkAlignment1.Add(this.vbox3);
			this.frame1.Add(this.GtkAlignment1);
			this.GtkLabel2 = new global::Gtk.Label();
			this.GtkLabel2.Name = "GtkLabel2";
			this.GtkLabel2.LabelProp = global::Mono.Unix.Catalog.GetString("<b>Комментарии и названия содержат:</b>");
			this.GtkLabel2.UseMarkup = true;
			this.frame1.LabelWidget = this.GtkLabel2;
			this.vbox2.Add(this.frame1);
			global::Gtk.Box.BoxChild w14 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.frame1]));
			w14.Position = 1;
			// Container child vbox2.Gtk.Box+BoxChild
			this.buttonRun = new global::Gtk.Button();
			this.buttonRun.CanFocus = true;
			this.buttonRun.Name = "buttonRun";
			this.buttonRun.UseUnderline = true;
			this.buttonRun.Label = global::Mono.Unix.Catalog.GetString("Сформировать отчет");
			this.vbox2.Add(this.buttonRun);
			global::Gtk.Box.BoxChild w15 = ((global::Gtk.Box.BoxChild)(this.vbox2[this.buttonRun]));
			w15.PackType = ((global::Gtk.PackType)(1));
			w15.Position = 2;
			w15.Expand = false;
			w15.Fill = false;
			this.Add(this.vbox2);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.buttonRun.Clicked += new global::System.EventHandler(this.OnButtonRunClicked);
		}
	}
}
