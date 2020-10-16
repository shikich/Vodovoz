using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Bill;
using Vodovoz.Domain.Orders.Documents.UPD;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class UPDDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов SelfDeliveryOrder (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentSelfDeliveryOrderWithoutDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.WaitForPayment);
            selfDeliveryOrderMock.PayAfterShipment.Returns(true);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Sum.Returns(500);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            updDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.UPD));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов DeliveryOrder (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentDeliveryOrderWithoutDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            deliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Sum.Returns(500);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            deliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            deliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            updDocumentUpdater.UpdateDocument(deliveryOrderMock);
            
            // assert
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.UPD));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            UPDDocument updDocumentMock = Substitute.For<UPDDocument>();
            updDocumentMock.Type.Returns(OrderDocumentType.UPD);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(updDocumentMock);
            
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Sum.Returns(500);

            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            updDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            UPDDocument updDocumentMock = Substitute.For<UPDDocument>();
            updDocumentMock.Type.Returns(OrderDocumentType.UPD);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(updDocumentMock);
            
           // act
           updDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            UPDDocument updDocumentMock = Substitute.For<UPDDocument>();
            updDocumentMock.Type.Returns(OrderDocumentType.UPD);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            updDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, updDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.UPD));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            UPDDocument updDocumentMock1 = Substitute.For<UPDDocument>();
            updDocumentMock1.Type.Returns(OrderDocumentType.UPD);
            UPDDocument updDocumentMock2 = Substitute.For<UPDDocument>();
            updDocumentMock2.Type.Returns(OrderDocumentType.UPD);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(updDocumentMock1);
            
            // act
            updDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, updDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            UPDDocument updDocumentMock1 = Substitute.For<UPDDocument>();
            updDocumentMock1.Type.Returns(OrderDocumentType.UPD);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(updDocumentMock1);
            
            // act
            updDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, updDocumentMock1);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}