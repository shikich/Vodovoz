using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Invoice;
using Vodovoz.Services;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class InvoiceDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentSelfDeliveryOrderWithoutDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            InvoiceDocumentFactory invoiceDocumentFactoryMock = Substitute.For<InvoiceDocumentFactory>();
            InvoiceDocumentUpdater invoiceDocumentUpdater = new InvoiceDocumentUpdater(invoiceDocumentFactoryMock, nomenclatureParametersProviderMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.WaitForPayment);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);
            
            // act
            invoiceDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Invoice));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentDeliveryOrderWithoutDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            InvoiceDocumentFactory invoiceDocumentFactoryMock = Substitute.For<InvoiceDocumentFactory>();
            InvoiceDocumentUpdater invoiceDocumentUpdater = new InvoiceDocumentUpdater(invoiceDocumentFactoryMock, nomenclatureParametersProviderMock);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            deliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            deliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            deliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            deliveryOrderMock.ObservableOrderItems.Add(orderItemMock);
            
            // act
            invoiceDocumentUpdater.UpdateDocument(deliveryOrderMock);
            
            // assert
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Invoice));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentVisitingMasterOrderWithoutDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            InvoiceDocumentFactory invoiceDocumentFactoryMock = Substitute.For<InvoiceDocumentFactory>();
            InvoiceDocumentUpdater invoiceDocumentUpdater = new InvoiceDocumentUpdater(invoiceDocumentFactoryMock, nomenclatureParametersProviderMock);
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            deliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            deliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            deliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            deliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            deliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            deliveryOrderMock.ObservableOrderItems.Add(orderItemMock);
            
            // act
            invoiceDocumentUpdater.UpdateDocument(deliveryOrderMock);
            
            // assert
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Invoice));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            InvoiceDocumentFactory invoiceDocumentFactoryMock = Substitute.For<InvoiceDocumentFactory>();
            InvoiceDocumentUpdater invoiceDocumentUpdater = new InvoiceDocumentUpdater(invoiceDocumentFactoryMock, nomenclatureParametersProviderMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.WaitForPayment);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            invoiceDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            InvoiceDocumentFactory invoiceDocumentFactoryMock = Substitute.For<InvoiceDocumentFactory>();
            InvoiceDocumentUpdater invoiceDocumentUpdater = new InvoiceDocumentUpdater(invoiceDocumentFactoryMock, nomenclatureParametersProviderMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceDocument invoiceDocumentMock = Substitute.For<InvoiceDocument>();
            invoiceDocumentMock.Type.Returns(OrderDocumentType.Invoice);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceDocumentMock);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            
            // act
            invoiceDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            InvoiceDocumentFactory invoiceDocumentFactoryMock = Substitute.For<InvoiceDocumentFactory>();
            InvoiceDocumentUpdater invoiceDocumentUpdater = new InvoiceDocumentUpdater(invoiceDocumentFactoryMock, nomenclatureParametersProviderMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceDocument invoiceDocumentMock = Substitute.For<InvoiceDocument>();
            invoiceDocumentMock.Type.Returns(OrderDocumentType.Invoice);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            invoiceDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, invoiceDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Invoice));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            InvoiceDocumentFactory invoiceDocumentFactoryMock = Substitute.For<InvoiceDocumentFactory>();
            InvoiceDocumentUpdater invoiceDocumentUpdater = new InvoiceDocumentUpdater(invoiceDocumentFactoryMock, nomenclatureParametersProviderMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceDocument invoiceDocumentMock = Substitute.For<InvoiceDocument>();
            invoiceDocumentMock.Type.Returns(OrderDocumentType.Invoice);
            InvoiceDocument invoiceDocumentMock2 = Substitute.For<InvoiceDocument>();
            invoiceDocumentMock2.Type.Returns(OrderDocumentType.Invoice);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceDocumentMock);
            
            // act
            invoiceDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, invoiceDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            InvoiceDocumentFactory invoiceDocumentFactoryMock = Substitute.For<InvoiceDocumentFactory>();
            InvoiceDocumentUpdater invoiceDocumentUpdater = new InvoiceDocumentUpdater(invoiceDocumentFactoryMock, nomenclatureParametersProviderMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            InvoiceDocument invoiceDocumentMock = Substitute.For<InvoiceDocument>();
            invoiceDocumentMock.Type.Returns(OrderDocumentType.Invoice);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceDocumentMock);
            
            // act
            invoiceDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, invoiceDocumentMock);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}