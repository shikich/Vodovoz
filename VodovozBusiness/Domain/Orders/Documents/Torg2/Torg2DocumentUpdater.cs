using System.Linq;

namespace Vodovoz.Domain.Orders.Documents.Torg2 {
    public class Torg2DocumentUpdater : OrderDocumentUpdaterBase {

        private readonly Torg2DocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.Torg2;

        public Torg2DocumentUpdater(Torg2DocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private Torg2Document CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) => order.Counterparty.Torg2Count.HasValue;

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