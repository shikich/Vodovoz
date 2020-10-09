using System.Linq;
using System.Xml.XPath;

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
            switch (order.Type) {
                case OrderType.DeliveryOrder:
                case OrderType.SelfDeliveryOrder:
                case OrderType.ClosingDocOrder:
                case OrderType.VisitingMasterOrder:
                    var eShopOrder = order as IEShopOrder;
                    return eShopOrder.EShopOrder.HasValue;
                default:
                    return false;
            }
        }

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