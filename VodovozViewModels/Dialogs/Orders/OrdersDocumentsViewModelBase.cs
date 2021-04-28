using System;
using System.Collections.Generic;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Autofac;
using NHibernate.Transform;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.ViewModels;
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
    public abstract class OrdersDocumentsViewModelBase : UoWWidgetViewModelBase, IAutofacScopeHolder
    {
        private Counterparty counterparty;
        public Counterparty Counterparty
        {
            get => counterparty;
            set => SetField(ref counterparty, value);
        }

        private string validatedOrderNumText;
        public string ValidatedOrderNumText
        {
            get => validatedOrderNumText;
            set => SetField(ref validatedOrderNumText, value);
        }

        public SelectedOrdersDocumentVMNode SelectedDoc { get; set; }

        public IList<SelectedOrdersDocumentVMNode> Documents { get; } =
            new GenericObservableList<SelectedOrdersDocumentVMNode>();

        public event EventHandler<int> OrderActivated;

        protected OrdersDocumentsViewModelBase(
            IUnitOfWork uow,
            Counterparty counterparty)
        {
            UoW = uow ?? throw new ArgumentNullException(nameof(uow));
            Counterparty = counterparty;

            Configure();
        }

        private void Configure()
        {
            UpdateNodes();
        }

        public virtual void UpdateNodes()
        {
            Documents.Clear();

            SelectedOrdersDocumentVMNode resultAlias = null;
            Order orderAlias = null;
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

            var docs = query.JoinAlias(() => orderDocumentAlias.Order, () => orderAlias)
                .JoinAlias(() => orderAlias.Client, () => counterpartyAlias)
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

        public IEnumerable<SelectedOrdersDocumentVMNode> GetSelectedDocuments() => 
            Documents.Where(x => x.Selected);

        public void TreeDocumentsRowActivated()
        {
            if (SelectedDoc == null) return;

            OrderActivated?.Invoke(this, SelectedDoc.DocumentId);
        }

        public ILifetimeScope AutofacScope { get; set; }
    }

    public class SelectedOrdersDocumentVMNode
    {
        public bool Selected { get; set; } = false;
        public int OrderId { get; set; }
        public DateTime OrderDate { get; set; }
        public int ClientId { get; set; }
        public string ClientName { get; set; }
        public int DocumentId { get; set; }
        public OrderDocumentType DocumentType
        {
            get
            {
                OrderDocumentType result;
                Enum.TryParse<OrderDocumentType>(DocumentTypeString, out result);
                return result;
            }
        }
        public string DocumentTypeString { get; set; }
        public DateTime DocumentDate { get; set; }
        public string AddressString { get; set; }
    }
}
