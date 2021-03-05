using QS.Navigation;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class SelfDeliveryOrderMainViewModel : OrderMainViewModelBase
    {
        //public override OrderInfoViewModelBase OrderInfoViewModelBase => selfDeliveryInfoViewModel;
        //private readonly SelfDeliveryOrderInfoViewModel selfDeliveryInfoViewModel;
        
        public SelfDeliveryOrderMainViewModel(
            SelfDeliveryOrderInfoViewModel selfDeliveryInfoViewModel,
            ITdiCompatibilityNavigation tdiCompatibilityNavigation) 
            : base (selfDeliveryInfoViewModel, tdiCompatibilityNavigation)
        {
            //this.selfDeliveryInfoViewModel = selfDeliveryInfoViewModel;
            Order = new SelfDeliveryOrder();
            selfDeliveryInfoViewModel.Order = Order as SelfDeliveryOrder;
        }
    }
}