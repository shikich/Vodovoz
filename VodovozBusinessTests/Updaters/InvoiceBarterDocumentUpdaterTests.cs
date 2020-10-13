using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Invoice;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class InvoiceBarterDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            InvoiceBarterDocumentFactory invoiceBarterDocumentFactoryMock = Substitute.For<InvoiceBarterDocumentFactory>();
            InvoiceBarterDocumentUpdater invoiceBarterDocumentUpdater = new InvoiceBarterDocumentUpdater(invoiceBarterDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.barter);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            invoiceBarterDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.InvoiceBarter));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            InvoiceBarterDocumentFactory invoiceBarterDocumentFactoryMock = Substitute.For<InvoiceBarterDocumentFactory>();
            InvoiceBarterDocumentUpdater invoiceBarterDocumentUpdater = new InvoiceBarterDocumentUpdater(invoiceBarterDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.barter);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            InvoiceBarterDocument assemblyListDocumentMock = Substitute.For<InvoiceBarterDocument>();
            assemblyListDocumentMock.Type.Returns(OrderDocumentType.InvoiceBarter);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(assemblyListDocumentMock);

            // act
            invoiceBarterDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            InvoiceBarterDocumentFactory invoiceBarterDocumentFactoryMock = Substitute.For<InvoiceBarterDocumentFactory>();
            InvoiceBarterDocumentUpdater invoiceBarterDocumentUpdater = new InvoiceBarterDocumentUpdater(invoiceBarterDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceBarterDocument assemblyListDocumentMock = Substitute.For<InvoiceBarterDocument>();
            assemblyListDocumentMock.Type.Returns(OrderDocumentType.InvoiceBarter);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(assemblyListDocumentMock);
            
           // act
           invoiceBarterDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            InvoiceBarterDocumentFactory invoiceBarterDocumentFactoryMock = Substitute.For<InvoiceBarterDocumentFactory>();
            InvoiceBarterDocumentUpdater invoiceBarterDocumentUpdater = new InvoiceBarterDocumentUpdater(invoiceBarterDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceBarterDocument assemblyListDocumentMock = Substitute.For<InvoiceBarterDocument>();
            assemblyListDocumentMock.Type.Returns(OrderDocumentType.InvoiceBarter);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            invoiceBarterDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, assemblyListDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.InvoiceBarter));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            InvoiceBarterDocumentFactory invoiceBarterDocumentFactoryMock = Substitute.For<InvoiceBarterDocumentFactory>();
            InvoiceBarterDocumentUpdater invoiceBarterDocumentUpdater = new InvoiceBarterDocumentUpdater(invoiceBarterDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceBarterDocument invoiceBarterDocumentMock = Substitute.For<InvoiceBarterDocument>();
            invoiceBarterDocumentMock.Type.Returns(OrderDocumentType.InvoiceBarter);
            InvoiceBarterDocument assemblyListDocumentMock2 = Substitute.For<InvoiceBarterDocument>();
            assemblyListDocumentMock2.Type.Returns(OrderDocumentType.InvoiceBarter);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceBarterDocumentMock);
            
            // act
            invoiceBarterDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, assemblyListDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            InvoiceBarterDocumentFactory invoiceBarterDocumentFactoryMock = Substitute.For<InvoiceBarterDocumentFactory>();
            InvoiceBarterDocumentUpdater invoiceBarterDocumentUpdater = new InvoiceBarterDocumentUpdater(invoiceBarterDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceBarterDocument invoiceBarterDocumentMock = Substitute.For<InvoiceBarterDocument>();
            invoiceBarterDocumentMock.Type.Returns(OrderDocumentType.InvoiceBarter);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceBarterDocumentMock);
            
            // act
            invoiceBarterDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, invoiceBarterDocumentMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}