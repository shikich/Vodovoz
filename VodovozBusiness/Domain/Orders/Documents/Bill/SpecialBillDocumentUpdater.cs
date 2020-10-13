using System;
using System.Linq;

namespace Vodovoz.Domain.Orders.Documents.Bill {
    public class SpecialBillDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly SpecialBillDocumentFactory documentFactory;
        private readonly BillDocumentUpdater billDocumentUpdater;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.SpecialBill;

        public SpecialBillDocumentUpdater(SpecialBillDocumentFactory documentFactory,
                                          BillDocumentUpdater billDocumentUpdater) {
            this.documentFactory = documentFactory;
            this.billDocumentUpdater = billDocumentUpdater ?? throw new ArgumentNullException(nameof(billDocumentUpdater));
        }

        private SpecialBillDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            return billDocumentUpdater.NeedCreateDocument(order)
                && order.Counterparty.UseSpecialDocFields;
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