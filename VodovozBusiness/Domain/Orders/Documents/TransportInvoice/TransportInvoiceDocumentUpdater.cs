using System.Linq;
using Vodovoz.Domain.Client;

namespace Vodovoz.Domain.Orders.Documents.TransportInvoice {
    public class TransportInvoiceDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly TransportInvoiceDocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.TransportInvoice;

        public TransportInvoiceDocumentUpdater(TransportInvoiceDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private TransportInvoiceDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            return order.Counterparty.TTNCount.HasValue && 
                   order.ObservableOrderItems.Any();
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