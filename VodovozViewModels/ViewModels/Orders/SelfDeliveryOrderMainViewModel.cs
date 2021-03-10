using QS.Navigation;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class SelfDeliveryOrderMainViewModel : OrderMainViewModelBase
    {
        public SelfDeliveryOrderMainViewModel(
            SelfDeliveryOrderInfoViewModel selfDeliveryInfoViewModel,
            ITdiCompatibilityNavigation tdiCompatibilityNavigation) 
            : base (selfDeliveryInfoViewModel, tdiCompatibilityNavigation)
        {
            Order = new SelfDeliveryOrder();
            selfDeliveryInfoViewModel.Order = Order as SelfDeliveryOrder;
        }
    }
}