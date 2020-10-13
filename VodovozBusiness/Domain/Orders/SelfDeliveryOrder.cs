using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;
using QS.HistoryLog;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;

namespace Vodovoz.Domain.Orders {
	[Appellative(Gender = GrammaticalGender.Masculine,
		NominativePlural = "заказы-самовывозы",
		Nominative = "заказ-самовывоз",
		Prepositional = "заказе-самовывозе",
		PrepositionalPlural = "заказах-самовывозах"
	)]
	[HistoryTrace]
	[EntityPermission]
    public class SelfDeliveryOrder : OrderBase, IEShopOrder, IDefaultOrderDocumentType {
        
	    int? bottlesReturn;
	    [Display(Name = "Бутылей на возврат")]
	    public virtual int? BottlesReturn {
		    get => bottlesReturn;
		    set => SetField(ref bottlesReturn, value);
	    }
	    
	    bool payAfterShipment;
	    [Display(Name = "Оплата после отгрузки")]
	    public virtual bool PayAfterShipment {
		    get => payAfterShipment;
		    set => SetField(ref payAfterShipment, value);
	    }

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
	    
	    public override OrderType Type => OrderType.SelfDeliveryOrder;

	    private int? orderNumberFromOnlineStore;
	    [Display(Name = "Номер онлайн заказа")]
	    public virtual int? OrderNumberFromOnlineStore {
		    get => orderNumberFromOnlineStore;
		    set => SetField(ref orderNumberFromOnlineStore, value);
	    }

	    int? returnedTare;
	    [Display(Name = "Возвратная тара")]
	    public virtual int? ReturnedTare {
		    get => returnedTare;
		    set => SetField(ref returnedTare, value);
	    }

	    Employee loadAllowedBy;
	    [Display(Name = "Отгрузку разрешил")]
	    public virtual Employee LoadAllowedBy {
		    get => loadAllowedBy;
		    set => SetField(ref loadAllowedBy, value);
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