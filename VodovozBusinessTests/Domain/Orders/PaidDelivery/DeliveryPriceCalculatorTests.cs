using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.PaidDelivery;
using Vodovoz.Domain.Sale;

namespace VodovozBusinessTests.Domain.Orders.PaidDelivery {
    [TestFixture]
    public class DeliveryPriceCalculatorTests {
        
        [Test(Description = "Проверка метода при передаче заказа без точки доставки GetDeliveryPrice(OrderBase order)")]
        public void GetDeliveryPriceMethodWithOrderWithoutDeliveryPointTest()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            
            DeliveryPriceCalculator deliveryPriceCalculator = new DeliveryPriceCalculator();

            // act
            var result = deliveryPriceCalculator.GetDeliveryPrice(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, result);
        }
        
        [Test(Description = "Проверка метода при передаче заказа с точкой доставки без района GetDeliveryPrice(OrderBase order)")]
        public void GetDeliveryPriceMethodWithOrderWithoutDistrictTest()
        {
            // arrange
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            deliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            deliveryOrderMock.DeliveryPoint.District = null;
            
            DeliveryPriceCalculator deliveryPriceCalculator = new DeliveryPriceCalculator();

            // act
            var result = deliveryPriceCalculator.GetDeliveryPrice(deliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, result);
        }
        
        [Test(Description = "Проверка метода при передаче заказа без даты доставки с количеством воды меньше необходимого для бесплатной доставки GetDeliveryPrice(OrderBase order)")]
        public void GetDeliveryPriceMethodWithOrderWithoutDeliveryDateTest1()
        {
            // arrange
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock.TareVolume.Returns(TareVolume.Vol19L);
            nomenclatureMock.IsWater19L.Returns(true);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            orderItemMock.Count.Returns(1);

            DeliveryPriceRule deliveryPriceRuleMock = Substitute.For<DeliveryPriceRule>();
            deliveryPriceRuleMock.Water19LCount.Returns(2);
            deliveryPriceRuleMock.EqualsCount6LFor19L.Returns(20);
            deliveryPriceRuleMock.EqualsCount1500mlFor19L.Returns(36);
            deliveryPriceRuleMock.EqualsCount600mlFor19L.Returns(96);
            CommonDistrictRuleItem commonDistrictRuleItemMock = Substitute.For<CommonDistrictRuleItem>();
            commonDistrictRuleItemMock.Price.Returns(300);
            commonDistrictRuleItemMock.DeliveryPriceRule.Returns(deliveryPriceRuleMock);
            IList<CommonDistrictRuleItem> ruleItems = new List<CommonDistrictRuleItem> {commonDistrictRuleItemMock};
            
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            deliveryPointMock.District.CommonDistrictRuleItems.Returns(ruleItems);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            deliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);

            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem> {orderItemMock};
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);

            DeliveryPriceCalculator deliveryPriceCalculator = new DeliveryPriceCalculator();

            // act
            var result = deliveryPriceCalculator.GetDeliveryPrice(deliveryOrderMock);
            
            // assert
            Assert.AreEqual(300, result);
        }
        
        [Test(Description = "Проверка метода при передаче заказа без даты доставки с количеством воды больше необходимого для бесплатной доставки GetDeliveryPrice(OrderBase order)")]
        public void GetDeliveryPriceMethodWithOrderWithoutDeliveryDateTest2()
        {
            // arrange
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock.TareVolume.Returns(TareVolume.Vol19L);
            nomenclatureMock.IsWater19L.Returns(true);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            orderItemMock.Count.Returns(2);

            DeliveryPriceRule deliveryPriceRuleMock = Substitute.For<DeliveryPriceRule>();
            deliveryPriceRuleMock.Water19LCount.Returns(2);
            deliveryPriceRuleMock.EqualsCount6LFor19L.Returns(20);
            deliveryPriceRuleMock.EqualsCount1500mlFor19L.Returns(36);
            deliveryPriceRuleMock.EqualsCount600mlFor19L.Returns(96);
            CommonDistrictRuleItem commonDistrictRuleItemMock = Substitute.For<CommonDistrictRuleItem>();
            commonDistrictRuleItemMock.Price.Returns(300);
            commonDistrictRuleItemMock.DeliveryPriceRule.Returns(deliveryPriceRuleMock);
            IList<CommonDistrictRuleItem> ruleItems = new List<CommonDistrictRuleItem> {commonDistrictRuleItemMock};
            
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            deliveryPointMock.District.CommonDistrictRuleItems.Returns(ruleItems);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            deliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);

            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem> {orderItemMock};
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);

            DeliveryPriceCalculator deliveryPriceCalculator = new DeliveryPriceCalculator();

            // act
            var result = deliveryPriceCalculator.GetDeliveryPrice(deliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, result);
        }
        
        [Test(Description = "Проверка метода при передаче заказа с датой доставки Сегодня и правилами доставки на сегодня," +
                            " с количеством воды больше необходимого для бесплатной доставки GetDeliveryPrice(OrderBase order)")]
        public void GetDeliveryPriceMethodWithOrderWithDeliveryDateTest1()
        {
            // arrange
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock.TareVolume.Returns(TareVolume.Vol19L);
            nomenclatureMock.IsWater19L.Returns(true);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            orderItemMock.Count.Returns(2);

            DeliveryPriceRule deliveryPriceRuleMock = Substitute.For<DeliveryPriceRule>();
            deliveryPriceRuleMock.Water19LCount.Returns(2);
            deliveryPriceRuleMock.EqualsCount6LFor19L.Returns(20);
            deliveryPriceRuleMock.EqualsCount1500mlFor19L.Returns(36);
            deliveryPriceRuleMock.EqualsCount600mlFor19L.Returns(96);
            WeekDayDistrictRuleItem weekDayDistrictRuleItemMock = Substitute.For<WeekDayDistrictRuleItem>();
            weekDayDistrictRuleItemMock.Price.Returns(500);
            weekDayDistrictRuleItemMock.DeliveryPriceRule.Returns(deliveryPriceRuleMock);
            GenericObservableList<WeekDayDistrictRuleItem> ruleItems = 
                new GenericObservableList<WeekDayDistrictRuleItem> {weekDayDistrictRuleItemMock};
            
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            deliveryPointMock.District.ObservableTodayDistrictRuleItems.Returns(ruleItems);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            deliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            deliveryOrderMock.DeliveryDate.Returns(DateTime.Today);

            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem> {orderItemMock};
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);

            DeliveryPriceCalculator deliveryPriceCalculator = new DeliveryPriceCalculator();

            // act
            var result = deliveryPriceCalculator.GetDeliveryPrice(deliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, result);
        }
        
        [Test(Description = "Проверка метода при передаче заказа с датой доставки Сегодня и правилами доставки на сегодня," +
                            " с количеством воды меньше необходимого для бесплатной доставки GetDeliveryPrice(OrderBase order)")]
        public void GetDeliveryPriceMethodWithOrderWithDeliveryDateTest2()
        {
            // arrange
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            nomenclatureMock1.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock1.TareVolume.Returns(TareVolume.Vol19L);
            nomenclatureMock1.IsWater19L.Returns(true);
            OrderItem orderItemMock1 = Substitute.For<OrderItem>();
            orderItemMock1.Nomenclature.Returns(nomenclatureMock1);
            orderItemMock1.Count.Returns(1);
            Nomenclature nomenclatureMock2 = Substitute.For<Nomenclature>();
            nomenclatureMock2.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock2.TareVolume.Returns(TareVolume.Vol600ml);
            nomenclatureMock2.IsDisposableTare.Returns(true);
            OrderItem orderItemMock2 = Substitute.For<OrderItem>();
            orderItemMock2.Nomenclature.Returns(nomenclatureMock2);
            orderItemMock2.Count.Returns(8);

            DeliveryPriceRule deliveryPriceRuleMock = Substitute.For<DeliveryPriceRule>();
            deliveryPriceRuleMock.Water19LCount.Returns(2);
            deliveryPriceRuleMock.EqualsCount6LFor19L.Returns(20);
            deliveryPriceRuleMock.EqualsCount1500mlFor19L.Returns(36);
            deliveryPriceRuleMock.EqualsCount600mlFor19L.Returns(96);
            WeekDayDistrictRuleItem weekDayDistrictRuleItemMock = Substitute.For<WeekDayDistrictRuleItem>();
            weekDayDistrictRuleItemMock.Price.Returns(500);
            weekDayDistrictRuleItemMock.DeliveryPriceRule.Returns(deliveryPriceRuleMock);
            GenericObservableList<WeekDayDistrictRuleItem> ruleItems = 
                new GenericObservableList<WeekDayDistrictRuleItem> {weekDayDistrictRuleItemMock};
            
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            deliveryPointMock.District.ObservableTodayDistrictRuleItems.Returns(ruleItems);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            deliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            deliveryOrderMock.DeliveryDate.Returns(DateTime.Today);

            GenericObservableList<OrderItem> observableItems = 
                new GenericObservableList<OrderItem> {orderItemMock1, orderItemMock2};
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);

            DeliveryPriceCalculator deliveryPriceCalculator = new DeliveryPriceCalculator();

            // act
            var result = deliveryPriceCalculator.GetDeliveryPrice(deliveryOrderMock);
            
            // assert
            Assert.AreEqual(500, result);
        }
        
        [Test(Description = "Проверка метода при передаче заказа с датой доставки без правил доставки на сегодня" +
                            " с количеством воды меньше необходимого для бесплатной доставки GetDeliveryPrice(OrderBase order)")]
        public void GetDeliveryPriceMethodWithOrderWithDeliveryDateTest()
        {
            // arrange
            Nomenclature nomenclatureMock1 = Substitute.For<Nomenclature>();
            nomenclatureMock1.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock1.TareVolume.Returns(TareVolume.Vol19L);
            nomenclatureMock1.IsWater19L.Returns(true);
            OrderItem orderItemMock1 = Substitute.For<OrderItem>();
            orderItemMock1.Nomenclature.Returns(nomenclatureMock1);
            orderItemMock1.Count.Returns(1);
            Nomenclature nomenclatureMock2 = Substitute.For<Nomenclature>();
            nomenclatureMock2.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock2.IsDisposableTare.Returns(true);
            nomenclatureMock2.TareVolume.Returns(TareVolume.Vol600ml);
            OrderItem orderItemMock2 = Substitute.For<OrderItem>();
            orderItemMock2.Nomenclature.Returns(nomenclatureMock2);
            orderItemMock2.Count.Returns(8);

            DeliveryPriceRule deliveryPriceRuleMock = Substitute.For<DeliveryPriceRule>();
            deliveryPriceRuleMock.Water19LCount.Returns(2);
            deliveryPriceRuleMock.EqualsCount6LFor19L.Returns(20);
            deliveryPriceRuleMock.EqualsCount1500mlFor19L.Returns(36);
            deliveryPriceRuleMock.EqualsCount600mlFor19L.Returns(96);
            WeekDayDistrictRuleItem weekDayDistrictRuleItemMock = Substitute.For<WeekDayDistrictRuleItem>();
            weekDayDistrictRuleItemMock.Price.Returns(500);
            weekDayDistrictRuleItemMock.DeliveryPriceRule.Returns(deliveryPriceRuleMock);
            GenericObservableList<WeekDayDistrictRuleItem> ruleItemsMock =
                new GenericObservableList<WeekDayDistrictRuleItem>();
            GenericObservableList<WeekDayDistrictRuleItem> ruleItems = 
                new GenericObservableList<WeekDayDistrictRuleItem> {weekDayDistrictRuleItemMock};
            
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            deliveryPointMock.District.ObservableTodayDistrictRuleItems.Returns(ruleItemsMock);
            deliveryPointMock.District
                             .GetWeekDayRuleItemCollectionByWeekDayName(WeekDayName.Monday)
                             .ReturnsForAnyArgs(ruleItems);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            deliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            deliveryOrderMock.DeliveryDate.Returns(DateTime.Today);

            GenericObservableList<OrderItem> observableItems = 
                new GenericObservableList<OrderItem> {orderItemMock1, orderItemMock2};
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);

            DeliveryPriceCalculator deliveryPriceCalculator = new DeliveryPriceCalculator();

            // act
            var result = deliveryPriceCalculator.GetDeliveryPrice(deliveryOrderMock);
            
            // assert
            Assert.AreEqual(500, result);
        }
    }
}