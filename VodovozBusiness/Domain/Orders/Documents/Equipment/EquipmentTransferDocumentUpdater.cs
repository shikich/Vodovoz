using System.Linq;
using Vodovoz.Domain.Goods;

namespace Vodovoz.Domain.Orders.Documents.Equipment {
    public class EquipmentTransferDocumentUpdater : OrderDocumentUpdaterBase {

        private readonly EquipmentTransferDocumentFactory documentFactory;
        
        public override OrderDocumentType DocumentType => OrderDocumentType.EquipmentTransfer;

        public EquipmentTransferDocumentUpdater(EquipmentTransferDocumentFactory documentFactory) {
            this.documentFactory = documentFactory;
        }

        private EquipmentTransferDocument CreateNewDocument() {
            return documentFactory.Create();
        }

        private bool NeedCreateDocument(OrderBase order) {
            var onlyEquipments = order.ObservableOrderEquipments.Where(
                x => x.Nomenclature.Category == NomenclatureCategory.equipment);
            
            return order.Status >= OrderStatus.Accepted &&
                   onlyEquipments.Any(e =>
                       (e.Direction == Direction.PickUp && e.DirectionReason != DirectionReason.Rent)
                       || (e.Direction == Direction.Deliver &&
                           (e.OwnType == OwnTypes.Duty || e.DirectionReason == DirectionReason.Rent)));
        }
        
        public override void UpdateDocument(OrderBase order) {
            if (NeedCreateDocument(order)) {
                AddDocument(order, CreateNewDocument());
            }
            else {
                RemoveDocument(order);
            }
        }

        public override void AddExistingDocument(OrderBase order, OrderDocument existingDocument) {
            AddDocument(order, existingDocument);
        }

        public override void RemoveExistingDocument(OrderBase order, OrderDocument existingDocument) {
           RemoveDocument(order, existingDocument);
        }
    }
}