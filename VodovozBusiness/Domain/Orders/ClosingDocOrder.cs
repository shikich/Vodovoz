using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;
using QS.HistoryLog;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Logistic;

namespace Vodovoz.Domain.Orders {
    [Appellative(Gender = GrammaticalGender.Masculine,
        NominativePlural = "заказы-закрытие документов",
        Nominative = "заказ-закрытие документов",
        Prepositional = "заказе-закрытии документов",
        PrepositionalPlural = "заказах-закрытии документов"
    )]
    [HistoryTrace]
    [EntityPermission]
    public class ClosingDocOrder : OrderBase {
        private int? eShopOrder;
        [Display(Name = "Заказ из интернет магазина")]
        public virtual int? EShopOrder {
            get => eShopOrder;
            set => SetField(ref eShopOrder, value);
        }
	    
        PaymentFrom paymentByCardFrom;
        [Display(Name = "Место, откуда проведена оплата")]
        public virtual PaymentFrom PaymentByCardFrom {
            get => paymentByCardFrom;
            set => SetField(ref paymentByCardFrom, value);
        }
        
        DefaultDocumentType? defaultDocumentType;
        [Display(Name = "Тип безналичных документов")]
        public virtual DefaultDocumentType? DefaultDocumentType {
            get => defaultDocumentType;
            set => SetField(ref defaultDocumentType, value);
        }
        
        public override OrderType Type => OrderType.ClosingDocOrder;
	    
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