
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz.Filters.GtkViews
{
	public partial class CarFilterView
	{
		private global::Gtk.ScrolledWindow GtkScrolledWindow;

		private global::Gtk.Table table1;

		private global::QS.Widgets.GtkUI.EntityViewModelEntry entryModel;

		private global::Gtk.ScrolledWindow GtkScrolledWindowOwnType;

		private global::Gamma.Widgets.EnumCheckList enumcheckCarOwnType;

		private global::Gtk.ScrolledWindow GtkScrolledWindowTypeOfUse;

		private global::Gamma.Widgets.EnumCheckList enumcheckCarTypeOfUse;

		private global::Gtk.HBox hbox2;

		private global::QS.Widgets.NullableCheckButton nullablecheckArchive;

		private global::Gtk.HBox hbox3;

		private global::QS.Widgets.NullableCheckButton nullablecheckVisitingMaster;

		private global::Gamma.GtkWidgets.yLabel ylabel1;

		private global::Gamma.GtkWidgets.yLabel ylabelArchive;

		private global::Gamma.GtkWidgets.yLabel ylabelModel;

		private global::Gamma.GtkWidgets.yLabel ylabelOwnType;

		private global::Gamma.GtkWidgets.yLabel ylabelTypeOfUse;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.Filters.GtkViews.CarFilterView
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.Filters.GtkViews.CarFilterView";
			// Container child Vodovoz.Filters.GtkViews.CarFilterView.Gtk.Container+ContainerChild
			this.GtkScrolledWindow = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindow.Name = "GtkScrolledWindow";
			this.GtkScrolledWindow.VscrollbarPolicy = ((global::Gtk.PolicyType)(2));
			this.GtkScrolledWindow.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindow.Gtk.Container+ContainerChild
			global::Gtk.Viewport w1 = new global::Gtk.Viewport();
			w1.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child GtkViewport.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table(((uint)(4)), ((uint)(4)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.entryModel = new global::QS.Widgets.GtkUI.EntityViewModelEntry();
			this.entryModel.Events = ((global::Gdk.EventMask)(256));
			this.entryModel.Name = "entryModel";
			this.entryModel.CanEditReference = false;
			this.table1.Add(this.entryModel);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.entryModel]));
			w2.TopAttach = ((uint)(2));
			w2.BottomAttach = ((uint)(3));
			w2.LeftAttach = ((uint)(1));
			w2.RightAttach = ((uint)(2));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindowOwnType = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindowOwnType.WidthRequest = 200;
			this.GtkScrolledWindowOwnType.Name = "GtkScrolledWindowOwnType";
			this.GtkScrolledWindowOwnType.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindowOwnType.Gtk.Container+ContainerChild
			global::Gtk.Viewport w3 = new global::Gtk.Viewport();
			w3.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child GtkViewport2.Gtk.Container+ContainerChild
			this.enumcheckCarOwnType = new global::Gamma.Widgets.EnumCheckList();
			this.enumcheckCarOwnType.Name = "enumcheckCarOwnType";
			w3.Add(this.enumcheckCarOwnType);
			this.GtkScrolledWindowOwnType.Add(w3);
			this.table1.Add(this.GtkScrolledWindowOwnType);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindowOwnType]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(4));
			w6.LeftAttach = ((uint)(3));
			w6.RightAttach = ((uint)(4));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.GtkScrolledWindowTypeOfUse = new global::Gtk.ScrolledWindow();
			this.GtkScrolledWindowTypeOfUse.WidthRequest = 200;
			this.GtkScrolledWindowTypeOfUse.Name = "GtkScrolledWindowTypeOfUse";
			this.GtkScrolledWindowTypeOfUse.ShadowType = ((global::Gtk.ShadowType)(1));
			// Container child GtkScrolledWindowTypeOfUse.Gtk.Container+ContainerChild
			global::Gtk.Viewport w7 = new global::Gtk.Viewport();
			w7.ShadowType = ((global::Gtk.ShadowType)(0));
			// Container child GtkViewport1.Gtk.Container+ContainerChild
			this.enumcheckCarTypeOfUse = new global::Gamma.Widgets.EnumCheckList();
			this.enumcheckCarTypeOfUse.Name = "enumcheckCarTypeOfUse";
			w7.Add(this.enumcheckCarTypeOfUse);
			this.GtkScrolledWindowTypeOfUse.Add(w7);
			this.table1.Add(this.GtkScrolledWindowTypeOfUse);
			global::Gtk.Table.TableChild w10 = ((global::Gtk.Table.TableChild)(this.table1[this.GtkScrolledWindowTypeOfUse]));
			w10.TopAttach = ((uint)(1));
			w10.BottomAttach = ((uint)(4));
			w10.LeftAttach = ((uint)(2));
			w10.RightAttach = ((uint)(3));
			w10.XPadding = ((uint)(10));
			w10.XOptions = ((global::Gtk.AttachOptions)(4));
			w10.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox2 = new global::Gtk.HBox();
			this.hbox2.Name = "hbox2";
			this.hbox2.Spacing = 6;
			// Container child hbox2.Gtk.Box+BoxChild
			this.nullablecheckArchive = new global::QS.Widgets.NullableCheckButton();
			this.nullablecheckArchive.CanFocus = true;
			this.nullablecheckArchive.Name = "nullablecheckArchive";
			this.nullablecheckArchive.UseUnderline = true;
			this.hbox2.Add(this.nullablecheckArchive);
			global::Gtk.Box.BoxChild w11 = ((global::Gtk.Box.BoxChild)(this.hbox2[this.nullablecheckArchive]));
			w11.Position = 0;
			w11.Expand = false;
			w11.Fill = false;
			this.table1.Add(this.hbox2);
			global::Gtk.Table.TableChild w12 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox2]));
			w12.LeftAttach = ((uint)(1));
			w12.RightAttach = ((uint)(2));
			w12.XOptions = ((global::Gtk.AttachOptions)(4));
			w12.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.hbox3 = new global::Gtk.HBox();
			this.hbox3.Name = "hbox3";
			this.hbox3.Spacing = 6;
			// Container child hbox3.Gtk.Box+BoxChild
			this.nullablecheckVisitingMaster = new global::QS.Widgets.NullableCheckButton();
			this.nullablecheckVisitingMaster.CanFocus = true;
			this.nullablecheckVisitingMaster.Name = "nullablecheckVisitingMaster";
			this.nullablecheckVisitingMaster.UseUnderline = true;
			this.hbox3.Add(this.nullablecheckVisitingMaster);
			global::Gtk.Box.BoxChild w13 = ((global::Gtk.Box.BoxChild)(this.hbox3[this.nullablecheckVisitingMaster]));
			w13.Position = 0;
			w13.Expand = false;
			w13.Fill = false;
			this.table1.Add(this.hbox3);
			global::Gtk.Table.TableChild w14 = ((global::Gtk.Table.TableChild)(this.table1[this.hbox3]));
			w14.TopAttach = ((uint)(1));
			w14.BottomAttach = ((uint)(2));
			w14.LeftAttach = ((uint)(1));
			w14.RightAttach = ((uint)(2));
			w14.XOptions = ((global::Gtk.AttachOptions)(4));
			w14.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabel1 = new global::Gamma.GtkWidgets.yLabel();
			this.ylabel1.Name = "ylabel1";
			this.ylabel1.Xalign = 1F;
			this.ylabel1.LabelProp = global::Mono.Unix.Catalog.GetString("Водитель - выездной мастер:");
			this.table1.Add(this.ylabel1);
			global::Gtk.Table.TableChild w15 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabel1]));
			w15.TopAttach = ((uint)(1));
			w15.BottomAttach = ((uint)(2));
			w15.XOptions = ((global::Gtk.AttachOptions)(4));
			w15.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelArchive = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelArchive.Name = "ylabelArchive";
			this.ylabelArchive.Xalign = 1F;
			this.ylabelArchive.LabelProp = global::Mono.Unix.Catalog.GetString("Архивные:");
			this.table1.Add(this.ylabelArchive);
			global::Gtk.Table.TableChild w16 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelArchive]));
			w16.XOptions = ((global::Gtk.AttachOptions)(4));
			w16.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelModel = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelModel.Name = "ylabelModel";
			this.ylabelModel.Xalign = 1F;
			this.ylabelModel.LabelProp = global::Mono.Unix.Catalog.GetString("Модель:");
			this.table1.Add(this.ylabelModel);
			global::Gtk.Table.TableChild w17 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelModel]));
			w17.TopAttach = ((uint)(2));
			w17.BottomAttach = ((uint)(3));
			w17.XOptions = ((global::Gtk.AttachOptions)(4));
			w17.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelOwnType = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelOwnType.Name = "ylabelOwnType";
			this.ylabelOwnType.LabelProp = global::Mono.Unix.Catalog.GetString("Принадлежность:");
			this.table1.Add(this.ylabelOwnType);
			global::Gtk.Table.TableChild w18 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelOwnType]));
			w18.LeftAttach = ((uint)(3));
			w18.RightAttach = ((uint)(4));
			w18.XOptions = ((global::Gtk.AttachOptions)(4));
			w18.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.ylabelTypeOfUse = new global::Gamma.GtkWidgets.yLabel();
			this.ylabelTypeOfUse.Name = "ylabelTypeOfUse";
			this.ylabelTypeOfUse.LabelProp = global::Mono.Unix.Catalog.GetString("Тип:");
			this.table1.Add(this.ylabelTypeOfUse);
			global::Gtk.Table.TableChild w19 = ((global::Gtk.Table.TableChild)(this.table1[this.ylabelTypeOfUse]));
			w19.LeftAttach = ((uint)(2));
			w19.RightAttach = ((uint)(3));
			w19.XOptions = ((global::Gtk.AttachOptions)(4));
			w19.YOptions = ((global::Gtk.AttachOptions)(4));
			w1.Add(this.table1);
			this.GtkScrolledWindow.Add(w1);
			this.Add(this.GtkScrolledWindow);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
		}
	}
}
