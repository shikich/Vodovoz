using System;
using System.Linq;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders.Documents.UPD;

namespace Vodovoz.Domain.Orders.Documents.Torg12 {
    public class Torg12DocumentUpdater : OrderDocumentUpdaterBase {

        private readonly Torg12DocumentFactory documentFactory;
        private readonly UPDDocumentUpdater updDocumentUpdater;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.Torg12;

        public Torg12DocumentUpdater(Torg12DocumentFactory documentFactory, 
                                     UPDDocumentUpdater updDocumentUpdater) {
            this.documentFactory = documentFactory;
            this.updDocumentUpdater =
                updDocumentUpdater ?? throw new ArgumentNullException(nameof(updDocumentUpdater));
        }

        private Torg12Document CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            if (order is IDefaultOrderDocumentType defaultDoc) {
                return updDocumentUpdater.NeedCreateDocument(order) && 
                       defaultDoc.DefaultDocumentType.HasValue &&
                       defaultDoc.DefaultDocumentType == DefaultDocumentType.torg12;
            }

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