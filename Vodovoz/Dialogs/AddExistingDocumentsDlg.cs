using System;
using System.Collections.Generic;
using System.Linq;
using QS.DomainModel.UoW;
using QS.Report;
using QSReport;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.OrderContract;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.Dialogs
{
	public partial class AddExistingDocumentsDlg : QS.Dialog.Gtk.TdiTabBase
	{
		private readonly IUnitOfWorkGeneric<Order> uow;

        public AddExistingDocumentsDlg(
            IUnitOfWorkGeneric<Order> uow,
            OrdersDocumentsViewModel ordersDocumentsViewModel,
            CounterpartyDocumentsViewModel counterpartyDocumentsViewModel)
		{
			this.Build();
			this.uow = uow;
            counterpartydocumentsview1.ViewModel = counterpartyDocumentsViewModel;
            ordersdocumentsview1.ViewModel = ordersDocumentsViewModel;
            ordersdocumentsview1.ViewModel.OrderActivated += Orderselectedview1_OrderActivated;
            TabName = "Добавление документов";
		}

        protected void OnButtonAddSelectedDocumentsClicked(object sender, EventArgs e)
		{
			var counterpartyDocuments = counterpartydocumentsview1.ViewModel.GetSelectedDocuments();
			var orderDocuments = ordersdocumentsview1.ViewModel.GetSelectedDocuments();

			List<OrderDocument> resultList = new List<OrderDocument>();

			//Контракты
			var documentsContract = 
				uow.Session.QueryOver<OrderContract>()
                   .WhereRestrictionOn(x => x.Contract.Id)
                   .IsIn(counterpartyDocuments
						.Select(y => y.Document)
						.OfType<CounterpartyContract>()
						.Select(x => x.Id)
						.ToList()
                        )
                   .List()
                   .Distinct();
			resultList.AddRange(documentsContract);

			//Документы заказа
			var documentsOrder = uow.Session.QueryOver<OrderDocument>()
			   .WhereRestrictionOn(x => x.Id)
			   .IsIn(orderDocuments.Select(y => y.DocumentId).ToList())
			   .List();
			resultList.AddRange(documentsOrder);

			uow.Root.AddAdditionalDocuments(resultList);

			this.OnCloseTab(false);
		}

		void Orderselectedview1_OrderActivated(object sender, int e)
		{
			var doc = uow.GetById<OrderDocument>(e) as IPrintableRDLDocument;
			if(doc == null) {
				return;
			}
			TabParent.AddTab(DocumentPrinter.GetPreviewTab(doc), this, false);
		}
	}
}