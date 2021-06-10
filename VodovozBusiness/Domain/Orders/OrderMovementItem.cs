using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using QS.HistoryLog;
using Vodovoz.Domain.Goods;

namespace Vodovoz.Domain.Orders
{
    [Appellative(Gender = GrammaticalGender.Feminine,
        NominativePlural = "строки оборудования в заказе",
        Nominative = "строка оборудования в заказе")]
    [HistoryTrace]
    public class OrderMovementItem : OrderItemBase
    {
        private Nomenclature nomenclature;
        [Display(Name = "Номенклатура")]
        public virtual Nomenclature Nomenclature
        {
            get => nomenclature;
            set => SetField(ref nomenclature, value);
        }

        private MovementDirection movementdirection;
        [Display(Name = "Направление")]
        public virtual MovementDirection MovementDirection
        {
            get => movementdirection;
            set => SetField(ref movementdirection, value);
        }

        private EquipmentMovementReason equipmentMovementReason;
        [Display(Name = "Причина перемещения оборудования")]
        public virtual EquipmentMovementReason EquipmentMovementReason
        {
            get => equipmentMovementReason;
            set => SetField(ref equipmentMovementReason, value);
        }
        
        private EquipmentOwnType ownType;
        [Display(Name = "Принадлежность")]
        public virtual EquipmentOwnType OwnType
        {
            get => ownType;
            set => SetField(ref ownType, value);
        }
        
        private MovementReason movementReason;
        [Display(Name = "Причина перемещения")]
        public virtual MovementReason MovementReason
        {
            get => movementReason;
            set => SetField(ref movementReason, value);
        }

        private OrderSalesItem dependentSalesItem;
        [Display(Name = "Связанная строка заказа")]
        public virtual OrderSalesItem DependentSalesItem
        {
            get => dependentSalesItem;
            set => SetField(ref dependentSalesItem, value);
        }
        
        CounterpartyMovementOperation counterpartyMovementOperation;
        public virtual CounterpartyMovementOperation CounterpartyMovementOperation 
        {
            get => counterpartyMovementOperation;
            set => SetField(ref counterpartyMovementOperation, value);
        }

        string receivingComment;
        [Display(Name = "Комментарий по забору")]
        [StringLength(200)]
        public virtual string ReceivingComment 
        {
            get => receivingComment;
            set => SetField(ref receivingComment, value);
        }
    }
}