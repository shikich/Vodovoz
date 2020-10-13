using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Equipment;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class EquipmentTransferDocumentUpdaterTests {
         
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            EquipmentTransferDocumentFactory equipmentTransferDocumentFactoryMock = Substitute.For<EquipmentTransferDocumentFactory>();
            EquipmentTransferDocumentUpdater equipmentTransferDocumentUpdater = 
                new EquipmentTransferDocumentUpdater(equipmentTransferDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.equipment);
            OrderEquipment orderEquipmentMock = Substitute.For<OrderEquipment>();
            orderEquipmentMock.Nomenclature.Returns(nomenclatureMock);
            orderEquipmentMock.Direction.Returns(Direction.PickUp);
            orderEquipmentMock.DirectionReason.Returns(DirectionReason.Cleaning);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Add(orderEquipmentMock);
            
            // act
            equipmentTransferDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(
                x => x.Type == OrderDocumentType.EquipmentTransfer));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            EquipmentTransferDocumentFactory equipmentTransferDocumentFactoryMock = Substitute.For<EquipmentTransferDocumentFactory>();
            EquipmentTransferDocumentUpdater equipmentTransferDocumentUpdater = new EquipmentTransferDocumentUpdater(equipmentTransferDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.equipment);
            OrderEquipment orderEquipmentMock = Substitute.For<OrderEquipment>();
            orderEquipmentMock.Nomenclature.Returns(nomenclatureMock);
            orderEquipmentMock.Direction.Returns(Direction.PickUp);
            orderEquipmentMock.DirectionReason.Returns(DirectionReason.Repair);
            EquipmentTransferDocument equipmentTransferDocumentMock = Substitute.For<EquipmentTransferDocument>();
            equipmentTransferDocumentMock.Type.Returns(OrderDocumentType.EquipmentTransfer);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(equipmentTransferDocumentMock);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Add(orderEquipmentMock);
            
            // act
            equipmentTransferDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            EquipmentTransferDocumentFactory equipmentTransferDocumentFactoryMock = Substitute.For<EquipmentTransferDocumentFactory>();
            EquipmentTransferDocumentUpdater equipmentTransferDocumentUpdater = new EquipmentTransferDocumentUpdater(equipmentTransferDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            EquipmentTransferDocument equipmentTransferDocumentMock = Substitute.For<EquipmentTransferDocument>();
            equipmentTransferDocumentMock.Type.Returns(OrderDocumentType.EquipmentTransfer);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(equipmentTransferDocumentMock);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);

            // act
            equipmentTransferDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            EquipmentTransferDocumentFactory equipmentTransferDocumentFactoryMock = Substitute.For<EquipmentTransferDocumentFactory>();
            EquipmentTransferDocumentUpdater equipmentTransferDocumentUpdater = new EquipmentTransferDocumentUpdater(equipmentTransferDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            EquipmentTransferDocument equipmentTransferDocumentMock = Substitute.For<EquipmentTransferDocument>();
            equipmentTransferDocumentMock.Type.Returns(OrderDocumentType.EquipmentTransfer);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            equipmentTransferDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, equipmentTransferDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.EquipmentTransfer));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            EquipmentTransferDocumentFactory equipmentTransferDocumentFactoryMock = Substitute.For<EquipmentTransferDocumentFactory>();
            EquipmentTransferDocumentUpdater equipmentTransferDocumentUpdater = new EquipmentTransferDocumentUpdater(equipmentTransferDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            EquipmentTransferDocument doneWorkDocumentMock1 = Substitute.For<EquipmentTransferDocument>();
            doneWorkDocumentMock1.Type.Returns(OrderDocumentType.EquipmentTransfer);
            EquipmentTransferDocument doneWorkDocumentMock2 = Substitute.For<EquipmentTransferDocument>();
            doneWorkDocumentMock2.Type.Returns(OrderDocumentType.EquipmentTransfer);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(doneWorkDocumentMock1);
            
            // act
            equipmentTransferDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, doneWorkDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            EquipmentTransferDocumentFactory equipmentTransferDocumentFactoryMock = Substitute.For<EquipmentTransferDocumentFactory>();
            EquipmentTransferDocumentUpdater equipmentTransferDocumentUpdater = new EquipmentTransferDocumentUpdater(equipmentTransferDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            EquipmentTransferDocument equipmentTransferDocumentMock = Substitute.For<EquipmentTransferDocument>();
            equipmentTransferDocumentMock.Type.Returns(OrderDocumentType.EquipmentTransfer);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(equipmentTransferDocumentMock);
            
            // act
            equipmentTransferDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, equipmentTransferDocumentMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}