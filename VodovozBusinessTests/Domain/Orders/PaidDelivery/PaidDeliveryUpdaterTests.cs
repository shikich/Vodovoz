using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.PaidDelivery;
using Vodovoz.Domain.Sale;
using Vodovoz.EntityFactories;
using Vodovoz.EntityRepositories.Goods;

namespace VodovozBusinessTests.Domain.Orders.PaidDelivery {
    [TestFixture]
    public class PaidDeliveryUpdaterTests {
        
        [Test(Description = "Проверка метода UpdatePaidDelivery(IUnitOfWork uow) для самовывоза")]
        public void UpdatePaidDeliveryMethodForSelfDeliveryOrder()
        {
            // arrange
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();

            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            OrderItemFactory orderItemFactoryMock = new OrderItemFactory();
            Nomenclature paidDeliveryMock = Substitute.For<Nomenclature>();
            paidDeliveryMock.Id.Returns(189);
            paidDeliveryMock.Name.Returns("Доставка");
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            nomenclatureRepositoryMock.GetPaidDeliveryNomenclature(uowMock).Returns(paidDeliveryMock);
            DeliveryPriceCalculator deliveryPriceCalculatorMock = Substitute.For<DeliveryPriceCalculator>();
            
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            
            PaidDeliveryUpdater paidDeliveryUpdater = new PaidDeliveryUpdater(selfDeliveryOrderMock, orderItemFactoryMock,
                nomenclatureRepositoryMock, deliveryPriceCalculatorMock);

            // act
            paidDeliveryUpdater.UpdatePaidDelivery(uowMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderItems.All(x => x.Nomenclature.Id != paidDeliveryMock.Id));
        }
        
        [Test(Description = "Проверка метода UpdatePaidDelivery(IUnitOfWork uow) для сервисного заказа")]
        public void UpdatePaidDeliveryMethodForVisitingMasterOrder()
        {
            // arrange
            VisitingMasterOrder visitingMasterOrderMock = Substitute.For<VisitingMasterOrder>();
            visitingMasterOrderMock.Type.Returns(OrderType.VisitingMasterOrder);

            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            OrderItemFactory orderItemFactoryMock = new OrderItemFactory();
            Nomenclature paidDeliveryMock = Substitute.For<Nomenclature>();
            paidDeliveryMock.Id.Returns(189);
            paidDeliveryMock.Name.Returns("Доставка");
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            nomenclatureRepositoryMock.GetPaidDeliveryNomenclature(uowMock).Returns(paidDeliveryMock);
            DeliveryPriceCalculator deliveryPriceCalculatorMock = Substitute.For<DeliveryPriceCalculator>();
            
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            visitingMasterOrderMock.ObservableOrderItems.Returns(observableItems);
            
            PaidDeliveryUpdater paidDeliveryUpdater = new PaidDeliveryUpdater(visitingMasterOrderMock, orderItemFactoryMock,
                nomenclatureRepositoryMock, deliveryPriceCalculatorMock);

            // act
            paidDeliveryUpdater.UpdatePaidDelivery(uowMock);
            
            // assert
            Assert.True(visitingMasterOrderMock.ObservableOrderItems.All(x => x.Nomenclature.Id != paidDeliveryMock.Id));
        }
        
        [Test(Description = "Проверка метода UpdatePaidDelivery(IUnitOfWork uow) для заказа-доставки" +
                            " без доставки в заказе, но с условиями платной доставки")]
        public void UpdatePaidDeliveryMethodForDeliveryOrderWithoutDeliveryInOrderItems()
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
            
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            OrderItemFactory orderItemFactoryMock = new OrderItemFactory();
            Nomenclature paidDeliveryMock = Substitute.For<Nomenclature>();
            paidDeliveryMock.Id.Returns(189);
            paidDeliveryMock.Name.Returns("Доставка");
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            nomenclatureRepositoryMock.GetPaidDeliveryNomenclature(uowMock).Returns(paidDeliveryMock);
            DeliveryPriceCalculator deliveryPriceCalculatorMock = Substitute.For<DeliveryPriceCalculator>();
            
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>{orderItemMock};
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            
            PaidDeliveryUpdater paidDeliveryUpdater = new PaidDeliveryUpdater(deliveryOrderMock, orderItemFactoryMock,
                nomenclatureRepositoryMock, deliveryPriceCalculatorMock);

            // act
            paidDeliveryUpdater.UpdatePaidDelivery(uowMock);
            
            // assert
            Assert.True(deliveryOrderMock.ObservableOrderItems.Any(
                x => x.Nomenclature.Id == paidDeliveryMock.Id && x.Price == 300m));
        }
        
        [Test(Description = "Проверка метода UpdatePaidDelivery(IUnitOfWork uow) для заказа-доставки" +
                            " с доставкой в заказе, при добавлении доставки с другой ценой")]
        public void UpdatePaidDeliveryMethodForDeliveryOrderWithDeliveryInOrderItems()
        {
            // arrange
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock.TareVolume.Returns(TareVolume.Vol19L);
            nomenclatureMock.IsWater19L.Returns(true);
            OrderItem orderItemMock1 = Substitute.For<OrderItem>();
            orderItemMock1.Nomenclature.Returns(nomenclatureMock);
            orderItemMock1.Count.Returns(1);

            DeliveryPriceRule deliveryPriceRuleMock = Substitute.For<DeliveryPriceRule>();
            deliveryPriceRuleMock.Water19LCount.Returns(2);
            deliveryPriceRuleMock.EqualsCount6LFor19L.Returns(20);
            deliveryPriceRuleMock.EqualsCount1500mlFor19L.Returns(36);
            deliveryPriceRuleMock.EqualsCount600mlFor19L.Returns(96);
            CommonDistrictRuleItem commonDistrictRuleItemMock = Substitute.For<CommonDistrictRuleItem>();
            commonDistrictRuleItemMock.Price.Returns(500);
            commonDistrictRuleItemMock.DeliveryPriceRule.Returns(deliveryPriceRuleMock);
            IList<CommonDistrictRuleItem> ruleItems = new List<CommonDistrictRuleItem> {commonDistrictRuleItemMock};
            
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            deliveryPointMock.District.CommonDistrictRuleItems.Returns(ruleItems);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            deliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            OrderItemFactory orderItemFactoryMock = new OrderItemFactory();
            Nomenclature paidDeliveryMock = Substitute.For<Nomenclature>();
            paidDeliveryMock.Id.Returns(189);
            paidDeliveryMock.Name.Returns("Доставка");
            OrderItem orderItemMock2 = Substitute.For<OrderItem>();
            orderItemMock2.Count.Returns(1);
            orderItemMock2.Nomenclature.Returns(paidDeliveryMock);
            orderItemMock2.Price.Returns(300m);
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            nomenclatureRepositoryMock.GetPaidDeliveryNomenclature(uowMock).Returns(paidDeliveryMock);
            DeliveryPriceCalculator deliveryPriceCalculatorMock = Substitute.For<DeliveryPriceCalculator>();
            
            GenericObservableList<OrderItem> observableItems = 
                new GenericObservableList<OrderItem> {orderItemMock1, orderItemMock2};
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            
            PaidDeliveryUpdater paidDeliveryUpdater = new PaidDeliveryUpdater(deliveryOrderMock, orderItemFactoryMock,
                nomenclatureRepositoryMock, deliveryPriceCalculatorMock);

            // act
            paidDeliveryUpdater.UpdatePaidDelivery(uowMock);
            
            // assert
            Assert.AreEqual(2, deliveryOrderMock.ObservableOrderItems.Count);
            Assert.True(deliveryOrderMock.ObservableOrderItems.Any(
                x => x.Nomenclature.Id == paidDeliveryMock.Id && x.Price == 500m));
        }
        
        [Test(Description = "Проверка метода UpdatePaidDelivery(IUnitOfWork uow) для заказа-доставки" +
                            " с доставкой в заказе, при достижении бесплатной доставки")]
        public void UpdatePaidDeliveryMethodForDeliveryOrderWithDeliveryInOrderItems2()
        {
            // arrange
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock.TareVolume.Returns(TareVolume.Vol19L);
            nomenclatureMock.IsWater19L.Returns(true);
            OrderItem orderItemMock1 = Substitute.For<OrderItem>();
            orderItemMock1.Nomenclature.Returns(nomenclatureMock);
            orderItemMock1.Count.Returns(2);

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
            
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            OrderItemFactory orderItemFactoryMock = new OrderItemFactory();
            Nomenclature paidDeliveryMock = Substitute.For<Nomenclature>();
            paidDeliveryMock.Id.Returns(189);
            paidDeliveryMock.Name.Returns("Доставка");
            OrderItem orderItemMock2 = Substitute.For<OrderItem>();
            orderItemMock2.Count.Returns(1);
            orderItemMock2.Nomenclature.Returns(paidDeliveryMock);
            orderItemMock2.Price.Returns(300m);
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            nomenclatureRepositoryMock.GetPaidDeliveryNomenclature(uowMock).Returns(paidDeliveryMock);
            DeliveryPriceCalculator deliveryPriceCalculatorMock = Substitute.For<DeliveryPriceCalculator>();
            
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>{orderItemMock1, orderItemMock2};
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            deliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            
            PaidDeliveryUpdater paidDeliveryUpdater = new PaidDeliveryUpdater(deliveryOrderMock, orderItemFactoryMock,
                nomenclatureRepositoryMock, deliveryPriceCalculatorMock);

            // act
            paidDeliveryUpdater.UpdatePaidDelivery(uowMock);
            
            // assert
            Assert.AreEqual(1, deliveryOrderMock.ObservableOrderItems.Count);
            Assert.True(deliveryOrderMock.ObservableOrderItems.All(
                x => x.Nomenclature.Id != paidDeliveryMock.Id));
        }
        
        [Test(Description = "Проверка метода UpdatePaidDelivery(IUnitOfWork uow) для заказа-закрытия документов" +
                            " с доставкой в заказе, при достижении бесплатной доставки")]
        public void UpdatePaidDeliveryMethodForClosingDocOrderWithDeliveryInOrderItems()
        {
            // arrange
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.water);
            nomenclatureMock.TareVolume.Returns(TareVolume.Vol6L);
            nomenclatureMock.IsDisposableTare.Returns(true);
            OrderItem orderItemMock1 = Substitute.For<OrderItem>();
            orderItemMock1.Nomenclature.Returns(nomenclatureMock);
            orderItemMock1.Count.Returns(40);

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
            ClosingDocOrder closingDocOrderMock = Substitute.For<ClosingDocOrder>();
            closingDocOrderMock.Type.Returns(OrderType.ClosingDocOrder);
            closingDocOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            OrderItemFactory orderItemFactoryMock = new OrderItemFactory();
            Nomenclature paidDeliveryMock = Substitute.For<Nomenclature>();
            paidDeliveryMock.Id.Returns(189);
            paidDeliveryMock.Name.Returns("Доставка");
            OrderItem orderItemMock2 = Substitute.For<OrderItem>();
            orderItemMock2.Count.Returns(1);
            orderItemMock2.Nomenclature.Returns(paidDeliveryMock);
            orderItemMock2.Price.Returns(300m);
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            nomenclatureRepositoryMock.GetPaidDeliveryNomenclature(uowMock).Returns(paidDeliveryMock);
            DeliveryPriceCalculator deliveryPriceCalculatorMock = Substitute.For<DeliveryPriceCalculator>();
            
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>{orderItemMock1, orderItemMock2};
            closingDocOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            closingDocOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            closingDocOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            
            PaidDeliveryUpdater paidDeliveryUpdater = new PaidDeliveryUpdater(closingDocOrderMock, orderItemFactoryMock,
                nomenclatureRepositoryMock, deliveryPriceCalculatorMock);

            // act
            paidDeliveryUpdater.UpdatePaidDelivery(uowMock);
            
            // assert
            Assert.AreEqual(1, closingDocOrderMock.ObservableOrderItems.Count);
            Assert.True(closingDocOrderMock.ObservableOrderItems.All(
                x => x.Nomenclature.Id != paidDeliveryMock.Id));
        }
    }
}