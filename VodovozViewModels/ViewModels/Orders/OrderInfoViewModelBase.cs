using Autofac;
using QS.Navigation;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public abstract class OrderInfoViewModelBase : UoWWidgetViewModelBase, IAutofacScopeHolder
    {
        public ILifetimeScope AutofacScope { get; set; }
        public virtual OrderItemsViewModel OrderItemsViewModel { get; }
        public DialogViewModelBase ParentTab { get; set; }
        public OrderInfoExpandedPanelViewModel ExpandedPanelViewModel { get; set; }

        protected OrderInfoViewModelBase(
            OrderInfoExpandedPanelViewModel expandedPanelViewModel)
        {
            ExpandedPanelViewModel = expandedPanelViewModel;
        }
    }
}