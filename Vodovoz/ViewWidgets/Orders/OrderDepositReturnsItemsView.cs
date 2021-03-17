using System;
using Gamma.ColumnConfig;
using Gtk;
using Vodovoz.Domain.Orders;
using Vodovoz.Infrastructure.Converters;
using QS.Views.GtkUI;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewWidgets.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderDepositReturnsItemsView : WidgetViewBase<OrderDepositReturnsItemsViewModel>
    {
        public OrderDepositReturnsItemsView(OrderDepositReturnsItemsViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            ConfigureTree();

            ybtnRemoveDeposit.Clicked += (sender, args) => ViewModel.RemoveDepositCommand.Execute();
            ybtnAddEquipmentDeposit.Clicked += (sender, args) => ViewModel.AddEquipmentDepositCommand.Execute();
            ybtnRemoveDeposit.Binding.AddBinding(ViewModel, vm => vm.HasSelectedDeposit, w => w.Sensitive).InitializeFromSource();
        }

        private void ConfigureTree()
        {
            ytreeViewDepositReturnsItems.ColumnsConfig = FluentColumnsConfig<OrderDepositReturnsItem>.Create()
                .AddColumn("Тип")
                .AddTextRenderer(node => node.DepositTypeString)
                .AddColumn("Название")
                .AddTextRenderer(node => node.EquipmentNomenclature != null ? node.EquipmentNomenclature.Name : string.Empty)
                .AddColumn("Кол-во")
                .AddNumericRenderer(node => node.Count)
                .Adjustment(new Adjustment(1, 0, 100000, 1, 100, 1))
                //.Editing(!(MyTab is OrderReturnsView))
                .AddColumn("Факт. кол-во")
                .AddNumericRenderer(node => node.ActualCount, new NullValueToZeroConverter())
                .Adjustment(new Adjustment(1, 0, 100000, 1, 100, 1))
                //.Editing(MyTab is OrderReturnsView)
                .AddColumn("Цена")
                .AddNumericRenderer(node => node.DepositValue)
                .Adjustment(new Adjustment(1, 0, 1000000, 1, 100, 1))
                .Editing(true)
                .AddColumn("Сумма")
                .AddNumericRenderer(node => node.Total)
                .Finish();

            ytreeViewDepositReturnsItems.ItemsDataSource = ViewModel.Order.ObservableOrderDepositReturnsItems;
            ytreeViewDepositReturnsItems.Selection.Changed += TreeDepositRefundItemsOnSelectionChanged;
            
            GtkScrolledWindow.VscrollbarPolicy = ViewModel.DepositsScrolled ? PolicyType.Always : PolicyType.Never;
        }
        
        void TreeDepositRefundItemsOnSelectionChanged(object sender, EventArgs e)
        {
            ViewModel.SelectedDeposit = ytreeViewDepositReturnsItems.GetSelectedObject();
        }
    }
}
