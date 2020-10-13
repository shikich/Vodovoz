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
    public class InvoiceContractDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            InvoiceContractDocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<InvoiceContractDocumentFactory>();
            InvoiceContractDocumentUpdater invoiceContractDocumentUpdater = new InvoiceContractDocumentUpdater(invoiceContractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.ContractDoc);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            invoiceContractDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.InvoiceContractDoc));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            InvoiceContractDocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<InvoiceContractDocumentFactory>();
            InvoiceContractDocumentUpdater invoiceContractDocumentUpdater = new InvoiceContractDocumentUpdater(invoiceContractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.ContractDoc);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            InvoiceContractDocument invoiceContractDocumentMock = Substitute.For<InvoiceContractDocument>();
            invoiceContractDocumentMock.Type.Returns(OrderDocumentType.InvoiceContractDoc);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceContractDocumentMock);

            // act
            invoiceContractDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            InvoiceContractDocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<InvoiceContractDocumentFactory>();
            InvoiceContractDocumentUpdater invoiceContractDocumentUpdater = new InvoiceContractDocumentUpdater(invoiceContractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceContractDocument invoiceContractDocumentMock = Substitute.For<InvoiceContractDocument>();
            invoiceContractDocumentMock.Type.Returns(OrderDocumentType.InvoiceContractDoc);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceContractDocumentMock);
            
           // act
           invoiceContractDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            InvoiceContractDocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<InvoiceContractDocumentFactory>();
            InvoiceContractDocumentUpdater invoiceContractDocumentUpdater = new InvoiceContractDocumentUpdater(invoiceContractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceContractDocument invoiceContractDocumentMock = Substitute.For<InvoiceContractDocument>();
            invoiceContractDocumentMock.Type.Returns(OrderDocumentType.InvoiceContractDoc);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            invoiceContractDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, invoiceContractDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.InvoiceContractDoc));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            InvoiceContractDocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<InvoiceContractDocumentFactory>();
            InvoiceContractDocumentUpdater invoiceContractDocumentUpdater = new InvoiceContractDocumentUpdater(invoiceContractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceContractDocument invoiceContractDocumentMock = Substitute.For<InvoiceContractDocument>();
            invoiceContractDocumentMock.Type.Returns(OrderDocumentType.InvoiceContractDoc);
            InvoiceContractDocument invoiceContractDocumentMock2 = Substitute.For<InvoiceContractDocument>();
            invoiceContractDocumentMock2.Type.Returns(OrderDocumentType.InvoiceContractDoc);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceContractDocumentMock);
            
            // act
            invoiceContractDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, invoiceContractDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            InvoiceContractDocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<InvoiceContractDocumentFactory>();
            InvoiceContractDocumentUpdater invoiceContractDocumentUpdater = new InvoiceContractDocumentUpdater(invoiceContractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceContractDocument invoiceContractDocumentMock = Substitute.For<InvoiceContractDocument>();
            invoiceContractDocumentMock.Type.Returns(OrderDocumentType.InvoiceContractDoc);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceContractDocumentMock);
            
            // act
            invoiceContractDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, invoiceContractDocumentMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}