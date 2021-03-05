using QS.ViewModels;
using QS.DomainModel.UoW;
using Vodovoz.Domain.Client;
using System.Collections.Generic;
using System;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using QS.Commands;

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

        public IList<CounterpartyDocumentNode> CounterpartyDocs { get; } = 
            new GenericObservableList<CounterpartyDocumentNode>();

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

        private DelegateCommand viewDocCommand;
        public DelegateCommand ViewDocCommand => viewDocCommand ?? (
            viewDocCommand = new DelegateCommand(
                () =>
                {

                },
                () => SelectedDoc != null
            )
        );

        #endregion

        public IEnumerable<CounterpartyDocumentNode> GetSelectedDocuments() =>
            CounterpartyDocs.Where(x => x.Selected);

        public void LoadData()
        {
            CounterpartyDocs.Clear();
            
            CounterpartyContract contractAlias = null;

            //получаем список контрактов
            var contracts = UoW.Session.QueryOver(() => contractAlias)
                .Where(() => contractAlias.Counterparty.Id == Counterparty.Id)
                .List();

            foreach (var contract in contracts)
            {
                CounterpartyDocumentNode contractNode = new CounterpartyDocumentNode
                {
                    Document = contract
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

                return "";
            }
        }

        public object Document { get; set; }
        public DeliveryPoint DeliveryPoint { get; set; }
    }
}
