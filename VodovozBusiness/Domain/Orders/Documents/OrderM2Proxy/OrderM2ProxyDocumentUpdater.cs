using System.Linq;

namespace Vodovoz.Domain.Orders.Documents.OrderM2Proxy {
    public class OrderM2ProxyDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly OrderM2ProxyDocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.M2Proxy;

        public OrderM2ProxyDocumentUpdater(OrderM2ProxyDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private OrderM2Proxy CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            if (order.Contract != null)
                return true;

            return false;
        }
        
        public override void UpdateDocument(OrderBase order) {
            if (NeedCreateDocument(order)) {
                var contract =
                    order.ObservableOrderDocuments.OfType<OrderContract.OrderContract>().SingleOrDefault();
                
                if(contract == null)
                    AddExistingDocument(order, CreateNewDocument());
                else if (contract.Contract.Id != order.Contract.Id)
                    contract.Contract = order.Contract;
            }
            else {
                var contract =
                    order.ObservableOrderDocuments.OfType<OrderContract.OrderContract>().SingleOrDefault();

                if (contract != null) {
                    RemoveExistingDocument(order, contract);
                }
            }
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            if (order.ObservableOrderDocuments.All(x => x.Type != DocumentType)) {
                //order.AddDocument(existingDocument);
            }
        }

        public override void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument) {
            //order.RemoveDocument(existingDocument);
        }
    }
}