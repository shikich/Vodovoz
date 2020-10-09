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

            var cashless = (order.PaymentType == PaymentType.cashless && 
                                (order.ObservableOrderItems.Sum(i => i.Sum) <= 0m)) &&
                                (!order.ObservableOrderDepositItems.Any() || order.BottlesReturn > 0);
            var byCard = order.PaymentType == PaymentType.ByCard && order.ObservableOrderItems.Any();
            var cash = (order.PaymentType == PaymentType.cash || order.PaymentType == PaymentType.BeveragesWorld);

            if(order.IsSelfDelivery) {
                return (cashless || byCard || cash) && waitForPayment;
            } else {
                return (cashless || byCard || cash ) && accepted;
            }
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