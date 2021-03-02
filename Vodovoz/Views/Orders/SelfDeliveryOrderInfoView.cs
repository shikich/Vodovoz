using Autofac;
using QS.Views.GtkUI;
using QS.Views.Resolve;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class SelfDeliveryOrderInfoView : WidgetViewBase<SelfDeliveryOrderInfoViewModel>
    {
        public SelfDeliveryOrderInfoView(SelfDeliveryOrderInfoViewModel viewModel) : base (viewModel)
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
                .Resolve(ViewModel.SelfDeliveryOrderInfoPanelViewModel);
            
            orderInfoExpandedPanelView.ViewModel = ViewModel.ExpandedPanelViewModel;
            orderInfoExpandedPanelView.AddPanel(orderInfoPanelView);
        }
    }
}
