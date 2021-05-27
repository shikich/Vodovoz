using System;
using Autofac;
using Autofac.Core;
using QS.Navigation;
using QS.Services;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.Factories;
using Vodovoz.ViewModels.Dialogs.Orders;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public abstract class OrderInfoViewModelBase : UoWWidgetViewModelBase, IAutofacScopeHolder
    {
        private readonly INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory;
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
                        new TypedParameter(typeof(ICommonServices), AutofacScope.Resolve<ICommonServices>()),
                        new TypedParameter(typeof(INomenclatureRepository), AutofacScope.Resolve<INomenclatureRepository>()),
                        new TypedParameter(
                            typeof(IRentPackagesJournalsViewModelsFactory), AutofacScope.Resolve<IRentPackagesJournalsViewModelsFactory>()),
                        new TypedParameter(
                            typeof(INonSerialEquipmentsForRentJournalViewModelFactory),
                            AutofacScope.Resolve<INonSerialEquipmentsForRentJournalViewModelFactory>()),
                        new TypedParameter(typeof(ICurrentUserSettings), AutofacScope.Resolve<ICurrentUserSettings>()),
                        new TypedParameter(typeof(OrderBase), Order),
                        new TypedParameter(typeof(OrderInfoExpandedPanelViewModel), ExpandedPanelViewModel),
                        new TypedParameter(typeof(INomenclaturesJournalViewModelFactory),
                            nomenclaturesJournalViewModelFactory)
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
            OrderInfoExpandedPanelViewModel expandedPanelViewModel,
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory)
        {
            Order = order;
            ExpandedPanelViewModel = expandedPanelViewModel;
            this.nomenclaturesJournalViewModelFactory =
                nomenclaturesJournalViewModelFactory ??
                throw new ArgumentNullException(nameof(nomenclaturesJournalViewModelFactory));
        }
    }
}