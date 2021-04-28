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
    public class ClosingDocOrderValidatorTests {
        
        #region OrderValidator без параметров
        
        [Test(Description = "Проверка валидирования заказа-закрытия доков без контрагента")]
        public void ValidateClosingDocOrderWithoutCounterparty()
        {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.Counterparty = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =  new ValidationResult("В заказе необходимо заполнить поле \"клиент\".",
                new[] {nameof(closingDocOrderMock.Counterparty)});
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-закрытия доков без даты доставки")]
        public void ValidateClosingDocOrderWithoutDeliveryDate()
        {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes = new ValidationResult("В заказе не указана дата доставки.",
                new[] { nameof(closingDocOrderMock.DeliveryDate) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-закрытия доков с отрицательным залогом")]
        public void ValidateClosingDocOrderWithNegativeDeposit()
        {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            OrderDepositItem orderDepositItemMock = Substitute.For<OrderDepositItem>();
            orderDepositItemMock.Total.Returns(-250);

            GenericObservableList<OrderDepositItem> observableDepositItems =
                new GenericObservableList<OrderDepositItem> {orderDepositItemMock};
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItems);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes = new ValidationResult("В возврате залогов в заказе необходимо вводить положительную сумму.");
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        [Test(Description = "Проверка валидирования заказа-закрытия доков с оборудованием, без причины забора-доставки")]
        public void ValidateClosingDocOrderWithOrderEquipmentsWithoutDirectionReason() {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);

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

            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия доков с оборудованием, без принадлежности")]
        public void ValidateClosingDocOrderWithOrderEquipmentsWithoutOwnType()
        {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);

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

            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
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

        [Test(Description = "Проверка валидирования заказа-закрытия доков с товарами со скидкой, без указания причины скидки")]
        public void ValidateClosingDocOrderWithOrderItemsWithDiscountWithoutDisountReason()
        {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Discount.Returns(25m);
            orderItemMock.IsDiscountInMoney.Returns(true);
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            orderItemMock.DiscountReason = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItems =
                new GenericObservableList<OrderItem>{ orderItemMock };
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItems);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
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

        [Test(Description = "Проверка валидирования заказа-закрытия доков с забором оборудования, без указания причины не забора в комментарии")]
        public void ValidateClosingDocOrderWithOrderEquipmentsWithDirectionPickUpWithoutComment()
        {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            nomenclatureMock1.Category.Returns(NomenclatureCategory.equipment);
            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            orderEquipmentMock1.Nomenclature.Returns(nomenclatureMock1);
            orderEquipmentMock1.Direction.Returns(Direction.PickUp);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                new GenericObservableList<OrderEquipment> { orderEquipmentMock1 };

            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Close } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    $"Забор оборудования {orderEquipmentMock1.NameString} по заказу {closingDocOrderMock.Id} не произведен, а в комментарии не указана причина.",
                    new[] { nameof(closingDocOrderMock.OrderEquipments) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-закрытия доков с залогами, для типа оплаты cashless")]
        public void ValidateClosingDocOrderWithOrderDepositItemsAndPaymentTypeCashless()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            OrderDepositItem orderDepositItemMock = Substitute.For<OrderDepositItem>();
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.PaymentType.Returns(PaymentType.cashless);
            closingDocOrderMock.Counterparty.Returns(counterpartyMock1);
        
            GenericObservableList<OrderDepositItem> observableDepositItems =
                new GenericObservableList<OrderDepositItem> {orderDepositItemMock};
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItems);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
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

        [Test(Description = "Проверка валидирования заказа-закрытия доков с товарами или оборудованием с количеством <= 0")]
        public void ValidateClosingDocOrderWithOrderItemsCountOrOrderEquipmentsCountEqualsZero()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderEquipment orderEquipmentMock = Substitute.For<OrderEquipment>();
            orderEquipmentMock.Nomenclature.Returns(nomenclatureMock);

            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.Counterparty.Returns(counterpartyMock);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                new GenericObservableList<OrderEquipment> { orderEquipmentMock };
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия доков для клиента с закрытыми поставками и определенными типами оплат")]
        public void ValidateClosingDocOrderForCounterpartyWithClosedDeliveries()
        {
            // arrange
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.IsDeliveriesClosed.Returns(true);
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.Counterparty.Returns(counterpartyMock);
            closingDocOrderMock.PaymentType.Returns(PaymentType.cashless);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,uowMock, closingDocOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "В заказе неверно указан тип оплаты (для данного клиента закрыты поставки)",
                    new[] { nameof(closingDocOrderMock.PaymentType) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-закрытия доков с точкой доставки без района")]
        public void ValidateClosingDocOrderWithDeliveryPointWithoutDistrict()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            closingDocOrderMock.Counterparty.Returns(counterpartyMock);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
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

        #region ClosingDocOrderValidator без параметров

        [Test(Description = "Проверка валидирования заказа-закрытия документов без точки доставки")]
        public void ValidateClosingDocOrderWithoutDeliveryPoint()
        {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.DeliveryPoint = null;
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =  new ValidationResult("В заказе необходимо заполнить точку доставки.",
                new[] { nameof(closingDocOrderMock.DeliveryPoint) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с оплатой по карте, без заполненного номера онлайн-заказа")]
        public void ValidateClosingDocOrderWithPaymentTypeByCardWithoutOrderNumberFromOnlineStore()
        {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.PaymentType.Returns(PaymentType.ByCard);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Если в заказе выбран тип оплаты по карте, необходимо заполнить номер онлайн заказа.",
                    new[] { nameof(closingDocOrderMock.OrderNumberFromOnlineStore) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с оплатой по карте, без указания откуда оплата")]
        public void ValidateClosingDocOrderWithPaymentTypeByCardWithoutPaymentFrom()
        {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.PaymentType.Returns(PaymentType.ByCard);
            closingDocOrderMock.PaymentByCardFrom = null;
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "Выбран тип оплаты по карте. Необходимо указать откуда произведена оплата.",
                    new[] { nameof(closingDocOrderMock.PaymentByCardFrom) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с номенклатурой из интернет-магазина, без указания номера заказа ИМ")]
        public void ValidateClosingDocOrderWithOrderItemsFromWebStoreWithoutEShopOrder()
        {
            // arrange
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            OnlineStore onlineStoreMock = Substitute.For<OnlineStore>();
            closingDocOrderMock.EShopOrder = null;
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.OnlineStore.Returns(onlineStoreMock);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItems =
                new GenericObservableList<OrderItem>{ orderItemMock };
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItems);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
        
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.RootProductGroupForOnlineStoreNomenclatures.Returns(5);
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "При добавлении в заказ номенклатур с группой товаров интернет-магазина необходимо указать номер заказа интернет-магазина.",
                    new[] { nameof(closingDocOrderMock.EShopOrder) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        #endregion

        #region ClosingDocOrderValidator с параметрами

        [Test(Description = "Проверка валидирования заказа-закрытия документов, выставленного на контрагента без телефонов")]
        public void ValidateClosingDocOrderWithCounterpartyWithoutPhones()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();

            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.Counterparty.Returns(counterpartyMock);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с точкой доставки без: парадной, этажа, номера помещения")]
        public void ValidateClosingDocOrderWithDeliveryPoint()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            closingDocOrderMock.Counterparty.Returns(counterpartyMock);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(closingDocOrderMock.OrderDepositItems);
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(closingDocOrderMock.OrderItems);
            closingDocOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(closingDocOrderMock.OrderEquipments);
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, closingDocOrderMock);
            
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