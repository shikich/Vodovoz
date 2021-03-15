using Autofac;
using Autofac.Core;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class SelfDeliveryOrderInfoViewModel : OrderInfoViewModelBase
    {
        public SelfDeliveryOrder SelfDeliveryOrder => Order as SelfDeliveryOrder;
        
        private SelfDeliveryOrderInfoPanelViewModel selfDeliveryOrderInfoPanelViewModel;
        public SelfDeliveryOrderInfoPanelViewModel SelfDeliveryOrderInfoPanelViewModel
        {
            get
            {
                if (selfDeliveryOrderInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(SelfDeliveryOrder), SelfDeliveryOrder),
                    };
                    selfDeliveryOrderInfoPanelViewModel = AutofacScope.Resolve<SelfDeliveryOrderInfoPanelViewModel>(parameters);
                    selfDeliveryOrderInfoPanelViewModel.AutofacScope = AutofacScope;
                    selfDeliveryOrderInfoPanelViewModel.ParentTab = ParentTab;
                }

                return selfDeliveryOrderInfoPanelViewModel;
            }
        }

        public SelfDeliveryOrderInfoViewModel(
            SelfDeliveryOrder selfDeliveryOrder,
            OrderInfoExpandedPanelViewModel expandedPanelViewModel) : base(selfDeliveryOrder, expandedPanelViewModel)
        {
            
        }
    }
}
