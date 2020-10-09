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
                    return billDocumentUpdater.NeedCreateDocument(order) && order.Status >= OrderStatus.Accepted;
                case OrderType.SelfDeliveryOrder:
                    var selfDeliveryOrder = order as SelfDeliveryOrder;
                    return billDocumentUpdater.NeedCreateDocument(order) && selfDeliveryOrder.PayAfterShipment;
            }

            return false;
            /*return billDocumentUpdater.NeedCreateDocument(order) && 
                   (order.Status >= OrderStatus.Accepted || 
                   (order.Status == OrderStatus.WaitForPayment && order.IsSelfDelivery && order.PayAfterShipment));*/
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