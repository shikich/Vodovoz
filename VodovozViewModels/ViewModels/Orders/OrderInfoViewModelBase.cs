using QS.ViewModels;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class OrderInfoViewModelBase : UoWWidgetViewModelBase
    {
        public OrderSalesItemsViewModel OrderSalesItemsViewModel { get; set; }
        public OrderInfoExpandedPanelViewModel ExpandedPanelViewModel { get; set; }
        //public NomenclatureJournalViewModel NomenclatureJournalViewModel { get; set; }

        protected OrderInfoViewModelBase(OrderInfoExpandedPanelViewModel expandedPanelViewModel)
        {
            ExpandedPanelViewModel = expandedPanelViewModel;
        }
    }
}