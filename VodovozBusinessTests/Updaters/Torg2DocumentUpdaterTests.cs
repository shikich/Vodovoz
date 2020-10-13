using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Invoice;
using Vodovoz.Domain.Orders.Documents.Torg2;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class Torg2DocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            Torg2DocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<Torg2DocumentFactory>();
            Torg2DocumentUpdater invoiceContractDocumentUpdater = new Torg2DocumentUpdater(invoiceContractDocumentFactoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.Torg2Count.Returns(1);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            invoiceContractDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Torg2));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            Torg2DocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<Torg2DocumentFactory>();
            Torg2DocumentUpdater invoiceContractDocumentUpdater = new Torg2DocumentUpdater(invoiceContractDocumentFactoryMock);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.Torg2Count.Returns(1);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            Torg2Document torg2DocumentMock = Substitute.For<Torg2Document>();
            torg2DocumentMock.Type.Returns(OrderDocumentType.Torg2);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(torg2DocumentMock);

            // act
            invoiceContractDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            Torg2DocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<Torg2DocumentFactory>();
            Torg2DocumentUpdater invoiceContractDocumentUpdater = new Torg2DocumentUpdater(invoiceContractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            Torg2Document torg2DocumentMock = Substitute.For<Torg2Document>();
            torg2DocumentMock.Type.Returns(OrderDocumentType.Torg2);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(torg2DocumentMock);
            
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
            Torg2DocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<Torg2DocumentFactory>();
            Torg2DocumentUpdater invoiceContractDocumentUpdater = new Torg2DocumentUpdater(invoiceContractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            Torg2Document torg2DocumentMock = Substitute.For<Torg2Document>();
            torg2DocumentMock.Type.Returns(OrderDocumentType.Torg2);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            invoiceContractDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, torg2DocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Torg2));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            Torg2DocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<Torg2DocumentFactory>();
            Torg2DocumentUpdater invoiceContractDocumentUpdater = new Torg2DocumentUpdater(invoiceContractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            Torg2Document torg2DocumentMock = Substitute.For<Torg2Document>();
            torg2DocumentMock.Type.Returns(OrderDocumentType.Torg2);
            Torg2Document torg2DocumentMock2 = Substitute.For<Torg2Document>();
            torg2DocumentMock2.Type.Returns(OrderDocumentType.Torg2);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(torg2DocumentMock);
            
            // act
            invoiceContractDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, torg2DocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            Torg2DocumentFactory invoiceContractDocumentFactoryMock = Substitute.For<Torg2DocumentFactory>();
            Torg2DocumentUpdater invoiceContractDocumentUpdater = new Torg2DocumentUpdater(invoiceContractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            Torg2Document torg2DocumentMock = Substitute.For<Torg2Document>();
            torg2DocumentMock.Type.Returns(OrderDocumentType.Torg2);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(torg2DocumentMock);
            
            // act
            invoiceContractDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, torg2DocumentMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}