using System.Linq;

namespace Vodovoz.Domain.Orders.Documents {
    public abstract class OrderDocumentUpdaterBase {
        public abstract OrderDocumentType DocumentType { get; }
        public abstract void UpdateDocument(OrderBase order);
        public abstract void AddExistingDocument(OrderBase order, OrderDocument existingDocument);
        public abstract void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument);

        protected virtual void AddNewDocument(OrderBase order, OrderDocument document) {
            if (!order.ObservableOrderDocuments.Any(x => x.NewOrder.Id == order.Id && x.Type == document.Type)) {
                document.NewOrder = order;
                document.AttachedToNewOrder = order;
                order.ObservableOrderDocuments.Add(document);
            }
        }

        protected virtual void RemoveDocument(OrderBase order) {
            var doc = order.ObservableOrderDocuments.SingleOrDefault(
                x => x.Type == DocumentType && x.NewOrder.Id == order.Id);

            if (doc != null) {
                order.ObservableOrderDocuments.Remove(doc);
            }
        }
        
        protected virtual void RemoveDocument(OrderBase order, OrderDocument existingDocument) {
            order.ObservableOrderDocuments.Remove(existingDocument);
        }
    }
}