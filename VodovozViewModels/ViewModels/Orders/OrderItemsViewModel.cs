using System;
using System.Linq;
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
using Vodovoz.ViewModels.Journals.JournalViewModels.Goods;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class OrderItemsViewModel : UoWWidgetViewModelBase
    {
        private bool isMovementItemsVisible;
        private OrderBase Order { get; set; }
        public IUnitOfWork UoW { get; set; }
        
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

        #region Commands

        private DelegateCommand addSalesItemCommand;
        public DelegateCommand AddSalesItemCommand => addSalesItemCommand ?? (
            addSalesItemCommand = new DelegateCommand(
                () =>
                {
                    if(!CanAddNomenclaturesToOrder())
                        return;

                    var defaultCategory = NomenclatureCategory.water;
                    /*if(CurrentUserSettings.Settings.DefaultSaleCategory.HasValue)
                        defaultCategory = CurrentUserSettings.Settings.DefaultSaleCategory.Value;*/

                    var nomenclatureFilter = new NomenclatureFilterViewModel();
                    nomenclatureFilter.SetAndRefilterAtOnce(
                        x => x.AvailableCategories = Nomenclature.GetCategoriesForSaleToOrder(),
                        x => x.SelectCategory = defaultCategory,
                        x => x.SelectSaleCategory = SaleCategory.forSale,
                        x => x.RestrictArchive = false
                    );

                    NomenclaturesJournalViewModel journalViewModel = new NomenclaturesJournalViewModel(
                        nomenclatureFilter,
                        UnitOfWorkFactory.GetDefaultFactory,
                        ServicesConfig.CommonServices,
                        employeeService,
                        nomenclatureSelectorFactory,
                        counterpartySelectorFactory,
                        nomenclatureRepository,
                        userRepository
                    ) {
                        SelectionMode = JournalSelectionMode.Single,
                    };
                    journalViewModel.AdditionalJournalRestriction = new NomenclaturesForOrderJournalRestriction(ServicesConfig.CommonServices);
                    journalViewModel.TabName = "Номенклатура на продажу";
                    journalViewModel.OnEntitySelectedResult += (s, ea) => {
                        var selectedNode = ea.SelectedNodes.FirstOrDefault();
                        if(selectedNode == null)
                            return;
                        TryAddNomenclature(UoW.Session.Get<Nomenclature>(selectedNode.Id));
                    };
                    //this.TabParent.AddSlaveTab(this, journalViewModel);
                },
                () => true
            )
        );

        #endregion Commands
        
        private readonly IEmployeeService employeeService;
        private readonly IUserRepository userRepository;
        private readonly INomenclatureRepository nomenclatureRepository;
        private readonly IInteractiveService interactiveService;
        private readonly IEntityAutocompleteSelectorFactory counterpartySelectorFactory;
        private readonly IEntityAutocompleteSelectorFactory nomenclatureSelectorFactory;
        private readonly ITdiCompatibilityNavigation tdiCompatibilityNavigation;

        public OrderItemsViewModel(
            IEmployeeService employeeService,
            IUserRepository userRepository,
            IInteractiveService interactiveService,
            INomenclatureRepository nomenclatureRepository,
            IEntityAutocompleteSelectorFactory counterpartySelectorFactory,
            IEntityAutocompleteSelectorFactory nomenclatureSelectorFactory)
        {
            this.employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            this.userRepository = userRepository ?? throw new ArgumentNullException(nameof(userRepository));
            this.interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
            this.nomenclatureRepository = 
                nomenclatureRepository ?? throw new ArgumentNullException(nameof(nomenclatureRepository));
            this.counterpartySelectorFactory = 
                counterpartySelectorFactory ?? throw new ArgumentNullException(nameof(counterpartySelectorFactory));
            this.nomenclatureSelectorFactory = 
                nomenclatureSelectorFactory ?? throw new ArgumentNullException(nameof(nomenclatureSelectorFactory));

        }
        
        public OrderItemsViewModel(){}
        
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
    }
}
