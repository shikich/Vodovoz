using System;
using System.Linq;
using Vodovoz.Domain.Orders.Documents.Bill;

namespace Vodovoz.Domain.Orders.Documents.UPD {
    public class UPDDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly UPDDocumentFactory documentFactory;
        private readonly BillDocumentUpdater billDocumentUpdater;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.UPD;

        public UPDDocumentUpdater(UPDDocumentFactory documentFactory, 
                                  BillDocumentUpdater billDocumentUpdater) {
            this.documentFactory = documentFactory;
            this.billDocumentUpdater =
                billDocumentUpdater ?? throw new ArgumentNullException(nameof(billDocumentUpdater));
        }

        private UPDDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        public bool NeedCreateDocument(OrderBase order) {
            switch (order.Type) {
                case OrderType.DeliveryOrder:
                case OrderType.VisitingMasterOrder:
                case OrderType.ClosingDocOrder:
                case OrderType.OrderFrom1c:
                    return billDocumentUpdater.NeedCreateDocument(order) &&
                           order.Status >= OrderStatus.Accepted;
                case OrderType.SelfDeliveryOrder:
                    var selfDeliveryOrder = order as SelfDeliveryOrder;
                    return billDocumentUpdater.NeedCreateDocument(order) &&
                           (order.Status >= OrderStatus.Accepted ||
                           (order.Status == OrderStatus.WaitForPayment &&
                           selfDeliveryOrder.PayAfterShipment));
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