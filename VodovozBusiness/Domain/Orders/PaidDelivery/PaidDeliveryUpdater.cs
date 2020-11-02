using System;
using System.Linq;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Goods;
using Vodovoz.EntityFactories;
using Vodovoz.EntityRepositories.Goods;

namespace Vodovoz.Domain.Orders.PaidDelivery {
    public class PaidDeliveryUpdater {

        private readonly OrderBase order;
        private OrderItem paidDelivery;
        private readonly OrderItemFactory orderItemFactory;
        private readonly INomenclatureRepository nomenclatureRepository;
        private readonly DeliveryPriceCalculator deliveryPriceCalculator;

        public PaidDeliveryUpdater(OrderBase order,
                                   OrderItemFactory orderItemFactory,
                                   INomenclatureRepository nomenclatureRepository,
                                   DeliveryPriceCalculator deliveryPriceCalculator) {
            this.order = order;
            this.orderItemFactory = orderItemFactory;
            this.nomenclatureRepository = nomenclatureRepository ?? throw new ArgumentNullException(nameof(nomenclatureRepository));
            this.deliveryPriceCalculator = deliveryPriceCalculator ?? throw new ArgumentNullException(nameof(deliveryPriceCalculator));
        }
        
        public void UpdatePaidDelivery(IUnitOfWork uow) {

            if (paidDelivery == null) {
                paidDelivery = orderItemFactory.Create();
                paidDelivery.Count = 1;
                paidDelivery.Nomenclature = nomenclatureRepository.GetPaidDeliveryNomenclature(uow);
            }
            
            var paidDeliveryItem =
                order.ObservableOrderItems.SingleOrDefault(x => x.Nomenclature.Id == paidDelivery.Nomenclature.Id);
            
            var isFreeDelivery = HasFreeDelivery(paidDelivery.Nomenclature.Id);

            if (isFreeDelivery) {
                if (paidDeliveryItem != null) {
                    order.ObservableOrderItems.Remove(paidDeliveryItem);
                }
                
                return;
            }

            var price = deliveryPriceCalculator.GetDeliveryPrice(order);

            if(price != 0) {
                if (paidDeliveryItem == null) {
                    paidDeliveryItem = paidDelivery;
                    paidDeliveryItem.Price = price;
                    paidDeliveryItem.NewOrder = order;
                    
                    order.ObservableOrderItems.Add(paidDeliveryItem);
                }
                else if (paidDeliveryItem.Price != price) {
                    paidDeliveryItem.Price = price;
                }
            }
            else if(paidDeliveryItem != null)
                order.ObservableOrderItems.Remove(paidDeliveryItem);
        }

        private bool HasFreeDelivery(int paidDeliveryNomenclatureId) {
            switch (order.Type) {
                case OrderType.SelfDeliveryOrder:
                case OrderType.VisitingMasterOrder:
                    return true;
                case OrderType.DeliveryOrder:
                    var deliveryOrder = order as DeliveryOrder;
                    return deliveryOrder.DeliveryPoint.AlwaysFreeDelivery
                           || deliveryOrder.ObservableOrderItems.Any(n =>
                               n.Nomenclature.Category == NomenclatureCategory.spare_parts)
                           || !deliveryOrder.ObservableOrderItems.Any(
                               n => n.Nomenclature.Id != paidDeliveryNomenclatureId)
                           && (deliveryOrder.BottlesReturn > 0 || deliveryOrder.ObservableOrderEquipments.Any() ||
                               deliveryOrder.ObservableOrderDepositItems.Any())
                           || deliveryPriceCalculator.IsOnlineStoreFreeDeliverySumReached(order);
                case OrderType.OrderFrom1c:
                case OrderType.ClosingDocOrder:
                    return order.DeliveryPoint.AlwaysFreeDelivery
                           || order.ObservableOrderItems.Any(n =>
                               n.Nomenclature.Category == NomenclatureCategory.spare_parts)
                           || !order.ObservableOrderItems.Any(
                               n => n.Nomenclature.Id != paidDeliveryNomenclatureId)
                           && (order.ObservableOrderEquipments.Any() || order.ObservableOrderDepositItems.Any())
                           || deliveryPriceCalculator.IsOnlineStoreFreeDeliverySumReached(order);
            }

            return false;
        }
    }
}