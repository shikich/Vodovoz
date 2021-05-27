using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Gamma.Utilities;
using QS.HistoryLog;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Orders.Documents;

namespace Vodovoz.Domain.Orders {
    public abstract class OrderBase : DomainObjectBase 
    {
        DateTime version;
		[Display(Name = "Версия")]
		public virtual DateTime Version {
			get => version;
			set => SetField(ref version, value);
		}

		DateTime? createDate;
		[Display(Name = "Дата создания")]
		public virtual DateTime? CreateDate {
			get => createDate;
			set => SetField(ref createDate, value);
		}
		
		Employee author;
		[Display(Name = "Создатель заказа")]
		[IgnoreHistoryTrace]
		public virtual Employee Author {
			get => author;
			set => SetField(ref author, value);
		}
		
		Employee lastEditor;
		[Display(Name = "Последний редактор")]
		[IgnoreHistoryTrace]
		public virtual Employee LastEditor {
			get => lastEditor;
			set => SetField(ref lastEditor, value);
		}

		DateTime lastEditedTime;
		[Display(Name = "Последние изменения")]
		[IgnoreHistoryTrace]
		public virtual DateTime LastEditedTime {
			get => lastEditedTime;
			set => SetField(ref lastEditedTime, value);
		}
		
		private int? dailyNumber;
		/// <summary>
		/// Уникапльный номер в передлах одного дня.
		/// ВАЖНО! Номер генерируется и изменяется на стороне БД
		/// </summary>
		[Display(Name = "Ежедневный номер")]
		public virtual int? DailyNumber {
			get => dailyNumber;
			set => SetField(ref dailyNumber, value);
		}
		
		DateTime? timeDelivered;
		[Display(Name = "Время доставки")]
		public virtual DateTime? TimeDelivered {
			get => timeDelivered;
			set => SetField(ref timeDelivered, value);
		}
		
		OrderStatus status;
		[Display(Name = "Статус заказа")]
		public virtual OrderStatus Status {
			get => status;
			set => SetField(ref status, value);
		}

		Counterparty counterparty;
		[Display(Name = "Клиент")]
		public virtual Counterparty Counterparty {
			get => counterparty;
			set => SetField(ref counterparty, value);
		}
		
		CounterpartyContract contract;
		[Display(Name = "Договор")]
		public virtual CounterpartyContract Contract {
			get => contract;
			set => SetField(ref contract, value);
		}

		DeliveryPoint deliveryPoint;
		[Display(Name = "Точка доставки")]
		public virtual DeliveryPoint DeliveryPoint {
			get => deliveryPoint;
			set => SetField(ref deliveryPoint, value);
		}
		
		private bool isBottleStock;
		[Display(Name = "Акция \"Бутыль\" ")]
		public virtual bool IsBottleStock {
			get => isBottleStock;
			set => SetField(ref isBottleStock, value);
		}

		private int bottlesByStockCount;
		[Display(Name = "Количество бутылей по акции")]
		public virtual int BottlesByStockCount {
			get => bottlesByStockCount;
			set => SetField(ref bottlesByStockCount, value);
		}

		private int bottlesByStockActualCount;
		[Display(Name = "Фактическое количество бутылей по акции")]
		public virtual int BottlesByStockActualCount {
			get => bottlesByStockActualCount;
			set => SetField(ref bottlesByStockActualCount, value);
		}

		DateTime billDate = DateTime.Now;
		[Display(Name = "Дата счета")]
		[HistoryDateOnly]
		public virtual DateTime BillDate {
			get => billDate;
			set => SetField(ref billDate, value);
		}
		
		string tareInformation;
		[Display(Name = "Информация о таре")]
		public virtual string TareInformation {
			get => tareInformation;
			set => SetField(ref tareInformation, value);
		}
		
		DriverCallType driverCallType;
		[Display(Name = "Водитель отзвонился")]
		public virtual DriverCallType DriverCallType {
			get => driverCallType;
			set => SetField(ref driverCallType, value);
		}

		int? driverCallNumber;
		[Display(Name = "Номер звонка водителя")]
		public virtual int? DriverCallNumber {
			get => driverCallNumber;
			set => SetField(ref driverCallNumber, value);
		}

		NonReturnReason tareNonReturnReason;
		[Display(Name = "Причина несдачи тары")]
		public virtual NonReturnReason TareNonReturnReason {
			get => tareNonReturnReason;
			set => SetField(ref tareNonReturnReason, value);
		}
		
		bool isTareNonReturnReasonChangedByUser;
		[Display(Name = "Причина невозврата тары указана пользователем")]
		[IgnoreHistoryTrace]
		public virtual bool IsTareNonReturnReasonChangedByUser {
			get => isTareNonReturnReasonChangedByUser;
			set => SetField(ref isTareNonReturnReasonChangedByUser, value);
		}
		
		bool isFirstOrder;
		[Display(Name = "Первый заказ")]
		public virtual bool IsFirstOrder {
			get => isFirstOrder;
			set => SetField(ref isFirstOrder, value);
		}

		BottlesMovementOperation bottlesMovementOperation;
		[IgnoreHistoryTrace]
		public virtual BottlesMovementOperation BottlesMovementOperation {
			get => bottlesMovementOperation;
			set => SetField(ref bottlesMovementOperation, value);
		}
		
		MoneyMovementOperation moneyMovementOperation;
		[IgnoreHistoryTrace]
		public virtual MoneyMovementOperation MoneyMovementOperation {
			get => moneyMovementOperation;
			set => SetField(ref moneyMovementOperation, value);
		}
		
		OrderPaymentStatus orderPaymentStatus;
		[Display(Name = "Статус оплаты заказа")]
		public virtual OrderPaymentStatus OrderPaymentStatus {
			get => orderPaymentStatus;
			set => SetField(ref orderPaymentStatus, value);
		}

		private decimal orderCashSum;
		[Display(Name = "Наличных к получению")]
		public virtual decimal OrderCashSum {
			get => orderCashSum;
			set => SetField(ref orderCashSum, value);
		}
		
		private decimal totalSum;
		[Display(Name = "Сумма заказа")]
		public virtual decimal TotalSum {
			get => totalSum;
			set => SetField(ref totalSum, value);
		}
		
		private Employee acceptedOrderEmployee;
		[Display(Name = "Заказ подтвердил")]
		public virtual Employee AcceptedOrderEmployee {
			get => acceptedOrderEmployee;
			set => SetField(ref acceptedOrderEmployee, value);
		}
		
		private OrderSource orderSource = OrderSource.VodovozApp;
		[Display(Name = "Источник заказа")]
		public virtual OrderSource OrderSource {
			get => orderSource;
			set => SetField(ref orderSource, value);
		}

		bool needAddCertificates;
		[Display(Name = "Нужны сертификаты?")]
		public virtual bool NeedAddCertificates {
			get => needAddCertificates;
			set => SetField(ref needAddCertificates, value);
		}
		
		bool isContactlessDelivery;
		[Display(Name = "Бесконтактная доставка")]
		public virtual bool IsContactlessDelivery {
			get => isContactlessDelivery;
			set => SetField(ref isContactlessDelivery, value);
		}
		
		bool isPaymentBySms;
		[Display(Name = "Оплата по SMS")]
		public virtual bool IsPaymentBySms {
			get => isPaymentBySms;
			set => SetField(ref isPaymentBySms, value);
		}
		
		string comment;
		[Display(Name = "Комментарий")]
		public virtual string Comment {
			get => comment;
			set => SetField(ref comment, value);
		}
		
		string commentManager;
		/// <summary>
		/// Комментарий менеджера ответственного за водительский телефон
		/// </summary>
		[Display(Name = "Комментарий менеджера")]
		public virtual string CommentManager {
			get => commentManager;
			set => SetField(ref commentManager, value);
		}
		
		DateTime? deliveryDate;
		[Display(Name = "Дата доставки")]
		[HistoryDateOnly]
		public virtual DateTime? DeliveryDate {
			get => deliveryDate;
			set => SetField(ref deliveryDate, value);
		}

		PaymentType paymentType;
		[Display(Name = "Форма оплаты")]
		public virtual PaymentType PaymentType {
			get => paymentType;
			set => SetField(ref paymentType, value);
		}
		
		public abstract OrderType Type { get; }

		IList<OrderItem> orderItems = new List<OrderItem>();
		[Display(Name = "Строки заказа")]
		public virtual IList<OrderItem> OrderItems {
			get => orderItems;
			set => SetField(ref orderItems, value);
		}

		GenericObservableList<OrderItem> observableOrderItems;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderItem> ObservableOrderItems {
			get => observableOrderItems ??
			       (observableOrderItems = new GenericObservableList<OrderItem>(OrderItems));
		}
		
		IList<OrderSalesItem> orderSalesItems = new List<OrderSalesItem>();
		[Display(Name = "Строки заказа")]
		public virtual IList<OrderSalesItem> OrderSalesItems {
			get => orderSalesItems;
			set => SetField(ref orderSalesItems, value);
		}

		GenericObservableList<OrderSalesItem> observableOrderSalesItems;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderSalesItem> ObservableOrderSalesItems {
			get => observableOrderSalesItems ??
			       (observableOrderSalesItems = new GenericObservableList<OrderSalesItem>(OrderSalesItems));
		}

		IList<OrderEquipment> orderEquipments = new List<OrderEquipment>();
		[Display(Name = "Список оборудования")]
		public virtual IList<OrderEquipment> OrderEquipments {
			get => orderEquipments;
			set => SetField(ref orderEquipments, value);
		}

		GenericObservableList<OrderEquipment> observableOrderEquipments;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderEquipment> ObservableOrderEquipments {
			get => observableOrderEquipments ?? (observableOrderEquipments =
					new GenericObservableList<OrderEquipment>(OrderEquipments));
		}
		
		IList<OrderMovementItem> orderMovements = new List<OrderMovementItem>();
		[Display(Name = "Список оборудования")]
		public virtual IList<OrderMovementItem> OrderMovements {
			get => orderMovements;
			set => SetField(ref orderMovements, value);
		}

		GenericObservableList<OrderMovementItem> observableOrderMovements;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderMovementItem> ObservableOrderMovements {
			get => observableOrderMovements ?? (observableOrderMovements =
				new GenericObservableList<OrderMovementItem>(OrderMovements));
		}
		
		IList<OrderDepositItem> orderDepositItems = new List<OrderDepositItem>();
		[Display(Name = "Залоги заказа")]
		public virtual IList<OrderDepositItem> OrderDepositItems {
			get => orderDepositItems;
			set => SetField(ref orderDepositItems, value);
		}

		GenericObservableList<OrderDepositItem> observableOrderDepositItems;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderDepositItem> ObservableOrderDepositItems {
			get => observableOrderDepositItems ?? (observableOrderDepositItems =
					new GenericObservableList<OrderDepositItem>(OrderDepositItems));
		}
		
		IList<OrderDepositReturnsItem> orderDepositReturnsItems = new List<OrderDepositReturnsItem>();
		[Display(Name = "Залоги заказа")]
		public virtual IList<OrderDepositReturnsItem> OrderDepositReturnsItems {
			get => orderDepositReturnsItems;
			set => SetField(ref orderDepositReturnsItems, value);
		}

		GenericObservableList<OrderDepositReturnsItem> observableOrderDepositReturnsItems;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderDepositReturnsItem> ObservableOrderDepositReturnsItems {
			get => observableOrderDepositReturnsItems ?? (observableOrderDepositReturnsItems =
				new GenericObservableList<OrderDepositReturnsItem>(OrderDepositReturnsItems));
		}

		IList<OrderDocument> orderDocuments = new List<OrderDocument>();
		[Display(Name = "Документы заказа")]
		public virtual IList<OrderDocument> OrderDocuments {
			get => orderDocuments;
			set => SetField(ref orderDocuments, value);
		}

		GenericObservableList<OrderDocument> observableOrderDocuments;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<OrderDocument> ObservableOrderDocuments {
			get => observableOrderDocuments ??
			       (observableOrderDocuments = new GenericObservableList<OrderDocument>(OrderDocuments));
		}

		IList<DepositOperation> depositOperations = new List<DepositOperation>();
		public virtual IList<DepositOperation> DepositOperations {
			get => depositOperations;
			set => SetField(ref depositOperations, value);
		}

		GenericObservableList<DepositOperation> observableDepositOperations;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<DepositOperation> ObservableDepositOperations {
			get => observableDepositOperations ??
			       (observableDepositOperations = new GenericObservableList<DepositOperation>(DepositOperations));
		}
		
		public virtual decimal GetFixedPrice(OrderItem item) => item.GetWaterFixedPrice() ?? default(decimal);
		
		public virtual decimal GetNomenclaturePrice(OrderItem item)
		{
			decimal nomenclaturePrice = 0M;
			if(item.Nomenclature.IsWater19L) {
				nomenclaturePrice = item.Nomenclature.GetPrice(GetTotalWater19LCount());
			} else {
				nomenclaturePrice = item.Nomenclature.GetPrice(item.Count);
			}
			return nomenclaturePrice;
		}
		
		public virtual int GetTotalWater19LCount(bool doNotCountWaterFromPromoSets = false)
		{
			var water19L = ObservableOrderItems.Where(x => x.Nomenclature.IsWater19L);
			if(doNotCountWaterFromPromoSets)
				water19L = water19L.Where(x => x.PromoSet == null);
			return (int)water19L.Sum(x => x.Count);
		}
		
		public virtual bool CanEditOrder => EditableOrderStatuses.Contains(Status);
		
		private OrderStatus[] EditableOrderStatuses {
			get {
				if(Type == OrderType.SelfDeliveryOrder) {
					return new [] {OrderStatus.NewOrder};
				}

				return new[] {OrderStatus.NewOrder, OrderStatus.WaitForPayment};
			}
		}

		public override string ToString()
		{
			return Status == OrderStatus.NewOrder
				? $"Новый {Type.GetEnumTitle().ToLower()}"
				: $"{Type.GetEnumTitle()} от {DeliveryDate:dd.MM.yyyy}";
		}

		public OrderBase()
		{
			Status = OrderStatus.NewOrder;
		}
		
		//TODO узнать про поле документов по умолчанию для самовывоза
		public virtual void UpdateClientDefaultParam()
		{
			if(Counterparty == null) return;
			if(Status != OrderStatus.NewOrder) return;

			DeliveryDate = null;
			DeliveryPoint = null;
			//DeliverySchedule = null;
			Contract = null;
			//DefaultDocumentType = Counterparty.DefaultDocumentType ?? DefaultDocumentType.upd;

			if(Type != OrderType.SelfDeliveryOrder && Counterparty.DeliveryPoints?.Count == 1)
				DeliveryPoint = Counterparty.DeliveryPoints.FirstOrDefault();

			PaymentType = Counterparty.PaymentMethod;
		}
    }
}