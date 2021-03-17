using QS.Navigation;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class ClosingDocOrderMainViewModel : OrderMainViewModelBase
    {
        public ClosingDocOrder ClosingDocOrder => Order as ClosingDocOrder;
        
        public ClosingDocOrderMainViewModel(
            ClosingDocOrder closingDocOrder,
            ClosingDocOrderInfoViewModel closingDocOrderInfoViewModel,
            ITdiCompatibilityNavigation tdiCompatibilityNavigation) 
            : base (closingDocOrder, closingDocOrderInfoViewModel, tdiCompatibilityNavigation)
        {
            
        }
    }
}