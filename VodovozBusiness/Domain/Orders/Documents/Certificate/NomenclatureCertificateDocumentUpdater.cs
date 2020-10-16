using System;
using System.Collections.Generic;
using System.Linq;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Goods;
using Vodovoz.EntityRepositories.Goods;

namespace Vodovoz.Domain.Orders.Documents.Certificate {
    public class NomenclatureCertificateDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly NomenclatureCertificateDocumentFactory documentFactory;
        private readonly INomenclatureRepository nomenclatureRepository;
        private readonly IUnitOfWork uow;
        private Domain.Certificate Certificate { get; set; }
        
        public override OrderDocumentType DocumentType => OrderDocumentType.ProductCertificate;
        public IList<Nomenclature> NomenclaturesNeedsUpdate { get; } = new List<Nomenclature>();

        public NomenclatureCertificateDocumentUpdater(NomenclatureCertificateDocumentFactory documentFactory,
                                                      INomenclatureRepository nomenclatureRepository,
                                                      IUnitOfWork uow) {
            this.documentFactory = documentFactory;
            this.nomenclatureRepository = nomenclatureRepository ??
                                          throw new ArgumentNullException(nameof(nomenclatureRepository));
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
        }

        private NomenclatureCertificateDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            return order.NeedAddCertificates && order.DeliveryDate.HasValue;
        }
        
        public override void UpdateDocument(OrderBase order) {
            if(NeedCreateDocument(order)) {
                IList<Domain.Certificate> newList = new List<Domain.Certificate>();

                var certificates =
                    nomenclatureRepository.GetDictionaryWithCertificatesForNomenclatures(uow,
                        order.ObservableOrderItems.Select(i => i.Nomenclature).ToArray());
                
                foreach(var item in certificates) {
                    if(item.Value.All(c => c.IsArchive || c.ExpirationDate.HasValue && c.ExpirationDate.Value < order.DeliveryDate))
                        NomenclaturesNeedsUpdate.Add(item.Key);
                    else
                        newList.Add(item.Value.FirstOrDefault(c => c.ExpirationDate == item.Value.Max(cert => cert.ExpirationDate)));
                }

                newList = newList.Distinct().ToList();
                var oldList = order.ObservableOrderDocuments.Where(d => d.Type == OrderDocumentType.ProductCertificate)
                                   .Cast<NomenclatureCertificateDocument>()
                                   .Select(c => c.Certificate)
                                   .ToList();

                foreach(var cer in oldList) {
                    if(newList.All(c => c != cer)) {
                        var removingDoc = order.ObservableOrderDocuments.Where(d => d.Type == OrderDocumentType.ProductCertificate)
                                                        .Cast<NomenclatureCertificateDocument>()
                                                        .FirstOrDefault(d => d.Certificate == cer);
                        RemoveDocument(order, removingDoc);
                    }
                }

                foreach(var cer in newList) {
                    if (oldList.All(c => c != cer)) {
                        Certificate = cer;
                        AddNewDocument(order, CreateNewDocument());
                    }
                }
            }
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            if (!order.ObservableOrderDocuments.Any(
                x => x.NewOrder.Id == order.Id && x.Type == existingDocument.Type)) {
                var doc = CreateNewDocument();

                if (existingDocument is NomenclatureCertificateDocument certificateDoc)
                    doc.Certificate = certificateDoc.Certificate;
                
                doc.NewOrder = existingDocument.NewOrder;
                doc.AttachedToNewOrder = order;
                order.ObservableOrderDocuments.Add(doc);
            }
        }

        public override void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument) {
            RemoveDocument(order, existingDocument);
        }

        protected override void AddNewDocument(OrderBase order, OrderDocument document) {
            
            if (document is NomenclatureCertificateDocument certificateDoc)
                certificateDoc.Certificate = Certificate;
            
            document.NewOrder = order;
            document.AttachedToNewOrder = order;
            order.ObservableOrderDocuments.Add(document);
        }
    }
}