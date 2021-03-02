using QS.Navigation;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class SelfDeliveryOrderMainViewModel : OrderMainViewModel<SelfDeliveryOrder>
    {
        public SelfDeliveryOrderInfoViewModel SelfDeliveryInfoViewModel { get; set; }
        
        public SelfDeliveryOrderMainViewModel(
            SelfDeliveryOrderInfoViewModel selfDeliveryInfoViewModel,
            ITdiCompatibilityNavigation tdiCompatibilityNavigation) : base (tdiCompatibilityNavigation)
        {
            SelfDeliveryInfoViewModel = selfDeliveryInfoViewModel;
            Entity = new SelfDeliveryOrder();
            SelfDeliveryInfoViewModel.Order = Entity;
        }
    }
}