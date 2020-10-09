using Vodovoz.Domain.Documents;

namespace Vodovoz.Domain.Orders.Documents {
    public abstract class OrderDocumentUpdaterBase {
        public abstract OrderDocumentType DocumentType { get; }
        public abstract void UpdateDocument(OrderBase order);
        public abstract void AddExistingDocument(OrderBase order, OrderDocument existingDocument);
        public abstract void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument);
    }
}