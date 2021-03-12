using Autofac;
using Autofac.Core;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class ClosingDocOrderInfoViewModel : OrderInfoViewModelBase
    {
        public ClosingDocOrder Order { get; set; }

        private ClosingDocOrderInfoPanelViewModel closingDocOrderInfoPanelViewModel;
        public ClosingDocOrderInfoPanelViewModel ClosingDocOrderInfoPanelViewModel
        {
            get
            {
                if (closingDocOrderInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(ClosingDocOrder), Order),
                    };
                    closingDocOrderInfoPanelViewModel = AutofacScope.Resolve<ClosingDocOrderInfoPanelViewModel>(parameters);
                    closingDocOrderInfoPanelViewModel.AutofacScope = AutofacScope;
                    closingDocOrderInfoPanelViewModel.ParentTab = ParentTab;
                }

                return closingDocOrderInfoPanelViewModel;
            }
        }

        private OrderItemsViewModel orderItemsViewModel;
        public override OrderItemsViewModel OrderItemsViewModel
        {
            get
            {
                if (orderItemsViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(IInteractiveService), AutofacScope.Resolve<IInteractiveService>()),
                        new TypedParameter(typeof(OrderBase), Order),
                        new TypedParameter(typeof(OrderInfoExpandedPanelViewModel), ExpandedPanelViewModel)
                    };
                    orderItemsViewModel = AutofacScope.Resolve<OrderItemsViewModel>(parameters);
                    orderItemsViewModel.AutofacScope = AutofacScope;
                }

                return orderItemsViewModel;
            }
        }

        public ClosingDocOrderInfoViewModel(
            OrderInfoExpandedPanelViewModel expandedPanelViewModel) : base(expandedPanelViewModel)
        {
        }
    }
}
