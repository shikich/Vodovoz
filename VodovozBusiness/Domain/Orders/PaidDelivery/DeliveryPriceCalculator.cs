using System;
using System.Collections.Generic;
using System.Linq;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Sale;

namespace Vodovoz.Domain.Orders.PaidDelivery {
    public class DeliveryPriceCalculator {
        public decimal GetDeliveryPrice(OrderBase order) {

            var district = order.DeliveryPoint?.District;
            
            if (district != null) {
                if(order.DeliveryDate.HasValue) {
                    if(order.DeliveryDate.Value.Date == DateTime.Today && district.ObservableTodayDistrictRuleItems.Any()) {
                        return GetDeliveryPrice(district.ObservableTodayDistrictRuleItems, order);
                    }
                    
                    var dayOfWeekRules = 
                        district.GetWeekDayRuleItemCollectionByWeekDayName(District.ConvertDayOfWeekToWeekDayName(order.DeliveryDate.Value.DayOfWeek));
                    
                    if(dayOfWeekRules.Any()) {
                        return GetDeliveryPrice(dayOfWeekRules, order);
                    }
                }

                return GetDeliveryPrice(district.CommonDistrictRuleItems, order);
            }

            return 0m;
        }
        
        public bool IsOnlineStoreFreeDeliverySumReached(OrderBase order)
        {
            var SumToFreeDelivery = 
                order.DeliveryPoint?.District?.DistrictsSet.OnlineStoreOrderSumForFreeDelivery ?? 0m;
            var OnlineStoreItemsSum = 
                order.ObservableOrderItems.Sum(x => x.Nomenclature?.OnlineStoreExternalId != null ? x.ActualSum : 0m );
            
            return SumToFreeDelivery < OnlineStoreItemsSum;
        }
        
        private decimal GetDeliveryPrice<TRule>(IList<TRule> list, OrderBase order)
            where TRule : DistrictRuleItemBase
        {
            var result = list.Where(x => CompareWithDeliveryPriceRule(x.DeliveryPriceRule, order)).ToList();
            
            return result.Any() ? result.Max(x => x.Price) : 0m;
        }
        
        private bool CompareWithDeliveryPriceRule(IDeliveryPriceRule rule, OrderBase order) {
            
            var water19LCount = order.ObservableOrderItems
                                 .Where(x => x.Nomenclature != null && x.Nomenclature.IsWater19L)
                                 .Sum(x => x.Count);

            var disposableWater19LCount = GetDisposableWaterCount(order, TareVolume.Vol19L);
            var disposableWater6LCount = GetDisposableWaterCount(order, TareVolume.Vol6L);
            var disposableWater1500mlCount = GetDisposableWaterCount(order, TareVolume.Vol1500ml);
            var disposableWater600mlCount = GetDisposableWaterCount(order, TareVolume.Vol600ml);
                
            var total19LWater = water19LCount + disposableWater19LCount;
            decimal totalNo19LWater = (decimal)disposableWater6LCount / (decimal)rule.EqualsCount6LFor19L;
            totalNo19LWater += (decimal)disposableWater1500mlCount / (decimal)rule.EqualsCount1500mlFor19L;
            totalNo19LWater += (decimal)disposableWater600mlCount / (decimal)rule.EqualsCount600mlFor19L;
            total19LWater += (int)totalNo19LWater;

            bool result = total19LWater < rule.Water19LCount;

            return result;
        }

        private int GetDisposableWaterCount(OrderBase order, TareVolume tareVolume) {
            return (int)order.ObservableOrderItems.
                         Where(x => x.Nomenclature != null
                                             && x.Nomenclature.Category == NomenclatureCategory.water
                                             && x.Nomenclature.IsDisposableTare
                                             && x.Nomenclature.TareVolume == tareVolume)
                        .Sum(x => x.Count);
        }
    }
}