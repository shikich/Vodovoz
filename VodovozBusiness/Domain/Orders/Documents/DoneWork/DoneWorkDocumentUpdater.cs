using System.Linq;
using Vodovoz.Domain.Goods;

namespace Vodovoz.Domain.Orders.Documents.DoneWork {
    public class DoneWorkDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly DoneWorkDocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.DoneWorkReport;

        public DoneWorkDocumentUpdater(DoneWorkDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private DoneWorkDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        public bool NeedCreateDocument(OrderBase order) {
            var onlyEquipments = order.ObservableOrderEquipments.Where(
                x => x.Nomenclature.Category == NomenclatureCategory.equipment);

            return order.Status >= OrderStatus.Accepted
                && (
                    //Условие для оборудования возвращенного из ремонта
                    onlyEquipments.Any(e => e.Direction == Direction.Deliver &&
                                            (e.DirectionReason == DirectionReason.Repair ||
                                             e.DirectionReason == DirectionReason.RepairAndCleaning ||
                                             e.DirectionReason == DirectionReason.Cleaning)) ||
                    //Условия для выезда мастера
                    order.OrderItems.Any(i => i.Nomenclature.Category == NomenclatureCategory.master)
                );
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