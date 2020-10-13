using System.Linq;

namespace Vodovoz.Domain.Orders.Documents.Certificate {
    public class NomenclatureCertificateDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly NomenclatureCertificateDocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.ProductCertificate;

        public NomenclatureCertificateDocumentUpdater(NomenclatureCertificateDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private NomenclatureCertificateDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            return false;
        }
        
        public override void UpdateDocument(OrderBase order) {
            
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            AddDocument(order, existingDocument);
        }

        public override void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument) {
            RemoveDocument(order, existingDocument);
        }
    }
}