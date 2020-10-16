using System;
using System.Linq;

namespace Vodovoz.Domain.Orders.Documents.UPD {
    public class SpecialUPDDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly SpecialUPDDocumentFactory documentFactory;
        private readonly UPDDocumentUpdater updDocumentUpdater;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.SpecialUPD;

        public SpecialUPDDocumentUpdater(SpecialUPDDocumentFactory documentFactory, 
                                         UPDDocumentUpdater updDocumentUpdater) {
            this.documentFactory = documentFactory;
            this.updDocumentUpdater =
                updDocumentUpdater ?? throw new ArgumentNullException(nameof(updDocumentUpdater));
        }

        private SpecialUPDDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            return updDocumentUpdater.NeedCreateDocument(order) &&
                   order.Counterparty.UseSpecialDocFields;
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