using FluentNHibernate.Mapping;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;

namespace Vodovoz.HibernateMapping.Order
{
    public class OrderSalesItemMap : ClassMap<OrderSalesItem>
    {
        public OrderSalesItemMap()
        {
            Table ("order_sales_items");
            Not.LazyLoad ();

            Id (x => x.Id).Column("id").GeneratedBy.Native ();

            Map (x => x.ActualCount).Column("actual_count");
            Map (x => x.Count).Column("count");
            Map (x => x.IsDiscountInMoney).Column("is_discount_in_money");
            Map (x => x.DiscountPercent).Column("discount_percent");
            Map (x => x.OriginalDiscountPercent).Column("original_discount_percent");
            Map (x => x.DiscountMoney).Column("discount_money");
            Map	(x => x.OriginalDiscountMoney).Column("original_discount_money");
            Map	(x => x.DiscountByStockBottle).Column("discount_by_stock_bottle");
            Map (x => x.IncludedVAT).Column ("included_vat");
            Map (x => x.Price).Column("price");
            Map (x => x.IsUserPrice).Column("is_user_price");
            Map (x => x.VATType).Column("vat_type").CustomType<VATStringType>();
            
            References (x => x.CounterpartyMovementOperation).Column ("counterparty_movement_operation_id").Cascade.All();
            References (x => x.Nomenclature).Column("nomenclature_id");
            References (x => x.Order).Column("order_id");
            References (x => x.FreeRentEquipment).Column("free_rent_equipment_id").Cascade.All();
            References (x => x.PaidRentEquipment).Column("paid_rent_equipment_id").Cascade.All();
            References (x => x.DiscountReason).Column("discount_reason_id");
            References (x => x.OriginalDiscountReason).Column("original_discount_reason_id");
            References (x => x.PromoSet).Column("promotional_set_id");
        }
    }
}