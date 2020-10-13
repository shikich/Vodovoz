using System.Linq;

namespace Vodovoz.Domain.Orders.Documents.AssemblyList {
    public class AssemblyListDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly AssemblyListDocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.AssemblyList;

        public AssemblyListDocumentUpdater(AssemblyListDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private AssemblyListDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            if(order is IEShopOrder eShopOrder)
               return eShopOrder.EShopOrder.HasValue;
            
            return false;
        }

        public override void UpdateDocument(OrderBase order) {
            if (NeedCreateDocument(order)) {
                AddDocument(order, CreateNewDocument());
            }
            else {
                RemoveDocument(order);
            }
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            AddDocument(order, existingDocument);
        }

        public override void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument) {
            RemoveDocument(order, existingDocument);
        }
    }
}