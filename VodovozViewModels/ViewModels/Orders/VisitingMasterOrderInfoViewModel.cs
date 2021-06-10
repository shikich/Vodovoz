using Autofac;
using Autofac.Core;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Factories;
using Vodovoz.Services;
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
                        new TypedParameter(typeof(ICommonServices), CommonServices),
                        new TypedParameter(typeof(IOrderRepository), AutofacScope.Resolve<IOrderRepository>()),
                        new TypedParameter(typeof(IOrderParametersProvider), AutofacScope.Resolve<IOrderParametersProvider>())
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
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory,
            ICommonServices commonServices) 
            : base(visitingMasterOrder, expandedPanelViewModel, nomenclaturesJournalViewModelFactory, commonServices)
        {

        }
    }
}
