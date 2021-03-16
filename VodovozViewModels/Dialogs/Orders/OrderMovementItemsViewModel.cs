﻿using System;
using System.Linq;
using QS.BusinessCommon.Domain;
using QS.Commands;
using QS.Dialog;
using QS.Project.Journal;
using QS.Services;
using QS.ViewModels;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Orders;
using Vodovoz.Factories;
using Vodovoz.ViewModels.Journals.JournalViewModels.Goods;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderMovementItemsViewModel : UoWWidgetViewModelBase
    {
        private readonly IInteractiveService interactiveService;
        private readonly INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory;
        private readonly INomenclatureFilterViewModelFactory nomenclatureFilterViewModelFactory;
        public OrderBase Order { get; set; }
        public NomenclaturesJournalViewModel MovementItemsToClientJournalViewModel { get; private set; }
        public NomenclaturesJournalViewModel MovementItemsFromClientJournalViewModel { get; private set; }

        public event Action<NomenclaturesJournalViewModel> UpdateActiveViewModel;
        public event Action RemoveActiveViewModel;

        #region Команды
        
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
                    
                    UpdateJournalSubscribes(MovementItemsToClientJournalViewModel);

                    if (MovementItemsToClientJournalViewModel is null)
                    {
                        MovementItemsToClientJournalViewModel =
                            nomenclaturesJournalViewModelFactory.CreateNomenclaturesJournalViewModel(
                                nomenclatureFilterViewModelFactory.CreateMovementItemsFilter()
                            );
                        
                        MovementItemsToClientJournalViewModel.OnEntitySelectedResultWithoutClose +=
                            MovementItemsToClientJournalVMOnEntitySelected;
                    }

                    UpdateActiveViewModel?.Invoke(MovementItemsToClientJournalViewModel);
                },
                () => true
            )
        );
        
        private DelegateCommand addMovementItemFromClientCommand;
        public DelegateCommand AddMovementItemFromClientCommand => addMovementItemFromClientCommand ?? (
            addMovementItemFromClientCommand = new DelegateCommand(
                () =>
                {
                    if (Order.Counterparty == null)
                    {
                        interactiveService.ShowMessage(
                            ImportanceLevel.Warning,"Для добавления товара на продажу должен быть выбран клиент.");
                        return;
                    }
                    
                    UpdateJournalSubscribes(MovementItemsFromClientJournalViewModel);

                    if (MovementItemsFromClientJournalViewModel is null)
                    {
                        MovementItemsFromClientJournalViewModel =
                            nomenclaturesJournalViewModelFactory.CreateNomenclaturesJournalViewModel(
                                nomenclatureFilterViewModelFactory.CreateMovementItemsFilter()
                            );
                        
                        MovementItemsFromClientJournalViewModel.OnEntitySelectedResultWithoutClose +=
                            MovementItemsFromClientJournalVMOnEntitySelected;
                    }

                    UpdateActiveViewModel?.Invoke(MovementItemsFromClientJournalViewModel);
                },
                () => true
            )
        );
        
        #endregion
        
        public OrderMovementItemsViewModel(
            IInteractiveService interactiveService,
            INomenclatureFilterViewModelFactory nomenclatureFilterViewModelFactory,
            OrderBase order,
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory)
        {
            this.interactiveService = interactiveService ?? throw new ArgumentNullException(nameof(interactiveService));
            this.nomenclaturesJournalViewModelFactory = 
                nomenclaturesJournalViewModelFactory ?? 
                    throw new ArgumentNullException(nameof(nomenclaturesJournalViewModelFactory));
            this.nomenclatureFilterViewModelFactory = nomenclatureFilterViewModelFactory;
            Order = order;
        }
        
        private void MovementItemsToClientJournalVMOnEntitySelected(Object sender, JournalSelectedNodesEventArgs ea)
        {
            var selectedNode = ea.SelectedNodes.FirstOrDefault();

            if (selectedNode == null)
                return;

            //AddNomenclatureToClient(UoW.Session.Get<Nomenclature>(selectedNode.Id), true);
            RemoveActiveViewModel?.Invoke();
        }

        private void MovementItemsFromClientJournalVMOnEntitySelected(Object sender, JournalSelectedNodesEventArgs ea)
        {
            var selectedNode = ea.SelectedNodes.FirstOrDefault();

            if (selectedNode == null)
                return;

            //AddNomenclatureFromClient(UoW.Session.Get<Nomenclature>(selectedNode.Id), false);
            RemoveActiveViewModel?.Invoke();
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
        
        void AddNomenclatureFromClient(Nomenclature nomenclature)
        {
            //Order.AddEquipmentNomenclatureFromClient(nomenclature, ViewModel.UoW);
        }

        /*private void UnsubscribeAll()
        {
            MovementItemsToClientJournalViewModel.OnEntitySelectedResultWithoutClose -=
                MovementItemsToClientJournalVMOnEntitySelected;
            MovementItemsFromClientJournalViewModel.OnEntitySelectedResultWithoutClose -=
                MovementItemsFromClientJournalVMOnEntitySelected;
        }*/

        private void UpdateJournalSubscribes(NomenclaturesJournalViewModel journalViewModel)
        {
            journalViewModel?.UpdateOnChanges(
                typeof(Nomenclature),
                typeof(MeasurementUnits),
                typeof(WarehouseMovementOperation),
                typeof(Order),
                typeof(OrderItem)
            );
        }
    }
}
