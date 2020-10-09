using System;
using System.Linq;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders.Documents.UPD;

namespace Vodovoz.Domain.Orders.Documents.ShetFactura {
    public class ShetFacturaDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly ShetFacturaDocumentFactory documentFactory;
        private readonly UPDDocumentUpdater updDocumentUpdater;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.ShetFactura;

        public ShetFacturaDocumentUpdater(ShetFacturaDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
            this.updDocumentUpdater =
                updDocumentUpdater ?? throw new ArgumentNullException(nameof(updDocumentUpdater));
        }

        private ShetFacturaDocument CreateNewDocument() {
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

    public interface IDefaultOrderDocumentType {
        DefaultDocumentType? DocumentType { get; set; }
    }
}