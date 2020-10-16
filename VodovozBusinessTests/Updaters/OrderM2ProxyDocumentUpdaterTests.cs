using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.OrderM2Proxy;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class OrderM2ProxyDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            M2ProxyDocument m2ProxyDoc = Substitute.For<M2ProxyDocument>();
            OrderM2ProxyDocumentFactory orderM2ProxyDocumentFactoryMock = Substitute.For<OrderM2ProxyDocumentFactory>();
            OrderM2ProxyDocumentUpdater orderM2ProxyDocumentUpdater = new OrderM2ProxyDocumentUpdater(orderM2ProxyDocumentFactoryMock);
            orderM2ProxyDocumentUpdater.M2ProxyDocument = m2ProxyDoc;
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            orderM2ProxyDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.M2Proxy));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            M2ProxyDocument m2ProxyDoc = Substitute.For<M2ProxyDocument>();
            OrderM2ProxyDocumentFactory orderM2ProxyDocumentFactoryMock = Substitute.For<OrderM2ProxyDocumentFactory>();
            OrderM2ProxyDocumentUpdater orderM2ProxyDocumentUpdater = new OrderM2ProxyDocumentUpdater(orderM2ProxyDocumentFactoryMock);
            orderM2ProxyDocumentUpdater.M2ProxyDocument = m2ProxyDoc;
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            OrderM2Proxy orderM2ProxyDocumentMock = Substitute.For<OrderM2Proxy>();
            orderM2ProxyDocumentMock.Type.Returns(OrderDocumentType.M2Proxy);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(orderM2ProxyDocumentMock);

            // act
            orderM2ProxyDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            OrderM2ProxyDocumentFactory orderM2ProxyDocumentFactoryMock = Substitute.For<OrderM2ProxyDocumentFactory>();
            OrderM2ProxyDocumentUpdater orderM2ProxyDocumentUpdater = new OrderM2ProxyDocumentUpdater(orderM2ProxyDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            OrderM2Proxy orderM2ProxyDocumentMock = Substitute.For<OrderM2Proxy>();
            orderM2ProxyDocumentMock.Type.Returns(OrderDocumentType.M2Proxy);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(orderM2ProxyDocumentMock);
            
            // act
            orderM2ProxyDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            OrderM2ProxyDocumentFactory orderM2ProxyDocumentFactoryMock = Substitute.For<OrderM2ProxyDocumentFactory>();
            OrderM2ProxyDocumentUpdater orderM2ProxyDocumentUpdater = new OrderM2ProxyDocumentUpdater(orderM2ProxyDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            OrderM2Proxy orderM2ProxyDocumentMock = Substitute.For<OrderM2Proxy>();
            orderM2ProxyDocumentMock.Type.Returns(OrderDocumentType.M2Proxy);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            orderM2ProxyDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, orderM2ProxyDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.M2Proxy));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            M2ProxyDocument m2ProxyDoc = Substitute.For<M2ProxyDocument>();
            OrderM2ProxyDocumentFactory orderM2ProxyDocumentFactoryMock = Substitute.For<OrderM2ProxyDocumentFactory>();
            OrderM2ProxyDocumentUpdater orderM2ProxyDocumentUpdater = new OrderM2ProxyDocumentUpdater(orderM2ProxyDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            OrderM2Proxy orderM2ProxyDocumentMock1 = Substitute.For<OrderM2Proxy>();
            orderM2ProxyDocumentMock1.Type.Returns(OrderDocumentType.M2Proxy);
            orderM2ProxyDocumentMock1.M2Proxy.Returns(m2ProxyDoc);
            OrderM2Proxy orderM2ProxyDocumentMock2 = Substitute.For<OrderM2Proxy>();
            orderM2ProxyDocumentMock2.Type.Returns(OrderDocumentType.M2Proxy);
            orderM2ProxyDocumentMock2.M2Proxy.Returns(m2ProxyDoc);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(orderM2ProxyDocumentMock1);
            
            // act
            orderM2ProxyDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, orderM2ProxyDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            OrderM2ProxyDocumentFactory orderM2ProxyDocumentFactoryMock = Substitute.For<OrderM2ProxyDocumentFactory>();
            OrderM2ProxyDocumentUpdater orderM2ProxyDocumentUpdater = new OrderM2ProxyDocumentUpdater(orderM2ProxyDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            OrderM2Proxy orderM2ProxyDocumentMock1 = Substitute.For<OrderM2Proxy>();
            orderM2ProxyDocumentMock1.Type.Returns(OrderDocumentType.M2Proxy);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(orderM2ProxyDocumentMock1);
            
            // act
            orderM2ProxyDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, orderM2ProxyDocumentMock1);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}