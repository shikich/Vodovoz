using QS.ViewModels;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class OrderItemsViewModel : UoWWidgetViewModelBase
    {
        private bool isBtnMovementsEquipmentActive;
        public bool IsBtnMovementsEquipmentActive
        {
            get => isBtnMovementsEquipmentActive;
            set => SetField(ref isBtnMovementsEquipmentActive, value);
        }
        
        private bool isBtnDepositsActive;
        public bool IsBtnDepositsActive
        {
            get => isBtnDepositsActive;
            set => SetField(ref isBtnDepositsActive, value);
        }

        public OrderItemsViewModel()
        {
        }
    }
}
