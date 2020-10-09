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