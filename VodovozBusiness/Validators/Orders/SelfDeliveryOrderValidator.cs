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
    public class SelfDeliveryOrderValidator : OrderValidator {
        private SelfDeliveryOrder order;

        public SelfDeliveryOrderValidator(ICurrentPermissionService currentPermissionService,
                                          INomenclatureParametersProvider nomenclatureParametersProvider,
                                          IUnitOfWork uow,
                                          SelfDeliveryOrder order) : base(currentPermissionService, nomenclatureParametersProvider, uow, order) {
            this.order = order;
        }
        public override IEnumerable<ValidationResult> Validate() {
            var result = base.Validate();
            
            if (result.Any()) {
                foreach (var validationResult in result) {
                    yield return validationResult;
                }
            }
            
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
            
            if(order.PaymentType == PaymentType.ContractDoc) {
                yield return new ValidationResult(
                    "Тип оплаты - контрактная документация невозможен для самовывоза",
                    new[] { nameof(order.PaymentType) }
                );
            }
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
                
                if (order.BottlesReturn.HasValue && order.BottlesReturn > 0 && order.GetTotalWater19LCount() == 0 &&
                    order.ReturnTareReason == null)
                    yield return new ValidationResult("Необходимо указать причину забора тары.",
                        new[] {nameof(order.ReturnTareReason)});
                
                if (order.BottlesReturn.HasValue && order.BottlesReturn > 0 &&
                    order.GetTotalWater19LCount() == 0 &&
                    order.ReturnTareReasonCategory == null)
                    yield return new ValidationResult("Необходимо указать категорию причины забора тары.",
                        new[] {nameof(order.ReturnTareReasonCategory)});

                if(order.BottlesReturn == null && 
                   order.ObservableOrderItems.Any(x => x.Nomenclature.Category == NomenclatureCategory.water && !x.Nomenclature.IsDisposableTare))
                    yield return new ValidationResult("В заказе не указана планируемая тара.",
                        new[] { nameof(order.BottlesReturn) });
                
                //если ни у точки доставки, ни у контрагента нет ни одного номера телефона
                if(!((order.DeliveryPoint != null && order.DeliveryPoint.Phones.Any()) || order.Counterparty.Phones.Any()))
                    yield return new ValidationResult("Ни для контрагента, ни для точки доставки заказа не указано ни одного номера телефона.");
            }
        }
    }
}