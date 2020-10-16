using System.Linq;

namespace Vodovoz.Domain.Orders.Documents.OrderContract {
    public class OrderContractDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly OrderContractDocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.Contract;

        public OrderContractDocumentUpdater(OrderContractDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private OrderContract CreateNewDocument() {
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
                    order.ObservableOrderDocuments.OfType<OrderContract>().SingleOrDefault();
                
                if(contract == null)
                    AddNewDocument(order, CreateNewDocument());
                else if (contract.Contract.Id != order.Contract.Id)
                    contract.Contract = order.Contract;
            }
            else 
                RemoveDocument(order);
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            var contractDoc = existingDocument as OrderContract;
           
            if (!order.ObservableOrderDocuments.OfType<OrderContract>().Any(x => 
                x.NewOrder.Id == order.Id &&
                x.Contract == contractDoc.Contract)) {
                
                var doc = CreateNewDocument();
                doc.Contract = contractDoc.Contract;
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
                
                if(document is OrderContract contract)
                    contract.Contract = order.Contract;
                
                document.NewOrder = order;
                document.AttachedToNewOrder = order;
                order.ObservableOrderDocuments.Add(document);
            }
        }
    }
}