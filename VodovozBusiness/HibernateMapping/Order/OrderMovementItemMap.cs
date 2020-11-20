using FluentNHibernate.Mapping;
using Vodovoz.Domain.Orders;

namespace Vodovoz.HibernateMapping.Order
{
    public class OrderMovementItemMap : ClassMap<OrderMovementItem>
    {
        public OrderMovementItemMap()
        {
            Table ("order_movement_items");

            Id (x => x.Id).Column ("id").GeneratedBy.Native ();

            Map(x => x.MovementDirection).Column("movement_direction").CustomType<MovementDirectionStringType>();
            Map(x => x.EquipmentMovementReason).Column("equipment_movement_reason").CustomType<EquipmentMovementReason>();
            Map(x => x.MovementReason).Column("movement_reason").CustomType<MovementReasonStringType> ();
            Map(x => x.ReceivingComment).Column("receiving_comment");
            Map(x => x.OwnType).Column("own_type").CustomType<EquipmentOwnTypeStringType>();
            Map(x => x.Count).Column("count");
            Map(x => x.ActualCount).Column("actual_count");

            References(x => x.Order).Column("order_id");
            References(x => x.DependentSalesItem).Column("dependent_sales_item_id");
            References(x => x.Nomenclature).Column("nomenclature_id");
            References(x => x.CounterpartyMovementOperation).Column("counterparty_movement_operation_id").Cascade.All();
        }
    }
}