using Autofac;
using Autofac.Core;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class ClosingDocOrderInfoViewModel : OrderInfoViewModelBase
    {
        public ClosingDocOrder ClosingDocOrder => Order as ClosingDocOrder;

        private ClosingDocOrderInfoPanelViewModel closingDocOrderInfoPanelViewModel;
        public ClosingDocOrderInfoPanelViewModel ClosingDocOrderInfoPanelViewModel
        {
            get
            {
                if (closingDocOrderInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(ClosingDocOrder), ClosingDocOrder),
                    };
                    closingDocOrderInfoPanelViewModel = AutofacScope.Resolve<ClosingDocOrderInfoPanelViewModel>(parameters);
                    closingDocOrderInfoPanelViewModel.AutofacScope = AutofacScope;
                    closingDocOrderInfoPanelViewModel.ParentTab = ParentTab;
                }

                return closingDocOrderInfoPanelViewModel;
            }
        }

        public ClosingDocOrderInfoViewModel(
            ClosingDocOrder closingDocOrder,
            OrderInfoExpandedPanelViewModel expandedPanelViewModel) : base(closingDocOrder, expandedPanelViewModel)
        {
        }
    }
}
