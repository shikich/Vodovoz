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
    public class SpecialUPDDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов SelfDeliveryOrder (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentSelfDeliveryOrderWithoutDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            SpecialUPDDocumentFactory specialUPDDocumentFactoryMock = Substitute.For<SpecialUPDDocumentFactory>();
            SpecialUPDDocumentUpdater specialUPDDocumentUpdater = new SpecialUPDDocumentUpdater(specialUPDDocumentFactoryMock, updDocumentUpdater);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.UseSpecialDocFields.Returns(true);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
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
            specialUPDDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.SpecialUPD));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов DeliveryOrder (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentDeliveryOrderWithoutDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            SpecialUPDDocumentFactory specialUPDDocumentFactoryMock = Substitute.For<SpecialUPDDocumentFactory>();
            SpecialUPDDocumentUpdater specialUPDDocumentUpdater = new SpecialUPDDocumentUpdater(specialUPDDocumentFactoryMock, updDocumentUpdater);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.UseSpecialDocFields.Returns(true);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Counterparty.Returns(counterpartyMock);
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
            specialUPDDocumentUpdater.UpdateDocument(deliveryOrderMock);
            
            // assert
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.SpecialUPD));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            SpecialUPDDocumentFactory specialUPDDocumentFactoryMock = Substitute.For<SpecialUPDDocumentFactory>();
            SpecialUPDDocumentUpdater specialUPDDocumentUpdater = new SpecialUPDDocumentUpdater(specialUPDDocumentFactoryMock, updDocumentUpdater);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.UseSpecialDocFields.Returns(true);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            SpecialUPDDocument specialUPDDocumentMock = Substitute.For<SpecialUPDDocument>();
            specialUPDDocumentMock.Type.Returns(OrderDocumentType.SpecialUPD);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialUPDDocumentMock);
            
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Sum.Returns(500);

            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            specialUPDDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
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
            SpecialUPDDocumentFactory specialUPDDocumentFactoryMock = Substitute.For<SpecialUPDDocumentFactory>();
            SpecialUPDDocumentUpdater specialUPDDocumentUpdater = new SpecialUPDDocumentUpdater(specialUPDDocumentFactoryMock, updDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            SpecialUPDDocument specialUPDDocumentMock = Substitute.For<SpecialUPDDocument>();
            specialUPDDocumentMock.Type.Returns(OrderDocumentType.SpecialUPD);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialUPDDocumentMock);
            
            // act
            specialUPDDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
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
            SpecialUPDDocumentFactory specialUPDDocumentFactoryMock = Substitute.For<SpecialUPDDocumentFactory>();
            SpecialUPDDocumentUpdater specialUPDDocumentUpdater = new SpecialUPDDocumentUpdater(specialUPDDocumentFactoryMock, updDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            SpecialUPDDocument specialUPDDocumentMock = Substitute.For<SpecialUPDDocument>();
            specialUPDDocumentMock.Type.Returns(OrderDocumentType.SpecialUPD);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            specialUPDDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, specialUPDDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.SpecialUPD));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock);
            UPDDocumentFactory updDocumentFactoryMock = Substitute.For<UPDDocumentFactory>();
            UPDDocumentUpdater updDocumentUpdater = new UPDDocumentUpdater(updDocumentFactoryMock, billDocumentUpdater);
            SpecialUPDDocumentFactory specialUPDDocumentFactoryMock = Substitute.For<SpecialUPDDocumentFactory>();
            SpecialUPDDocumentUpdater specialUPDDocumentUpdater = new SpecialUPDDocumentUpdater(specialUPDDocumentFactoryMock, updDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            SpecialUPDDocument specialUPDDocumentMock1 = Substitute.For<SpecialUPDDocument>();
            specialUPDDocumentMock1.Type.Returns(OrderDocumentType.SpecialUPD);
            SpecialUPDDocument specialUPDDocumentMock2 = Substitute.For<SpecialUPDDocument>();
            specialUPDDocumentMock2.Type.Returns(OrderDocumentType.SpecialUPD);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialUPDDocumentMock1);
            
            // act
            specialUPDDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, specialUPDDocumentMock2);
            
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
            SpecialUPDDocumentFactory specialUPDDocumentFactoryMock = Substitute.For<SpecialUPDDocumentFactory>();
            SpecialUPDDocumentUpdater specialUPDDocumentUpdater = new SpecialUPDDocumentUpdater(specialUPDDocumentFactoryMock, updDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            SpecialUPDDocument specialUPDDocumentMock1 = Substitute.For<SpecialUPDDocument>();
            specialUPDDocumentMock1.Type.Returns(OrderDocumentType.SpecialUPD);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialUPDDocumentMock1);
            
            // act
            specialUPDDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, specialUPDDocumentMock1);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}