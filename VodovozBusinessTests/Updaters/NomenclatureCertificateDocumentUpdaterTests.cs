using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using NSubstitute;
using NUnit.Framework;
using QS.DomainModel.UoW;
using Vodovoz.Domain;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Certificate;
using Vodovoz.EntityRepositories.Goods;

namespace VodovozBusinessTests.Updaters {
    [TestFixture]
    public class NomenclatureCertificateDocumentUpdaterTests {
         
        #region UpdateDocumentMethod
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении в пустой список документов (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentOrderWithoutDocument()
        {
            // arrange
            NomenclatureCertificateDocumentFactory assemblyDocumentFactoryMock = Substitute.For<NomenclatureCertificateDocumentFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            Nomenclature nomenclatureMock2 = Substitute.For<Nomenclature>();
            Certificate certificateMock = Substitute.For<Certificate>();
            certificateMock.ExpirationDate.Returns(DateTime.Today);
            Dictionary<Nomenclature, IList<Certificate>> dictionaryCertsMock = new Dictionary<Nomenclature, IList<Certificate>> {
                {nomenclatureMock2, new List<Certificate> { certificateMock }}
            };
            Nomenclature nomenclatureMock = Substitute.For<Nomenclature>();
            nomenclatureMock.Id.Returns(1);
            nomenclatureRepositoryMock.GetDictionaryWithCertificatesForNomenclatures(uowMock, new Nomenclature[]{nomenclatureMock})
                                      .Returns(dictionaryCertsMock);
            NomenclatureCertificateDocumentUpdater certificateDocumentUpdater = 
                new NomenclatureCertificateDocumentUpdater(assemblyDocumentFactoryMock, nomenclatureRepositoryMock, uowMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.NeedAddCertificates.Returns(true);
            selfDeliveryOrderMock.DeliveryDate.Returns(DateTime.Today.AddMonths(-1));
            OrderItem orderItemMock = Substitute.For<OrderItem>();
            orderItemMock.Nomenclature.Returns(nomenclatureMock);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            GenericObservableList<OrderItem> observableItems = new GenericObservableList<OrderItem>();
            selfDeliveryOrderMock.ObservableOrderItems.Returns(observableItems);
            selfDeliveryOrderMock.ObservableOrderItems.Add(orderItemMock);

            // act
            certificateDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.ProductCertificate));
        }
        
        [Test(Description = "Проверка метода UpdateDocument при добавлении дубликата документа (ветка true)")]
        public void TestUpdateMethodIfNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            NomenclatureCertificateDocumentFactory assemblyDocumentFactoryMock = Substitute.For<NomenclatureCertificateDocumentFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            NomenclatureCertificateDocumentUpdater certificateDocumentUpdater = 
                new NomenclatureCertificateDocumentUpdater(assemblyDocumentFactoryMock, nomenclatureRepositoryMock, uowMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.EShopOrder.Returns(123);
            NomenclatureCertificateDocument certificateDocumentMock = Substitute.For<NomenclatureCertificateDocument>();
            certificateDocumentMock.Type.Returns(OrderDocumentType.ProductCertificate);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(certificateDocumentMock);

            // act
            certificateDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        [Test(Description = "Проверка метода UpdateDocument (ветка false)")]
        public void TestUpdateMethodIfDoNotNeedCreateDocumentAndOrderWithOrderDocument()
        {
            // arrange
            NomenclatureCertificateDocumentFactory assemblyDocumentFactoryMock = Substitute.For<NomenclatureCertificateDocumentFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            NomenclatureCertificateDocumentUpdater certificateDocumentUpdater = 
                new NomenclatureCertificateDocumentUpdater(assemblyDocumentFactoryMock, nomenclatureRepositoryMock, uowMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            NomenclatureCertificateDocument certificateDocumentMock = Substitute.For<NomenclatureCertificateDocument>();
            certificateDocumentMock.Type.Returns(OrderDocumentType.ProductCertificate);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(certificateDocumentMock);
            
            // act
            certificateDocumentUpdater.UpdateDocument(selfDeliveryOrderMock);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region AddExistingDocumentMethod

        [Test(Description = "Проверка метода AddExistingDocument без документов")]
        public void TestAddExistingDocumentMethodWithoutDocument()
        {
            // arrange
            NomenclatureCertificateDocumentFactory assemblyDocumentFactoryMock = Substitute.For<NomenclatureCertificateDocumentFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            NomenclatureCertificateDocumentUpdater certificateDocumentUpdater = 
                new NomenclatureCertificateDocumentUpdater(assemblyDocumentFactoryMock, nomenclatureRepositoryMock, uowMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            NomenclatureCertificateDocument certificateDocumentMock = Substitute.For<NomenclatureCertificateDocument>();
            certificateDocumentMock.Type.Returns(OrderDocumentType.ProductCertificate);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            
            // act
            certificateDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, certificateDocumentMock);
            
            // assert
            Assert.True(selfDeliveryOrderMock.ObservableOrderDocuments.Any(x => x.Type == OrderDocumentType.ProductCertificate));
        }
        
        [Test(Description = "Проверка метода AddExistingDocument при добавлении дубликата документа")]
        public void TestAddExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            NomenclatureCertificateDocumentFactory assemblyDocumentFactoryMock = Substitute.For<NomenclatureCertificateDocumentFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            NomenclatureCertificateDocumentUpdater certificateDocumentUpdater = 
                new NomenclatureCertificateDocumentUpdater(assemblyDocumentFactoryMock, nomenclatureRepositoryMock, uowMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            NomenclatureCertificateDocument certificateDocumentMock1 = Substitute.For<NomenclatureCertificateDocument>();
            certificateDocumentMock1.Type.Returns(OrderDocumentType.ProductCertificate);
            NomenclatureCertificateDocument certificateDocumentMock2 = Substitute.For<NomenclatureCertificateDocument>();
            certificateDocumentMock2.Type.Returns(OrderDocumentType.ProductCertificate);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(certificateDocumentMock1);
            
            // act
            certificateDocumentUpdater.AddExistingDocument(selfDeliveryOrderMock, certificateDocumentMock2);
            
            // assert
            Assert.AreEqual(1, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion

        #region RemoveExistingDocumentMethod
        
        [Test(Description = "Проверка метода RemoveExistingDocument")]
        public void TestRemoveExistingDocumentMethodAndOrderWithOrderDocument()
        {
            // arrange
            NomenclatureCertificateDocumentFactory assemblyDocumentFactoryMock = Substitute.For<NomenclatureCertificateDocumentFactory>();
            IUnitOfWork uowMock = Substitute.For<IUnitOfWork>();
            INomenclatureRepository nomenclatureRepositoryMock = Substitute.For<INomenclatureRepository>();
            NomenclatureCertificateDocumentUpdater certificateDocumentUpdater = 
                new NomenclatureCertificateDocumentUpdater(assemblyDocumentFactoryMock, nomenclatureRepositoryMock, uowMock);
            SelfDeliveryOrder selfDeliveryOrderMock = Substitute.For<SelfDeliveryOrder>();
            selfDeliveryOrderMock.PaymentType.Returns(PaymentType.cashless);
            NomenclatureCertificateDocument certificateDocumentMock1 = Substitute.For<NomenclatureCertificateDocument>();
            certificateDocumentMock1.Type.Returns(OrderDocumentType.ProductCertificate);
            GenericObservableList<OrderDocument> observableDocuments = new GenericObservableList<OrderDocument>();
            selfDeliveryOrderMock.ObservableOrderDocuments.Returns(observableDocuments);
            selfDeliveryOrderMock.ObservableOrderDocuments.Add(certificateDocumentMock1);
            
            // act
            certificateDocumentUpdater.RemoveExistingDocument(selfDeliveryOrderMock, certificateDocumentMock1);
            
            // assert
            Assert.AreEqual(0, selfDeliveryOrderMock.ObservableOrderDocuments.Count);
        }
        
        #endregion
    }
}