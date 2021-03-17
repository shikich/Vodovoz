using System.Collections.Generic;

namespace Vodovoz.Domain.Orders.Documents {
    public class OrderDocumentsModel {

        private readonly OrderBase order;
        private readonly Dictionary<OrderDocumentType, OrderDocumentUpdaterBase> documentUpdaters;

        public OrderDocumentsModel(OrderBase order, 
                                   IOrderDocumentUpdatersFactory documentUpdatersFactory) {
            this.order = order;
            documentUpdaters = new Dictionary<OrderDocumentType, OrderDocumentUpdaterBase>();
            
            FillDocumentUpdaters(documentUpdatersFactory);
        }

        private void FillDocumentUpdaters(IOrderDocumentUpdatersFactory documentUpdatersFactory) {
            var updaters = documentUpdatersFactory.CreateUpdaters();
            
            foreach (var updater in updaters) {
                documentUpdaters.Add(updater.DocumentType, updater);
            }
        }

        public void UpdateDocuments() {
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
            AddExistingDocuments(fromOrder.ObservableOrderDocuments);
        }
        
        public void RemoveExistingDocuments(IEnumerable<OrderDocument> existingDocuments) {
            foreach (var document in existingDocuments) {
                documentUpdaters[document.Type].RemoveExistingDocument(order, document);
            }
        }
    }
}