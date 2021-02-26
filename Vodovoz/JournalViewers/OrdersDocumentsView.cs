using System.ComponentModel.DataAnnotations;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using Gtk;
using QS.Views.GtkUI;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.JournalViewers
{
	[System.ComponentModel.ToolboxItem(true)]
	public partial class OrdersDocumentsView : WidgetViewBase<OrdersDocumentsViewModelBase>
    {
        public OrdersDocumentsView()
        {
            this.Build();
        }

        protected override void ConfigureWidget()
        {
            ConfigureTree();

            representationEntryCounterparty.RepresentationModel = new ViewModel.CounterpartyVM(new CounterpartyFilter(ViewModel.UoW));
            representationEntryCounterparty.Binding.AddBinding(ViewModel, vm => vm.Counterparty, w => w.Subject).InitializeFromSource();
            representationEntryCounterparty.ChangedByUser += (sender, e) => ViewModel.UpdateNodes();
            yvalidatedOrderNum.ValidationMode = QSWidgetLib.ValidationType.numeric;
            yvalidatedOrderNum.Binding.AddBinding(ViewModel, vm => vm.ValidatedOrderNumText, w => w.Text).InitializeFromSource();
            yvalidatedOrderNum.Changed += (sender, e) => ViewModel.UpdateNodes();
        }

        private void ConfigureTree()
        {
            var colorGreen = new Gdk.Color(0x90, 0xee, 0x90);
            var colorWhite = new Gdk.Color(0xff, 0xff, 0xff);

            datatreeviewOrderDocuments.ColumnsConfig = FluentColumnsConfig<SelectedOrdersDocumentVMNode>.Create()
                .AddColumn("Выбрать")
                    .SetDataProperty(node => node.Selected)
                .AddColumn("Заказ")
                    .SetDataProperty(node => node.OrderId)
                .AddColumn("Дата")
                    .SetDataProperty(node => node.OrderDate.ToString("d"))
                .AddColumn("Клиент")
                    .SetDataProperty(node => node.ClientName)
                .AddColumn("Документ")
                    .AddTextRenderer(node => node.DocumentType.GetAttribute<DisplayAttribute>().Name)
                .AddColumn("Адрес")
                    .AddTextRenderer(node => node.AddressString)
                .RowCells()
                .AddSetter<CellRenderer>((c, n) => { c.CellBackgroundGdk = n.Selected ? colorGreen : colorWhite; })
                .Finish();

            datatreeviewOrderDocuments.RowActivated += OnDatatreeviewOrderDocumentsRowActivated;
            datatreeviewOrderDocuments.ItemsDataSource = ViewModel.Documents;
        }

        void OnDatatreeviewOrderDocumentsRowActivated(object o, RowActivatedArgs args)
        {
            ViewModel.SelectedDoc = datatreeviewOrderDocuments.GetSelectedObject() as SelectedOrdersDocumentVMNode;
            ViewModel.TreeDocumentsRowActivated();
        }
	}
}
