using Autofac;
using Gtk;
using QS.Views.GtkUI;
using QS.Views.Resolve;
using QSWidgetLib;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.Views.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class SelfDeliveryOrderInfoView : WidgetViewBase<SelfDeliveryOrderInfoViewModel>
    {
        private MenuItem menuItemSendForLoadingSelfDelivery;
        private MenuItem menuItemAcceptPaymentSelfDelivery;
        
        public SelfDeliveryOrderInfoView(SelfDeliveryOrderInfoViewModel viewModel) : base (viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            ConfigureButtonActions();
            CreateOrderItemsView();
            CreateSelfDeliveryOrderInfoPanelView();
            
            UpdateButtonState();
            ViewModel.UpdateState += UpdateButtonState;
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
        
        private void ConfigureButtonActions()
        {
            menubuttonActions.MenuAllocation = ButtonMenuAllocation.Top;
            menubuttonActions.MenuAlignment = ButtonMenuAlignment.Right;
            Menu menu = new Menu();

            menuItemSendForLoadingSelfDelivery = new MenuItem("Самовывоз на погрузку");
            menuItemSendForLoadingSelfDelivery.Activated += ViewModel.OnButtonSendForLoadingSelfDeliveryClicked;
            menu.Add(menuItemSendForLoadingSelfDelivery);

            menuItemAcceptPaymentSelfDelivery = new MenuItem("Принять оплату самовывоза");
            menuItemAcceptPaymentSelfDelivery.Activated += ViewModel.OnButtonAcceptPaymentSelfDeliveryClicked;
            menu.Add(menuItemAcceptPaymentSelfDelivery);

            menubuttonActions.Menu = menu;
            menubuttonActions.LabelXAlign = 0.5f;
            menu.ShowAll();
        }

        private void UpdateButtonState()
        {
            menuItemSendForLoadingSelfDelivery.Sensitive = ViewModel.CanSendForLoadingSelfDelivery;
            menuItemAcceptPaymentSelfDelivery.Sensitive = ViewModel.CanAcceptPaymentSelfDelivery;
        }

        public override void Destroy()
        {
            ViewModel.UpdateState -= UpdateButtonState;
            base.Destroy();
        }
    }
}
