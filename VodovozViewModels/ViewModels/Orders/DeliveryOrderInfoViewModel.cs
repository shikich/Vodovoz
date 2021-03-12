using Autofac;
using Autofac.Core;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class DeliveryOrderInfoViewModel : OrderInfoViewModelBase
    {
        public DeliveryOrder Order { get; set; }

        private DeliveryOrderInfoPanelViewModel deliveryOrderInfoPanelViewModel;
        public DeliveryOrderInfoPanelViewModel DeliveryOrderInfoPanelViewModel
        {
            get
            {
                if (deliveryOrderInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(DeliveryOrder), Order),
                    };
                    deliveryOrderInfoPanelViewModel = AutofacScope.Resolve<DeliveryOrderInfoPanelViewModel>(parameters);
                    deliveryOrderInfoPanelViewModel.AutofacScope = AutofacScope;
                    deliveryOrderInfoPanelViewModel.ParentTab = ParentTab;
                }

                return deliveryOrderInfoPanelViewModel;
            }
        }

        private OrderItemsViewModel orderItemsViewModel;
        public override OrderItemsViewModel OrderItemsViewModel
        {
            get
            {
                if (orderItemsViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(IInteractiveService), AutofacScope.Resolve<IInteractiveService>()),
                        new TypedParameter(typeof(OrderBase), Order),
                        new TypedParameter(typeof(OrderInfoExpandedPanelViewModel), ExpandedPanelViewModel)
                    };
                    orderItemsViewModel = AutofacScope.Resolve<OrderItemsViewModel>(parameters);
                    orderItemsViewModel.AutofacScope = AutofacScope;
                }

                return orderItemsViewModel;
            }
        }

        public DeliveryOrderInfoViewModel(
            OrderInfoExpandedPanelViewModel expandedPanelViewModel) : base(expandedPanelViewModel)
        {
        }
    }
}
