using System.Linq;
using Vodovoz.Domain.Employees;

namespace Vodovoz.Domain.Orders.Documents.OrderM2Proxy {
    public class OrderM2ProxyDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly OrderM2ProxyDocumentFactory documentFactory;
        public M2ProxyDocument M2ProxyDocument { get; set; }

        public override OrderDocumentType DocumentType => OrderDocumentType.M2Proxy;

        public OrderM2ProxyDocumentUpdater(OrderM2ProxyDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private OrderM2Proxy CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            if (M2ProxyDocument != null)
                return true;

            return false;
        }
        
        public override void UpdateDocument(OrderBase order) {
            if (NeedCreateDocument(order)) {
                AddNewDocument(order, CreateNewDocument());
            }
            else 
                RemoveDocument(order);
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            var orderM2ProxyDoc = existingDocument as OrderM2Proxy;
            
            if (!order.ObservableOrderDocuments.OfType<OrderM2Proxy>().Any(
                x => x.NewOrder.Id == order.Id &&
                                            x.M2Proxy == orderM2ProxyDoc.M2Proxy)) {
                
                var doc = CreateNewDocument();
                doc.M2Proxy = orderM2ProxyDoc.M2Proxy;
                doc.NewOrder = existingDocument.NewOrder;
                doc.AttachedToNewOrder = order;
                order.ObservableOrderDocuments.Add(doc);
            }
        }

        public override void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument) {
            RemoveDocument(order, existingDocument);
        }
        
        protected override void AddNewDocument(OrderBase order, OrderDocument document) {
            if (!order.ObservableOrderDocuments.Any(x => x.NewOrder.Id == order.Id && x.Type == document.Type)) {
                
                if(document is OrderM2Proxy orderM2Proxy)
                    orderM2Proxy.M2Proxy = M2ProxyDocument;
                
                document.NewOrder = order;
                document.AttachedToNewOrder = order;
                order.ObservableOrderDocuments.Add(document);
            }
        }
    }
}