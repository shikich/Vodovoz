using Autofac;
using Autofac.Core;
using QS.Navigation;
using QS.Services;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public abstract class OrderInfoViewModelBase : UoWWidgetViewModelBase, IAutofacScopeHolder
    {
        protected OrderBase Order { get; set; }
        public ILifetimeScope AutofacScope { get; set; }

        private OrderItemsViewModel orderItemsViewModel;
        public virtual OrderItemsViewModel OrderItemsViewModel
        {
            get
            {
                if (orderItemsViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(IInteractiveService), AutofacScope.Resolve<IInteractiveService>()),
                        new TypedParameter(typeof(ICurrentUserSettings), AutofacScope.Resolve<ICurrentUserSettings>()),
                        new TypedParameter(typeof(OrderBase), Order),
                        new TypedParameter(typeof(OrderInfoExpandedPanelViewModel), ExpandedPanelViewModel)
                    };
                    
                    orderItemsViewModel = AutofacScope.Resolve<OrderItemsViewModel>(parameters);
                }

                return orderItemsViewModel;
            }
        }
        
        public DialogViewModelBase ParentTab { get; set; }
        public OrderInfoExpandedPanelViewModel ExpandedPanelViewModel { get; }

        protected OrderInfoViewModelBase(
            OrderBase order,
            OrderInfoExpandedPanelViewModel expandedPanelViewModel)
        {
            Order = order;
            ExpandedPanelViewModel = expandedPanelViewModel;
        }
    }
}