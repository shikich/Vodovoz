using System;
using System.Linq;
using Vodovoz.Services;

namespace Vodovoz.Domain.Orders.Documents.TransportInvoice {
    public class TransportInvoiceDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly TransportInvoiceDocumentFactory documentFactory;
        private readonly INomenclatureParametersProvider nomenclatureParametersProvider;

        public override OrderDocumentType DocumentType => OrderDocumentType.TransportInvoice;

        public TransportInvoiceDocumentUpdater(TransportInvoiceDocumentFactory documentFactory,
                                               INomenclatureParametersProvider nomenclatureParametersProvider) {
            this.documentFactory = documentFactory;
            this.nomenclatureParametersProvider = nomenclatureParametersProvider ??
                                                  throw new ArgumentNullException(nameof(nomenclatureParametersProvider));
        }

        private TransportInvoiceDocument CreateNewDocument() {
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
            
            return order.Counterparty.TTNCount.HasValue && hasOrderItems;
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