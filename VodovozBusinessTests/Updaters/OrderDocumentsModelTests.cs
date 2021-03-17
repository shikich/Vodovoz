using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NSubstitute.Extensions;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Bill;
using Vodovoz.Domain.Orders.Documents.DriverTicket;
using Vodovoz.Domain.Orders.Documents.Invoice;
using Vodovoz.Services;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class OrderDocumentsModelTests {
        
        [Test(Description = "Проверка метода UpdateDocuments")]
        public void TestUpdateDocumentsMethod()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.PaidDeliveryNomenclatureId.Returns(5);
            var orderDocumentUpdatersFactoryMock = Substitute.For<IOrderDocumentUpdatersFactory>();
            var billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdaterMock = 
                new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            orderDocumentUpdatersFactoryMock.CreateUpdaters().Returns(new []{billDocumentUpdaterMock});
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Status.Returns(OrderStatus.Accepted);
            OrderDocumentsModel orderDocumentsModel = new OrderDocumentsModel(selfDeliveryOrderMock, orderDocumentUpdatersFactoryMock);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            orderItemMock.Sum.Returns(500);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderEquipment> observableEquipments = new GenericObservableList<OrderEquipment>();
            selfDeliveryOrderMock.ObservableOrderEquipments.Returns(observableEquipments);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            orderDocumentsModel.UpdateDocuments();
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Bill));
        }
        
        [Test(Description = "Проверка метода AddExistingDocuments(IEnumerable<OrderDocument> existingDocuments) (добавление в пустой список доков)")]
        public void TestAddExistingDocumentsMethod()
        {
            // arrange
            OrderDocumentUpdatersFactory orderDocumentUpdatersFactoryMock = Substitute.For<OrderDocumentUpdatersFactory>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            OrderDocumentsModel orderDocumentsModel = new OrderDocumentsModel(deliveryOrderMock, orderDocumentUpdatersFactoryMock);
            InvoiceDocument invoiceDocumentMock = Substitute.For<InvoiceDocument>();
            invoiceDocumentMock.Type.Returns(OrderDocumentType.Invoice);
            DriverTicketDocument driverTicketDocumentMock = Substitute.For<DriverTicketDocument>();
            driverTicketDocumentMock.Type.Returns(OrderDocumentType.DriverTicket);
            IList<OrderDocument> docs = new List<OrderDocument>();
            docs.Add(invoiceDocumentMock);
            docs.Add(driverTicketDocumentMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            deliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            orderDocumentsModel.AddExistingDocuments(docs);
            
            // assert
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Invoice));
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.DriverTicket));
        }
        
        [Test(Description = "Проверка метода AddExistingDocuments(IEnumerable<OrderDocument> existingDocuments) (добавление в непустой список доков)")]
        public void TestAddExistingDocumentsMethod2()
        {
            // arrange
            OrderDocumentUpdatersFactory orderDocumentUpdatersFactoryMock = Substitute.For<OrderDocumentUpdatersFactory>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            OrderDocumentsModel orderDocumentsModel = new OrderDocumentsModel(deliveryOrderMock, orderDocumentUpdatersFactoryMock);
            InvoiceDocument invoiceDocumentMock = Substitute.For<InvoiceDocument>();
            invoiceDocumentMock.Type.Returns(OrderDocumentType.Invoice);
            DriverTicketDocument driverTicketDocumentMock = Substitute.For<DriverTicketDocument>();
            driverTicketDocumentMock.Type.Returns(OrderDocumentType.DriverTicket);
            DriverTicketDocument driverTicketDocumentMock2 = Substitute.For<DriverTicketDocument>();
            driverTicketDocumentMock2.Type.Returns(OrderDocumentType.DriverTicket);
            driverTicketDocumentMock2.NewOrder.Id.Returns(5);
            IList<OrderDocument> docs = new List<OrderDocument>();
            docs.Add(invoiceDocumentMock);
            docs.Add(driverTicketDocumentMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            deliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            deliveryOrderMock.ObservableOrderDocuments.Add(driverTicketDocumentMock2);
            
            // act
            orderDocumentsModel.AddExistingDocuments(docs);
            
            // assert
            Assert.AreEqual(3, deliveryOrderMock.ObservableOrderDocuments.Count);
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Invoice));
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.DriverTicket));
        }
        
        [Test(Description = "Проверка метода AddExistingDocuments(OrderBase fromOrder) (добавление в пустой список доков)")]
        public void TestAddExistingDocumentsMethodFromOtherOrder()
        {
            // arrange
            OrderDocumentUpdatersFactory orderDocumentUpdatersFactoryMock = Substitute.For<OrderDocumentUpdatersFactory>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            OrderDocumentsModel orderDocumentsModel = new OrderDocumentsModel(deliveryOrderMock, orderDocumentUpdatersFactoryMock);
            InvoiceDocument invoiceDocumentMock = Substitute.For<InvoiceDocument>();
            invoiceDocumentMock.Type.Returns(OrderDocumentType.Invoice);
            DriverTicketDocument driverTicketDocumentMock = Substitute.For<DriverTicketDocument>();
            driverTicketDocumentMock.Type.Returns(OrderDocumentType.DriverTicket);
            GenericObservableList<OrderDocument> observableDocuments2 = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments2);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceDocumentMock);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(driverTicketDocumentMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            deliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            orderDocumentsModel.AddExistingDocuments(selfDeliveryOrderMock);
            
            // assert
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Invoice));
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.DriverTicket));
        }
        
        [Test(Description = "Проверка метода AddExistingDocuments(OrderBase fromOrder) (добавление в непустой список доков)")]
        public void TestAddExistingDocumentsMethodFromOtherOrder2()
        {
            // arrange
            OrderDocumentUpdatersFactory orderDocumentUpdatersFactoryMock = Substitute.For<OrderDocumentUpdatersFactory>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            OrderDocumentsModel orderDocumentsModel = new OrderDocumentsModel(deliveryOrderMock, orderDocumentUpdatersFactoryMock);
            InvoiceDocument invoiceDocumentMock = Substitute.For<InvoiceDocument>();
            invoiceDocumentMock.Type.Returns(OrderDocumentType.Invoice);
            DriverTicketDocument driverTicketDocumentMock = Substitute.For<DriverTicketDocument>();
            driverTicketDocumentMock.Type.Returns(OrderDocumentType.DriverTicket);
            DriverTicketDocument driverTicketDocumentMock2 = Substitute.For<DriverTicketDocument>();
            driverTicketDocumentMock2.Type.Returns(OrderDocumentType.DriverTicket);
            driverTicketDocumentMock2.NewOrder.Id.Returns(5);
            GenericObservableList<OrderDocument> observableDocuments2 = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments2);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(invoiceDocumentMock);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(driverTicketDocumentMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            deliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            deliveryOrderMock.ObservableOrderDocuments.Add(driverTicketDocumentMock2);
            
            // act
            orderDocumentsModel.AddExistingDocuments(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(3, deliveryOrderMock.ObservableOrderDocuments.Count);
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.Invoice));
            Assert.True(deliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.DriverTicket));
        }
        
        [Test(Description = "Проверка метода RemoveExistingDocuments(IEnumerable<OrderDocument> existingDocuments)")]
        public void TestRemoveExistingDocumentsMethod()
        {
            // arrange
            OrderDocumentUpdatersFactory orderDocumentUpdatersFactoryMock = Substitute.For<OrderDocumentUpdatersFactory>();
            DeliveryOrder deliveryOrderMock = Substitute.For<DeliveryOrder>();
            deliveryOrderMock.Type.Returns(OrderType.DeliveryOrder);
            OrderDocumentsModel orderDocumentsModel = new OrderDocumentsModel(deliveryOrderMock, orderDocumentUpdatersFactoryMock);
            InvoiceDocument invoiceDocumentMock = Substitute.For<InvoiceDocument>();
            invoiceDocumentMock.Type.Returns(OrderDocumentType.Invoice);
            DriverTicketDocument driverTicketDocumentMock = Substitute.For<DriverTicketDocument>();
            driverTicketDocumentMock.Type.Returns(OrderDocumentType.DriverTicket);
            IList<OrderDocument> docs = new List<OrderDocument>();
            docs.Add(invoiceDocumentMock);
            docs.Add(driverTicketDocumentMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            deliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            deliveryOrderMock.ObservableOrderDocuments.Add(invoiceDocumentMock);
            deliveryOrderMock.ObservableOrderDocuments.Add(driverTicketDocumentMock);
            
            // act
            orderDocumentsModel.RemoveExistingDocuments(docs);
            
            // assert
            Assert.AreEqual(0, deliveryOrderMock.ObservableOrderDocuments.Count);
        }
    }
}