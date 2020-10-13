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