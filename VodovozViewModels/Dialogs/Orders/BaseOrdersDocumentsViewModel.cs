using NHibernate.Transform;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.Bill;
using Vodovoz.Domain.Orders.Documents.DoneWork;
using Vodovoz.Domain.Orders.Documents.DriverTicket;
using Vodovoz.Domain.Orders.Documents.Equipment;
using Vodovoz.Domain.Orders.Documents.Invoice;
using Vodovoz.Domain.Orders.Documents.ShetFactura;
using Vodovoz.Domain.Orders.Documents.Torg12;
using Vodovoz.Domain.Orders.Documents.UPD;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class BaseOrdersDocumentsViewModel : OrdersDocumentsViewModelBase
    {
        public BaseOrdersDocumentsViewModel(
            IUnitOfWork uow, 
            Counterparty counterparty
        ) : base(uow, counterparty)
        {

        }

        public override void UpdateNodes()
        {
            Documents.Clear();

            SelectedOrdersDocumentVMNode resultAlias = null;
            OrderBase orderAlias = null;
            Counterparty counterpartyAlias = null;
            OrderDocument orderDocumentAlias = null;
            DeliveryPoint deliveryPointAlias = null;

            var query = UoW.Session.QueryOver(() => orderDocumentAlias);

            if (Counterparty != null)
            {
                query.Where(() => counterpartyAlias.Id == Counterparty.Id);
            }

            if (int.TryParse(ValidatedOrderNumText, out var orderId))
            {
                query.WhereRestrictionOn(() => orderAlias.Id).IsLike(orderId);
            }

            var docs = query.JoinAlias(() => orderDocumentAlias.NewOrder, () => orderAlias)
                .JoinAlias(() => orderAlias.Counterparty, () => counterpartyAlias)
                .JoinAlias(() => orderAlias.DeliveryPoint, () => deliveryPointAlias)
                .Where(() =>
                          orderDocumentAlias.GetType() == typeof(BillDocument)
                       || orderDocumentAlias.GetType() == typeof(DoneWorkDocument)
                       || orderDocumentAlias.GetType() == typeof(EquipmentTransferDocument)
                       || orderDocumentAlias.GetType() == typeof(InvoiceBarterDocument)
                       || orderDocumentAlias.GetType() == typeof(InvoiceDocument)
                       || orderDocumentAlias.GetType() == typeof(InvoiceContractDocument)
                       || orderDocumentAlias.GetType() == typeof(UPDDocument)
                       || orderDocumentAlias.GetType() == typeof(DriverTicketDocument)
                       || orderDocumentAlias.GetType() == typeof(Torg12Document)
                       || orderDocumentAlias.GetType() == typeof(ShetFacturaDocument)
                      )
                .SelectList(list => list
                   .Select(() => orderAlias.Id).WithAlias(() => resultAlias.OrderId)
                   .Select(() => orderAlias.DeliveryDate).WithAlias(() => resultAlias.OrderDate)
                   .Select(() => counterpartyAlias.Id).WithAlias(() => resultAlias.ClientId)
                   .Select(() => counterpartyAlias.Name).WithAlias(() => resultAlias.ClientName)
                   .Select(() => orderDocumentAlias.Id).WithAlias(() => resultAlias.DocumentId)
                   .Select(() => orderDocumentAlias.GetType()).WithAlias(() => resultAlias.DocumentTypeString)
                   .Select(() => deliveryPointAlias.CompiledAddress).WithAlias(() => resultAlias.AddressString)
                ).OrderBy(() => orderAlias.DeliveryDate).Desc
                .TransformUsing(Transformers.AliasToBean<SelectedOrdersDocumentVMNode>())
                .List<SelectedOrdersDocumentVMNode>();

            foreach (var doc in docs)
            {
                Documents.Add(doc);
            }
        }
    }
}