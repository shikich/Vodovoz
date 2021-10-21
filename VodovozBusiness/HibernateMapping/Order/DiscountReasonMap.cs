using FluentNHibernate.Mapping;
using Vodovoz.Domain.Orders;

namespace Vodovoz.HibernateMapping.Order
{
	public class DiscountReasonMap : ClassMap<DiscountReason>
	{
		public DiscountReasonMap()
		{
			Table("discount_reasons");

			Id (x => x.Id).Column("id").GeneratedBy.Native();
			Map(x => x.Name).Column("name");
			Map(x => x.IsArchive).Column("is_archive");
			Map(x => x.DiscountTargetType).Column("target_type").CustomType<DiscoutTargetTypeStringType>();
			Map(x => x.DiscountPermissionType).Column("permission_type").CustomType<DiscountPermissionTypeStringType>();
			Map(x => x.SkillLevel).Column("skill_level");
			Map(x => x.DiscountUsage).Column("usage").CustomType<DiscountUsageStringType>();
			Map(x => x.DiscountCounterpartyType).Column("counterparty_type").CustomType<DiscountCounterpartyTypeStringType>();
			Map(x => x.ForSelfdelivery).Column("for_selfdelivery");
			Map(x => x.ForPromotionalSet).Column("for_promotional_set");
			Map(x => x.IncludeReclamation).Column("include_reclamation");
			Map(x => x.UseWithOtherDiscountReasons).Column("with_other_discounts");
			Map(x => x.DiscountWithOtherDiscounts).Column("other_discounts").CustomType<DiscountWithOtherDiscountsStringType>();
			Map(x => x.DiscountMinValue).Column("discount_min");
			Map(x => x.DiscountMaxValue).Column("discount_max");
			Map(x => x.DiscountValueType).Column("value_type").CustomType<DiscountValueTypeStringType>();


			References(x => x.SubdivisionPermission).Column("permission_subdivision_id");
			References(x => x.EmployeePermission).Column("permission_employee_id");
			References(x => x.PostPermission).Column("permission_post_id");
		}
	}

	public class DiscountMap : ClassMap<Discount>
	{
		public DiscountMap()
		{
			Table("discounts");
			Id(x => x.Id).Column("id").GeneratedBy.Native();
			Map(x => x.Type).Column("type").CustomType<DiscountRowTypeStringType>();
			Map(x => x.DiscountMinValue).Column("min_value");
			Map(x => x.DiscountMaxValue).Column("max_value");
			Map(x => x.DiscountValueType).Column("value_type").CustomType<DiscountValueTypeStringType>();
			Map(x => x.DiscountMinQuantity).Column("min_quantity");
			Map(x => x.DiscountMaxQuantity).Column("max_quantity");
			Map(x => x.AndBlockNum).Column("and_block_num");

			References(x => x.DiscountReason).Column("discount_reason_id");
		}
	}
}
