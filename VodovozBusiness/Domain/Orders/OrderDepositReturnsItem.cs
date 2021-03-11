using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Operations;

namespace Vodovoz.Domain.Orders
{
    [Appellative (Gender = GrammaticalGender.Masculine,
        NominativePlural = "залоги в заказе",
        Nominative = "залог в заказе"
    )]
    public class OrderDepositReturnsItem : OrderItemBase
    {
        private Nomenclature equipmentNomenclature;
        [Display(Name = "Номенклатура оборудования")]
        public virtual Nomenclature EquipmentNomenclature
        {
            get => equipmentNomenclature;
            set => SetField(ref equipmentNomenclature, value);
        }
        
        private DepositType depositType;
        [Display(Name = "Тип залога")]
        public DepositType DepositType
        {
            get => depositType;
            set => SetField(ref depositType, value);
        }

        private decimal depositValue;
        [Display(Name = "Сумма залога")]
        public decimal DepositValue
        {
            get => depositValue;
            set => SetField(ref depositValue, value);
        }
        
        private DepositOperation depositOperation;
        
        public DepositOperation DepositOperation
        {
            get => depositOperation;
            set => SetField(ref depositOperation, value);
        }
        
        PaidRentEquipment paidRentItem;
        [Display(Name = "Залог за оплачиваемое оборудование")]
        public virtual PaidRentEquipment PaidRentItem {
            get => paidRentItem;
            set => SetField(ref paidRentItem, value);
        }

        FreeRentEquipment freeRentItem;
        [Display(Name = "Залог за бесплатное оборудование")]
        public virtual FreeRentEquipment FreeRentItem {
            get => freeRentItem;
            set => SetField(ref freeRentItem, value);
        }

        public virtual decimal Total => CurrentCount * DepositValue;
        
        public virtual string DepositTypeString {
            get { 
                switch (DepositType) {
                    case DepositType.Bottles:
                        return "Возврат залога за бутыли";
                    case DepositType.Equipment:
                        return "Возврат залога за оборудование";
                    default:
                        return "Не определено";
                }
            } 
        }
    }
}