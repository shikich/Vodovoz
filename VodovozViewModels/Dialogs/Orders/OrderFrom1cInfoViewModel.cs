using Autofac;
using Autofac.Core;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Factories;
using Vodovoz.Services;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderFrom1cInfoViewModel : OrderInfoViewModelBase
    {
        public OrderFrom1c OrderFrom1c => Order as OrderFrom1c;

        private OrderFrom1cInfoPanelViewModel orderFrom1cInfoPanelViewModel;
        public OrderFrom1cInfoPanelViewModel OrderFrom1cInfoPanelViewModel
        {
            get
            {
                if (orderFrom1cInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(OrderFrom1c), OrderFrom1c),
                        new TypedParameter(typeof(ICommonServices), AutofacScope.Resolve<ICommonServices>()),
                        new TypedParameter(typeof(IOrderRepository), AutofacScope.Resolve<IOrderRepository>()),
                        new TypedParameter(typeof(IOrderParametersProvider), AutofacScope.Resolve<IOrderParametersProvider>())
                    };
                    orderFrom1cInfoPanelViewModel = AutofacScope.Resolve<OrderFrom1cInfoPanelViewModel>(parameters);
                    orderFrom1cInfoPanelViewModel.AutofacScope = AutofacScope;
                    orderFrom1cInfoPanelViewModel.ParentTab = ParentTab;
                }

                return orderFrom1cInfoPanelViewModel;
            }
        }

        public OrderFrom1cInfoViewModel(
            OrderFrom1c orderFrom1C,
            OrderInfoExpandedPanelViewModel expandedPanelViewModel,
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory)
            : base(orderFrom1C, expandedPanelViewModel, nomenclaturesJournalViewModelFactory)
        {

        }
    }
}
