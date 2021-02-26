using System;
using Gamma.ColumnConfig;
using Gtk;
using Vodovoz.Domain.Orders;
using Vodovoz.Infrastructure.Converters;
using QS.Dialog.Gtk;

namespace Vodovoz.ViewWidgets.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderDepositReturnsItemsView : WidgetOnDialogBase
    {
        public OrderDepositReturnsItemsView()
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {/*
            ytreeViewDepositReturnsItems.ColumnsConfig = FluentColumnsConfig<OrderDepositReturnsItem>.Create()
                .AddColumn("Тип")
                    .AddTextRenderer(node => node.DepositTypeString)
                .AddColumn("Название")
                    .AddTextRenderer(node => node.EquipmentNomenclature != null ? node.EquipmentNomenclature.Name : string.Empty)
                .AddColumn("Кол-во")
                    .AddNumericRenderer(node => node.Count)
                        .Adjustment(new Adjustment(1, 0, 100000, 1, 100, 1))
                        .Editing(!(MyTab is OrderReturnsView))
                .AddColumn("Факт. кол-во")
                    .AddNumericRenderer(node => node.ActualCount, new NullValueToZeroConverter())
                        .Adjustment(new Adjustment(1, 0, 100000, 1, 100, 1))
                        .Editing(MyTab is OrderReturnsView)
                        .AddColumn("Цена")
                    .AddNumericRenderer(node => node.Deposit)
                        .Adjustment(new Adjustment(1, 0, 1000000, 1, 100, 1))
                        .Editing(true)
                .AddColumn("Сумма")
                    .AddNumericRenderer(node => node.Total)
                .Finish();

            ytreeViewDepositReturnsItems.ItemsDataSource = ViewModel.Order.ObservableOrderDepositReturnsItems;
            ytreeViewDepositReturnsItems.Selection.Changed += TreeDepositRefundItems_Selection_Changed;
            */
            //scrolledwindow2.VscrollbarPolicy = scrolled ? PolicyType.Always : PolicyType.Never;
        }
    }
}
