namespace Vodovoz.Domain.Sale {
    public interface IDistrictRuleItem {
        DeliveryPriceRule DeliveryPriceRule { get; set; }
        decimal Price { get; set; }
    }
}