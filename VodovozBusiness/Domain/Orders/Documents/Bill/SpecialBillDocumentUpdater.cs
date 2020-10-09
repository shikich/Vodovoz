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