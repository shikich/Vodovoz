using System;
using Gamma.Binding;
using Gamma.ColumnConfig;
using QS.Dialog.Gtk;
using QS.Report;
using QS.Tdi;
using QSReport;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders.Documents;
using QS.Views.GtkUI;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewWidgets
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class CounterpartyDocumentsView : WidgetViewBase<CounterpartyDocumentsViewModel>
	{
		public CounterpartyDocumentsView()
		{
			this.Build();
		}

        protected override void ConfigureWidget()
        {
			ytreeDocuments.Selection.Mode = Gtk.SelectionMode.Single;
			ytreeDocuments.Selection.Changed += TreeDocumentsSelectionChanged;
			ytreeDocuments.RowActivated += (o, args) => ybtnViewDoc.Click();
            //ybtnViewDoc.Clicked += (sender, e) => ;
            ybtnViewDoc.Binding.AddBinding(ViewModel, vm => vm.BtnViewDocSensitive, w => w.Sensitive).InitializeFromSource();

            ConfigureTree();
            ViewModel.LoadData();
            CreateRecursiveTreeModel();
		}

        private void ConfigureTree()
        {
	        var columnConfig = FluentColumnsConfig<CounterpartyDocumentNode>.Create();

	        if (ViewModel.HasTreeDocsSelectColumn)
	        {
		        columnConfig.AddColumn("Выбрать").SetDataProperty(x => x.Selected);
	        }

	        ytreeDocuments.ColumnsConfig = columnConfig
		        .AddColumn("Документ")
					.AddTextRenderer(x => x.Title)
		        .AddColumn("Номер")
					.AddTextRenderer(x => x.Number)
		        .AddColumn("Дата")
					.AddTextRenderer(x => x.Date)
		        .AddColumn("Точка доставки")
					.AddTextRenderer(x => x.DeliveryPoint != null ? x.DeliveryPoint.ShortAddress : string.Empty)
		        .Finish();
        }

        private void CreateRecursiveTreeModel() =>
	        ytreeDocuments.YTreeModel = 
		        new RecursiveTreeModel<CounterpartyDocumentNode>(
			        ViewModel.CounterpartyDocs,
			        x => x.Parent,
			        x => x.Documents);
		

		protected void OnButtonViewDocumentClicked(object sender, EventArgs e)
		{
			ITdiTab mytab = DialogHelper.FindParentTab(this);
            if (mytab == null)
                return;

			if(ViewModel.SelectedDoc.Document is CounterpartyContract contract) {
				int contractID = contract.Id;
				ITdiDialog dlg = new CounterpartyContractDlg(contractID);
				mytab.TabParent.AddTab(dlg, mytab);
			}

			if(ViewModel.SelectedDoc.Document is OrderDocument) {
				var rdlDoc = (ViewModel.SelectedDoc.Document as IPrintableRDLDocument);
				if(rdlDoc != null) {
					mytab.TabParent.AddTab(DocumentPrinter.GetPreviewTab(rdlDoc), mytab);
				}
			}
		}

		void TreeDocumentsSelectionChanged(object sender, EventArgs e)
		{
			ViewModel.SelectedDoc = ytreeDocuments.GetSelectedObject() as CounterpartyDocumentNode;
		}
	}
}
