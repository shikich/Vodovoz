using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QS.DomainModel.UoW;
using QS.Services;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Services;

namespace Vodovoz.Validators.Orders {
    public class DeliveryOrderValidator : OrderValidator {
        private DeliveryOrder order;
        private readonly IOrderRepository orderRepository;

        public DeliveryOrderValidator(ICurrentPermissionService currentPermissionService,
                                      INomenclatureParametersProvider nomenclatureParametersProvider,
                                      IOrderRepository orderRepository,
                                      IUnitOfWork uow,
                                      DeliveryOrder order) : base(currentPermissionService, nomenclatureParametersProvider, uow, order) {
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.order = order;
        }
        public override IEnumerable<ValidationResult> Validate() {
            var result = base.Validate();
            
            if (result.Any()) {
                foreach (var validationResult in result) {
                    yield return validationResult;
                }
            }
            
            if(order.DeliveryPoint == null)
                yield return new ValidationResult("В заказе необходимо заполнить точку доставки.",
                    new[] { nameof(order.DeliveryPoint) });
            
            if(order.PaymentType == PaymentType.ByCard && order.OrderNumberFromOnlineStore == null)
                yield return new ValidationResult("Если в заказе выбран тип оплаты по карте, необходимо заполнить номер онлайн заказа.",
                    new[] { nameof(order.OrderNumberFromOnlineStore) });
            
            if (order.ObservableOrderItems.Any(oi => !string.IsNullOrWhiteSpace(oi.Nomenclature.OnlineStoreExternalId))
                && order.EShopOrder == null)
            {
                yield return new ValidationResult(
                    "В заказе есть товары ИМ, но не указан номер заказа ИМ",
                    new[] { nameof(order.EShopOrder) }
                );
            }
            
            if(order.PaymentType == PaymentType.ByCard && order.PaymentByCardFrom == null)
                yield return new ValidationResult(
                    "Выбран тип оплаты по карте. Необходимо указать откуда произведена оплата.",
                    new[] { nameof(order.PaymentByCardFrom) }
                );
        }

        public override IEnumerable<ValidationResult> Validate(OrderValidateParameters validateParameters) {
            IEnumerable<ValidationResult> result;
            
            result = Validate();
            
            if (result.Any()) {
                foreach (var validationResult in result) {
                    yield return validationResult;
                }
            }
            
            result = base.Validate(validateParameters);

            if (result.Any()) {
                foreach (var validationResult in result) {
                    yield return validationResult;
                }
            }

            if ((validateParameters.OrderAction == OrderValidateAction.Accept || 
                 validateParameters.OrderAction == OrderValidateAction.WaitForPayment) && order.Counterparty != null) {
                if (order.BottlesReturn.HasValue && order.BottlesReturn > 0 && order.GetTotalWater19LCount() == 0 &&
                    order.ReturnTareReason == null)
                    yield return new ValidationResult("Необходимо указать причину забора тары.",
                        new[] {nameof(order.ReturnTareReason)});
                
                if (order.BottlesReturn.HasValue && order.BottlesReturn > 0 &&
                    order.GetTotalWater19LCount() == 0 &&
                    order.ReturnTareReasonCategory == null)
                    yield return new ValidationResult("Необходимо указать категорию причины забора тары.",
                        new[] {nameof(order.ReturnTareReasonCategory)});
                
                #region OrderStateKey
                
                var hasOrderItems = false;
                if(!order.ObservableOrderItems.Any() || 
                   (order.ObservableOrderItems.Count == 1 && order.ObservableOrderItems.Any(x => 
                       x.Nomenclature.Id == nomenclatureParametersProvider.PaidDeliveryNomenclatureId))) 
                {
                    hasOrderItems = false;
                }
                else
                {
                    hasOrderItems = true;
                }
                
                if(!hasOrderItems && !order.ObservableOrderDepositItems.Any() && (order.PaymentType == PaymentType.ByCard &&
                    (order.ObservableOrderEquipments.Any() || (!order.ObservableOrderEquipments.Any() && order.BottlesReturn > 0)))) {
                    yield return new ValidationResult("Невозможно подтвердить или перевести в статус ожидания оплаты заказ в текущем состоянии без суммы");
                }
					
                if(!hasOrderItems && !order.ObservableOrderDepositItems.Any() && order.BottlesReturn > 0 && 
                   !order.ObservableOrderEquipments.Any()) {
                    yield return new ValidationResult("Невозможно подтвердить или перевести в статус ожидания оплаты пустой заказ");
                }

                #endregion
                
                if(order.DeliverySchedule == null)
                    yield return new ValidationResult("В заказе не указано время доставки.",
                    new[] { nameof(order.DeliverySchedule) });
                
                if(order.Trifle == null && order.TotalSum > 0m &&
                   (order.PaymentType == PaymentType.cash || order.PaymentType == PaymentType.BeveragesWorld))
                    yield return new ValidationResult("В заказе не указана сдача.",
                        new[] { nameof(order.Trifle) });
                
                if(order.BottlesReturn == null && 
                   order.ObservableOrderItems.Any(x => x.Nomenclature.Category == NomenclatureCategory.water && !x.Nomenclature.IsDisposableTare))
                    yield return new ValidationResult("В заказе не указана планируемая тара.",
                    new[] { nameof(order.BottlesReturn) });
                
                //если ни у точки доставки, ни у контрагента нет ни одного номера телефона
                if(!((order.DeliveryPoint != null && order.DeliveryPoint.Phones.Any()) || order.Counterparty.Phones.Any()))
                    yield return new ValidationResult("Ни для контрагента, ни для точки доставки заказа не указано ни одного номера телефона.");
                
                if(order.DeliveryPoint != null) {
                    if(string.IsNullOrWhiteSpace(order.DeliveryPoint.Entrance)) {
                        yield return new ValidationResult("Не заполнена парадная в точке доставки");
                    }
                    if(string.IsNullOrWhiteSpace(order.DeliveryPoint.Floor)) {
                        yield return new ValidationResult("Не заполнен этаж в точке доставки");
                    }
                    if(string.IsNullOrWhiteSpace(order.DeliveryPoint.Room)) {
                        yield return new ValidationResult("Не заполнен номер помещения в точке доставки");
                    }
                }
                
                //FIXME Исправить проверку
                //создание нескольких заказов на одну дату и точку доставки
                if(order.DeliveryPoint != null) {
                    var ordersForDeliveryPoints = orderRepository.GetLatestOrdersForDeliveryPoint(uow, order.DeliveryPoint)
                                                                 .Where(
                                                                     o => o.Id != order.Id
                                                                          && o.DeliveryDate == order.DeliveryDate
                                                                          && !orderRepository.GetGrantedStatusesToCreateSeveralOrders().Contains(o.OrderStatus)
                                                                          && !o.IsService
                                                                 );

                    if(!currentPermissionService.ValidatePresetPermission("can_create_several_orders_for_date_and_deliv_point")
                       && ordersForDeliveryPoints.Any()
                       && !validateParameters.CreatedFromUndeliveryOrder) {
                        yield return new ValidationResult(
                            $"Создать заказ нельзя, т.к. для этой даты и точки доставки уже создан заказ №{ordersForDeliveryPoints.First().Id}",
                            new[] { nameof(order.OrderEquipments) });
                    }
                }
            }
        }
    }
}