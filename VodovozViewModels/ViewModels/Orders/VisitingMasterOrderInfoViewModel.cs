using Autofac;
using Autofac.Core;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class VisitingMasterOrderInfoViewModel : OrderInfoViewModelBase
    {
        public VisitingMasterOrder Order { get; set; }
        
        private VisitingMasterOrderInfoPanelViewModel visitingMasterOrderInfoPanelViewModel;
        public VisitingMasterOrderInfoPanelViewModel VisitingMasterOrderInfoPanelViewModel
        {
            get
            {
                if (visitingMasterOrderInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(VisitingMasterOrder), Order),
                    };
                    visitingMasterOrderInfoPanelViewModel = AutofacScope.Resolve<VisitingMasterOrderInfoPanelViewModel>(parameters);
                    visitingMasterOrderInfoPanelViewModel.AutofacScope = AutofacScope;
                    visitingMasterOrderInfoPanelViewModel.ParentTab = ParentTab;
                }

                return visitingMasterOrderInfoPanelViewModel;
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

        public VisitingMasterOrderInfoViewModel(
            OrderInfoExpandedPanelViewModel expandedPanelViewModel) : base(expandedPanelViewModel)
        {

        }
    }
}
