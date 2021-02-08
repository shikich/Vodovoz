using System;
using System.Linq;
using Vodovoz.Domain.Client;
using Vodovoz.Services;

namespace Vodovoz.Domain.Orders.Documents.Bill {
    public class BillDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly BillDocumentFactory documentFactory;
        private readonly INomenclatureParametersProvider nomenclatureParametersProvider;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.Bill;

        public BillDocumentUpdater(BillDocumentFactory documentFactory,
                                   INomenclatureParametersProvider nomenclatureParametersProvider) {
            this.documentFactory = documentFactory;
            this.nomenclatureParametersProvider = nomenclatureParametersProvider ??
                                                throw new ArgumentNullException(nameof(nomenclatureParametersProvider));
        }

        private BillDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        public bool NeedCreateDocument(OrderBase order) {
            bool hasOrderItems;
            
            if(!order.ObservableOrderItems.Any() || 
               (order.ObservableOrderItems.Count == 1 && order.ObservableOrderItems.Any(x => 
                   x.Nomenclature.Id == nomenclatureParametersProvider.PaidDeliveryNomenclatureId))) 
            {
                hasOrderItems = false;
            }
            else
            {
                hasOrderItems = true;
            }
            
            return order.PaymentType == PaymentType.cashless
                && !(order.ObservableOrderItems.Sum(i => i.Sum) <= 0m)
                && !order.ObservableOrderDepositItems.Any()
                && hasOrderItems;
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