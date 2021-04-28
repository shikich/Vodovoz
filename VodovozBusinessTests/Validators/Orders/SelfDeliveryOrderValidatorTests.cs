using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using QS.DomainModel.UoW;
using QS.Services;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Services;
using Vodovoz.Validators.Orders;

namespace VodovozBusinessTests.Validators.Orders {
    [TestFixture]
    public class SelfDeliveryOrderValidatorTests {

        #region OrderValidator без параметров
        
        [Test(Description = "Проверка валидирования заказа-самовывоза без контрагента")]
        public void ValidateSelfDeliveryOrderWithoutCounterparty()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =  new ValidationResult("В заказе необходимо заполнить поле \"клиент\".",
                new[] {nameof(selfDeliveryOrderMock.Counterparty)});
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза без даты доставки")]
        public void ValidateSelfDeliveryOrderWithoutDeliveryDate()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes = new ValidationResult("В заказе не указана дата доставки.",
                new[] { nameof(selfDeliveryOrderMock.DeliveryDate) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза с отрицательным залогом")]
        public void ValidateSelfDeliveryOrderWithNegativeDeposit()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            OrderDepositItem orderDepositItemMock = Substitute.For<OrderDepositItem>();
            orderDepositItemMock.Total.Returns(-250);

            GenericObservableList<OrderDepositItem> observableDepositItems =
                new GenericObservableList<OrderDepositItem> {orderDepositItemMock};
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItems);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes = new ValidationResult("В возврате залогов в заказе необходимо вводить положительную сумму.");
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        [Test(Description = "Проверка валидирования заказа-самовывоза с оборудованием, без причины забора-доставки")]
        public void ValidateSelfDeliveryOrderWithOrderEquipmentsWithoutDirectionReason() {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);

            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            nomenclatureMock1.Category.Returns(NomenclatureCategory.equipment);
            Nomenclature nomenclatureMock2 = Substitute.For<Nomenclature>();
            nomenclatureMock2.Category.Returns(NomenclatureCategory.water);
            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            orderEquipmentMock1.Nomenclature.Returns(nomenclatureMock1);
            OrderEquipment orderEquipmentMock2 = Substitute.For<OrderEquipment>();
            orderEquipmentMock2.Nomenclature.Returns(nomenclatureMock2);

            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                    new GenericObservableList<OrderEquipment> { orderEquipmentMock1, orderEquipmentMock2 };

            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("У оборудования в заказе должна быть указана причина забор-доставки.");
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза с оборудованием, без принадлежности")]
        public void ValidateSelfDeliveryOrderWithOrderEquipmentsWithoutOwnType()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);

            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            nomenclatureMock1.Category.Returns(NomenclatureCategory.equipment);
            Nomenclature nomenclatureMock2 = Substitute.For<Nomenclature>();
            nomenclatureMock2.Category.Returns(NomenclatureCategory.water);
            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            orderEquipmentMock1.Nomenclature.Returns(nomenclatureMock1);
            OrderEquipment orderEquipmentMock2 = Substitute.For<OrderEquipment>();
            orderEquipmentMock2.Nomenclature.Returns(nomenclatureMock2);

            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                    new GenericObservableList<OrderEquipment> { orderEquipmentMock1, orderEquipmentMock2 };

            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, unitOfWorkMock, selfDeliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("У оборудования в заказе должна быть выбрана принадлежность.");
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        [Test(Description = "Проверка валидирования заказа-самовывоза с товарами со скидкой, без указания причины скидки")]
        public void ValidateSelfDeliveryOrderWithOrderItemsWithDiscountWithoutDisountReason()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            OnlineStore onlineStoreMock = Substitute.For<OnlineStore>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.OnlineStore.Returns(onlineStoreMock);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Discount.Returns(25m);
            orderItemMock.IsDiscountInMoney.Returns(true);
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            orderItemMock.DiscountReason = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItems =
                new GenericObservableList<OrderItem>{ orderItemMock };
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItems);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(154);
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "Если в заказе указана скидка на товар, то обязательно должно быть заполнено поле 'Основание'.");
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        #endregion

        #region OrderValidator с параметрами

        [Test(Description = "Проверка валидирования заказа-самовывоза с забором оборудования, без указания причины не забора в комментарии")]
        public void ValidateSelfDeliveryOrderWithOrderEquipmentsWithDirectionPickUpWithoutComment()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            nomenclatureMock1.Category.Returns(NomenclatureCategory.equipment);
            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            orderEquipmentMock1.Nomenclature.Returns(nomenclatureMock1);
            orderEquipmentMock1.Direction.Returns(Direction.PickUp);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                new GenericObservableList<OrderEquipment> { orderEquipmentMock1 };

            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Close } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    $"Забор оборудования {orderEquipmentMock1.NameString} по заказу {selfDeliveryOrderMock.Id} не произведен, а в комментарии не указана причина.",
                    new[] { nameof(selfDeliveryOrderMock.OrderEquipments) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза с залогами, для типа оплаты cashless")]
        public void ValidateSelfDeliveryOrderWithOrderDepositItemsAndPaymentTypeCashless()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            OrderDepositItem orderDepositItemMock = Substitute.For<OrderDepositItem>();
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock1);
        
            GenericObservableList<OrderDepositItem> observableDepositItems =
                new GenericObservableList<OrderDepositItem> {orderDepositItemMock};
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItems);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Возврат залога не применим для заказа в текущем состоянии");
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        [Test(Description = "Проверка валидирования заказа-самовывоза с товарами или оборудованием с количеством <= 0")]
        public void ValidateSelfDeliveryOrderWithOrderItemsCountOrOrderEquipmentsCountEqualsZero()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderEquipment orderEquipmentMock = Substitute.For<OrderEquipment>();
            orderEquipmentMock.Nomenclature.Returns(nomenclatureMock);

            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                new GenericObservableList<OrderEquipment> { orderEquipmentMock };
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("В заказе должно быть указано количество во всех позициях товара и оборудования");
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза для клиента с закрытыми поставками и определенными типами оплат")]
        public void ValidateSelfDeliveryOrderForCounterpartyWithClosedDeliveries()
        {
            // arrange
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.IsDeliveriesClosed.Returns(true);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,uowMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "В заказе неверно указан тип оплаты (для данного клиента закрыты поставки)",
                    new[] { nameof(selfDeliveryOrderMock.PaymentType) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза с точкой доставки без района")]
        public void ValidateSelfDeliveryOrderWithDeliveryPointWithoutDistrict()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "Район доставки не найден. Укажите правильные координаты или разметьте район доставки.",
                    new[] { nameof(DeliveryPoint) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        #endregion
        
        #region SelfDeliveryOrderValidator

        [Test(Description = "Проверка валидирования заказа-самовывоза с оплатой по карте, без заполненного номера онлайн-заказа")]
        public void ValidateSelfDeliveryOrderWithPaymentTypeByCardWithoutOrderNumberFromOnlineStore()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.ByCard);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Если в заказе выбран тип оплаты по карте, необходимо заполнить номер онлайн заказа.",
                    new[] { nameof(selfDeliveryOrderMock.OrderNumberFromOnlineStore) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза с оплатой по карте, без указания откуда оплата")]
        public void ValidateSelfDeliveryOrderWithPaymentTypeByCardWithoutPaymentFrom()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.ByCard);
            selfDeliveryOrderMock.PaymentByCardFrom = null;
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "Выбран тип оплаты по карте. Необходимо указать откуда произведена оплата.",
                    new[] { nameof(selfDeliveryOrderMock.PaymentByCardFrom) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза с номенклатурой из интернет-магазина, без указания номера заказа ИМ")]
        public void ValidateSelfDeliveryOrderWithOrderItemsFromWebStoreWithoutEShopOrder()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.EShopOrder = null;
            OnlineStore onlineStoreMock = Substitute.For<OnlineStore>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.OnlineStore.Returns(onlineStoreMock);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItems =
                new GenericObservableList<OrderItem>{ orderItemMock };
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItems);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
        
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.RootProductGroupForOnlineStoreNomenclatures.Returns(5);
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "При добавлении в заказ номенклатур с группой товаров интернет-магазина необходимо указать номер заказа интернет-магазина.",
                    new[] { nameof(selfDeliveryOrderMock.EShopOrder) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза с контрактной документацией")]
        public void ValidateSelfDeliveryOrderWithPaymentTypeContractDoc()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.ContractDoc);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "Тип оплаты - контрактная документация невозможен для самовывоза",
                    new[] { nameof(selfDeliveryOrderMock.PaymentType) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        #endregion

        #region SelfDeliveryOrderValidator с параметрами

        [Test(Description = "Проверка валидирования заказа-самовывоза в определенных условиях")]
        public void ValidateSelfDeliveryOrderOrderStateKey2()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.ByCard);
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            selfDeliveryOrderMock.BottlesReturn.Returns(5);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Невозможно подтвердить или перевести в статус ожидания оплаты заказ в текущем состоянии без суммы");
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза в определенных условиях")]
        public void ValidateSelfDeliveryOrderOrderStateKey3()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            selfDeliveryOrderMock.BottlesReturn.Returns(5);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Невозможно подтвердить или перевести в статус ожидания оплаты пустой заказ");
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза без продажи воды, но с забором тары и без указания причины этого забора")]
        public void ValidateSelfDeliveryOrderWithBottlesReturnWithoutReturnTareReason()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            selfDeliveryOrderMock.BottlesReturn.Returns(5);
            selfDeliveryOrderMock.ReturnTareReason = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
            Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
            Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
            Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
        
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
            Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
            nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Необходимо указать причину забора тары.",
                    new[] {nameof(selfDeliveryOrderMock.ReturnTareReason)});
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза без продажи воды, но с забором тары и без указания категории причины этого забора")]
        public void ValidateSelfDeliveryOrderWithBottlesReturnWithoutReturnTareReasonCategory()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            selfDeliveryOrderMock.BottlesReturn.Returns(5);
            selfDeliveryOrderMock.ReturnTareReasonCategory = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
        
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Необходимо указать категорию причины забора тары.",
                    new[] {nameof(selfDeliveryOrderMock.ReturnTareReasonCategory)});
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза с продажей воды, без возвратной тары")]
        public void ValidateSelfDeliveryOrderWithWaterAndWithoutBottlesReturn()
        {
            // arrange
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock.ProductGroup = null;
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();

            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            selfDeliveryOrderMock.BottlesReturn = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItems =
                new GenericObservableList<OrderItem>{ orderItemMock };
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItems);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
        
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(154);
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("В заказе не указана планируемая тара.",
                    new[] { nameof(selfDeliveryOrderMock.Contract) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-самовывоза выставленного на контрагента без телефонов")]
        public void ValidateSelfDeliveryOrderWithCounterpartyWithoutPhones()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(selfDeliveryOrderMock.OrderDepositItems);
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(selfDeliveryOrderMock.OrderItems);
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(selfDeliveryOrderMock.OrderEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
        
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
            SelfDeliveryOrderValidator validator = new SelfDeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, selfDeliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Ни для контрагента, ни для точки доставки заказа не указано ни одного номера телефона.");
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        #endregion
    }
}