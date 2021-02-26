using QS.Commands;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Orders;
namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderDepositReturnsItemsViewModel
    {
        private OrderBase Order { get; set; }

        public object SelectedObj { get; set; }
        /*
        private DelegateCommand addBottleDeposit;
        public DelegateCommand AddBottleDeposit => addBottleDeposit ?? (
            addBottleDeposit = new DelegateCommand(
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

        private DelegateCommand addEquipmentDeposit;
        public DelegateCommand AddEquipmentDeposit => addEquipmentDeposit ?? (
            addEquipmentDeposit = new DelegateCommand(
                () =>
                {
                    OrmReference SelectDialog =
                new OrmReference(
                    typeof(Nomenclature),
                    UoW,
                    NomenclatureRepository.NomenclatureEquipmentsQuery()
                                          .GetExecutableQueryOver(UoW.Session)
                                          .RootCriteria
                    )
                {
                    Mode = OrmReferenceMode.Select,
                    TabName = "Оборудование",
                    FilterClass = typeof(NomenclatureEquipTypeFilter)
                };
                    SelectDialog.ObjectSelected += SelectDialog_ObjectSelected;
                    MyTab.TabParent.AddSlaveTab(MyTab, SelectDialog);

                },
                () => true
            )
        );

        private DelegateCommand removeDeposit;
        public DelegateCommand RemoveDeposit => removeDeposit ?? (
            removeDeposit = new DelegateCommand(
                () =>
                {
                    if (SelectedObj is OrderDepositReturnsItem depositItem)
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
        */
        public OrderDepositReturnsItemsViewModel()
        {

        }
    }
}
