using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Bill;
using Vodovoz.Domain.Orders.Documents.ShetFactura;
using Vodovoz.Domain.Orders.Documents.UPD;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class ShetFacturaDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов SelfDeliveryOrder (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentSelfDeliveryOrderWithoutDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            ShetFacturaDocumentFactory shetFacturaDocumentFactoryMock = Substitute.For<ShetFacturaDocumentFactory>();
            ShetFacturaDocumentUpdater torg12DocumentUpdater = 
                new ShetFacturaDocumentUpdater(shetFacturaDocumentFactoryMock, updDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.WaitForPayment);
            selfDeliveryOrderMock.PayAfterShipment.Returns(true);
            selfDeliveryOrderMock.DefaultDocumentType.Returns(DefaultDocumentType.torg12);
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
            torg12DocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.ShetFactura));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов DeliveryOrder (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentDeliveryOrderWithoutDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            ShetFacturaDocumentFactory shetFacturaDocumentFactoryMock = Substitute.For<ShetFacturaDocumentFactory>();
            ShetFacturaDocumentUpdater torg12DocumentUpdater = 
                new ShetFacturaDocumentUpdater(shetFacturaDocumentFactoryMock, updDocumentUpdater);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            deliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            deliveryOrderMock.DefaultDocumentType.Returns(DefaultDocumentType.torg12);
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
            torg12DocumentUpdater.UpdateDocument(deliveryOrderMock);
            
            // assert
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.ShetFactura));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            ShetFacturaDocumentFactory shetFacturaDocumentFactoryMock = Substitute.For<ShetFacturaDocumentFactory>();
            ShetFacturaDocumentUpdater torg12DocumentUpdater = 
                new ShetFacturaDocumentUpdater(shetFacturaDocumentFactoryMock, updDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            selfDeliveryOrderMock.DefaultDocumentType.Returns(DefaultDocumentType.torg12);
            ShetFacturaDocument shetFacturaDocumentMock = Substitute.For<ShetFacturaDocument>();
            shetFacturaDocumentMock.Type.Returns(OrderDocumentType.ShetFactura);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(shetFacturaDocumentMock);
            
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Sum.Returns(500);

            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            torg12DocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
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
            ShetFacturaDocumentFactory shetFacturaDocumentFactoryMock = Substitute.For<ShetFacturaDocumentFactory>();
            ShetFacturaDocumentUpdater torg12DocumentUpdater = 
                new ShetFacturaDocumentUpdater(shetFacturaDocumentFactoryMock, updDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            ShetFacturaDocument shetFacturaDocumentMock = Substitute.For<ShetFacturaDocument>();
            shetFacturaDocumentMock.Type.Returns(OrderDocumentType.ShetFactura);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(shetFacturaDocumentMock);
            
           // act
           torg12DocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
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
            ShetFacturaDocumentFactory shetFacturaDocumentFactoryMock = Substitute.For<ShetFacturaDocumentFactory>();
            ShetFacturaDocumentUpdater torg12DocumentUpdater = 
                new ShetFacturaDocumentUpdater(shetFacturaDocumentFactoryMock, updDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            ShetFacturaDocument shetFacturaDocumentMock = Substitute.For<ShetFacturaDocument>();
            shetFacturaDocumentMock.Type.Returns(OrderDocumentType.ShetFactura);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            torg12DocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, shetFacturaDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.ShetFactura));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            ShetFacturaDocumentFactory shetFacturaDocumentFactoryMock = Substitute.For<ShetFacturaDocumentFactory>();
            ShetFacturaDocumentUpdater torg12DocumentUpdater = 
                new ShetFacturaDocumentUpdater(shetFacturaDocumentFactoryMock, updDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            ShetFacturaDocument shetFacturaDocumentMock1 = Substitute.For<ShetFacturaDocument>();
            shetFacturaDocumentMock1.Type.Returns(OrderDocumentType.ShetFactura);
            ShetFacturaDocument shetFacturaDocumentMock2 = Substitute.For<ShetFacturaDocument>();
            shetFacturaDocumentMock2.Type.Returns(OrderDocumentType.ShetFactura);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(shetFacturaDocumentMock1);
            
            // act
            torg12DocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, shetFacturaDocumentMock2);
            
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
            ShetFacturaDocumentFactory shetFacturaDocumentFactoryMock = Substitute.For<ShetFacturaDocumentFactory>();
            ShetFacturaDocumentUpdater torg12DocumentUpdater = 
                new ShetFacturaDocumentUpdater(shetFacturaDocumentFactoryMock, updDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            ShetFacturaDocument shetFacturaDocumentMock1 = Substitute.For<ShetFacturaDocument>();
            shetFacturaDocumentMock1.Type.Returns(OrderDocumentType.ShetFactura);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(shetFacturaDocumentMock1);
            
            // act
            torg12DocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, shetFacturaDocumentMock1);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}