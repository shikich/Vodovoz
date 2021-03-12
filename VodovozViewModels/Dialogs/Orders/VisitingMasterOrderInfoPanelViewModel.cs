using Autofac;
using QS.Navigation;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class VisitingMasterOrderInfoPanelViewModel : UoWWidgetViewModelBase, IAutofacScopeHolder
    {
        public VisitingMasterOrder Order { get; set; }
        public DialogViewModelBase ParentTab { get; set; }
        public ILifetimeScope AutofacScope { get; set; }

        public VisitingMasterOrderInfoPanelViewModel(VisitingMasterOrder order)
        {
            Order = order;
        }
    }
}
