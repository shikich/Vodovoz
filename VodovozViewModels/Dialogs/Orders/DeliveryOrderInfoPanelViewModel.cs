using Autofac;
using QS.Navigation;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class DeliveryOrderInfoPanelViewModel : UoWWidgetViewModelBase, IAutofacScopeHolder
    {
        public DeliveryOrder Order { get; set; }
        public DialogViewModelBase ParentTab { get; set; }
        public ILifetimeScope AutofacScope { get; set; }

        public DeliveryOrderInfoPanelViewModel(DeliveryOrder order)
        {
            Order = order;
        }
    }
}
