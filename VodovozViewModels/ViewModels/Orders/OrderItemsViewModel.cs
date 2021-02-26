using QS.Commands;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Project.Journal;
using QS.Project.Services;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.FilterViewModels.Goods;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class OrderItemsViewModel : UoWWidgetViewModelBase
    {
        private bool isMovementItemsVisible;
        private readonly IInteractiveService interactiveService;
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

        #region Commands

        private DelegateCommand addSalesItemCommand;
        public DelegateCommand AddSalesItemCommand => addSalesItemCommand ?? (
            addSalesItemCommand = new DelegateCommand(
                () =>
                {
                    if(!CanAddNomenclaturesToOrder())
                        return;

                    var defaultCategory = NomenclatureCategory.water;
                    if(CurrentUserSettings.Settings.DefaultSaleCategory.HasValue)
                        defaultCategory = CurrentUserSettings.Settings.DefaultSaleCategory.Value;

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
                        NomenclatureSelectorFactory,
                        CounterpartySelectorFactory,
                        NomenclatureRepository,
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
                        TryAddNomenclature(UoWGeneric.Session.Get<Nomenclature>(selectedNode.Id));
                    };
                    this.TabParent.AddSlaveTab(this, journalViewModel);
                },
                () => true
            )    
        );

        #endregion Commands

        public OrderItemsViewModel()
        {
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
    }
}
