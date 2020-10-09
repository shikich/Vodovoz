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