using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Bill;
using Vodovoz.Domain.Orders.Documents.DriverTicket;
using Vodovoz.Services;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class DriverTicketDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            DriverTicketDocumentFactory driverTicketDocumentFactoryMock = Substitute.For<DriverTicketDocumentFactory>();
            DriverTicketDocumentUpdater driverTicketDocumentUpdater = 
                new DriverTicketDocumentUpdater(driverTicketDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Sum.Returns(500);
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            driverTicketDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.DriverTicket));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            DriverTicketDocumentFactory driverTicketDocumentFactoryMock = Substitute.For<DriverTicketDocumentFactory>();
            DriverTicketDocumentUpdater driverTicketDocumentUpdater = 
                new DriverTicketDocumentUpdater(driverTicketDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            DriverTicketDocument specialBillDocumentMock = Substitute.For<DriverTicketDocument>();
            specialBillDocumentMock.Type.Returns(OrderDocumentType.DriverTicket);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialBillDocumentMock);
            
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Sum.Returns(500);
            orderItemMock.Nomenclature.Returns(nomenclatureMock);

            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            driverTicketDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            DriverTicketDocumentFactory driverTicketDocumentFactoryMock = Substitute.For<DriverTicketDocumentFactory>();
            DriverTicketDocumentUpdater driverTicketDocumentUpdater = 
                new DriverTicketDocumentUpdater(driverTicketDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            DriverTicketDocument specialBillDocumentMock = Substitute.For<DriverTicketDocument>();
            specialBillDocumentMock.Type.Returns(OrderDocumentType.DriverTicket);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialBillDocumentMock);
            
           // act
           driverTicketDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            DriverTicketDocumentFactory driverTicketDocumentFactoryMock = Substitute.For<DriverTicketDocumentFactory>();
            DriverTicketDocumentUpdater driverTicketDocumentUpdater = 
                new DriverTicketDocumentUpdater(driverTicketDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            DriverTicketDocument specialBillDocumentMock = Substitute.For<DriverTicketDocument>();
            specialBillDocumentMock.Type.Returns(OrderDocumentType.DriverTicket);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            driverTicketDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, specialBillDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.DriverTicket));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            DriverTicketDocumentFactory driverTicketDocumentFactoryMock = Substitute.For<DriverTicketDocumentFactory>();
            DriverTicketDocumentUpdater driverTicketDocumentUpdater = 
                new DriverTicketDocumentUpdater(driverTicketDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            DriverTicketDocument specialBillDocumentMock1 = Substitute.For<DriverTicketDocument>();
            specialBillDocumentMock1.Type.Returns(OrderDocumentType.DriverTicket);
            DriverTicketDocument specialBillDocumentMock2 = Substitute.For<DriverTicketDocument>();
            specialBillDocumentMock2.Type.Returns(OrderDocumentType.DriverTicket);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialBillDocumentMock1);
            
            // act
            driverTicketDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, specialBillDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            DriverTicketDocumentFactory driverTicketDocumentFactoryMock = Substitute.For<DriverTicketDocumentFactory>();
            DriverTicketDocumentUpdater driverTicketDocumentUpdater = 
                new DriverTicketDocumentUpdater(driverTicketDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            DriverTicketDocument specialBillDocumentMock1 = Substitute.For<DriverTicketDocument>();
            specialBillDocumentMock1.Type.Returns(OrderDocumentType.Bill);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialBillDocumentMock1);
            
            // act
            driverTicketDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, specialBillDocumentMock1);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}