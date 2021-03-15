using QS.Navigation;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class SelfDeliveryOrderMainViewModel : OrderMainViewModelBase
    {
        public SelfDeliveryOrder SelfDeliveryOrder => Order as SelfDeliveryOrder;
        
        public SelfDeliveryOrderMainViewModel(
            SelfDeliveryOrder selfDeliveryOrder,
            SelfDeliveryOrderInfoViewModel selfDeliveryInfoViewModel,
            ITdiCompatibilityNavigation tdiCompatibilityNavigation) 
            : base (selfDeliveryOrder, selfDeliveryInfoViewModel, tdiCompatibilityNavigation)
        {
            
        }
    }
}