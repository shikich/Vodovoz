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
    public class ClosingDocOrderValidatorTests {
        
        #region OrderValidator без параметров
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с точкой доставки без координат")]
        public void ValidateClosingDocOrderWithDeliveryPointWithoutCoordinates()
        {
            // arrange
            DeliveryPoint deliveryPointMock1 = Substitute.For<DeliveryPoint>();
            
            ClosingDocOrder testOrder = new ClosingDocOrder {
                DeliveryPoint = deliveryPointMock1
            };
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов без контрагента")]
        public void ValidateClosingDocOrderWithoutCounterparty()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
                new NomenclatureParametersProvider() , UnitOfWorkFactory.CreateWithoutRoot(), testOrder);
            
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов без даты доставки")]
        public void ValidateClosingDocOrderWithoutDeliveryDate()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с отрицательным залогом")]
        public void ValidateClosingDocOrderWithNegativeDeposit()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder();
            OrderDepositItem orderDepositItem = new OrderDepositItem {
                Deposit = -250m,
                Count = 1
            };

            testOrder.ObservableOrderDepositItems.Add(orderDepositItem);
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с оборудованием, без причины забора-доставки")]
        public void ValidateDeliveryOrderWithOrderEquipmentsWithoutDirectionReason()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder();

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
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с оборудованием, без принадлежности")]
        public void ValidateClosingDocOrderWithOrderEquipmentsWithoutOwnType()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder();

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
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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

        /*[Test(Description = "Проверка валидирования заказа-закрытия документов с товарами со скидкой, без указания причины скидки")]
        public void ValidateClosingDocOrderWithOrderItemsWithDiscountWithoutDisountReason()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder();

            OrderItem orderItemMock1 = Substitute.For<OrderItem>();
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            
            orderItemMock1.Discount = 25m;
            orderItemMock1.IsDiscountInMoney = true;
            orderItemMock1.Nomenclature = nomenclatureMock1;

            testOrder.ObservableOrderItems.Add(orderItemMock1);
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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

        [Test(Description = "Проверка валидирования заказа-закрытия документов с забором оборудования, без указания причины не забора в комментарии")]
        public void ValidateClosingDocOrderWithOrderEquipmentsWithDirectionPickUpWithoutComment()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder();

            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            
            nomenclatureMock1.Category = NomenclatureCategory.equipment;
            
            orderEquipmentMock1.Nomenclature = nomenclatureMock1;
            orderEquipmentMock1.Direction = Direction.PickUp;
            
            testOrder.ObservableOrderEquipments.Add(orderEquipmentMock1);
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с залогами, для типа оплаты cashless")]
        public void ValidateClosingDocOrderWithOrderDepositItemsAndPaymentTypeCashless()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder { PaymentType = PaymentType.cashless };

            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            OrderDepositItem orderDepositItemMock1 = Substitute.For<OrderDepositItem>();

            testOrder.ObservableOrderDepositItems.Add(orderDepositItemMock1);
            testOrder.Counterparty = counterpartyMock1;
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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

        [Test(Description = "Проверка валидирования заказа-закрытия документов с товарами или оборудованием с количеством <= 0")]
        public void ValidateClosingDocOrderWithOrderItemsCountOrOrderEquipmentsCountEqualsZero()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            OrderEquipment orderEquipmentMock1 = Substitute.For<OrderEquipment>();
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();

            ClosingDocOrder testOrder = new ClosingDocOrder {
                Counterparty = counterpartyMock1
            };

            orderEquipmentMock1.Nomenclature = nomenclatureMock1;
            testOrder.ObservableOrderEquipments.Add(orderEquipmentMock1);
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        //Исправить
        [Test(Description = "Проверка валидирования заказа-закрытия документов для клиента с закрытыми поставками и определенными типами оплат")]
        public void ValidateClosingDocOrderForCounterpartyWithClosedDeliveries()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            //counterpartyMock1.IsDeliveriesClosed = true;
            
            ClosingDocOrder testOrder = new ClosingDocOrder {
                Counterparty = counterpartyMock1,
                PaymentType = PaymentType.cashless
            };

            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с точкой доставки без района")]
        public void ValidateClosingDocOrderWithDeliveryPointWithoutDistrict()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock1 = Substitute.For<DeliveryPoint>();
            
            ClosingDocOrder testOrder = new ClosingDocOrder {
                DeliveryPoint = deliveryPointMock1,
                Counterparty = counterpartyMock1
            };

            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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

        #region ClosingDocOrderValidator без параметров

        [Test(Description = "Проверка валидирования заказа-закрытия документов без точки доставки")]
        public void ValidateClosingDocOrderWithoutDeliveryPoint()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder();
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с оплатой по карте, без заполненного номера онлайн-заказа")]
        public void ValidateClosingDocOrderWithPaymentTypeByCardWithoutOrderNumberFromOnlineStore()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder {
                PaymentType = PaymentType.ByCard
            };
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с оплатой по карте, без указания откуда оплата")]
        public void ValidateClosingDocOrderWithPaymentTypeByCardWithoutPaymentFrom()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder {
                PaymentType = PaymentType.ByCard
            };
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        
        /*[Test(Description = "Проверка валидирования заказа-закрытия документов с номенклатурой из интернет-магазина, без указания номера заказа ИМ")]
        public void ValidateClosingDocOrderWithOrderItemsFromWebStoreWithoutEShopOrder()
        {
            // arrange
            ClosingDocOrder testOrder = new ClosingDocOrder();

            OrderItem orderItemMock1 = Substitute.For<OrderItem>();
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            ProductGroup productGroupMock1 = Substitute.For<ProductGroup>();

            productGroupMock1.IsOnlineStore = true;
            nomenclatureMock1.ProductGroup = productGroupMock1;
            orderItemMock1.Nomenclature = nomenclatureMock1;

            testOrder.ObservableOrderItems.Add(orderItemMock1);
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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

        #region ClosingDocOrderValidator с параметрами

        [Test(Description = "Проверка валидирования заказа-закрытия документов, выставленного на контрагента без телефонов")]
        public void ValidateClosingDocOrderWithCounterpartyWithoutPhones()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();

            ClosingDocOrder testOrder = new ClosingDocOrder {
                Counterparty = counterpartyMock1
            };
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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
        
        [Test(Description = "Проверка валидирования заказа-закрытия документов с точкой доставки без: парадной, этажа, номера помещения")]
        public void ValidateClosingDocOrderWithDeliveryPoint()
        {
            // arrange
            Counterparty counterpartyMock1 = Substitute.For<Counterparty>();
            DeliveryPoint deliveryPointMock1 = Substitute.For<DeliveryPoint>();

            ClosingDocOrder testOrder = new ClosingDocOrder {
                Counterparty = counterpartyMock1,
                DeliveryPoint = deliveryPointMock1
            };
            
            ClosingDocOrderValidator validator = new ClosingDocOrderValidator(new DefaultAllowedPermissionService(), 
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