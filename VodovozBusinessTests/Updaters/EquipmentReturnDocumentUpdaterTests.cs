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
    public class EquipmentReturnDocumentUpdaterTests {
         
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            EquipmentReturnDocumentFactory equipmentReturnDocumentFactoryMock = Substitute.For<EquipmentReturnDocumentFactory>();
            EquipmentReturnDocumentUpdater equipmentReturnDocumentUpdater = new EquipmentReturnDocumentUpdater(equipmentReturnDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.equipment);
            OrderEquipment orderEquipmentMock = Substitute.For<OrderEquipment>();
            orderEquipmentMock.Nomenclature.Returns(nomenclatureMock);
            orderEquipmentMock.Direction.Returns(Direction.PickUp);
            orderEquipmentMock.DirectionReason.Returns(DirectionReason.Rent);
            orderEquipmentMock.OwnType.Returns(OwnTypes.Rent);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Add(orderEquipmentMock);
            
            // act
            equipmentReturnDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(
                x => x.Type == OrderDocumentType.EquipmentReturn));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            EquipmentReturnDocumentFactory equipmentReturnDocumentFactoryMock = Substitute.For<EquipmentReturnDocumentFactory>();
            EquipmentReturnDocumentUpdater equipmentReturnDocumentUpdater = new EquipmentReturnDocumentUpdater(equipmentReturnDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Category.Returns(NomenclatureCategory.equipment);
            OrderEquipment orderEquipmentMock = Substitute.For<OrderEquipment>();
            orderEquipmentMock.Nomenclature.Returns(nomenclatureMock);
            orderEquipmentMock.Direction.Returns(Direction.PickUp);
            orderEquipmentMock.DirectionReason.Returns(DirectionReason.Rent);
            orderEquipmentMock.OwnType.Returns(OwnTypes.Rent);
            EquipmentReturnDocument equipmentReturnDocumentMock = Substitute.For<EquipmentReturnDocument>();
            equipmentReturnDocumentMock.Type.Returns(OrderDocumentType.EquipmentReturn);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(equipmentReturnDocumentMock);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            selfDeliveryOrderMock.ObservableOrderEquipments.Add(orderEquipmentMock);
            
            // act
            equipmentReturnDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            EquipmentReturnDocumentFactory equipmentReturnDocumentFactoryMock = Substitute.For<EquipmentReturnDocumentFactory>();
            EquipmentReturnDocumentUpdater equipmentReturnDocumentUpdater = new EquipmentReturnDocumentUpdater(equipmentReturnDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            EquipmentReturnDocument equipmentReturnDocumentMock = Substitute.For<EquipmentReturnDocument>();
            equipmentReturnDocumentMock.Type.Returns(OrderDocumentType.EquipmentReturn);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(equipmentReturnDocumentMock);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);

            // act
            equipmentReturnDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            EquipmentReturnDocumentFactory equipmentReturnDocumentFactoryMock = Substitute.For<EquipmentReturnDocumentFactory>();
            EquipmentReturnDocumentUpdater equipmentReturnDocumentUpdater = new EquipmentReturnDocumentUpdater(equipmentReturnDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            EquipmentReturnDocument equipmentReturnDocumentMock = Substitute.For<EquipmentReturnDocument>();
            equipmentReturnDocumentMock.Type.Returns(OrderDocumentType.EquipmentReturn);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            equipmentReturnDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, equipmentReturnDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.EquipmentReturn));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            EquipmentReturnDocumentFactory equipmentReturnDocumentFactoryMock = Substitute.For<EquipmentReturnDocumentFactory>();
            EquipmentReturnDocumentUpdater equipmentReturnDocumentUpdater = new EquipmentReturnDocumentUpdater(equipmentReturnDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            EquipmentReturnDocument doneWorkDocumentMock1 = Substitute.For<EquipmentReturnDocument>();
            doneWorkDocumentMock1.Type.Returns(OrderDocumentType.EquipmentReturn);
            EquipmentReturnDocument doneWorkDocumentMock2 = Substitute.For<EquipmentReturnDocument>();
            doneWorkDocumentMock2.Type.Returns(OrderDocumentType.EquipmentReturn);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(doneWorkDocumentMock1);
            
            // act
            equipmentReturnDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, doneWorkDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            EquipmentReturnDocumentFactory equipmentReturnDocumentFactoryMock = Substitute.For<EquipmentReturnDocumentFactory>();
            EquipmentReturnDocumentUpdater equipmentReturnDocumentUpdater = new EquipmentReturnDocumentUpdater(equipmentReturnDocumentFactoryMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            EquipmentReturnDocument equipmentReturnDocumentMock = Substitute.For<EquipmentReturnDocument>();
            equipmentReturnDocumentMock.Type.Returns(OrderDocumentType.EquipmentReturn);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(equipmentReturnDocumentMock);
            
            // act
            equipmentReturnDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, equipmentReturnDocumentMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}