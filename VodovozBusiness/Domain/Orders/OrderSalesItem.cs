using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using QS.HistoryLog;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;

namespace Vodovoz.Domain.Orders
{
    [Appellative(Gender = GrammaticalGender.Feminine,
        NominativePlural = "строки заказа",
        Nominative = "строка заказа")]
    [HistoryTrace]
    public class OrderSalesItem : OrderItemBase
    {
        private Nomenclature nomenclature;
        [Display(Name = "Номенклатура")]
        public virtual Nomenclature Nomenclature
        {
            get => nomenclature;
            set => SetField(ref nomenclature, value);
        }
        
        private decimal price;
        [Display(Name = "Цена")]
        public virtual decimal Price
        {
            get => price;
            set => SetField(ref price, value);
        }
        
        private bool isUserPrice;
        [Display(Name = "Цена установлена пользователем?")]
        public virtual bool IsUserPrice
        {
            get => isUserPrice;
            set => SetField(ref isUserPrice, value);
        }
        
        private decimal includedVAT;
        [Display(Name = "Включая НДС")]
        public virtual decimal IncludedVAT {
        	get => includedVAT;
        	set => SetField(ref includedVAT, value);
        }

        private VAT? vatType;
        [Display(Name = "Ставка НДС")]
        public virtual VAT? VATType
        {
	        get => vatType;
	        set => SetField(ref vatType, value);
        } 

        private bool isDiscountInMoney;
        [Display(Name = "Скидка деньгами?")]
        public virtual bool IsDiscountInMoney {
        	get => isDiscountInMoney;
            set => SetField(ref isDiscountInMoney, value);
        }

        private decimal discountPercent;
        [Display(Name = "Процент скидки на товар")]
        public virtual decimal DiscountPercent {
        	get => discountPercent;
            set => SetField(ref discountPercent, value);
        }

        private decimal? originalDiscountPercent;
        [Display(Name = "Процент скидки на товар которая была установлена до отмены заказа")]
        public virtual decimal? OriginalDiscountPercent {
        	get => originalDiscountPercent;
        	set => SetField(ref originalDiscountPercent, value);
        }

        private decimal discountMoney;
        [Display(Name = "Скидка на товар в деньгах")]
        public virtual decimal DiscountMoney {
        	get => discountMoney;
            set => SetField(ref discountMoney, value);
        }

        private decimal? originalDiscountMoney;
        [Display(Name = "Скидки на товар которая была установлена до отмены заказа")]
        public virtual decimal? OriginalDiscountMoney {
        	get => originalDiscountMoney;
        	set => SetField(ref originalDiscountMoney, value);
        }
        
        private decimal discountByStockBottle;
        [Display(Name = "Скидка по акции")]
        public virtual decimal DiscountByStockBottle {
            get => discountByStockBottle;
            set => SetField(ref discountByStockBottle, value);
        }

        private DiscountReason discountReason;
        [Display(Name = "Основание скидки на товар")]
        public virtual DiscountReason DiscountReason {
            get => discountReason;
            set => SetField(ref discountReason, value);
        }

        private DiscountReason originalDiscountReason;
        [Display(Name = "Основание скидки на товар до отмены заказа")]
        public virtual DiscountReason OriginalDiscountReason {
            get => originalDiscountReason;
            set => SetField(ref originalDiscountReason, value);
        }
        
        private CounterpartyMovementOperation counterpartyMovementOperation;
        public virtual CounterpartyMovementOperation CounterpartyMovementOperation {
            get => counterpartyMovementOperation;
            set => SetField(ref counterpartyMovementOperation, value);
        }
        
        private FreeRentEquipment freeRentEquipment;
        public virtual FreeRentEquipment FreeRentEquipment {
            get => freeRentEquipment;
            set => SetField(ref freeRentEquipment, value);
        }

        private PaidRentEquipment paidRentEquipment;
        public virtual PaidRentEquipment PaidRentEquipment {
            get => paidRentEquipment;
            set => SetField(ref paidRentEquipment, value);
        }

        private PromotionalSet promoSet;
        [Display(Name = "Добавлено из промо-набора")]
        public virtual PromotionalSet PromoSet {
            get => promoSet;
            set => SetField(ref promoSet, value);
        }
    }
}