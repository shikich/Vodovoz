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
            return updDocumentUpdater.NeedCreateDocument(order) &&
                   order.DocumentType == DefaultDocumentType.torg12;
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