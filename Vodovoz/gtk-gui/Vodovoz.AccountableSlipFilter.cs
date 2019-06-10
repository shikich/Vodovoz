
// This file has been generated by the GUI designer. Do not modify.
namespace Vodovoz
{
	public partial class AccountableSlipFilter
	{
		private global::Gtk.Table table1;

		private global::QSWidgetLib.DatePeriodPicker dateperiod;

		private global::Gtk.Label label1;

		private global::Gtk.Label label2;

		private global::Gtk.Label label3;

		private global::QS.Widgets.GtkUI.RepresentationEntry repEntryAccountable;

		private global::Gamma.Widgets.yEntryReference yentryExpense;

		protected virtual void Build()
		{
			global::Stetic.Gui.Initialize(this);
			// Widget Vodovoz.AccountableSlipFilter
			global::Stetic.BinContainer.Attach(this);
			this.Name = "Vodovoz.AccountableSlipFilter";
			// Container child Vodovoz.AccountableSlipFilter.Gtk.Container+ContainerChild
			this.table1 = new global::Gtk.Table(((uint)(2)), ((uint)(4)), false);
			this.table1.Name = "table1";
			this.table1.RowSpacing = ((uint)(6));
			this.table1.ColumnSpacing = ((uint)(6));
			// Container child table1.Gtk.Table+TableChild
			this.dateperiod = new global::QSWidgetLib.DatePeriodPicker();
			this.dateperiod.Events = ((global::Gdk.EventMask)(256));
			this.dateperiod.Name = "dateperiod";
			this.dateperiod.StartDate = new global::System.DateTime(0);
			this.dateperiod.EndDate = new global::System.DateTime(0);
			this.table1.Add(this.dateperiod);
			global::Gtk.Table.TableChild w1 = ((global::Gtk.Table.TableChild)(this.table1[this.dateperiod]));
			w1.LeftAttach = ((uint)(3));
			w1.RightAttach = ((uint)(4));
			w1.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label1 = new global::Gtk.Label();
			this.label1.Name = "label1";
			this.label1.Xalign = 1F;
			this.label1.LabelProp = global::Mono.Unix.Catalog.GetString("Сотрудник:");
			this.table1.Add(this.label1);
			global::Gtk.Table.TableChild w2 = ((global::Gtk.Table.TableChild)(this.table1[this.label1]));
			w2.XOptions = ((global::Gtk.AttachOptions)(4));
			w2.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label2 = new global::Gtk.Label();
			this.label2.Name = "label2";
			this.label2.Xalign = 1F;
			this.label2.LabelProp = global::Mono.Unix.Catalog.GetString("Период:");
			this.table1.Add(this.label2);
			global::Gtk.Table.TableChild w3 = ((global::Gtk.Table.TableChild)(this.table1[this.label2]));
			w3.LeftAttach = ((uint)(2));
			w3.RightAttach = ((uint)(3));
			w3.XOptions = ((global::Gtk.AttachOptions)(4));
			w3.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.label3 = new global::Gtk.Label();
			this.label3.Name = "label3";
			this.label3.Xalign = 1F;
			this.label3.LabelProp = global::Mono.Unix.Catalog.GetString("Статья расхода:");
			this.table1.Add(this.label3);
			global::Gtk.Table.TableChild w4 = ((global::Gtk.Table.TableChild)(this.table1[this.label3]));
			w4.TopAttach = ((uint)(1));
			w4.BottomAttach = ((uint)(2));
			w4.XOptions = ((global::Gtk.AttachOptions)(4));
			w4.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.repEntryAccountable = new global::QS.Widgets.GtkUI.RepresentationEntry();
			this.repEntryAccountable.Events = ((global::Gdk.EventMask)(256));
			this.repEntryAccountable.Name = "repEntryAccountable";
			this.table1.Add(this.repEntryAccountable);
			global::Gtk.Table.TableChild w5 = ((global::Gtk.Table.TableChild)(this.table1[this.repEntryAccountable]));
			w5.LeftAttach = ((uint)(1));
			w5.RightAttach = ((uint)(2));
			w5.XOptions = ((global::Gtk.AttachOptions)(4));
			w5.YOptions = ((global::Gtk.AttachOptions)(4));
			// Container child table1.Gtk.Table+TableChild
			this.yentryExpense = new global::Gamma.Widgets.yEntryReference();
			this.yentryExpense.Events = ((global::Gdk.EventMask)(256));
			this.yentryExpense.Name = "yentryExpense";
			this.table1.Add(this.yentryExpense);
			global::Gtk.Table.TableChild w6 = ((global::Gtk.Table.TableChild)(this.table1[this.yentryExpense]));
			w6.TopAttach = ((uint)(1));
			w6.BottomAttach = ((uint)(2));
			w6.LeftAttach = ((uint)(1));
			w6.RightAttach = ((uint)(2));
			w6.XOptions = ((global::Gtk.AttachOptions)(4));
			w6.YOptions = ((global::Gtk.AttachOptions)(4));
			this.Add(this.table1);
			if ((this.Child != null))
			{
				this.Child.ShowAll();
			}
			this.Hide();
			this.yentryExpense.Changed += new global::System.EventHandler(this.OnYentryExpenseChanged);
			this.repEntryAccountable.Changed += new global::System.EventHandler(this.OnYentryAccountableChanged);
			this.dateperiod.PeriodChanged += new global::System.EventHandler(this.OnDateperiodPeriodChanged);
		}
	}
}
