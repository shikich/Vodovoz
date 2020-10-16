using System;
using System.Linq;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders.Documents.UPD;

namespace Vodovoz.Domain.Orders.Documents.ShetFactura {
    public class ShetFacturaDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly ShetFacturaDocumentFactory documentFactory;
        private readonly UPDDocumentUpdater updDocumentUpdater;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.ShetFactura;

        public ShetFacturaDocumentUpdater(ShetFacturaDocumentFactory documentFactory, 
                                          UPDDocumentUpdater updDocumentUpdater) {
            this.documentFactory = documentFactory;
            this.updDocumentUpdater =
                updDocumentUpdater ?? throw new ArgumentNullException(nameof(updDocumentUpdater));
        }

        private ShetFacturaDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            if (order is IDefaultOrderDocumentType defaultDoc) {
                var defaultDocType = 
                    defaultDoc.DefaultDocumentType ?? order.Counterparty.DefaultDocumentType;
                
                return updDocumentUpdater.NeedCreateDocument(order) && 
                       defaultDocType == DefaultDocumentType.torg12;
            }

            return false;
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