using System.Linq;

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
                AddNewDocument(order, CreateNewDocument());
            }
            else {
                RemoveDocument(order);
            }
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            if (!order.ObservableOrderDocuments.Any(x => x.NewOrder.Id == order.Id && x.Type == existingDocument.Type)) {
                var doc = CreateNewDocument();
                doc.NewOrder = existingDocument.NewOrder;
                doc.AttachedToNewOrder = order;
                order.ObservableOrderDocuments.Add(doc);
            }
        }

        public override void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument) {
            RemoveDocument(order, existingDocument);
        }
    }
}