using System;
using System.Linq;
using QS.Commands;
using QS.Project.Journal;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Journals.JournalViewModels.Goods;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderMovementItemsViewModel : UoWWidgetViewModelBase
    {
        private readonly IInteractiveService interactiveService;
        public OrderBase Order { get; set; }
        public NomenclaturesJournalViewModel MovementItemsToClientJournalViewModel { get; }
        public NomenclaturesJournalViewModel MovementItemsFromClientJournalViewModel { get; }

        public event Action UpdateVisibilityMovementItemsToClientJournal;
        public event Action UpdateVisibilityMovementItemsFromClientJournal;

        #region Команды
        
        private DelegateCommand addMovementItemToClientCommand;
        public DelegateCommand AddMovementItemToClientCommand => addMovementItemToClientCommand ?? (
            addMovementItemToClientCommand = new DelegateCommand(
                () =>
                {
                    /*if (Order.Counterparty == null)
                    {
                        interactiveService.ShowMessage(
                            ImportanceLevel.Warning,"Для добавления товара на продажу должен быть выбран клиент.");
                        return;
                    }*/
                    
                    UnsubscribeAll();
                    MovementItemsToClientJournalViewModel.OnEntitySelectedResultWithoutClose +=
                        MovementItemsToClientJournalVMOnEntitySelected;

                    UpdateVisibilityMovementItemsToClientJournal?.Invoke();
                },
                () => true
            )
        );
        
        private DelegateCommand addMovementItemFromClientCommand;
        public DelegateCommand AddMovementItemFromClientCommand => addMovementItemFromClientCommand ?? (
            addMovementItemFromClientCommand = new DelegateCommand(
                () =>
                {
                    /*if (Order.Counterparty == null)
                    {
                        interactiveService.ShowMessage(
                            ImportanceLevel.Warning,"Для добавления товара на продажу должен быть выбран клиент.");
                        return;
                    }*/

                    UnsubscribeAll();
                    MovementItemsFromClientJournalViewModel.OnEntitySelectedResultWithoutClose +=
                        MovementItemsFromClientJournalVMOnEntitySelected;

                    UpdateVisibilityMovementItemsFromClientJournal?.Invoke();
                },
                () => true
            )
        );
        
        #endregion
        
        public OrderMovementItemsViewModel(
            IInteractiveService interactiveService,
            OrderBase order,
            NomenclaturesJournalViewModel movementItemsToClientJournalViewModel,
            NomenclaturesJournalViewModel movementItemsFromClientJournalViewModel)
        {
            this.interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
            Order = order;
            MovementItemsToClientJournalViewModel = movementItemsToClientJournalViewModel;
            MovementItemsFromClientJournalViewModel = movementItemsFromClientJournalViewModel;
        }
        
        private void MovementItemsToClientJournalVMOnEntitySelected(Object sender, JournalSelectedNodesEventArgs ea)
        {
            var selectedNode = ea.SelectedNodes.FirstOrDefault();

            if (selectedNode == null)
                return;

            //AddMovementItem(UoW.Session.Get<Nomenclature>(selectedNode.Id), true);
            UpdateVisibilityMovementItemsToClientJournal?.Invoke();
        }

        private void MovementItemsFromClientJournalVMOnEntitySelected(Object sender, JournalSelectedNodesEventArgs ea)
        {
            var selectedNode = ea.SelectedNodes.FirstOrDefault();

            if (selectedNode == null)
                return;

            //AddMovementItem(UoW.Session.Get<Nomenclature>(selectedNode.Id), false);
            UpdateVisibilityMovementItemsFromClientJournal?.Invoke();
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
        /*
        void AddNomenclatureToClient(Nomenclature nomenclature)
        {
            Order.AddEquipmentNomenclatureToClient(nomenclature, UoW);
        }*/
        
        void AddMovementItem(Nomenclature nomenclature, bool toClient)
        {
            //Order.AddEquipmentNomenclatureFromClient(nomenclature, ViewModel.UoW);
        }

        private void UnsubscribeAll()
        {
            MovementItemsToClientJournalViewModel.OnEntitySelectedResultWithoutClose -=
                MovementItemsToClientJournalVMOnEntitySelected;
            MovementItemsFromClientJournalViewModel.OnEntitySelectedResultWithoutClose -=
                MovementItemsFromClientJournalVMOnEntitySelected;
        }
    }
}
