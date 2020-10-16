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
                AddNewDocument(order, CreateNewDocument());
            }
            else {
                RemoveDocument(order);
            }
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            if (!order.ObservableOrderDocuments.Any(x => x.NewOrder.Id == order.Id && x.Type == existingDocument.Type)) {
                var doc = CreateNewDocument();
                doc.NewOrder = existingDocument.NewOrder;
                doc.AttachedToNewOrder = order;
                order.ObservableOrderDocuments.Add(doc);
            }
        }

        public override void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument) {
            RemoveDocument(order, existingDocument);
        }
    }
}