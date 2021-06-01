using System;
using System.Linq;
using QS.Commands;
using QS.Dialog;
using QS.Navigation;
using QS.Report;
using QS.Services;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.DriverTicket;
using Vodovoz.Domain.Orders.Documents.Invoice;
using Vodovoz.ViewModels.Infrastructure.Print;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class DocumentsPrinterViewModel : DialogViewModelBase
    {
        private readonly OrderBase _currentOrder;
		private readonly RouteList _currentRouteList;
		private SelectablePrintDocument _selectedDocument;
		private DelegateCommand _printSelectedCommand;
		
		public DocumentsPrinterViewModel(
			IEntityDocumentsPrinter entityDocumentsPrinter,
			IInteractiveService interactiveService,
			INavigationManager navigationManager, 
			OrderBase order) : base (navigationManager)
		{
			EntityDocumentsPrinter = entityDocumentsPrinter ?? throw new ArgumentNullException(nameof(entityDocumentsPrinter));

			if(interactiveService == null)
			{
				throw new ArgumentNullException(nameof(interactiveService));
			}
			
			Title = "Печать документов заказа";

			if(!string.IsNullOrEmpty(EntityDocumentsPrinter.ODTTemplateNotFoundMessages))
			{
				interactiveService.ShowMessage(ImportanceLevel.Warning, EntityDocumentsPrinter.ODTTemplateNotFoundMessages);
			}

			_currentOrder = order;
			DefaultPreviewDocument();
			EntityDocumentsPrinter.DocumentsPrinted += (o, args) => DocumentsPrinted?.Invoke(o, args);
		}
		
		public DocumentsPrinterViewModel(
			IEntityDocumentsPrinter entityDocumentsPrinter,
			INavigationManager navigationManager,
			RouteList routeList) : base (navigationManager)
		{
			EntityDocumentsPrinter = entityDocumentsPrinter ?? throw new ArgumentNullException(nameof(entityDocumentsPrinter));

			Title = "Печать документов МЛ";
			_currentRouteList = routeList;
			DefaultPreviewDocument();
			EntityDocumentsPrinter.DocumentsPrinted += (o, args) => DocumentsPrinted?.Invoke(o, args);
		}
		
		public event Action PreviewDocument;
		public event EventHandler DocumentsPrinted;

		public DelegateCommand PrintSelectedCommand => _printSelectedCommand ?? (_printSelectedCommand = new DelegateCommand(
			() => EntityDocumentsPrinter.Print(SelectedDocument),
			() => CanPrint
			)
		);
		
		public IEntityDocumentsPrinter EntityDocumentsPrinter { get; }

		public SelectablePrintDocument SelectedDocument
		{
			get => _selectedDocument;
			set
			{
				if(SetField(ref _selectedDocument, value))
				{
					OnPropertyChanged(nameof(CanPrint));
				}
			}
		}

		public bool CanPrint => SelectedDocument != null;

		private void DefaultPreviewDocument()
		{
			var printDocuments = EntityDocumentsPrinter.DocumentsToPrint;
			if(_currentOrder != null) 
			{ //если этот диалог вызван из заказа
				var documents =
					printDocuments.Where(x => x.Document is OrderDocument doc && doc.Order.Id == _currentOrder.Id).ToList();

				var driverTicket = documents.FirstOrDefault(x => x.Document is DriverTicketDocument);
				var invoiceDocument = documents.FirstOrDefault(x => x.Document is InvoiceDocument);
				
				if(driverTicket != null && _currentOrder.PaymentType == Domain.Client.PaymentType.cashless) 
				{
					SelectedDocument = driverTicket;
					PreviewDocument?.Invoke();
				} 
				else if(invoiceDocument != null) 
				{
					SelectedDocument = invoiceDocument;
					PreviewDocument?.Invoke();
				}
			}
			else if(_currentRouteList != null) 
			{ //если этот диалог вызван из МЛ
				SelectedDocument = printDocuments.FirstOrDefault(x => x.Selected) ?? printDocuments.FirstOrDefault();
				PreviewDocument?.Invoke();
			}
		}

		public void PrintAll() => EntityDocumentsPrinter.Print();

		public void ReportViewerOnReportPrinted(object o, EventArgs args) => DocumentsPrinted?.Invoke(o, args);
    }
}