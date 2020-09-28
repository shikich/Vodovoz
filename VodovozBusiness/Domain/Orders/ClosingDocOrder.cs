using System.ComponentModel.DataAnnotations;
using Vodovoz.Domain.Logistic;

namespace Vodovoz.Domain.Orders {
    public class ClosingDocOrder : OrderBase {
        private int? eShopOrder;
        [Display(Name = "Заказ из интернет магазина")]
        public virtual int? EShopOrder {
            get => eShopOrder;
            set => SetField(ref eShopOrder, value);
        }
	    
        DeliverySchedule deliverySchedule;
        [Display(Name = "Время доставки")]
        public virtual DeliverySchedule DeliverySchedule {
            get => deliverySchedule;
            set => SetField(ref deliverySchedule, value);
        }

        PaymentFrom paymentByCardFrom;
        [Display(Name = "Место, откуда проведена оплата")]
        public virtual PaymentFrom PaymentByCardFrom {
            get => paymentByCardFrom;
            set => SetField(ref paymentByCardFrom, value);
        }
        
        public override DefaultOrderType DefaultOrderType => DefaultOrderType.ClosingDocOrder;
	    
        private int? orderNumberFromOnlineStore;
        [Display(Name = "Номер онлайн заказа")]
        public virtual int? OrderNumberFromOnlineStore {
            get => orderNumberFromOnlineStore;
            set => SetField(ref orderNumberFromOnlineStore, value);
        }
	    
        private bool isContractCloser;
        [Display(Name = "Заказ - закрывашка по контракту?")]
        public virtual bool IsContractCloser {
            get => isContractCloser;
            set => SetField(ref isContractCloser, value);
        }
    }
}