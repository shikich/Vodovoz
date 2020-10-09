using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;
using QS.HistoryLog;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Logistic;

namespace Vodovoz.Domain.Orders {
	[Appellative(Gender = GrammaticalGender.Masculine,
		NominativePlural = "заказы-доставки",
		Nominative = "заказ-доставка",
		Prepositional = "заказе-доставке",
		PrepositionalPlural = "заказах-доставках"
	)]
	[HistoryTrace]
	[EntityPermission]
    public class DeliveryOrder : OrderBase, IEShopOrder {
	    int? bottlesReturn;
	    [Display(Name = "Бутылей на возврат")]
	    public virtual int? BottlesReturn {
		    get => bottlesReturn;
		    set => SetField(ref bottlesReturn, value);
	    }
	    
	    string commentForLogist;
	    [Display(Name = "Комментарий логиста")]
	    public virtual string CommentForLogist {
		    get => commentForLogist;
		    set => SetField(ref commentForLogist, value);
	    }
	    
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
	    
	    DefaultDocumentType? defaultDocumentType;
	    [Display(Name = "Тип безналичных документов")]
	    public virtual DefaultDocumentType? DefaultDocumentType {
		    get => defaultDocumentType;
		    set => SetField(ref defaultDocumentType, value);
	    }
	    
	    public override OrderType Type => OrderType.DeliveryOrder;
	    
	    private int? orderNumberFromOnlineStore;
	    [Display(Name = "Номер онлайн заказа")]
	    public virtual int? OrderNumberFromOnlineStore {
		    get => orderNumberFromOnlineStore;
		    set => SetField(ref orderNumberFromOnlineStore, value);
	    }
	    
	    int? trifle;
	    [Display(Name = "Сдача")]
	    public virtual int? Trifle {
		    get => trifle;
		    set => SetField(ref trifle, value);
	    }
	    
	    ReturnTareReason returnTareReason;
	    [Display(Name = "Причина забора тары")]
	    public virtual ReturnTareReason ReturnTareReason {
		    get => returnTareReason;
		    set => SetField(ref returnTareReason, value);
	    }

	    ReturnTareReasonCategory returnTareReasonCategory;
	    [Display(Name = "Категория причины забора тары")]
	    public virtual ReturnTareReasonCategory ReturnTareReasonCategory {
		    get => returnTareReasonCategory;
		    set => SetField(ref returnTareReasonCategory, value);
	    }
    }
}