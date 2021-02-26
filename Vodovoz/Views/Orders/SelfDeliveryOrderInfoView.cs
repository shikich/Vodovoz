using QS.Views.GtkUI;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class SelfDeliveryOrderInfoView : WidgetViewBase<SelfDeliveryInfoViewModel>
    {
        public SelfDeliveryOrderInfoView(SelfDeliveryInfoViewModel viewModel) : base (viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            CreateOrderItemsView();
        }

        private void CreateOrderItemsView()
        {
            var orderItemsView = new OrderItemsView (new OrderItemsViewModel());
            hboxOrderItems.Add(orderItemsView);
            orderItemsView.Show();

            var orderInfoView = new SelfDeliveryOrderInfoPanelView();
            orderInfoExpandedPanelView.ViewModel = ViewModel.ExpandedPanelViewModel;
            orderInfoExpandedPanelView.AddPanel(orderInfoView);
        }
    }
}
