using QS.Navigation;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class OrderFrom1cMainViewModel : OrderMainViewModelBase
    {
        public OrderFrom1c OrderFrom1C => Order as OrderFrom1c;
        
        public OrderFrom1cMainViewModel(
            OrderFrom1c orderFrom1C,
            OrderFrom1cInfoViewModel orderFrom1CInfoViewModel,
            ITdiCompatibilityNavigation tdiCompatibilityNavigation) 
            : base (orderFrom1C, orderFrom1CInfoViewModel, tdiCompatibilityNavigation)
        {
            
        }
    }
}