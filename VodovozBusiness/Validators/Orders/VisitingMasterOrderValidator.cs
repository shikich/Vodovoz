using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QS.DomainModel.UoW;
using QS.Services;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Services;

namespace Vodovoz.Validators.Orders {
    public class VisitingMasterOrderValidator : OrderValidator {
        private VisitingMasterOrder order;

        public VisitingMasterOrderValidator(ICurrentPermissionService currentPermissionService,
                                            INomenclatureParametersProvider nomenclatureParametersProvider,
                                            IUnitOfWork uow,
                                            VisitingMasterOrder order) : base(currentPermissionService, nomenclatureParametersProvider, uow, order) {
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

        public override IEnumerable<ValidationResult> Validate(OrderValidateParameters validateParameters)
        {
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

            if(order.PaymentType == PaymentType.cashless
               && validateParameters.OrderAction == OrderValidateAction.Accept
               && !currentPermissionService.ValidatePresetPermission("can_accept_cashles_service_orders")) {
                yield return new ValidationResult(
                    "Недостаточно прав для подтверждения безнального сервисного заказа. Обратитесь к руководителю.",
                    new[] { nameof(order.Status) }
                );
            }

            if ((validateParameters.OrderAction == OrderValidateAction.Accept || 
                 validateParameters.OrderAction == OrderValidateAction.WaitForPayment) && order.Counterparty != null) {
                if (order.DeliverySchedule == null)
                    yield return new ValidationResult("В заказе не указано время доставки.",
                        new[] {nameof(order.DeliverySchedule)});
                
                if(order.Trifle == null && order.TotalSum > 0m &&
                   (order.PaymentType == PaymentType.cash || order.PaymentType == PaymentType.BeveragesWorld))
                    yield return new ValidationResult("В заказе не указана сдача.",
                        new[] { nameof(order.Trifle) });
                
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
            }
        }
    }
}