using Autofac;
using Autofac.Core;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Factories;
using Vodovoz.Services;
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
                        new TypedParameter(typeof(ICommonServices), CommonServices),
                        new TypedParameter(typeof(IOrderRepository), AutofacScope.Resolve<IOrderRepository>()),
                        new TypedParameter(typeof(IOrderParametersProvider), AutofacScope.Resolve<IOrderParametersProvider>())
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
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory,
            ICommonServices commonServices) 
            : base(deliveryOrder, expandedPanelViewModel, nomenclaturesJournalViewModelFactory, commonServices)
        {
        }
    }
}
