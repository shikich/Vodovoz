using Autofac;
using Gtk;
using QS.Views.GtkUI;
using QS.Views.Resolve;
using QSWidgetLib;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class ClosingDocOrderInfoView : WidgetViewBase<ClosingDocOrderInfoViewModel>
    {
        private MenuItem menuItemCloseOrder;
        private MenuItem menuItemReturnToAccepted;
        
        public ClosingDocOrderInfoView(ClosingDocOrderInfoViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            ConfigureButtonActions();
            CreateOrderItemsView();
            CreateDeliveryOrderInfoPanelView();
            
            UpdateButtonState();
            ViewModel.UpdateState += UpdateButtonState;
        }
        
        private void ConfigureButtonActions()
        {
            menubuttonActions.MenuAllocation = ButtonMenuAllocation.Top;
            menubuttonActions.MenuAlignment = ButtonMenuAlignment.Right;
            Menu menu = new Menu();

            menuItemCloseOrder = new MenuItem("Закрыть без доставки");
            menuItemCloseOrder.Activated += ViewModel.OnButtonCloseOrderClicked;
            menu.Add(menuItemCloseOrder);

            menuItemReturnToAccepted = new MenuItem("Вернуть в Принят");
            menuItemReturnToAccepted.Activated += ViewModel.OnButtonReturnToAcceptedClicked;
            menu.Add(menuItemReturnToAccepted);

            menubuttonActions.Menu = menu;
            menubuttonActions.LabelXAlign = 0.5f;
            menu.ShowAll();
        }

        private void CreateOrderItemsView()
        {
            var orderItemsView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                .Resolve(ViewModel.OrderItemsViewModel);

            hboxOrderItems.Add(orderItemsView);
            orderItemsView.Show();
        }

        private void CreateDeliveryOrderInfoPanelView()
        {
            var orderInfoPanelView = ViewModel.AutofacScope.Resolve<IGtkViewResolver>()
                .Resolve(ViewModel.ClosingDocOrderInfoPanelViewModel);

            orderInfoExpandedPanelView.ViewModel = ViewModel.ExpandedPanelViewModel;
            orderInfoExpandedPanelView.AddPanel(orderInfoPanelView);
        }
        
        private void UpdateButtonState()
        {
            menuItemCloseOrder.Sensitive = ViewModel.CanCloseOrder;
            menuItemReturnToAccepted.Sensitive = ViewModel.CanReturnOrderToAccepted;
        }
        
        public override void Destroy()
        {
            ViewModel.UpdateState -= UpdateButtonState;
            base.Destroy();
        }
    }
}
