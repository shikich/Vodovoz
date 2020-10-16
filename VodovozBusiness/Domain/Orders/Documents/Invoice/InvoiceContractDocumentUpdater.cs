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