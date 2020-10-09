using System.Linq;
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
                if (order.ObservableOrderDocuments.All(x => x.Type != DocumentType)) {
                    AddExistingDocument(order, CreateNewDocument());
                }
            }
            else {
                var doc = order.ObservableOrderDocuments.SingleOrDefault(
                    x => x.Type == DocumentType);

                if (doc != null) {
                    RemoveExistingDocument(order, doc);
                }
            }
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            order.AddDocument(existingDocument);
        }

        public override void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument) {
            order.RemoveDocument(existingDocument);
        }
    }
}