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
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Services;
using Vodovoz.Validators.Orders;

namespace VodovozBusinessTests.Validators.Orders {
    [TestFixture]
    public class DeliveryOrderValidatorTests {
        
        #region OrderValidator без параметров
        
        [Test(Description = "Проверка валидирования заказа-доставки без контрагента")]
        public void ValidateDeliveryOrderWithoutCounterparty()
        {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock,unitOfWorkMock, deliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =  new ValidationResult("В заказе необходимо заполнить поле \"клиент\".",
                new[] {nameof(deliveryOrderMock.Counterparty)});
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-доставки без даты доставки")]
        public void ValidateDeliveryOrderWithoutDeliveryDate()
        {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes = new ValidationResult("В заказе не указана дата доставки.",
                new[] { nameof(deliveryOrderMock.DeliveryDate) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-доставки с отрицательным залогом")]
        public void ValidateDeliveryOrderWithNegativeDeposit()
        {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            OrderDepositItem orderDepositItemMock = Substitute.For<OrderDepositItem>();
            orderDepositItemMock.Total.Returns(-250);

            GenericObservableList<OrderDepositItem> observableDepositItems =
                new GenericObservableList<OrderDepositItem> {orderDepositItemMock};
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItems);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes = new ValidationResult("В возврате залогов в заказе необходимо вводить положительную сумму.");
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        [Test(Description = "Проверка валидирования заказа-доставки с оборудованием, без причины забора-доставки")]
        public void ValidateDeliveryOrderWithOrderEquipmentsWithoutDirectionReason() {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);

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

            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования заказа-доставки с оборудованием, без принадлежности")]
        public void ValidateDeliveryOrderWithOrderEquipmentsWithoutOwnType()
        {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);

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

            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
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

        [Test(Description = "Проверка валидирования заказа-доставки с товарами со скидкой, без указания причины скидки")]
        public void ValidateDeliveryOrderWithOrderItemsWithDiscountWithoutDisountReason()
        {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            OnlineStore onlineStoreMock = Substitute.For<OnlineStore>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.OnlineStore.Returns(onlineStoreMock);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Discount.Returns(25m);
            orderItemMock.IsDiscountInMoney.Returns(true);
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            orderItemMock.DiscountReason = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItems =
                new GenericObservableList<OrderItem>{ orderItemMock };
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItems);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
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

        [Test(Description = "Проверка валидирования заказа-доставки с забором оборудования, без указания причины не забора в комментарии")]
        public void ValidateDeliveryOrderWithOrderEquipmentsWithDirectionPickUpWithoutComment()
        {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            nomenclatureMock1.Category.Returns(NomenclatureCategory.equipment);
            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            orderEquipmentMock1.Nomenclature.Returns(nomenclatureMock1);
            orderEquipmentMock1.Direction.Returns(Direction.PickUp);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                new GenericObservableList<OrderEquipment> { orderEquipmentMock1 };

            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Close } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    $"Забор оборудования {orderEquipmentMock1.NameString} по заказу {deliveryOrderMock.Id} не произведен, а в комментарии не указана причина.",
                    new[] { nameof(deliveryOrderMock.OrderEquipments) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-доставки с залогами, для типа оплаты cashless")]
        public void ValidateDeliveryOrderWithOrderDepositItemsAndPaymentTypeCashless()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            OrderDepositItem orderDepositItemMock = Substitute.For<OrderDepositItem>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            deliveryOrderMock.Counterparty.Returns(counterpartyMock1);
        
            GenericObservableList<OrderDepositItem> observableDepositItems =
                new GenericObservableList<OrderDepositItem> {orderDepositItemMock};
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItems);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
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

        [Test(Description = "Проверка валидирования заказа-доставки с товарами или оборудованием с количеством <= 0")]
        public void ValidateDeliveryOrderWithOrderItemsCountOrOrderEquipmentsCountEqualsZero()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderEquipment orderEquipmentMock = Substitute.For<OrderEquipment>();
            orderEquipmentMock.Nomenclature.Returns(nomenclatureMock);

            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                new GenericObservableList<OrderEquipment> { orderEquipmentMock };
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования заказа-доставки для клиента с закрытыми поставками и определенными типами оплат")]
        public void ValidateDeliveryOrderForCounterpartyWithClosedDeliveries()
        {
            // arrange
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.IsDeliveriesClosed.Returns(true);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            deliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, uowMock, deliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "В заказе неверно указан тип оплаты (для данного клиента закрыты поставки)",
                    new[] { nameof(deliveryOrderMock.PaymentType) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-доставки с точкой доставки без района")]
        public void ValidateDeliveryOrderWithDeliveryPointWithoutDistrict()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
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

        #region DeliveryOrderValidator без параметров

        [Test(Description = "Проверка валидирования заказа-доставки без точки доставки")]
        public void ValidateDeliveryOrderWithoutDeliveryPoint()
        {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.DeliveryPoint = null;
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =  new ValidationResult("В заказе необходимо заполнить точку доставки.",
                new[] { nameof(deliveryOrderMock.DeliveryPoint) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        [Test(Description = "Проверка валидирования заказа-доставки с оплатой по карте, без заполненного номера онлайн-заказа")]
        public void ValidateDeliveryOrderWithPaymentTypeByCardWithoutOrderNumberFromOnlineStore()
        {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.PaymentType.Returns(PaymentType.ByCard);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Если в заказе выбран тип оплаты по карте, необходимо заполнить номер онлайн заказа.",
                    new[] { nameof(deliveryOrderMock.OrderNumberFromOnlineStore) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-доставки с оплатой по карте, без указания откуда оплата")]
        public void ValidateDeliveryOrderWithPaymentTypeByCardWithoutPaymentFrom()
        {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.PaymentType.Returns(PaymentType.ByCard);
            deliveryOrderMock.PaymentByCardFrom = null;
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "Выбран тип оплаты по карте. Необходимо указать откуда произведена оплата.",
                    new[] { nameof(deliveryOrderMock.PaymentByCardFrom) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-доставки с номенклатурой из интернет-магазина, без указания номера заказа ИМ")]
        public void ValidateDeliveryOrderWithOrderItemsFromWebStoreWithoutEShopOrder()
        {
            // arrange
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.EShopOrder = null;
            OnlineStore onlineStoreMock = Substitute.For<OnlineStore>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.OnlineStore.Returns(onlineStoreMock);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItems =
                new GenericObservableList<OrderItem>{ orderItemMock };
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItems);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
        
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.RootProductGroupForOnlineStoreNomenclatures.Returns(5);
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "При добавлении в заказ номенклатур с группой товаров интернет-магазина необходимо указать номер заказа интернет-магазина.",
                    new[] { nameof(deliveryOrderMock.EShopOrder) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        #endregion

        #region DeliveryOrderValidator с параметрами

        [Test(Description = "Проверка валидирования заказа-доставки в определенных условиях")]
        public void ValidateDeliveryOrderOrderStateKey2()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.PaymentType.Returns(PaymentType.ByCard);
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            deliveryOrderMock.BottlesReturn.Returns(5);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования заказа-доставки в определенных условиях")]
        public void ValidateDeliveryOrderOrderStateKey3()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            deliveryOrderMock.BottlesReturn.Returns(5);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования заказа-доставки без продажи воды, но с забором тары и без указания причины этого забора")]
        public void ValidateDeliveryOrderWithBottlesReturnWithoutReturnTareReason()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            deliveryOrderMock.BottlesReturn.Returns(5);
            deliveryOrderMock.ReturnTareReason = null;
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Необходимо указать причину забора тары.",
                    new[] {nameof(deliveryOrderMock.ReturnTareReason)});
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-доставки без продажи воды, но с забором тары и без указания категории причины этого забора")]
        public void ValidateDeliveryOrderWithBottlesReturnWithoutReturnTareReasonCategory()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            deliveryOrderMock.BottlesReturn.Returns(5);
            deliveryOrderMock.ReturnTareReasonCategory = null;
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Необходимо указать категорию причины забора тары.",
                    new[] {nameof(deliveryOrderMock.ReturnTareReasonCategory)});
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-доставки без времени доставки")]
        public void ValidateDeliveryOrderWithoutDeliverySchedule()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            deliveryOrderMock.DeliverySchedule = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("В заказе не указано время доставки.",
                    new[] { nameof(deliveryOrderMock.DeliverySchedule) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-доставки за наличку, без указания сдачи")]
        public void ValidateDeliveryOrderWithWithPaymentTypeCashWithoutTrifle()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            deliveryOrderMock.PaymentType.Returns(PaymentType.cash);
            deliveryOrderMock.TotalSum.Returns(5m);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("В заказе не указана сдача.",
                    new[] { nameof(deliveryOrderMock.Trifle) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        [Test(Description = "Проверка валидирования заказа-доставки с продажей воды, без возвратной тары")]
        public void ValidateDeliveryOrderWithWaterAndWithoutBottlesReturn()
        {
            // arrange
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock.ProductGroup = null;
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            deliveryOrderMock.BottlesReturn = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItems =
                new GenericObservableList<OrderItem>{ orderItemMock };
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItems);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(154);
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);

            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("В заказе не указана планируемая тара.",
                    new[] { nameof(deliveryOrderMock.Contract) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-доставки выставленного на контрагента без телефонов")]
        public void ValidateDeliveryOrderWithCounterpartyWithoutPhones()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования заказа-доставки с точкой доставки без: парадной, этажа, номера помещения")]
        public void ValidateDeliveryOrderWithDeliveryPointWithoutFloor()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
            deliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(deliveryOrderMock.OrderDepositItems);
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(deliveryOrderMock.OrderItems);
            deliveryOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(deliveryOrderMock.OrderEquipments);
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            IOrderRepository orderRepositoryMock = Substitute.For<IOrderRepository>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            DeliveryOrderValidator validator = new DeliveryOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock, orderRepositoryMock, unitOfWorkMock, deliveryOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes1 = new ValidationResult("Не заполнена парадная в точке доставки");
            var validationRes2 = new ValidationResult("Не заполнен этаж в точке доставки");
            var validationRes3 = new ValidationResult("Не заполнен номер помещения в точке доставки");
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes1.ErrorMessage ||
                                                                x.ErrorMessage == validationRes2.ErrorMessage ||
                                                                x.ErrorMessage == validationRes3.ErrorMessage));
        }
        
        #endregion
    }
}