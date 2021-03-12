using Autofac;
using Autofac.Core;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class SelfDeliveryOrderInfoViewModel : OrderInfoViewModelBase
    {
        public SelfDeliveryOrder Order { get; set; }
        
        private SelfDeliveryOrderInfoPanelViewModel selfDeliveryOrderInfoPanelViewModel;
        public SelfDeliveryOrderInfoPanelViewModel SelfDeliveryOrderInfoPanelViewModel
        {
            get
            {
                if (selfDeliveryOrderInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(SelfDeliveryOrder), Order),
                    };
                    selfDeliveryOrderInfoPanelViewModel = AutofacScope.Resolve<SelfDeliveryOrderInfoPanelViewModel>(parameters);
                    selfDeliveryOrderInfoPanelViewModel.AutofacScope = AutofacScope;
                    selfDeliveryOrderInfoPanelViewModel.ParentTab = ParentTab;
                }

                return selfDeliveryOrderInfoPanelViewModel;
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

        public SelfDeliveryOrderInfoViewModel(
            OrderInfoExpandedPanelViewModel expandedPanelViewModel) : base(expandedPanelViewModel)
        {

        }
    }
}
