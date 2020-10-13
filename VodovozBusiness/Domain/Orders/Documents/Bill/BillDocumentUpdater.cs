using System.Linq;
using Vodovoz.Domain.Client;

namespace Vodovoz.Domain.Orders.Documents.Bill {
    public class BillDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly BillDocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.Bill;

        public BillDocumentUpdater(BillDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private BillDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        public bool NeedCreateDocument(OrderBase order) {
            return order.PaymentType == PaymentType.cashless
                && !(order.ObservableOrderItems.Sum(i => i.Sum) <= 0m)
                && !order.ObservableOrderDepositItems.Any()
                && order.ObservableOrderItems.Any();
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