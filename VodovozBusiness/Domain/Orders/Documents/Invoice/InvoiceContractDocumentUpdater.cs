using Vodovoz.Domain.Client;

namespace Vodovoz.Domain.Orders.Documents.Invoice {
    public class InvoiceContractDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly InvoiceContractDocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.InvoiceContractDoc;

        public InvoiceContractDocumentUpdater(InvoiceContractDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private InvoiceContractDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            return order.PaymentType == PaymentType.ContractDoc
                   && order.Status >= OrderStatus.Accepted;
        }
        
        public override void UpdateDocument(OrderBase order) {
            if (NeedCreateDocument(order)) {
                AddDocument(order, CreateNewDocument());
            }
            else {
                RemoveDocument(order);
            }
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            AddDocument(order, existingDocument);
        }

        public override void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument) {
            RemoveDocument(order, existingDocument);
        }
    }
}