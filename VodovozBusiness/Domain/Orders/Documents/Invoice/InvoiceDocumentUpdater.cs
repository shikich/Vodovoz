using System;
using System.Linq;
using Vodovoz.Domain.Client;
using Vodovoz.Services;

namespace Vodovoz.Domain.Orders.Documents.Invoice {
    public class InvoiceDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly InvoiceDocumentFactory documentFactory;
        private readonly INomenclatureParametersProvider nomenclatureParametersProvider;

        public override OrderDocumentType DocumentType => OrderDocumentType.Invoice;

        public InvoiceDocumentUpdater(InvoiceDocumentFactory documentFactory,
                                      INomenclatureParametersProvider nomenclatureParametersProvider) {
            this.documentFactory = documentFactory;
            this.nomenclatureParametersProvider = nomenclatureParametersProvider ??
                                                  throw new ArgumentNullException(nameof(nomenclatureParametersProvider));
        }

        private InvoiceDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            bool hasOrderItems;
            
            if(!order.ObservableOrderItems.Any() || 
               (order.ObservableOrderItems.Count == 1 && order.ObservableOrderItems.Any(x => 
                   x.Nomenclature.Id == nomenclatureParametersProvider.GetPaidDeliveryNomenclatureId))) 
            {
                hasOrderItems = false;
            }
            else
            {
                hasOrderItems = true;
            }
            
            var accepted = order.Status >= OrderStatus.Accepted;
            var waitForPayment = order.Status >= OrderStatus.WaitForPayment;
            
            var byCard = order.PaymentType == PaymentType.ByCard && hasOrderItems;
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