using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Bill;
using Vodovoz.Services;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class SpecialBillDocumentUpdaterTests {
        
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.GetPaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            SpecialBillDocumentFactory specialBillDocumentFactoryMock = Substitute.For<SpecialBillDocumentFactory>();
            SpecialBillDocumentUpdater specialBillDocumentUpdater = 
                new SpecialBillDocumentUpdater(specialBillDocumentFactoryMock, billDocumentUpdater);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.UseSpecialDocFields.Returns(true);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Sum.Returns(500);
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            specialBillDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.SpecialBill));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.GetPaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            SpecialBillDocumentFactory specialBillDocumentFactoryMock = Substitute.For<SpecialBillDocumentFactory>();
            SpecialBillDocumentUpdater specialBillDocumentUpdater = 
                new SpecialBillDocumentUpdater(specialBillDocumentFactoryMock, billDocumentUpdater);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.UseSpecialDocFields.Returns(true);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            SpecialBillDocument specialBillDocumentMock = Substitute.For<SpecialBillDocument>();
            specialBillDocumentMock.Type.Returns(OrderDocumentType.SpecialBill);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialBillDocumentMock);
            
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Sum.Returns(500);
            orderItemMock.Nomenclature.Returns(nomenclatureMock);

            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            specialBillDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.GetPaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            SpecialBillDocumentFactory specialBillDocumentFactoryMock = Substitute.For<SpecialBillDocumentFactory>();
            SpecialBillDocumentUpdater specialBillDocumentUpdater = 
                new SpecialBillDocumentUpdater(specialBillDocumentFactoryMock, billDocumentUpdater);
            Counterparty counterpartyMock = Substitute.For<Counterparty>();
            counterpartyMock.UseSpecialDocFields.Returns(true);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            selfDeliveryOrderMock.Counterparty.Returns(counterpartyMock);
            SpecialBillDocument specialBillDocumentMock = Substitute.For<SpecialBillDocument>();
            specialBillDocumentMock.Type.Returns(OrderDocumentType.SpecialBill);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            GenericObservableList<OrderDepositItem> observableDeposits = new GenericObservableList<OrderDepositItem>();
            selfDeliveryOrderMock.ObservableOrderDepositItems.Returns(observableDeposits);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialBillDocumentMock);
            
           // act
           specialBillDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
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
            nomenclatureParametersProviderMock.GetPaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            SpecialBillDocumentFactory specialBillDocumentFactoryMock = Substitute.For<SpecialBillDocumentFactory>();
            SpecialBillDocumentUpdater specialBillDocumentUpdater = 
                new SpecialBillDocumentUpdater(specialBillDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            SpecialBillDocument specialBillDocumentMock = Substitute.For<SpecialBillDocument>();
            specialBillDocumentMock.Type.Returns(OrderDocumentType.SpecialBill);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            specialBillDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, specialBillDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.SpecialBill));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            INomenclatureParametersProvider nomenclatureParametersProviderMock =
                Substitute.For<INomenclatureParametersProvider>();
            nomenclatureParametersProviderMock.GetPaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            SpecialBillDocumentFactory specialBillDocumentFactoryMock = Substitute.For<SpecialBillDocumentFactory>();
            SpecialBillDocumentUpdater specialBillDocumentUpdater = 
                new SpecialBillDocumentUpdater(specialBillDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            SpecialBillDocument specialBillDocumentMock1 = Substitute.For<SpecialBillDocument>();
            specialBillDocumentMock1.Type.Returns(OrderDocumentType.SpecialBill);
            SpecialBillDocument specialBillDocumentMock2 = Substitute.For<SpecialBillDocument>();
            specialBillDocumentMock2.Type.Returns(OrderDocumentType.SpecialBill);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialBillDocumentMock1);
            
            // act
            specialBillDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, specialBillDocumentMock2);
            
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
            nomenclatureParametersProviderMock.GetPaidDeliveryNomenclatureId.Returns(5);
            BillDocumentFactory billDocumentFactoryMock = Substitute.For<BillDocumentFactory>();
            BillDocumentUpdater billDocumentUpdater = new BillDocumentUpdater(billDocumentFactoryMock, nomenclatureParametersProviderMock);
            SpecialBillDocumentFactory specialBillDocumentFactoryMock = Substitute.For<SpecialBillDocumentFactory>();
            SpecialBillDocumentUpdater specialBillDocumentUpdater = 
                new SpecialBillDocumentUpdater(specialBillDocumentFactoryMock, billDocumentUpdater);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            SpecialBillDocument specialBillDocumentMock1 = Substitute.For<SpecialBillDocument>();
            specialBillDocumentMock1.Type.Returns(OrderDocumentType.SpecialBill);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(specialBillDocumentMock1);
            
            // act
            specialBillDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, specialBillDocumentMock1);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}