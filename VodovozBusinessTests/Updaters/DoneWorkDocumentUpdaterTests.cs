using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.DoneWork;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class DoneWorkDocumentUpdaterTests {
         
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            DoneWorkDocumentFactory doneWorkDocumentFactoryMock = Substitute.For<DoneWorkDocumentFactory>();
            DoneWorkDocumentUpdater doneWorkDocumentUpdater = new DoneWorkDocumentUpdater(doneWorkDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.equipment);
            OrderEquipment orderEquipmentMock = Substitute.For<OrderEquipment>();
            orderEquipmentMock.Nomenclature.Returns(nomenclatureMock);
            orderEquipmentMock.Direction.Returns(Direction.Deliver);
            orderEquipmentMock.DirectionReason.Returns(DirectionReason.Repair);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Add(orderEquipmentMock);
            
            // act
            doneWorkDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(
                x => x.Type == OrderDocumentType.DoneWorkReport));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            DoneWorkDocumentFactory doneWorkDocumentFactoryMock = Substitute.For<DoneWorkDocumentFactory>();
            DoneWorkDocumentUpdater doneWorkDocumentUpdater = new DoneWorkDocumentUpdater(doneWorkDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.equipment);
            OrderEquipment orderEquipmentMock = Substitute.For<OrderEquipment>();
            orderEquipmentMock.Nomenclature.Returns(nomenclatureMock);
            orderEquipmentMock.Direction.Returns(Direction.Deliver);
            orderEquipmentMock.DirectionReason.Returns(DirectionReason.Repair);
            DoneWorkDocument doneWorkDocumentMock = Substitute.For<DoneWorkDocument>();
            doneWorkDocumentMock.Type.Returns(OrderDocumentType.DoneWorkReport);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(doneWorkDocumentMock);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Add(orderEquipmentMock);
            
            // act
            doneWorkDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            DoneWorkDocumentFactory doneWorkDocumentFactoryMock = Substitute.For<DoneWorkDocumentFactory>();
            DoneWorkDocumentUpdater doneWorkDocumentUpdater = new DoneWorkDocumentUpdater(doneWorkDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            DoneWorkDocument doneWorkDocumentMock = Substitute.For<DoneWorkDocument>();
            doneWorkDocumentMock.Type.Returns(OrderDocumentType.DoneWorkReport);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(doneWorkDocumentMock);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);

            // act
            doneWorkDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            DoneWorkDocumentFactory doneWorkDocumentFactoryMock = Substitute.For<DoneWorkDocumentFactory>();
            DoneWorkDocumentUpdater doneWorkDocumentUpdater = new DoneWorkDocumentUpdater(doneWorkDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            DoneWorkDocument doneWorkDocumentMock = Substitute.For<DoneWorkDocument>();
            doneWorkDocumentMock.Type.Returns(OrderDocumentType.DoneWorkReport);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            doneWorkDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, doneWorkDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.DoneWorkReport));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            DoneWorkDocumentFactory doneWorkDocumentFactoryMock = Substitute.For<DoneWorkDocumentFactory>();
            DoneWorkDocumentUpdater doneWorkDocumentUpdater = new DoneWorkDocumentUpdater(doneWorkDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            DoneWorkDocument doneWorkDocumentMock1 = Substitute.For<DoneWorkDocument>();
            doneWorkDocumentMock1.Type.Returns(OrderDocumentType.DoneWorkReport);
            DoneWorkDocument doneWorkDocumentMock2 = Substitute.For<DoneWorkDocument>();
            doneWorkDocumentMock2.Type.Returns(OrderDocumentType.DoneWorkReport);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(doneWorkDocumentMock1);
            
            // act
            doneWorkDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, doneWorkDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            DoneWorkDocumentFactory doneWorkDocumentFactoryMock = Substitute.For<DoneWorkDocumentFactory>();
            DoneWorkDocumentUpdater doneWorkDocumentUpdater = new DoneWorkDocumentUpdater(doneWorkDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            DoneWorkDocument doneWorkDocumentMock = Substitute.For<DoneWorkDocument>();
            doneWorkDocumentMock.Type.Returns(OrderDocumentType.DoneWorkReport);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(doneWorkDocumentMock);
            
            // act
            doneWorkDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, doneWorkDocumentMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}