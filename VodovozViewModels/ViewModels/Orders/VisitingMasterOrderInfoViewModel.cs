using Autofac;
using Autofac.Core;
using Vodovoz.Domain.Orders;
using Vodovoz.Factories;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class VisitingMasterOrderInfoViewModel : OrderInfoViewModelBase
    {
        public VisitingMasterOrder VisitingMasterOrder => Order as VisitingMasterOrder;
        
        private VisitingMasterOrderInfoPanelViewModel visitingMasterOrderInfoPanelViewModel;
        public VisitingMasterOrderInfoPanelViewModel VisitingMasterOrderInfoPanelViewModel
        {
            get
            {
                if (visitingMasterOrderInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(VisitingMasterOrder), VisitingMasterOrder),
                    };
                    visitingMasterOrderInfoPanelViewModel = AutofacScope.Resolve<VisitingMasterOrderInfoPanelViewModel>(parameters);
                    visitingMasterOrderInfoPanelViewModel.AutofacScope = AutofacScope;
                    visitingMasterOrderInfoPanelViewModel.ParentTab = ParentTab;
                }

                return visitingMasterOrderInfoPanelViewModel;
            }
        }

        public VisitingMasterOrderInfoViewModel(
            VisitingMasterOrder visitingMasterOrder,
            OrderInfoExpandedPanelViewModel expandedPanelViewModel,
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory) 
            : base(visitingMasterOrder, expandedPanelViewModel, nomenclaturesJournalViewModelFactory)
        {

        }
    }
}
