using System;
using QS.Views.GtkUI;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderItemsView : WidgetViewBase<OrderItemsViewModel>
    {
        public OrderItemsView(OrderItemsViewModel viewModel) : base (viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            //ybtnAddOrderItem.Clicked += (sender, args) => ViewModel.AddSalesItemCommand.Execute();
            ychkMovementEquipments.Toggled += YchkMovementEquipmentsOnToggled;
            ychkMovementEquipments.Binding.AddBinding(ViewModel, vm => vm.IsMovementItemsVisible, w => w.Active).InitializeFromSource();
            ychkDeposits.Toggled += YchkDepositsOnToggled;
            ychkDeposits.Binding.AddBinding(ViewModel, vm => vm.IsDepositsReturnsVisible, w => w.Active).InitializeFromSource();
        }

        private void YchkDepositsOnToggled(object sender, EventArgs e)
        {
            vboxDeposits.Visible = ViewModel.IsDepositsReturnsVisible;
        }

        private void YchkMovementEquipmentsOnToggled(object sender, EventArgs e)
        {
            orderEquipmentItemsView.Visible = ViewModel.IsMovementItemsVisible;
        }
    }
}
