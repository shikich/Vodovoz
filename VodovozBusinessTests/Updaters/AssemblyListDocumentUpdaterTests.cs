using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.AssemblyList;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class AssemblyListDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            AssemblyListDocumentFactory assemblyDocumentFactoryMock = Substitute.For<AssemblyListDocumentFactory>();
            AssemblyListDocumentUpdater assemblyDocumentUpdater = new AssemblyListDocumentUpdater(assemblyDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.EShopOrder.Returns(123);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            assemblyDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.AssemblyList));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            AssemblyListDocumentFactory assemblyDocumentFactoryMock = Substitute.For<AssemblyListDocumentFactory>();
            AssemblyListDocumentUpdater assemblyDocumentUpdater = new AssemblyListDocumentUpdater(assemblyDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.EShopOrder.Returns(123);
            AssemblyListDocument assemblyListDocumentMock = Substitute.For<AssemblyListDocument>();
            assemblyListDocumentMock.Type.Returns(OrderDocumentType.AssemblyList);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(assemblyListDocumentMock);

            // act
            assemblyDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            AssemblyListDocumentFactory assemblyDocumentFactoryMock = Substitute.For<AssemblyListDocumentFactory>();
            AssemblyListDocumentUpdater assemblyDocumentUpdater = new AssemblyListDocumentUpdater(assemblyDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            AssemblyListDocument assemblyListDocumentMock = Substitute.For<AssemblyListDocument>();
            assemblyListDocumentMock.Type.Returns(OrderDocumentType.AssemblyList);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(assemblyListDocumentMock);
            
           // act
           assemblyDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            AssemblyListDocumentFactory assemblyDocumentFactoryMock = Substitute.For<AssemblyListDocumentFactory>();
            AssemblyListDocumentUpdater assemblyDocumentUpdater = new AssemblyListDocumentUpdater(assemblyDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            AssemblyListDocument assemblyListDocumentMock = Substitute.For<AssemblyListDocument>();
            assemblyListDocumentMock.Type.Returns(OrderDocumentType.AssemblyList);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            assemblyDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, assemblyListDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.AssemblyList));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            AssemblyListDocumentFactory assemblyDocumentFactoryMock = Substitute.For<AssemblyListDocumentFactory>();
            AssemblyListDocumentUpdater assemblyDocumentUpdater = new AssemblyListDocumentUpdater(assemblyDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            AssemblyListDocument assemblyListDocumentMock1 = Substitute.For<AssemblyListDocument>();
            assemblyListDocumentMock1.Type.Returns(OrderDocumentType.AssemblyList);
            AssemblyListDocument assemblyListDocumentMock2 = Substitute.For<AssemblyListDocument>();
            assemblyListDocumentMock2.Type.Returns(OrderDocumentType.AssemblyList);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(assemblyListDocumentMock1);
            
            // act
            assemblyDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, assemblyListDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            AssemblyListDocumentFactory assemblyDocumentFactoryMock = Substitute.For<AssemblyListDocumentFactory>();
            AssemblyListDocumentUpdater assemblyDocumentUpdater = new AssemblyListDocumentUpdater(assemblyDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            AssemblyListDocument assemblyListDocumentMock1 = Substitute.For<AssemblyListDocument>();
            assemblyListDocumentMock1.Type.Returns(OrderDocumentType.AssemblyList);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(assemblyListDocumentMock1);
            
            // act
            assemblyDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, assemblyListDocumentMock1);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}