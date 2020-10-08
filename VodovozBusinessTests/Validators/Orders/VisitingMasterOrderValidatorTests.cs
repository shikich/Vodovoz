using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using QS.DomainModel.UoW;
using QS.Permissions;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Parameters;
using Vodovoz.Validators.Orders;

namespace VodovozBusinessTests.Validators.Orders {
    [TestFixture]
    public class VisitingMasterOrderValidatorTests {
        
        #region OrderValidator без параметров
        
        [Test(Description = "Проверка валидирования сервисного заказа с точкой доставки без координат")]
        public void ValidateVisitingMasterOrderWithDeliveryPointWithoutCoordinates()
        {
            // arrange
            DeliveryPoint deliveryPointMock1 = Substitute.For<DeliveryPoint>();
            
            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                DeliveryPoint = deliveryPointMock1
            };
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var results = new List<ValidationResult>();
            var validationRes =  new ValidationResult("В точке доставки необходимо указать координаты.",
                new[] { nameof(testOrder.DeliveryPoint) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        [Test(Description = "Проверка валидирования сервисного заказа без контрагента")]
        public void ValidateVisitingMasterOrderWithoutCounterparty()
        {
            // arrange
            VisitingMasterOrder testOrder = new VisitingMasterOrder();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() ,UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var results = new List<ValidationResult>();
            var validationRes =  new ValidationResult("В заказе необходимо заполнить поле \"клиент\".",
                new[] {nameof(testOrder.Counterparty)});
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
            VisitingMasterOrder testOrder = new VisitingMasterOrder();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var results = new List<ValidationResult>();
            var validationRes = new ValidationResult("В заказе не указана дата доставки.",
                new[] { nameof(testOrder.DeliveryDate) });
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
            VisitingMasterOrder testOrder = new VisitingMasterOrder();
            OrderDepositItem orderDepositItem = new OrderDepositItem {
                Deposit = -250m,
                Count = 1
            };

            testOrder.ObservableOrderDepositItems.Add(orderDepositItem);
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
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
        public void ValidateVisitingMasterOrderWithOrderEquipmentsWithoutDirectionReason()
        {
            // arrange
            VisitingMasterOrder testOrder = new VisitingMasterOrder();

            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            OrderEquipment orderEquipmentMock2 = Substitute.For<OrderEquipment>();
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            Nomenclature nomenclatureMock2 = Substitute.For<Nomenclature>();

            nomenclatureMock1.Category = NomenclatureCategory.equipment;
            nomenclatureMock2.Category = NomenclatureCategory.water;

            orderEquipmentMock1.Nomenclature = nomenclatureMock1;
            orderEquipmentMock2.Nomenclature = nomenclatureMock2;
            
            testOrder.ObservableOrderEquipments.Add(orderEquipmentMock1);
            testOrder.ObservableOrderEquipments.Add(orderEquipmentMock2);
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
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
            VisitingMasterOrder testOrder = new VisitingMasterOrder();

            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            OrderEquipment orderEquipmentMock2 = Substitute.For<OrderEquipment>();
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            Nomenclature nomenclatureMock2 = Substitute.For<Nomenclature>();

            nomenclatureMock1.Category = NomenclatureCategory.equipment;
            nomenclatureMock2.Category = NomenclatureCategory.water;

            orderEquipmentMock1.Nomenclature = nomenclatureMock1;
            orderEquipmentMock2.Nomenclature = nomenclatureMock2;
            
            testOrder.ObservableOrderEquipments.Add(orderEquipmentMock1);
            testOrder.ObservableOrderEquipments.Add(orderEquipmentMock2);
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
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

        /*[Test(Description = "Проверка валидирования сервисного заказа с товарами со скидкой, без указания причины скидки")]
        public void ValidateVisitingMasterOrderWithOrderItemsWithDiscountWithoutDisountReason()
        {
            // arrange
            VisitingMasterOrder testOrder = new VisitingMasterOrder();

            OrderItem orderItemMock1 = Substitute.For<OrderItem>();
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            
            orderItemMock1.Discount = 25m;
            orderItemMock1.IsDiscountInMoney = true;
            orderItemMock1.Nomenclature = nomenclatureMock1;

            testOrder.ObservableOrderItems.Add(orderItemMock1);
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
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
        }*/
        
        #endregion

        #region OrderValidator с параметрами

        [Test(Description = "Проверка валидирования сервисного заказа с забором оборудования, без указания причины не забора в комментарии")]
        public void ValidateVisitingMasterOrderWithOrderEquipmentsWithDirectionPickUpWithoutComment()
        {
            // arrange
            VisitingMasterOrder testOrder = new VisitingMasterOrder();

            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            
            nomenclatureMock1.Category = NomenclatureCategory.equipment;
            
            orderEquipmentMock1.Nomenclature = nomenclatureMock1;
            orderEquipmentMock1.Direction = Direction.PickUp;
            
            testOrder.ObservableOrderEquipments.Add(orderEquipmentMock1);
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Close } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    $"Забор оборудования {orderEquipmentMock1.NameString} по заказу {testOrder.Id} не произведен, а в комментарии не указана причина.",
                    new[] { nameof(testOrder.OrderEquipments) });
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
            VisitingMasterOrder testOrder = new VisitingMasterOrder { PaymentType = PaymentType.cashless };

            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            OrderDepositItem orderDepositItemMock1 = Substitute.For<OrderDepositItem>();

            testOrder.ObservableOrderDepositItems.Add(orderDepositItemMock1);
            testOrder.Counterparty = counterpartyMock1;
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
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
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();

            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                Counterparty = counterpartyMock1
            };

            orderEquipmentMock1.Nomenclature = nomenclatureMock1;
            testOrder.ObservableOrderEquipments.Add(orderEquipmentMock1);
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
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
        
        //Не выставить IsDeliveriesClosed из-за protected свойства
        [Test(Description = "Проверка валидирования сервисного заказа для клиента с закрытыми поставками и определенными типами оплат")]
        public void ValidateVisitingMasterOrderForCounterpartyWithCloseddeliveries()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            //counterpartyMock1.IsDeliveriesClosed = true;
            
            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                Counterparty = counterpartyMock1,
                PaymentType = PaymentType.cashless
            };

            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "В заказе неверно указан тип оплаты (для данного клиента закрыты поставки)",
                    new[] { nameof(testOrder.PaymentType) });
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
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock1 = Substitute.For<DeliveryPoint>();
            
            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                DeliveryPoint = deliveryPointMock1,
                Counterparty = counterpartyMock1
            };

            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
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

        [Test(Description = "Проверка валидирования сервисного заказа без точки доставки")]
        public void ValidateVisitingMasterOrderWithoutDeliveryPoint()
        {
            // arrange
            VisitingMasterOrder testOrder = new VisitingMasterOrder();
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var results = new List<ValidationResult>();
            var validationRes =  new ValidationResult("В заказе необходимо заполнить точку доставки.",
                new[] { nameof(testOrder.DeliveryPoint) });
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
            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                PaymentType = PaymentType.ByCard
            };
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("Если в заказе выбран тип оплаты по карте, необходимо заполнить номер онлайн заказа.",
                    new[] { nameof(testOrder.OrderNumberFromOnlineStore) });
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
            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                PaymentType = PaymentType.ByCard
            };
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "Выбран тип оплаты по карте. Необходимо указать откуда произведена оплата.",
                    new[] { nameof(testOrder.PaymentByCardFrom) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }
        
        /*[Test(Description = "Проверка валидирования сервисного заказа с номенклатурой из интернет-магазина, без указания номера заказа ИМ")]
        public void ValidateVisitingMasterOrderWithOrderItemsFromWebStoreWithoutEShopOrder()
        {
            // arrange
            VisitingMasterOrder testOrder = new VisitingMasterOrder();

            OrderItem orderItemMock1 = Substitute.For<OrderItem>();
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            ProductGroup productGroupMock1 = Substitute.For<ProductGroup>();

            productGroupMock1.IsOnlineStore = true;
            nomenclatureMock1.ProductGroup = productGroupMock1;
            orderItemMock1.Nomenclature = nomenclatureMock1;

            testOrder.ObservableOrderItems.Add(orderItemMock1);
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "При добавлении в заказ номенклатур с группой товаров интернет-магазина необходимо указать номер заказа интернет-магазина.",
                    new[] { nameof(testOrder.EShopOrder) });
            var vc = new ValidationContext(validator, null, null);

            // act
            var isValid = Validator.TryValidateObject(validator, vc, results, true);
            
            // assert
            Assert.False(isValid);
            Assert.True(results.Any(x => x.ErrorMessage == validationRes.ErrorMessage));
        }*/
        
        #endregion

        #region VisitingMasterOrderValidator с параметрами

        [Test(Description = "Проверка валидирования на право подтверждения безнального сервисного заказа")]
        public void ValidateVisitingMasterOrderWithPaymentTypeCashlessWithoutRule()
        {
            // arrange
            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                PaymentType = PaymentType.cashless
            };
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult(
                    "Недостаточно прав для подтверждения безнального сервисного заказа. Обратитесь к руководителю.",
                    new[] {nameof(testOrder.Status)});
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
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            
            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                Counterparty = counterpartyMock1
            };
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("В заказе не указано время доставки.",
                    new[] { nameof(testOrder.DeliverySchedule) });
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
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            
            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                Counterparty = counterpartyMock1,
                PaymentType = PaymentType.cash,
                TotalSum = 5m
            };
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
            var contextItems = new Dictionary<object, object> {
                { nameof(OrderValidateParameters), new OrderValidateParameters { OrderAction = OrderValidateAction.Accept } }
            };
            
            var results = new List<ValidationResult>();
            var validationRes =
                new ValidationResult("В заказе не указана сдача.",
                    new[] { nameof(testOrder.Trifle) });
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
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();

            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                Counterparty = counterpartyMock1
            };
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
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
        public void ValidateVisitingMasterOrderWithDeliveryPoint()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock1 = Substitute.For<DeliveryPoint>();

            VisitingMasterOrder testOrder = new VisitingMasterOrder {
                Counterparty = counterpartyMock1,
                DeliveryPoint = deliveryPointMock1
            };
            
            VisitingMasterOrderValidator validator = new VisitingMasterOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
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