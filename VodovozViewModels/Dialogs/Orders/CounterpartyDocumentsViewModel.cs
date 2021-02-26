using QS.ViewModels;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Client;
using System.Collections.Generic;
using System;
using Vodovoz.Domain.Orders.Documents;
using System.Linq;
using QS.Commands;
using Vodovoz.Domain.Orders;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class CounterpartyDocumentsViewModel : UoWWidgetViewModelBase
    {
        private Counterparty counterparty;
        public Counterparty Counterparty 
        {
            get => counterparty;
            set => SetField(ref counterparty, value);
        }

        public bool BtnViewDocSensitive => SelectedDoc != null;

        private CounterpartyDocumentNode selectedDoc;
        public CounterpartyDocumentNode SelectedDoc 
        {
            get => selectedDoc;
            set 
            {
                selectedDoc = value;
                OnPropertyChanged(nameof(BtnViewDocSensitive));
            }
        }

        public bool HasTreeDocsSelectColumn { get; }

        public IList<CounterpartyDocumentNode> CounterpartyDocs { get; private set; } = 
            new List<CounterpartyDocumentNode>();

        #region Конструктор

        public CounterpartyDocumentsViewModel(
            IUnitOfWork uow,
            Counterparty counterparty,
            bool hasTreeDocsSelectColumn = false)
        {
            UoW = uow ?? throw new ArgumentNullException(nameof(uow));
            this.counterparty = counterparty;
            HasTreeDocsSelectColumn = hasTreeDocsSelectColumn;
        }

        #endregion

        #region Команды

        private DelegateCommand viewDoc;
        public DelegateCommand ViewDoc => viewDoc ?? (
            viewDoc = new DelegateCommand(
                () =>
                {

                },
                () => SelectedDoc != null
            )
        );

        #endregion

        public List<CounterpartyDocumentNode> GetSelectedDocuments()
        {
            var result = new List<CounterpartyDocumentNode>();

            foreach (var item in CounterpartyDocs)
            {
                result.AddRange(item.GetSelectedDocuments());
            }

            return result;
        }

        public void LoadData()
        {
            //OrderDocument orderDocumentAlias = null;
            CounterpartyContract contractAlias = null;
            //Order orderAlias = null;

            //получаем список документов
            /*var orderDocuments = UoW.Session.QueryOver(() => orderDocumentAlias)
                .JoinAlias(x => x.Order, () => orderAlias, NHibernate.SqlCommand.JoinType.InnerJoin)
                .JoinAlias(() => orderAlias.Contract, () => contractAlias, NHibernate.SqlCommand.JoinType.InnerJoin)
                .Where(() => contractAlias.Counterparty.Id == Counterparty.Id)
                .List();*/

            //получаем список контрактов
            var contracts = UoW.Session.QueryOver(() => contractAlias)
                .Where(() => contractAlias.Counterparty.Id == Counterparty.Id)
                .List();

            foreach (var contract in contracts)
            {
                CounterpartyDocumentNode contractNode = new CounterpartyDocumentNode
                {
                    Document = contract,
                    Documents = new List<CounterpartyDocumentNode>()
                };

                CounterpartyDocs.Add(contractNode);
            }
        }
    }

    public class CounterpartyDocumentNode
    {
        public bool Selected { get; set; }
        public string Title
        {
            get
            {
                if (Document is CounterpartyContract)
                {
                    return "Договор";
                }

                return "";
            }
        }

        public string Number
        {
            get
            {
                if (Document is CounterpartyContract contract)
                {
                    return contract.ContractFullNumber;
                }

                return "";
            }
        }

        public string Date
        {
            get
            {
                if (Document is CounterpartyContract contract)
                {
                    return contract.IssueDate.ToShortDateString();
                }

                if (Document is OrderDocument orderDoc)
                {
                    return orderDoc.DocumentDateText;
                }

                return "";
            }
        }

        public object Document { get; set; }
        public DeliveryPoint DeliveryPoint { get; set; }
        public CounterpartyDocumentNode Parent { get; set; }
        public List<CounterpartyDocumentNode> Documents { get; set; }
        public List<CounterpartyDocumentNode> GetSelectedDocuments()
        {
            List<CounterpartyDocumentNode> result = new List<CounterpartyDocumentNode>();
            if (Selected)
            {
                result.Add(this);
            }
            if (Documents != null && Documents.Count > 0)
            {
                foreach (var item in Documents)
                {
                    result.AddRange(item.GetSelectedDocuments().Where(x => x.Selected));
                }
            }
            return result;
        }
    }
}
