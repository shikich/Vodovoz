using FluentNHibernate.Mapping;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;

namespace Vodovoz.HibernateMapping.Order {
	public class OrderBaseMap : ClassMap<OrderBase> {

		public OrderBaseMap() {
			Table("new_orders");

			OptimisticLock.Version();
			Version(x => x.Version).Column("version");

			Id(x => x.Id).Column("id").GeneratedBy.Native();
			DiscriminateSubClassesOnColumn("type");

			Map(x => x.CreateDate).Column("create_date");
			Map(x => x.IsFirstOrder).Column("is_first_order");
			Map(x => x.Comment).Column("comment");
			Map(x => x.DeliveryDate).Column("delivery_date");
			Map(x => x.OrderCashSum).Column("order_cash_sum");
			Map(x => x.DailyNumber).Column("daily_number").ReadOnly();
			Map(x => x.Status).Column("status");
			Map(x => x.LastEditedTime).Column("last_edited_time");
			Map(x => x.CommentManager).Column("comment_manager");
			Map(x => x.BillDate).Column("bill_date");
			Map(x => x.TareInformation).Column("tare_information");
			Map(x => x.IsTareNonReturnReasonChangedByUser).Column("is_reason_type_changed_by_user");
			Map(x => x.IsBottleStock).Column("is_bottle_stock");
			Map(x => x.BottlesByStockCount).Column("bottles_by_stock_count");
			Map(x => x.BottlesByStockActualCount).Column("bottles_by_stock_actual_count");
			Map(x => x.PaymentType).Column("payment_type").CustomType<PaymentTypeStringType>();
			Map(x => x.DriverCallNumber).Column("driver_call_number");
			Map(x => x.DriverCallType).Column("driver_call_type").CustomType<DriverCallTypeStringType>();
			Map(x => x.OrderSource).Column("order_source").CustomType<OrderSourceStringType>();
			Map(x => x.NeedAddCertificates).Column("need_add_certificates");
			Map(x => x.IsPaymentBySms).Column("is_payment_by_sms");
			Map(x => x.IsContactlessDelivery).Column("is_contactless_delivery");
			Map(x => x.OrderPaymentStatus).Column("order_payment_status").CustomType<OrderPaymentStatusStringType>();
			Map(x => x.TimeDelivered).Column("time_delivered");

			References(x => x.Author).Column("author_employee_id");
			References(x => x.AcceptedOrderEmployee).Column("accepted_order_employee_id");
			References(x => x.Counterparty).Column("counterparty_id");
			References(x => x.Contract).Column("counterparty_contract_id");
			References(x => x.DeliveryPoint).Column("delivery_point_id");
			References(x => x.BottlesMovementOperation).Column("bottles_movement_operation_id");
			References(x => x.MoneyMovementOperation).Column("money_movement_operation_id");
			References(x => x.LastEditor).Column("editor_employee_id");
			References(x => x.TareNonReturnReason).Column("tare_non_return_reason_id");

			HasMany(x => x.OrderDocuments).Cascade.AllDeleteOrphan().Inverse().LazyLoad()
			                              .KeyColumn("attached_to_order_id");
			HasMany(x => x.OrderDepositItems).Cascade.AllDeleteOrphan().Inverse().LazyLoad().KeyColumn("order_id");
			HasMany(x => x.OrderItems).Cascade.AllDeleteOrphan().Inverse().LazyLoad().KeyColumn("order_id");
			HasMany(x => x.OrderEquipments).Cascade.AllDeleteOrphan().Inverse().LazyLoad().KeyColumn("order_id");
			HasMany(x => x.DepositOperations).Cascade.AllDeleteOrphan().Inverse().LazyLoad().KeyColumn("order_id");
		}
	}

	public class SelfDeliveryOrderMap : SubclassMap<SelfDeliveryOrder> {
		public SelfDeliveryOrderMap() {
			DiscriminatorValue("SelfDeliveryOrder");

			Map(x => x.BottlesReturn).Column("bottles_return");
			Map(x => x.PayAfterShipment).Column("pay_after_shipment");
			Map(x => x.EShopOrder).Column("e_shop_order");
			Map(x => x.OrderNumberFromOnlineStore).Column("order_number_from_online_store");
			Map(x => x.ReturnedTare).Column("returned_tare");
			Map(x => x.DefaultDocumentType).Column("default_document_type").CustomType<DefaultDocumentTypeStringType>();

			References(x => x.PaymentByCardFrom).Column("payment_by_card_from_id");
			References(x => x.LoadAllowedBy).Column("load_allowed_employee_id");
			References(x => x.ReturnTareReason).Column("return_tare_reason_id");
			References(x => x.ReturnTareReasonCategory).Column("return_tare_reason_category_id");
		}
	}

	public class DeliveryOrderMap : SubclassMap<DeliveryOrder> {
		public DeliveryOrderMap() {
			DiscriminatorValue("DeliveryOrder");

			Map(x => x.BottlesReturn).Column("bottles_return");
			Map(x => x.CommentForLogist).Column("comment_for_logist");
			Map(x => x.EShopOrder).Column("e_shop_order");
			Map(x => x.OrderNumberFromOnlineStore).Column("order_number_from_online_store");
			Map(x => x.Trifle).Column("trifle");
			Map(x => x.DefaultDocumentType).Column("default_document_type").CustomType<DefaultDocumentTypeStringType>();

			References(x => x.DeliverySchedule).Column("delivery_schedule_id");
			References(x => x.PaymentByCardFrom).Column("payment_by_card_from_id");
			References(x => x.ReturnTareReason).Column("return_tare_reason_id");
			References(x => x.ReturnTareReasonCategory).Column("return_tare_reason_category_id");
		}
	}

	public class VisitingMasterOrderMap : SubclassMap<VisitingMasterOrder> {
		public VisitingMasterOrderMap() {
			DiscriminatorValue("VisitingMasterOrder");

			Map(x => x.CommentForLogist).Column("comment_for_logist");
			Map(x => x.EShopOrder).Column("e_shop_order");
			Map(x => x.OrderNumberFromOnlineStore).Column("order_number_from_online_store");
			Map(x => x.Trifle).Column("trifle");
			Map(x => x.DefaultDocumentType).Column("default_document_type").CustomType<DefaultDocumentTypeStringType>();

			References(x => x.DeliverySchedule).Column("delivery_schedule_id");
			References(x => x.PaymentByCardFrom).Column("payment_by_card_from_id");
		}
	}

	public class ClosingDocOrderMap : SubclassMap<ClosingDocOrder> {
		public ClosingDocOrderMap() {
			DiscriminatorValue("ClosingDocOrder");

			Map(x => x.EShopOrder).Column("e_shop_order");
			Map(x => x.OrderNumberFromOnlineStore).Column("order_number_from_online_store");
			Map(x => x.IsContractCloser).Column("is_contract_closer");
			Map(x => x.DefaultDocumentType).Column("default_document_type").CustomType<DefaultDocumentTypeStringType>();
			
			References(x => x.PaymentByCardFrom).Column("payment_by_card_from_id");
		}
	}

	public class OrderFrom1cMap : SubclassMap<OrderFrom1c> {
		public OrderFrom1cMap() {
			DiscriminatorValue("OrderFrom1c");

			Map(x => x.ReturnedTare).Column("returned_tare");
			Map(x => x.Code1c).Column("code1c");
			Map(x => x.Address1c).Column("address_1c");
			Map(x => x.Address1cCode).Column("address_1c_code");
			Map(x => x.DeliverySchedule1c).Column("delivery_schedule_1c");
			Map(x => x.ToClientText).Column("to_client_text");
			Map(x => x.FromClientText).Column("from_client_text");
			Map(x => x.ClientPhone).Column("client_phone");
		}
	}
}