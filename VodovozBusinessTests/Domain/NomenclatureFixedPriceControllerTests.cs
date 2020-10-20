using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using QS.DomainModel.UoW;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityFactories;
using Vodovoz.EntityRepositories.Goods;

namespace VodovozBusinessTests.Domain {
    [TestFixture]
    public class NomenclatureFixedPriceControllerTests {

        [Test(Description = "Проверка метода ContainsFixedPrice(OrderBase order, Nomenclature nomenclature) с точкой доставки")]
        public void TestContainsFixedPriceMethodFromOrderWithDeliveryPoint() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            deliveryPointMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            deliveryPointMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            var contains = nomenclatureFixedPriceController.ContainsFixedPrice(selfDeliveryOrderMock, nomenclatureMock);
            
            // assert
            Assert.True(contains);
        }
        
        [Test(Description = "Проверка метода ContainsFixedPrice(OrderBase order, Nomenclature nomenclature) без точки доставки")]
        public void TestContainsFixedPriceMethodFromOrderWithCounterparty() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            counterpartyMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            var contains = nomenclatureFixedPriceController.ContainsFixedPrice(selfDeliveryOrderMock, nomenclatureMock);
            
            // assert
            Assert.True(contains);
        }
        
        [Test(Description = "Проверка метода ContainsFixedPrice(OrderBase order, Nomenclature nomenclature) без точки доставки")]
        public void TestContainsFixedPriceMethodFromOrderWithoutFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);

            // act
            var contains = nomenclatureFixedPriceController.ContainsFixedPrice(selfDeliveryOrderMock, nomenclatureMock);
            
            // assert
            Assert.False(contains);
        }
        
        [Test(Description = "Проверка метода ContainsFixedPrice(Counterparty counterparty, Nomenclature nomenclature) с фиксой")]
        public void TestContainsFixedPriceMethodFromCounterpartyWithFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            counterpartyMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            var contains = nomenclatureFixedPriceController.ContainsFixedPrice(counterpartyMock, nomenclatureMock);
            
            // assert
            Assert.True(contains);
        }
        
        [Test(Description = "Проверка метода ContainsFixedPrice(Counterparty counterparty, Nomenclature nomenclature) без фиксы")]
        public void TestContainsFixedPriceMethodFromCounterpartyWithoutFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);

            // act
            var contains = nomenclatureFixedPriceController.ContainsFixedPrice(counterpartyMock, nomenclatureMock);
            
            // assert
            Assert.False(contains);
        }
        
        [Test(Description = "Проверка метода ContainsFixedPrice(DeliveryPoint deliveryPoint, Nomenclature nomenclature) с фиксой")]
        public void TestContainsFixedPriceMethodFromDeliveryPointWithFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            deliveryPointMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            deliveryPointMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            var contains = nomenclatureFixedPriceController.ContainsFixedPrice(deliveryPointMock, nomenclatureMock);
            
            // assert
            Assert.True(contains);
        }
        
        [Test(Description = "Проверка метода ContainsFixedPrice(DeliveryPoint deliveryPoint, Nomenclature nomenclature) без фиксы")]
        public void TestContainsFixedPriceMethodFromDeliveryPointWithoutFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            deliveryPointMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);

            // act
            var contains = nomenclatureFixedPriceController.ContainsFixedPrice(deliveryPointMock, nomenclatureMock);
            
            // assert
            Assert.False(contains);
        }
        
        [Test(Description = "Проверка метода TryGetFixedPrice(OrderBase order, Nomenclature nomenclature, out decimal fixedPrice) с точкой доставки")]
        public void TestTryGetFixedPriceMethodFromOrderWithDeliveryPoint() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            nomenclatureFixedPriceMock.FixedPrice.Returns(250);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.DeliveryPoint.Returns(deliveryPointMock);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            deliveryPointMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            deliveryPointMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            var contains = nomenclatureFixedPriceController.TryGetFixedPrice(selfDeliveryOrderMock, nomenclatureMock, out var fixedPrice);
            
            // assert
            Assert.True(contains);
            Assert.AreEqual(250, fixedPrice);
        }
        
        [Test(Description = "Проверка метода TryGetFixedPrice(OrderBase order, Nomenclature nomenclature, out decimal fixedPrice) без точки доставки")]
        public void TestTryGetFixedPriceMethodFromOrderWithCounterparty() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            nomenclatureFixedPriceMock.FixedPrice.Returns(300);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            counterpartyMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            var contains = nomenclatureFixedPriceController.TryGetFixedPrice(selfDeliveryOrderMock, nomenclatureMock, out var fixedPrice);
            
            // assert
            Assert.True(contains);
            Assert.AreEqual(300, fixedPrice);
        }
        
        [Test(Description = "Проверка метода TryGetFixedPrice(OrderBase order, Nomenclature nomenclature, out decimal fixedPrice) без точки доставки и без фиксы")]
        public void TestTryGetFixedPriceMethodFromOrderWithCounterpartyWithoutFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);

            // act
            var contains = nomenclatureFixedPriceController.TryGetFixedPrice(selfDeliveryOrderMock, nomenclatureMock, out var fixedPrice);
            
            // assert
            Assert.False(contains);
            Assert.AreEqual(0, fixedPrice);
        }
        
        [Test(Description = "Проверка метода TryGetFixedPrice(Counterparty counterparty, Nomenclature nomenclature, out decimal fixedPrice) с фиксой")]
        public void TestTryGetFixedPriceMethodFromCounterpartyWithFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            nomenclatureFixedPriceMock.FixedPrice.Returns(300);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            counterpartyMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            var contains = nomenclatureFixedPriceController.TryGetFixedPrice(counterpartyMock, nomenclatureMock, out var fixedPrice);
            
            // assert
            Assert.True(contains);
            Assert.AreEqual(300, fixedPrice);
        }
        
        [Test(Description = "Проверка метода TryGetFixedPrice(Counterparty counterparty, Nomenclature nomenclature, out decimal fixedPrice) без фиксы")]
        public void TestTryGetFixedPriceMethodFromCounterpartyWithoutFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);

            // act
            var contains = nomenclatureFixedPriceController.TryGetFixedPrice(counterpartyMock, nomenclatureMock, out var fixedPrice);
            
            // assert
            Assert.False(contains);
            Assert.AreEqual(0, fixedPrice);
        }
        
        [Test(Description = "Проверка метода TryGetFixedPrice(DeliveryPoint deliveryPoint, Nomenclature nomenclature, out decimal fixedPrice) с фиксой")]
        public void TestTryGetFixedPriceMethodFromDeliveryPointWithFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            nomenclatureFixedPriceMock.FixedPrice.Returns(250);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            deliveryPointMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            deliveryPointMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            var contains = nomenclatureFixedPriceController.TryGetFixedPrice(deliveryPointMock, nomenclatureMock, out var fixedPrice);
            
            // assert
            Assert.True(contains);
            Assert.AreEqual(250, fixedPrice);
        }
        
        [Test(Description = "Проверка метода TryGetFixedPrice(DeliveryPoint deliveryPoint, Nomenclature nomenclature, out decimal fixedPrice) без фиксы")]
        public void TestTryGetFixedPriceMethodFromDeliveryPointWithoutFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            deliveryPointMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            
            // act
            var contains = nomenclatureFixedPriceController.TryGetFixedPrice(deliveryPointMock, nomenclatureMock, out var fixedPrice);
            
            // assert
            Assert.False(contains);
            Assert.AreEqual(0, fixedPrice);
        }
        
        [Test(Description = "Проверка метода AddOrUpdateFixedPrice(DeliveryPoint deliveryPoint, Nomenclature nomenclature, decimal fixedPrice) с фиксой")]
        public void TestAddOrUpdateFixedPriceMethodFromDeliveryPointWithFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            nomenclatureFixedPriceMock.FixedPrice.Returns(250);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            deliveryPointMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            deliveryPointMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            decimal fixedPrice = 300;
            nomenclatureFixedPriceController.AddOrUpdateFixedPrice(deliveryPointMock, nomenclatureMock, fixedPrice);
            
            // assert
            Assert.AreEqual(300, nomenclatureFixedPriceMock.FixedPrice);
        }
        
        [Test(Description = "Проверка метода AddOrUpdateFixedPrice(DeliveryPoint deliveryPoint, Nomenclature nomenclature, decimal fixedPrice) без фиксы")]
        public void TestAddOrUpdateFixedPriceMethodFromDeliveryPointWithoutFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            Nomenclature semiozerieMock = Substitute.For<Nomenclature>();
            semiozerieMock.Id.Returns(1);
            Nomenclature snyatogorskayaMock = Substitute.For<Nomenclature>();
            snyatogorskayaMock.Id.Returns(2);
            Nomenclature stroykaMock = Substitute.For<Nomenclature>();
            stroykaMock.Id.Returns(7);
            Nomenclature kislorodnayaMock = Substitute.For<Nomenclature>();
            kislorodnayaMock.Id.Returns(12);
            Nomenclature kislorodnayaDeluxMock = Substitute.For<Nomenclature>();
            kislorodnayaDeluxMock.Id.Returns(655);
            Nomenclature ruchkiMock = Substitute.For<Nomenclature>();
            ruchkiMock.Id.Returns(15);
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            nomenclatureRepositoryMock.GetWaterSemiozerie(uowMock).Returns(semiozerieMock);
            nomenclatureRepositoryMock.GetWaterSnyatogorskaya(uowMock).Returns(snyatogorskayaMock);
            nomenclatureRepositoryMock.GetWaterStroika(uowMock).Returns(stroykaMock);
            nomenclatureRepositoryMock.GetWaterKislorodnaya(uowMock).Returns(kislorodnayaMock);
            nomenclatureRepositoryMock.GetWaterKislorodnayaDeluxe(uowMock).Returns(kislorodnayaDeluxMock);
            nomenclatureRepositoryMock.GetWaterRuchki(uowMock).Returns(ruchkiMock);
            nomenclatureRepositoryMock.GetWaterPriceIncrement.Returns(20);
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            deliveryPointMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);

            // act
            decimal fixedPrice = 250;
            nomenclatureFixedPriceController.AddOrUpdateFixedPrice(deliveryPointMock, kislorodnayaMock, fixedPrice);
            
            // assert
            Assert.True(deliveryPointMock.ObservableNomenclatureFixedPrices.Any(x =>
                x.FixedPrice == fixedPrice && x.Nomenclature.Id == kislorodnayaMock.Id));
        }
        
        [Test(Description = "Проверка метода AddOrUpdateFixedPrice(Counterparty counterparty, Nomenclature nomenclature, decimal fixedPrice) с фиксой")]
        public void TestAddOrUpdateFixedPriceMethodFromCounterpartyWithFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            nomenclatureFixedPriceMock.FixedPrice.Returns(300);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            counterpartyMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            decimal fixedPrice = 500;
            nomenclatureFixedPriceController.AddOrUpdateFixedPrice(counterpartyMock, nomenclatureMock, fixedPrice);
            
            // assert
            Assert.AreEqual(fixedPrice, nomenclatureFixedPriceMock.FixedPrice);
        }
        
        [Test(Description = "Проверка метода AddOrUpdateFixedPrice(Counterparty counterparty, Nomenclature nomenclature, decimal fixedPrice) без фиксы")]
        public void TestAddOrUpdateFixedPriceMethodFromCounterpartyWithoutFixedPrice() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            Nomenclature semiozerieMock = Substitute.For<Nomenclature>();
            semiozerieMock.Id.Returns(1);
            Nomenclature snyatogorskayaMock = Substitute.For<Nomenclature>();
            snyatogorskayaMock.Id.Returns(2);
            Nomenclature stroykaMock = Substitute.For<Nomenclature>();
            stroykaMock.Id.Returns(7);
            Nomenclature kislorodnayaMock = Substitute.For<Nomenclature>();
            kislorodnayaMock.Id.Returns(12);
            Nomenclature kislorodnayaDeluxMock = Substitute.For<Nomenclature>();
            kislorodnayaDeluxMock.Id.Returns(655);
            Nomenclature ruchkiMock = Substitute.For<Nomenclature>();
            ruchkiMock.Id.Returns(15);
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            nomenclatureRepositoryMock.GetWaterSemiozerie(uowMock).Returns(semiozerieMock);
            nomenclatureRepositoryMock.GetWaterSnyatogorskaya(uowMock).Returns(snyatogorskayaMock);
            nomenclatureRepositoryMock.GetWaterStroika(uowMock).Returns(stroykaMock);
            nomenclatureRepositoryMock.GetWaterKislorodnaya(uowMock).Returns(kislorodnayaMock);
            nomenclatureRepositoryMock.GetWaterKislorodnayaDeluxe(uowMock).Returns(kislorodnayaDeluxMock);
            nomenclatureRepositoryMock.GetWaterRuchki(uowMock).Returns(ruchkiMock);
            nomenclatureRepositoryMock.GetWaterPriceIncrement.Returns(20);
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);

            // act
            decimal fixedPrice = 200;
            nomenclatureFixedPriceController.AddOrUpdateFixedPrice(counterpartyMock, semiozerieMock, fixedPrice);
            
            // assert
            Assert.True(counterpartyMock.ObservableNomenclatureFixedPrices.Any(x =>
                x.FixedPrice == fixedPrice && x.Nomenclature.Id == semiozerieMock.Id));
        }

        [Test(Description = "Проверка метода DeleteFixedPrice(DeliveryPoint deliveryPoint, NomenclatureFixedPrice nomenclatureFixedPrice)")]
        public void TestDeleteFixedPriceMethodFromDeliveryPoint() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            DeliveryPoint deliveryPointMock = Substitute.For<DeliveryPoint>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            nomenclatureFixedPriceMock.FixedPrice.Returns(250);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            deliveryPointMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            deliveryPointMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            nomenclatureFixedPriceController.DeleteFixedPrice(deliveryPointMock, nomenclatureFixedPriceMock);
            
            // assert
            Assert.AreEqual(0, deliveryPointMock.ObservableNomenclatureFixedPrices.Count);
        }
        
        [Test(Description = "Проверка метода DeleteFixedPrice(Counterparty counterparty, NomenclatureFixedPrice nomenclatureFixedPrice)")]
        public void TestDeleteFixedPriceMethodFromCounterparty() {
            // arrange
            NomenclatureFixedPriceFactory fixedPriceFactoryMock = Substitute.For<NomenclatureFixedPriceFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            WaterFixedPricesGenerator waterFixedPricesGeneratorMock = 
                Substitute.For<WaterFixedPricesGenerator>(uowMock, nomenclatureRepositoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            NomenclatureFixedPrice nomenclatureFixedPriceMock = Substitute.For<NomenclatureFixedPrice>();
            nomenclatureFixedPriceMock.Nomenclature.Returns(nomenclatureMock);
            nomenclatureFixedPriceMock.FixedPrice.Returns(300);
            NomenclatureFixedPriceController nomenclatureFixedPriceController = 
                new NomenclatureFixedPriceController(fixedPriceFactoryMock, waterFixedPricesGeneratorMock);
            GenericObservableList<NomenclatureFixedPrice> observableFixedPrices = new GenericObservableList<NomenclatureFixedPrice>();
            counterpartyMock.ObservableNomenclatureFixedPrices.Returns(observableFixedPrices);
            counterpartyMock.ObservableNomenclatureFixedPrices.Add(nomenclatureFixedPriceMock);
            
            // act
            nomenclatureFixedPriceController.DeleteFixedPrice(counterpartyMock, nomenclatureFixedPriceMock);
            
            // assert
            Assert.AreEqual(0, counterpartyMock.ObservableNomenclatureFixedPrices.Count);
        }
    }
}