using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using QS.DomainModel.UoW;
using QS.Services;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Parameters;
using Vodovoz.Services;
using VodovozInfrastructure.Validators;

namespace Vodovoz.Validators.Orders {
    public abstract class OrderValidator : EntityValidator<OrderValidateParameters> {
        protected readonly ICurrentPermissionService currentPermissionService;
        protected readonly INomenclatureParametersProvider nomenclatureParametersProvider;
        protected readonly IUnitOfWork uow;
        private readonly OrderBase order;

        protected OrderValidator(ICurrentPermissionService currentPermissionService,
                                 INomenclatureParametersProvider nomenclatureParametersProvider,
                                 IUnitOfWork uow,
                                 OrderBase order) {
	        this.currentPermissionService = currentPermissionService ??
	                                        throw new ArgumentNullException(nameof(currentPermissionService));
	        this.nomenclatureParametersProvider = nomenclatureParametersProvider ??
	                                              throw new ArgumentNullException(nameof(nomenclatureParametersProvider));
	        this.uow = uow;
	        this.order = order;
        }
        
        public override IEnumerable<ValidationResult> Validate() {
	        
	        if(order.DeliveryPoint != null && (!order.DeliveryPoint.Latitude.HasValue || !order.DeliveryPoint.Longitude.HasValue)) {
		        yield return new ValidationResult("В точке доставки необходимо указать координаты.",
			        new[] { nameof(DeliveryPoint) });
	        }
	       
	        if(order.Counterparty == null)
		        yield return new ValidationResult("В заказе необходимо заполнить поле \"клиент\".",
			        new[] { nameof(order.Counterparty) });
	        
	        if(order.DeliveryDate == null || order.DeliveryDate == default(DateTime))
		        yield return new ValidationResult("В заказе не указана дата доставки.",
			        new[] { nameof(order.DeliveryDate) });
	        
	        if(order.ObservableOrderDepositItems.Any(x => x.Total < 0)) {
		        yield return new ValidationResult("В возврате залогов в заказе необходимо вводить положительную сумму.");
	        }
	        
	        if(!currentPermissionService.ValidatePresetPermission("can_can_create_order_in_advance")
	           && order.DeliveryDate.HasValue && order.DeliveryDate.Value < DateTime.Today
	           && order.Status <= OrderStatus.Accepted) {
		        yield return new ValidationResult(
			        "Указана дата заказа более ранняя чем сегодняшняя. Укажите правильную дату доставки.",
			        new[] { nameof(order.DeliveryDate) }
		        );
	        }
	        
	        if(order.ObservableOrderEquipments
	                .Where(x => x.Nomenclature.Category == NomenclatureCategory.equipment)
	                .Where(x => x.DirectionReason == DirectionReason.None && x.OwnType != OwnTypes.Duty)
	                .Any(x => x.Nomenclature?.SaleCategory != SaleCategory.forSale))
		        yield return new ValidationResult("У оборудования в заказе должна быть указана причина забор-доставки.");
	        
	        if(order.ObservableOrderEquipments
	                .Where(x => x.Nomenclature.Category == NomenclatureCategory.equipment)
	                .Any(x => x.OwnType == OwnTypes.None))
		        yield return new ValidationResult("У оборудования в заказе должна быть выбрана принадлежность.");

	        if (order.ObservableOrderItems.Any(x => x.Discount > 0 && x.DiscountReason == null))
		        yield return new ValidationResult(
			        "Если в заказе указана скидка на товар, то обязательно должно быть заполнено поле 'Основание'.");
        }

        public override IEnumerable<ValidationResult> Validate(OrderValidateParameters validateParameters) {
	        var result = Validate();

	        if (result.Any()) {
		        foreach (var validationResult in result) {
			        yield return validationResult;
		        }
	        }
	        
			if(validateParameters.OrderAction == OrderValidateAction.Close) {
				foreach(var equipment in order.OrderEquipments.Where(x => x.Direction == Direction.PickUp)) {
					if(!equipment.Confirmed && string.IsNullOrWhiteSpace(equipment.ConfirmedComment))
						yield return new ValidationResult(
							$"Забор оборудования {equipment.NameString} по заказу {order.Id} не произведен, а в комментарии не указана причина.",
							new[] { nameof(order.OrderEquipments) });
				}
			}
			
			if((validateParameters.OrderAction == OrderValidateAction.Accept /*|| validateParameters.WaitingForPayment*/) && order.Counterparty != null) {

				#region OrderStateKey

				var hasOrderItems = false;
				if(!order.ObservableOrderItems.Any() || 
				   (order.ObservableOrderItems.Count == 1 && order.ObservableOrderItems.Any(x => 
					   x.Nomenclature.Id == int.Parse(ParametersProvider.Instance.GetParameterValue("paid_delivery_nomenclature_id"))))) 
				{
					hasOrderItems = false;
				}
				else
				{
					hasOrderItems = true;
				}
				if(order.ObservableOrderDepositItems.Any() && 
				   ((order.PaymentType == PaymentType.cashless && hasOrderItems) || 
				    (order.PaymentType == PaymentType.cashless || order.PaymentType == PaymentType.ByCard) && !hasOrderItems))
					yield return new ValidationResult("Возврат залога не применим для заказа в текущем состоянии");
				
				#endregion OrderStateKey
				
				if(order.ObservableOrderItems.Any(x => x.Count <= 0) || 
				   order.ObservableOrderEquipments.Any(x => x.Count <= 0))
					yield return new ValidationResult("В заказе должно быть указано количество во всех позициях товара и оборудования");

				// Проверка соответствия цен в заказе ценам в номенклатуре
				string priceResult = "В заказе неверно указаны цены на следующие товары:\n";
				List<string> incorrectPriceItems = new List<string>();
				foreach(OrderItem item in order.ObservableOrderItems) {
					decimal fixedPrice = order.GetFixedPrice(item);
					decimal nomenclaturePrice = order.GetNomenclaturePrice(item);
					if(fixedPrice > 0m) {
						if(item.Price < fixedPrice) {
							incorrectPriceItems.Add(string.Format("{0} - цена: {1}, должна быть: {2}\n",
								item.NomenclatureString,
								item.Price,
								fixedPrice));
						}
					} else if(nomenclaturePrice > default(decimal) && item.Price < nomenclaturePrice) {
						incorrectPriceItems.Add(string.Format("{0} - цена: {1}, должна быть: {2}\n",
							item.NomenclatureString,
							item.Price,
							nomenclaturePrice));
					}
				}
				if(incorrectPriceItems.Any()) {
					foreach(string item in incorrectPriceItems) {
						priceResult += item;
					}
					yield return new ValidationResult(priceResult);
				}
				// Конец проверки цен
				
				if(order.Counterparty.IsDeliveriesClosed && order.PaymentType != PaymentType.cash && order.PaymentType != PaymentType.ByCard)
					yield return new ValidationResult(
						"В заказе неверно указан тип оплаты (для данного клиента закрыты поставки)",
						new[] { nameof(order.PaymentType) }
					);

				//FIXME Исправить изменение данных. В валидации нельзя менять объекты.
				if(order.DeliveryPoint != null && !order.DeliveryPoint.FindAndAssociateDistrict(uow))
					yield return new ValidationResult(
						"Район доставки не найден. Укажите правильные координаты или разметьте район доставки.",
						new[] { nameof(DeliveryPoint) }
					);


				
				/*
				//создание нескольких заказов на одну дату и точку доставки
				if(!order.SelfDelivery && order.DeliveryPoint != null) {
					var ordersForDeliveryPoints = orderRepository.GetLatestOrdersForDeliveryPoint(uow, order.DeliveryPoint)
																 .Where(
																	 o => o.Id != order.Id
																	 && o.DeliveryDate == order.DeliveryDate
																	 && !orderRepository.GetGrantedStatusesToCreateSeveralOrders().Contains(o.OrderStatus)
																	 && !o.IsService
																	);

					bool hasMaster = order.ObservableOrderItems.Any(i => i.Nomenclature.Category == NomenclatureCategory.master);

					if(!hasMaster
					   && !currentPermissionService.ValidatePresetPermission("can_create_several_orders_for_date_and_deliv_point")
					   && ordersForDeliveryPoints.Any()
					   && validationContext.Items.ContainsKey("IsCopiedFromUndelivery") && !(bool)validationContext.Items["IsCopiedFromUndelivery"]) {
						yield return new ValidationResult(
							string.Format("Создать заказ нельзя, т.к. для этой даты и точки доставки уже создан заказ №{0}", ordersForDeliveryPoints.First().Id),
							new[] { nameof(order.OrderEquipments) });
					}
				}*/
			}
        }

        public override IEnumerable<ValidationResult> Validate(ValidationContext validationContext) {
	        IEnumerable<ValidationResult> result;
	        
	        if (validationContext.Items.ContainsKey("OrderValidateParameters")) {
		        OrderValidateParameters orderValidateParameters =
			        (OrderValidateParameters) validationContext.Items["OrderValidateParameters"];
		        result = Validate(orderValidateParameters);
		        
		        if (result.Any()) {
			        foreach (var validationResult in result) {
				        yield return validationResult;
			        }
		        }
	        }
	        else {
		        result = Validate();

		        if (result.Any()) {
			        foreach (var validationResult in result) {
				        yield return validationResult;
			        }
		        }
	        }
        }
    }
}