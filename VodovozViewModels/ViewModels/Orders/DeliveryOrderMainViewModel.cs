using QS.Navigation;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class DeliveryOrderMainViewModel : OrderMainViewModelBase
    {
        public DeliveryOrder DeliveryOrder => Order as DeliveryOrder;
        
        public DeliveryOrderMainViewModel(
            DeliveryOrder deliveryOrder,
            DeliveryOrderInfoViewModel deliveryOrderInfoViewModel,
            ITdiCompatibilityNavigation tdiCompatibilityNavigation) 
            : base (deliveryOrder, deliveryOrderInfoViewModel, tdiCompatibilityNavigation)
        {
            
        }
    }
}