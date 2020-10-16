using System.Linq;
using Vodovoz.Domain.Client;

namespace Vodovoz.Domain.Orders.Documents.Invoice {
    public class InvoiceDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly InvoiceDocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.Invoice;

        public InvoiceDocumentUpdater(InvoiceDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private InvoiceDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            var accepted = order.Status >= OrderStatus.Accepted;
            var waitForPayment = order.Status >= OrderStatus.WaitForPayment;
            
            var byCard = order.PaymentType == PaymentType.ByCard && order.ObservableOrderItems.Any();
            var cash = (order.PaymentType == PaymentType.cash || order.PaymentType == PaymentType.BeveragesWorld);
            var cashlessWithoutBottlesReturn = order.PaymentType == PaymentType.cashless &&
                                               (order.ObservableOrderItems.Sum(i => i.Sum) <= 0m) &&
                                               !order.ObservableOrderDepositItems.Any();

            switch (order.Type) {
                case OrderType.SelfDeliveryOrder:
                    var selfDeliveryOrder = order as SelfDeliveryOrder;
                    return (selfDeliveryOrder.PaymentType == PaymentType.cashless &&
                           (selfDeliveryOrder.ObservableOrderItems.Sum(i => i.Sum) <= 0m) &&
                           (!selfDeliveryOrder.ObservableOrderDepositItems.Any() || selfDeliveryOrder.BottlesReturn > 0) ||
                           byCard || cash) && waitForPayment;
                case OrderType.DeliveryOrder:
                    var deliveryOrder = order as DeliveryOrder;
                    return (deliveryOrder.PaymentType == PaymentType.cashless &&
                           (deliveryOrder.ObservableOrderItems.Sum(i => i.Sum) <= 0m) &&
                           (!deliveryOrder.ObservableOrderDepositItems.Any() || deliveryOrder.BottlesReturn > 0) ||
                           byCard || cash) && accepted;
                case OrderType.VisitingMasterOrder:
                case OrderType.ClosingDocOrder:
                case OrderType.OrderFrom1c:
                    return (cashlessWithoutBottlesReturn || byCard || cash) && accepted;
                default:
                    return false;
            }
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