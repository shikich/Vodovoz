using System;
using System.Linq;
using QS.Commands;
using QS.Dialog;
using QS.Project.Dialogs;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.FilterViewModels.Goods;
using Vodovoz.ViewModelBased;
using Vodovoz.ViewModels.Journals.JournalViewModels.Goods;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderMovementItemsViewModel : UoWWidgetViewModelBase
    {
        private readonly IInteractiveService interactiveService;
        public OrderBase Order { get; set; }
        public NomenclaturesJournalViewModel MovementItemsJournalViewModel { get; private set; }

        public event Action UpdateVisibilityMovementItemsJournal;

        private DelegateCommand addMovementItemToClientCommand;
        public DelegateCommand AddMovementItemToClientCommand => addMovementItemToClientCommand ?? (
            addMovementItemToClientCommand = new DelegateCommand(
                () =>
                {
                    if (Order.Counterparty == null)
                    {
                        interactiveService.ShowMessage(
                            ImportanceLevel.Warning,"Для добавления товара на продажу должен быть выбран клиент.");
                        return;
                    }

                    var nomenclatureFilter = new NomenclatureFilterViewModel();
                    nomenclatureFilter.SetAndRefilterAtOnce(
                        x => x.AvailableCategories = Nomenclature.GetCategoriesForGoods(),
                        x => x.SelectCategory = NomenclatureCategory.equipment,
                        x => x.SelectSaleCategory = SaleCategory.notForSale
                    );
                    PermissionControlledRepresentationJournal SelectDialog = 
                        new PermissionControlledRepresentationJournal(new ViewModel.NomenclatureForSaleVM(nomenclatureFilter))
                        {
                            Mode = JournalSelectMode.Single,
                            ShowFilter = true
                        };
                    SelectDialog.CustomTabName("Оборудование к клиенту");
                    SelectDialog.ObjectSelected += NomenclatureToClient;
                    MyTab.TabParent.AddSlaveTab(MyTab, SelectDialog);
                },
                () => true
            )
        );

        public OrderMovementItemsViewModel(
            IInteractiveService interactiveService,
            NomenclaturesJournalViewModel movementItemsJournalViewModel)
        {
            this.interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
            ConfigureEquipmentsJournalViewModel(movementItemsJournalViewModel);
        }
        
        private void ConfigureEquipmentsJournalViewModel(NomenclaturesJournalViewModel movementItemsJournalViewModel)
        {
            MovementItemsJournalViewModel = movementItemsJournalViewModel;
            MovementItemsJournalViewModel.OnEntitySelectedResult += (s, ea) =>
            {
                var selectedNode = ea.SelectedNodes.FirstOrDefault();
                
                if (selectedNode == null)
                    return;
                
                //AddDepositEquipment(UoW.Session.Get<Nomenclature>(selectedNode.Id));
                UpdateVisibilityMovementItemsJournal?.Invoke();
            };
        }
        
        public virtual bool HideItemFromDirectionReasonComboInEquipment(OrderEquipment node, DirectionReason item)
        {
            switch (item)
            {
                case DirectionReason.None:
                    return true;
                case DirectionReason.Rent:
                    return node.Direction == Direction.Deliver;
                default:
                    return false;
            }
        }

        void NomenclatureToClient(object sender, JournalObjectSelectedEventArgs e)
        {
            var selectedId = e.GetSelectedIds().FirstOrDefault();
            if (selectedId == 0)
            {
                return;
            }
            AddNomenclatureToClient(UoW.Session.Get<Nomenclature>(selectedId));
        }

        void AddNomenclatureToClient(Nomenclature nomenclature)
        {
            Order.AddEquipmentNomenclatureToClient(nomenclature, UoW);
        }
    }
}
