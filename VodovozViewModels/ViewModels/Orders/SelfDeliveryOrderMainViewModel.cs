using QS.Navigation;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class SelfDeliveryOrderMainViewModel : OrderMainViewModel<SelfDeliveryOrder>
    {
        public SelfDeliveryInfoViewModel SelfDeliveryInfoViewModel { get; set; }
        
        public SelfDeliveryOrderMainViewModel(
            SelfDeliveryInfoViewModel selfDeliveryInfoViewModel,
            ITdiCompatibilityNavigation tdiCompatibilityNavigation) : base (tdiCompatibilityNavigation)
        {
            SelfDeliveryInfoViewModel = selfDeliveryInfoViewModel;
            SelfDeliveryInfoViewModel.Order = Entity;
        }
    }
}