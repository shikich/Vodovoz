using System;
using System.Linq;
using Autofac;
using Autofac.Core;
using QS.Commands;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Project.Journal;
using QS.Project.Journal.EntitySelector;
using QS.Project.Services;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.FilterViewModels.Goods;
using Vodovoz.Infrastructure.Services;
using Vodovoz.TempAdapters;
using Vodovoz.ViewModels.Dialogs.Orders;
using Vodovoz.ViewModels.Journals.JournalViewModels.Goods;
using Vodovoz.ViewModels.TempAdapters;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class OrderItemsViewModel : UoWWidgetViewModelBase, IAutofacScopeHolder
    {
        private bool isMovementItemsVisible;
        private OrderBase Order { get; set; }
        
        public bool IsMovementItemsVisible
        {
            get => isMovementItemsVisible;
            set => SetField(ref isMovementItemsVisible, value);
        }
        
        private bool isDepositsReturnsVisible;
        public bool IsDepositsReturnsVisible
        {
            get => isDepositsReturnsVisible;
            set => SetField(ref isDepositsReturnsVisible, value);
        }
        
        public NomenclaturesJournalViewModel NomenclaturesJournalViewModel { get; set; }
        
        private OrderDepositReturnsItemsViewModel orderDepositReturnsItemsViewModel;
        public OrderDepositReturnsItemsViewModel OrderDepositReturnsItemsViewModel
        {
            get
            {
                if (orderDepositReturnsItemsViewModel == null)
                {
                    var filterViewModel = AutofacScope.Resolve<NomenclatureFilterViewModel>();
                    ConfigureDepositEquipmentsFilter(filterViewModel);
                    
                    var depositEquipmentsJournalVM = GetNomenclaturesJournalViewModel(filterViewModel);
                    
                    Parameter[] parameters =
                    {
                        new TypedParameter(typeof(NomenclaturesJournalViewModel), depositEquipmentsJournalVM),
                        new TypedParameter(typeof(OrderBase), Order)
                    };
                    orderDepositReturnsItemsViewModel = 
                        AutofacScope.Resolve<OrderDepositReturnsItemsViewModel>(parameters);
                }

                return orderDepositReturnsItemsViewModel;
            }
        }
        
        private OrderMovementItemsViewModel orderMovementItemsViewModel;
        public OrderMovementItemsViewModel OrderMovementItemsViewModel
        {
            get
            {
                if (orderMovementItemsViewModel == null)
                {
                    var filterViewModel = AutofacScope.Resolve<NomenclatureFilterViewModel>();
                    ConfigureMovementItemsFilter(filterViewModel);
                    
                    var movementItemsToClientJournalVM = GetNomenclaturesJournalViewModel(filterViewModel);
                    var movementItemsFromClientJournalVM = GetNomenclaturesJournalViewModel(filterViewModel);

                    Parameter[] parameters =
                    {
                        new TypedParameter(typeof(IInteractiveService), interactiveService),
                        new TypedParameter(typeof(NomenclaturesJournalViewModel), movementItemsToClientJournalVM),
                        new TypedParameter(typeof(NomenclaturesJournalViewModel), movementItemsFromClientJournalVM),
                        new TypedParameter(typeof(OrderBase), Order)
                    };
                    orderMovementItemsViewModel = AutofacScope.Resolve<OrderMovementItemsViewModel>(parameters);
                }

                return orderMovementItemsViewModel;
            }
        }

        public event Action UpdateVisibilityNomenclaturesForSaleJournal;

        #region Команды

        private DelegateCommand addSalesItemCommand;
        public DelegateCommand AddSalesItemCommand => addSalesItemCommand ?? (
            addSalesItemCommand = new DelegateCommand(
                () =>
                {
                    /*if(!CanAddNomenclaturesToOrder())
                        return;
                    */
                    if (NomenclaturesJournalViewModel == null)
                    {
                        var nomenclatureFilter = AutofacScope.Resolve<NomenclatureFilterViewModel>();
                        ConfigureNomenclatureOnSaleFilter(nomenclatureFilter);

                        var journalViewModel = GetNomenclaturesJournalViewModel(nomenclatureFilter);
                        journalViewModel.AdditionalJournalRestriction =
                            new NomenclaturesForOrderJournalRestriction(ServicesConfig.CommonServices);
                        journalViewModel.TabName = "Номенклатура на продажу";
                        journalViewModel.OnEntitySelectedResultWithoutClose += (s, ea) =>
                        {
                            var selectedNode = ea.SelectedNodes.FirstOrDefault();
                            if (selectedNode == null)
                                return;
                            //TryAddNomenclature(UoW.Session.Get<Nomenclature>(selectedNode.Id));
                            UpdateVisibilityNomenclaturesForSaleJournal?.Invoke();
                        };

                        NomenclaturesJournalViewModel = journalViewModel;
                    }

                    UpdateVisibilityNomenclaturesForSaleJournal?.Invoke();
                },
                () => true
            )
        );

        #endregion Commands
        
        private readonly IInteractiveService interactiveService;
        private readonly ICurrentUserSettings currentUserSettings;
        public OrderInfoExpandedPanelViewModel OrderInfoExpandedPanelViewModel { get; }
        
        public OrderItemsViewModel(
            IInteractiveService interactiveService,
            OrderBase order,
            OrderInfoExpandedPanelViewModel orderInfoExpandedPanelViewModel)
        {
            this.interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
            Order = order;
            OrderInfoExpandedPanelViewModel = orderInfoExpandedPanelViewModel;
        }
        
        bool CanAddNomenclaturesToOrder()
        {
            if(Order.Counterparty == null) {
                interactiveService.ShowMessage(
                    ImportanceLevel.Warning, 
                    "Для добавления товара на продажу должен быть выбран клиент.");
                
                return false;
            }

            if(Order.DeliveryPoint == null && Order.Type != OrderType.SelfDeliveryOrder) {
                interactiveService.ShowMessage(
                    ImportanceLevel.Warning,
                    "Для добавления товара на продажу должна быть выбрана точка доставки.");
                
                return false;
            }

            return true;
        }
        
        private void TryAddNomenclature(
            Nomenclature nomenclature, 
            decimal count = 0, 
            decimal discount = 0, 
            DiscountReason discountReason = null)
        {
            if(Order is OrderFrom1c)
                return;

            if(Order.OrderSalesItems.Any(x => !Nomenclature.GetCategoriesForMaster().Contains(x.Nomenclature.Category))
               && nomenclature.Category == NomenclatureCategory.master) {
                interactiveService.ShowMessage(
                    ImportanceLevel.Info, "В не сервисный заказ нельзя добавить сервисную услугу");
                return;
            }

            if(Order.OrderSalesItems.Any(x => x.Nomenclature.Category == NomenclatureCategory.master)
               && !Nomenclature.GetCategoriesForMaster().Contains(nomenclature.Category)) {
                interactiveService.ShowMessage(
                    ImportanceLevel.Info, "В сервисный заказ нельзя добавить не сервисную услугу");
                return;
            }
            
            if(nomenclature.ProductGroup != null)
                if(nomenclature.ProductGroup.IsOnlineStore && !ServicesConfig.CommonServices.CurrentPermissionService
                    .ValidatePresetPermission("can_add_online_store_nomenclatures_to_order")) {
                    interactiveService.ShowMessage(
                        ImportanceLevel.Warning,
                        "У вас недостаточно прав для добавления на продажу номенклатуры интернет магазина");
                    return;
                }
			
            AddNomenclature(nomenclature, count, discount, false, discountReason);
        }
        
        //TODO Реализовать метод
        public virtual void AddNomenclature(
            Nomenclature nomenclature, 
            decimal count = 0, 
            decimal discount = 0, 
            bool discountInMoney = false, 
            DiscountReason discountReason = null, 
            PromotionalSet proSet = null)
        {
            
        }

        public ILifetimeScope AutofacScope { get; set; }

        private NomenclaturesJournalViewModel GetNomenclaturesJournalViewModel(NomenclatureFilterViewModel filterViewModel)
        {
            var uowFactory = AutofacScope.Resolve<IUnitOfWorkFactory>();
            var comServices = AutofacScope.Resolve<ICommonServices>();
            var emplService = AutofacScope.Resolve<IEmployeeService>();
            var counterpartySelector = AutofacScope.Resolve<ICounterpartyJournalFactory>();
            var nomenclatureSelector = AutofacScope.Resolve<INomenclatureSelectorFactory>();
            var nomRepository = AutofacScope.Resolve<INomenclatureRepository>();
            var usrRepository = AutofacScope.Resolve<IUserRepository>();
            var nomSelectorFilter = AutofacScope.Resolve<NomenclatureFilterViewModel>();

            Parameter[] parameters =
            {
                new TypedParameter(typeof(NomenclatureFilterViewModel), filterViewModel),
                new TypedParameter(typeof(IUnitOfWorkFactory), uowFactory),
                new TypedParameter(typeof(ICommonServices), comServices),
                new TypedParameter(typeof(IEmployeeService), emplService),
                new TypedParameter(typeof(IEntityAutocompleteSelectorFactory),
                    counterpartySelector.CreateCounterpartyAutocompleteSelectorFactory()),
                new TypedParameter(
                    typeof(IEntityAutocompleteSelectorFactory),
                    nomenclatureSelector.CreateNomenclatureAutocompleteSelectorFactory(nomSelectorFilter)),
                new TypedParameter(typeof(INomenclatureRepository), nomRepository),
                new TypedParameter(typeof(IUserRepository), usrRepository),
            };

            var journalVM = AutofacScope.Resolve<NomenclaturesJournalViewModel>(parameters);
            journalVM.SelectionMode = JournalSelectionMode.Single;

            return journalVM;
        }

        #region Настройка фильтров журналов

        private void ConfigureNomenclatureOnSaleFilter(NomenclatureFilterViewModel filterViewModel)
        {
            var defaultCategory = NomenclatureCategory.water;
            
            /*if(currentUserSettings.GetUserSettings().DefaultSaleCategory.HasValue) {
                defaultCategory = currentUserSettings.GetUserSettings().DefaultSaleCategory.Value;
            }*/

            filterViewModel.SetAndRefilterAtOnce(
                x => x.AvailableCategories = Nomenclature.GetCategoriesForSaleToOrder(),
                x => x.SelectCategory = defaultCategory,
                x => x.SelectSaleCategory = SaleCategory.forSale,
                x => x.RestrictArchive = false
            );
        }
        
        private void ConfigureDepositEquipmentsFilter(NomenclatureFilterViewModel filterViewModel)
        {
            filterViewModel.SetAndRefilterAtOnce(
                x => x.SelectCategory = NomenclatureCategory.equipment,
                x => x.RestrictArchive = false
            );
        }
        
        private void ConfigureMovementItemsFilter(NomenclatureFilterViewModel filterViewModel)
        {
            filterViewModel.SetAndRefilterAtOnce(
                x => x.AvailableCategories = Nomenclature.GetCategoriesForGoods(),
                x => x.SelectCategory = NomenclatureCategory.equipment,
                x => x.SelectSaleCategory = SaleCategory.notForSale
            );
        }

        #endregion
    }
}
