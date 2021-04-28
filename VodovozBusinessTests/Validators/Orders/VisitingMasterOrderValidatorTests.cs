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
    public class VisitingMasterOrderValidatorTests {
        
        #region OrderValidator без параметров
        
        [Test(Description = "Проверка валидирования сервисного заказа без контрагента")]
        public void ValidateVisitingMasterOrderWithoutCounterparty()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.Counterparty = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =  new ValidationResult("В заказе необходимо заполнить поле \"клиент\".",
                new[] {nameof(visitingMasterOrderMock.Counterparty)});
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования сервисного заказа без даты доставки")]
        public void ValidateVisitingMasterOrderWithoutDeliveryDate()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes = new ValidationResult("В заказе не указана дата доставки.",
                new[] { nameof(visitingMasterOrderMock.DeliveryDate) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования сервисного заказа с отрицательным залогом")]
        public void ValidateVisitingMasterOrderWithNegativeDeposit()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            OrderDepositItem orderDepositItemMock = Substitute.For<OrderDepositItem>();
            orderDepositItemMock.Total.Returns(-250);

            GenericObservableList<OrderDepositItem> observableDepositItems =
                new GenericObservableList<OrderDepositItem> {orderDepositItemMock};
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItems);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes = new ValidationResult("В возврате залогов в заказе необходимо вводить положительную сумму.");
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        [Test(Description = "Проверка валидирования сервисного заказа с оборудованием, без причины забора-доставки")]
        public void ValidateVisitingMasterOrderWithOrderEquipmentsWithoutDirectionReason() {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);

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

            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования сервисного заказа с оборудованием, без принадлежности")]
        public void ValidateVisitingMasterOrderWithOrderEquipmentsWithoutOwnType()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);

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

            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
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

        [Test(Description = "Проверка валидирования сервисного заказа с товарами со скидкой, без указания причины скидки")]
        public void ValidateVisitingMasterOrderWithOrderItemsWithDiscountWithoutDisountReason()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            OnlineStore onlineStoreMock = Substitute.For<OnlineStore>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.OnlineStore.Returns(onlineStoreMock);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Discount.Returns(25m);
            orderItemMock.IsDiscountInMoney.Returns(true);
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            orderItemMock.DiscountReason = null;

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItems =
                new GenericObservableList<OrderItem>{ orderItemMock };
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItems);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
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

        [Test(Description = "Проверка валидирования сервисного заказа с забором оборудования, без указания причины не забора в комментарии")]
        public void ValidateVisitingMasterOrderWithOrderEquipmentsWithDirectionPickUpWithoutComment()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            nomenclatureMock1.Category.Returns(NomenclatureCategory.equipment);
            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            orderEquipmentMock1.Nomenclature.Returns(nomenclatureMock1);
            orderEquipmentMock1.Direction.Returns(Direction.PickUp);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                new GenericObservableList<OrderEquipment> { orderEquipmentMock1 };

            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Close } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    $"Забор оборудования {orderEquipmentMock1.NameString} по заказу {visitingMasterOrderMock.Id} не произведен, а в комментарии не указана причина.",
                    new[] { nameof(visitingMasterOrderMock.OrderEquipments) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования сервисного заказа с залогами, для типа оплаты cashless")]
        public void ValidateVisitingMasterOrderWithOrderDepositItemsAndPaymentTypeCashless()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            OrderDepositItem orderDepositItemMock = Substitute.For<OrderDepositItem>();
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.PaymentType.Returns(PaymentType.cashless);
            visitingMasterOrderMock.Counterparty.Returns(counterpartyMock1);
        
            GenericObservableList<OrderDepositItem> observableDepositItems =
                new GenericObservableList<OrderDepositItem> {orderDepositItemMock};
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItems);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
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

        [Test(Description = "Проверка валидирования сервисного заказа с товарами или оборудованием с количеством <= 0")]
        public void ValidateVisitingMasterOrderWithOrderItemsCountOrOrderEquipmentsCountEqualsZero()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderEquipment orderEquipmentMock = Substitute.For<OrderEquipment>();
            orderEquipmentMock.Nomenclature.Returns(nomenclatureMock);

            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.Counterparty.Returns(counterpartyMock);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                new GenericObservableList<OrderEquipment> { orderEquipmentMock };
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования сервисного заказа для клиента с закрытыми поставками и определенными типами оплат")]
        public void ValidateVisitingMasterOrderForCounterpartyWithClosedDeliveries()
        {
            // arrange
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.IsDeliveriesClosed.Returns(true);
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.Counterparty.Returns(counterpartyMock);
            visitingMasterOrderMock.PaymentType.Returns(PaymentType.cashless);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,uowMock, visitingMasterOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "В заказе неверно указан тип оплаты (для данного клиента закрыты поставки)",
                    new[] { nameof(visitingMasterOrderMock.PaymentType) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования сервисного заказа с точкой доставки без района")]
        public void ValidateVisitingMasterOrderWithDeliveryPointWithoutDistrict()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            visitingMasterOrderMock.Counterparty.Returns(counterpartyMock);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
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

        #region VisitingMasterOrderValidator без параметров

        [Test(Description = "Проверка валидирования сервисного заказа без точки доставки")]
        public void ValidateVisitingMasterOrderWithoutDeliveryPoint()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.DeliveryPoint = null;
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =  new ValidationResult("В заказе необходимо заполнить точку доставки.",
                new[] { nameof(visitingMasterOrderMock.DeliveryPoint) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }

        [Test(Description = "Проверка валидирования сервисного заказа с оплатой по карте, без заполненного номера онлайн-заказа")]
        public void ValidateVisitingMasterOrderWithPaymentTypeByCardWithoutOrderNumberFromOnlineStore()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.PaymentType.Returns(PaymentType.ByCard);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Если в заказе выбран тип оплаты по карте, необходимо заполнить номер онлайн заказа.",
                    new[] { nameof(visitingMasterOrderMock.OrderNumberFromOnlineStore) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования сервисного заказа с оплатой по карте, без указания откуда оплата")]
        public void ValidateVisitingMasterOrderWithPaymentTypeByCardWithoutPaymentFrom()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.PaymentType.Returns(PaymentType.ByCard);
            visitingMasterOrderMock.PaymentByCardFrom = null;
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "Выбран тип оплаты по карте. Необходимо указать откуда произведена оплата.",
                    new[] { nameof(visitingMasterOrderMock.PaymentByCardFrom) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования сервисного заказа с номенклатурой из интернет-магазина, без указания номера заказа ИМ")]
        public void ValidateVisitingMasterOrderWithOrderItemsFromWebStoreWithoutEShopOrder()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.EShopOrder = null;
            OnlineStore onlineStoreMock = Substitute.For<OnlineStore>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.OnlineStore.Returns(onlineStoreMock);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItems =
                new GenericObservableList<OrderItem>{ orderItemMock };
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItems);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);
        
            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.RootProductGroupForOnlineStoreNomenclatures.Returns(5);
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
        
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "При добавлении в заказ номенклатур с группой товаров интернет-магазина необходимо указать номер заказа интернет-магазина.",
                    new[] { nameof(visitingMasterOrderMock.EShopOrder) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        #endregion

        #region VisitingMasterOrderValidator с параметрами

        [Test(Description = "Проверка валидирования на право подтверждения безнального сервисного заказа")]
        public void ValidateVisitingMasterOrderWithPaymentTypeCashlessWithoutRule()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.PaymentType.Returns(PaymentType.cashless);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "Недостаточно прав для подтверждения безнального сервисного заказа. Обратитесь к руководителю.",
                    new[] {nameof(visitingMasterOrderMock.Status)});
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования сервисного заказа без времени доставки")]
        public void ValidateVisitingMasterOrderWithoutDeliverySchedule()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.Counterparty.Returns(counterpartyMock);
            visitingMasterOrderMock.DeliverySchedule = null;
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("В заказе не указано время доставки.",
                    new[] { nameof(visitingMasterOrderMock.DeliverySchedule) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования сервисного заказа за наличку, без указания сдачи")]
        public void ValidateVisitingMasterOrderWithWithPaymentTypeCashWithoutTrifle()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.Counterparty.Returns(counterpartyMock);
            visitingMasterOrderMock.PaymentType.Returns(PaymentType.cash);
            visitingMasterOrderMock.TotalSum.Returns(5m);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("В заказе не указана сдача.",
                    new[] { nameof(visitingMasterOrderMock.Trifle) });
            var vc = new ValidationContext(validator, null, contextItems);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования сервисного заказа выставленного на контрагента без телефонов")]
        public void ValidateVisitingMasterOrderWithCounterpartyWithoutPhones()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.Counterparty.Returns(counterpartyMock);

            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
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
        
        [Test(Description = "Проверка валидирования сервисного заказа с точкой доставки без: парадной, этажа, номера помещения")]
        public void ValidateVisitingMasterOrderWithDeliveryPointWithoutFloor()
        {
            // arrange
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.Counterparty.Returns(counterpartyMock);
            visitingMasterOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            
            GenericObservableList<OrderDepositItem> observableDepositItemsMock =
                Substitute.For<GenericObservableList<OrderDepositItem>>(visitingMasterOrderMock.OrderDepositItems);
            visitingMasterOrderMock.ObservableOrderDepositItems.Returns(observableDepositItemsMock);
            GenericObservableList<OrderItem> observableOrderItemsMock =
                Substitute.For<GenericObservableList<OrderItem>>(visitingMasterOrderMock.OrderItems);
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableOrderItemsMock);
            GenericObservableList<OrderEquipment> observableEquipmentsMock =
                Substitute.For<GenericObservableList<OrderEquipment>>(visitingMasterOrderMock.OrderEquipments);
            visitingMasterOrderMock.ObservableOrderEquipments.Returns(observableEquipmentsMock);

            ICurrentPermissionService currentPermissionServiceMock = Substitute.For<ICurrentPermissionService>();
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            IUnitOfWork unitOfWorkMock = Substitute.For<IUnitOfWork>();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(currentPermissionServiceMock, 
                nomenclatureParametersProviderMock ,unitOfWorkMock, visitingMasterOrderMock);
            
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