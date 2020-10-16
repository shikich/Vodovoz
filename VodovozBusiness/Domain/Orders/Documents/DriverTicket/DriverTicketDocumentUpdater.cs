using System;
using System.Linq;
using Vodovoz.Domain.Orders.Documents.Bill;

namespace Vodovoz.Domain.Orders.Documents.DriverTicket {
    public class DriverTicketDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly DriverTicketDocumentFactory documentFactory;
        private readonly BillDocumentUpdater billDocumentUpdater;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.DriverTicket;

        public DriverTicketDocumentUpdater(DriverTicketDocumentFactory documentFactory, 
                                           BillDocumentUpdater billDocumentUpdater) {
            this.documentFactory = documentFactory;
            this.billDocumentUpdater =
                billDocumentUpdater ?? throw new ArgumentNullException(nameof(billDocumentUpdater));
        }

        private DriverTicketDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        public bool NeedCreateDocument(OrderBase order) {
            return billDocumentUpdater.NeedCreateDocument(order) &&
                   order.Status >= OrderStatus.Accepted;
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