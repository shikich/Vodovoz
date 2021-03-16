using Autofac;
using Autofac.Core;
using Vodovoz.Domain.Orders;
using Vodovoz.Factories;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class DeliveryOrderInfoViewModel : OrderInfoViewModelBase
    {
        public DeliveryOrder DeliveryOrder => Order as DeliveryOrder;

        private DeliveryOrderInfoPanelViewModel deliveryOrderInfoPanelViewModel;
        public DeliveryOrderInfoPanelViewModel DeliveryOrderInfoPanelViewModel
        {
            get
            {
                if (deliveryOrderInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(DeliveryOrder), DeliveryOrder),
                    };
                    deliveryOrderInfoPanelViewModel = AutofacScope.Resolve<DeliveryOrderInfoPanelViewModel>(parameters);
                    deliveryOrderInfoPanelViewModel.AutofacScope = AutofacScope;
                    deliveryOrderInfoPanelViewModel.ParentTab = ParentTab;
                }

                return deliveryOrderInfoPanelViewModel;
            }
        }

        public DeliveryOrderInfoViewModel(
            DeliveryOrder deliveryOrder,
            OrderInfoExpandedPanelViewModel expandedPanelViewModel,
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory) 
            : base(deliveryOrder, expandedPanelViewModel, nomenclaturesJournalViewModelFactory)
        {
        }
    }
}
