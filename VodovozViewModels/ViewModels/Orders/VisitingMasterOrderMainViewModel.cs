using QS.Navigation;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class VisitingMasterOrderMainViewModel : OrderMainViewModelBase
    {
        public VisitingMasterOrder VisitingMasterOrder => Order as VisitingMasterOrder;
        
        public VisitingMasterOrderMainViewModel(
            VisitingMasterOrder visitingMasterOrder,
            VisitingMasterOrderInfoViewModel visitingMasterOrderInfoViewModel,
            ITdiCompatibilityNavigation tdiCompatibilityNavigation) 
            : base (visitingMasterOrder, visitingMasterOrderInfoViewModel, tdiCompatibilityNavigation)
        {
            
        }
    }
}