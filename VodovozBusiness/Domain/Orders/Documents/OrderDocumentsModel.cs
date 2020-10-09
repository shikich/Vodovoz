using System.Collections.Generic;

namespace Vodovoz.Domain.Orders.Documents {
    public class OrderDocumentsModel {

        private readonly OrderBase order;
        private readonly Dictionary<OrderDocumentType, OrderDocumentUpdaterBase> documentUpdaters;

        public OrderDocumentsModel(OrderBase order, OrderDocumentUpdatersFactory documentUpdatersFactory) {
            this.order = order;
            
        }
        
        public void Updatedocuments() {
            foreach (var updater in documentUpdaters) {
                updater.Value.UpdateDocument(order);
            }
        }

        public void AddExistingDocuments(IEnumerable<OrderDocument> existingDocuments) {
            foreach (var document in existingDocuments) {
                documentUpdaters[document.Type].AddExistingDocument(order, document);
            }
        }
        
        public void AddExistingDocuments(OrderBase fromOrder) {
            
        }
        
        public void RemoveExistingDocuments(IEnumerable<OrderDocument> existingDocuments) {
            foreach (var document in existingDocuments) {
                documentUpdaters[document.Type].RemoveExistingDocument(order, document);
            }
        }
    }
}