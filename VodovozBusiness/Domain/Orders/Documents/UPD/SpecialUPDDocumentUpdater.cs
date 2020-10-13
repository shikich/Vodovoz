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