using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class SelfDeliveryInfoViewModel : OrderInfoViewModelBase
    {
        public SelfDeliveryOrder Order { get; set; }

        public SelfDeliveryInfoViewModel(
        OrderInfoExpandedPanelViewModel expandedPanelViewModel) : base(expandedPanelViewModel)
        {

        }
    }
}
