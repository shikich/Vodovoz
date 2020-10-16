using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.OrderContract;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class OrderContractDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument, когда Order.Contract != null при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentContractIsNotNull()
        {
            // arrange
            OrderContractDocumentFactory contractDocumentFactoryMock = Substitute.For<OrderContractDocumentFactory>();
            OrderContractDocumentUpdater contractDocumentUpdater = new OrderContractDocumentUpdater(contractDocumentFactoryMock);
            CounterpartyContract contractMock = Substitute.For<CounterpartyContract>();
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Contract.Returns(contractMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            contractDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Contract));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при разных ссылках у документа и заказа")]
        public void TestUpdateMethodIfNeedCreateDocumentContractIsNotNull2()
        {
            // arrange
            OrderContractDocumentFactory contractDocumentFactoryMock = Substitute.For<OrderContractDocumentFactory>();
            OrderContractDocumentUpdater contractDocumentUpdater = new OrderContractDocumentUpdater(contractDocumentFactoryMock);
            CounterpartyContract contractMock = Substitute.For<CounterpartyContract>();
            contractMock.Id.Returns(5);
            CounterpartyContract contractMock2 = Substitute.For<CounterpartyContract>();
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Contract.Returns(contractMock);
            OrderContract contractDocMock = Substitute.For<OrderContract>();
            contractDocMock.Contract.Returns(contractMock2);
            GenericObservableList<OrderDocument> observableDocuments =
                new GenericObservableList<OrderDocument> {contractDocMock};
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            contractDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreSame(contractMock, contractDocMock.Contract);
        }

        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            OrderContractDocumentFactory contractDocumentFactoryMock = Substitute.For<OrderContractDocumentFactory>();
            OrderContractDocumentUpdater contractDocumentUpdater = new OrderContractDocumentUpdater(contractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Contract = null;
            OrderContract contractDocumentMock = Substitute.For<OrderContract>();
            contractDocumentMock.Type.Returns(OrderDocumentType.Contract);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(contractDocumentMock);
            
           // act
           contractDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            OrderContractDocumentFactory contractDocumentFactoryMock = Substitute.For<OrderContractDocumentFactory>();
            OrderContractDocumentUpdater contractDocumentUpdater = new OrderContractDocumentUpdater(contractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            OrderContract contractListDocumentMock = Substitute.For<OrderContract>();
            contractListDocumentMock.Type.Returns(OrderDocumentType.Contract);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            contractDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, contractListDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Contract));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            OrderContractDocumentFactory contractDocumentFactoryMock = Substitute.For<OrderContractDocumentFactory>();
            OrderContractDocumentUpdater contractDocumentUpdater = new OrderContractDocumentUpdater(contractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            OrderContract contractDocumentMock1 = Substitute.For<OrderContract>();
            contractDocumentMock1.Type.Returns(OrderDocumentType.Contract);
            OrderContract contractDocumentMock2 = Substitute.For<OrderContract>();
            contractDocumentMock2.Type.Returns(OrderDocumentType.Contract);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(contractDocumentMock1);
            
            // act
            contractDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, contractDocumentMock2);
            
            // assert
            Assert.AreEqual(2, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            OrderContractDocumentFactory contractDocumentFactoryMock = Substitute.For<OrderContractDocumentFactory>();
            OrderContractDocumentUpdater contractDocumentUpdater = new OrderContractDocumentUpdater(contractDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            OrderContract contractDocumentMock1 = Substitute.For<OrderContract>();
            contractDocumentMock1.Type.Returns(OrderDocumentType.Contract);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(contractDocumentMock1);
            
            // act
            contractDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, contractDocumentMock1);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}