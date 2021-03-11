using System;
using System.Linq;
using QS.Commands;
using QS.ViewModels;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Orders;
using Vodovoz.ViewModels.Journals.JournalViewModels.Goods;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderDepositReturnsItemsViewModel : UoWWidgetViewModelBase
    {
        public OrderBase Order { get; set; }

        private object selectedDeposit;
        public object SelectedDeposit
        {
            get => selectedDeposit;
            set
            {
                if (SetField(ref selectedDeposit, value))
                {
                    OnPropertyChanged(nameof(HasSelectedDeposit));
                }
            }
        }
        
        public NomenclaturesJournalViewModel EquipmentsJournalViewModel { get; private set; }

        public event Action UpdateVisibilityEquipmentJournal;

        public bool HasSelectedDeposit => SelectedDeposit != null;
        public bool DepositsScrolled { get; } 
        
        private DelegateCommand addBottleDepositCommand;
        public DelegateCommand AddBottleDepositCommand => addBottleDepositCommand ?? (
            addBottleDepositCommand = new DelegateCommand(
                () => 
                {
                    var newDepositItem = new OrderDepositReturnsItem
                    {
                        Count = MyTab is OrderReturnsView ? 0 : 1,
                        ActualCount = null,
                        Order = Order,
                        DepositType = DepositType.Bottles
                    };
                    Order.ObservableOrderDepositReturnsItems.Add(newDepositItem);
                },
                () => true
            )
        );

        private DelegateCommand addEquipmentDepositCommand;
        public DelegateCommand AddEquipmentDepositCommand => addEquipmentDepositCommand ?? (
            addEquipmentDepositCommand = new DelegateCommand(
                () =>
                {
                    UpdateVisibilityEquipmentJournal?.Invoke();
                },
                () => true
            )
        );
        
        private DelegateCommand removeDepositCommand;
        public DelegateCommand RemoveDepositCommand => removeDepositCommand ?? (
            removeDepositCommand = new DelegateCommand(
                () =>
                {
                    if (SelectedDeposit is OrderDepositReturnsItem depositItem)
                        if (MyTab is OrderReturnsView)
                        {
                            //Удаление только новых залогов добавленных из закрытия МЛ
                            if (depositItem.Count == 0)
                                Order.ObservableOrderDepositReturnsItems.Remove(depositItem);
                        }
                        else
                            Order.ObservableOrderDepositReturnsItems.Remove(depositItem);
                },
                () => true
            )
        );
        
        public OrderDepositReturnsItemsViewModel(
            NomenclaturesJournalViewModel equipmentsJournalViewModel,
            OrderBase order,
            bool depositsScrolled = false)
        {
            ConfigureEquipmentsJournalViewModel(equipmentsJournalViewModel);
            Order = order;
            DepositsScrolled = depositsScrolled;
        }

        private void ConfigureEquipmentsJournalViewModel(NomenclaturesJournalViewModel equipmentsJournalViewModel)
        {
            EquipmentsJournalViewModel = equipmentsJournalViewModel;
            EquipmentsJournalViewModel.OnEntitySelectedResult += (s, ea) =>
            {
                var selectedNode = ea.SelectedNodes.FirstOrDefault();
                
                if (selectedNode == null)
                    return;
                
                //AddDepositEquipment(UoW.Session.Get<Nomenclature>(selectedNode.Id));
                UpdateVisibilityEquipmentJournal?.Invoke();
            };
        }
        
        private void AddDepositEquipment(Nomenclature nomenclature)
        {
            var newDepositItem = new OrderDepositReturnsItem {
                Count = 0,
                ActualCount = null,
                Order = Order,
                EquipmentNomenclature = nomenclature,
                DepositType = DepositType.Equipment
            };
            Order.ObservableOrderDepositReturnsItems.Add(newDepositItem);
        }
    }
}
