using Autofac;
using QS.Views.GtkUI;
using QS.Views.Resolve;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderFrom1cInfoView : WidgetViewBase<OrderFrom1cInfoViewModel>
    {
        public OrderFrom1cInfoView(OrderFrom1cInfoViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            CreateOrderItemsView();
            CreateSelfDeliveryOrderInfoPanelView();
        }

        private void CreateOrderItemsView()
        {
            var orderItemsView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                .Resolve(ViewModel.OrderItemsViewModel);

            hboxOrderItems.Add(orderItemsView);
            orderItemsView.Show();
        }

        private void CreateSelfDeliveryOrderInfoPanelView()
        {
            var orderInfoPanelView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                .Resolve(ViewModel.OrderFrom1cInfoPanelViewModel);

            orderInfoExpandedPanelView.ViewModel = ViewModel.ExpandedPanelViewModel;
            orderInfoExpandedPanelView.AddPanel(orderInfoPanelView);
        }
    }
}
