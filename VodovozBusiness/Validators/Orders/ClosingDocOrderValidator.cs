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
    public class ClosingDocOrderValidator : OrderValidator {
        private ClosingDocOrder order;

        public ClosingDocOrderValidator(ICurrentPermissionService currentPermissionService,
                                        INomenclatureParametersProvider nomenclatureParametersProvider,
                                        IUnitOfWork uow,
                                        ClosingDocOrder order) : base(currentPermissionService, nomenclatureParametersProvider, uow, order) {
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

            if(order.IsContractCloser && 
               !currentPermissionService.ValidatePresetPermission("can_set_contract_closer")) {
                yield return new ValidationResult(
                    "Недостаточно прав для подтверждения зыкрывашки по контракту. Обратитесь к руководителю.",
                    new[] { nameof(order.IsContractCloser) }
                );
            }

            if ((validateParameters.OrderAction == OrderValidateAction.Accept || 
                 validateParameters.OrderAction == OrderValidateAction.WaitForPayment) && order.Counterparty != null) {
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