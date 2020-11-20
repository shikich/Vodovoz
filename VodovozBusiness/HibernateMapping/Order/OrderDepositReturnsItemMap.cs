using FluentNHibernate.Mapping;
using Vodovoz.Domain.Operations;
using Vodovoz.Domain.Orders;

namespace Vodovoz.HibernateMapping.Order
{
    public class OrderDepositReturnsItemMap : ClassMap<OrderDepositReturnsItem>
    {
        public OrderDepositReturnsItemMap()
        {
            Table ("order_deposit_returns_items");
            Not.LazyLoad ();

            Id (x => x.Id).Column ("id").GeneratedBy.Native ();

            Map (x => x.DepositValue).Column ("deposit_value");
            Map (x => x.Count).Column ("count");
            Map (x => x.ActualCount).Column("actual_count");
            Map (x => x.DepositType).Column ("deposit_type").CustomType<DepositTypeStringType> ();

            References (x => x.EquipmentNomenclature).Column("equipment_nomenclature_id");
            References (x => x.Order).Column ("order_id");
            References (x => x.DepositOperation).Column ("deposit_operation_id");
            References (x => x.PaidRentItem).Column ("paid_rent_equipment_id");
            References (x => x.FreeRentItem).Column ("free_rent_equipment_id");
        }
    }
}