using System;
using System.ComponentModel.DataAnnotations;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using Gtk;
using QS.Dialog.Gtk;
using QS.ViewModels.Control.EEVM;
using QS.Views.GtkUI;
using Vodovoz.Domain.Client;
using Vodovoz.JournalViewModels;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewWidgets.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class BaseOrdersDocumentsView : WidgetViewBase<OrdersDocumentsViewModelBase>
    {
        public BaseOrdersDocumentsView()
        {
            this.Build();
        }

        protected override void ConfigureWidget()
        {
            ConfigureTree();

            yvalidatedOrderNum.ValidationMode = QSWidgetLib.ValidationType.numeric;
            yvalidatedOrderNum.Binding.AddBinding(ViewModel, vm => vm.ValidatedOrderNumText, w => w.Text).InitializeFromSource();
            yvalidatedOrderNum.Changed += (sender, e) => ViewModel.UpdateNodes();

            var builder =
                new CommonEEVMBuilderFactory<Counterparty>(MainClass.MainWin.NavigationManager.CurrentPage.ViewModel, ViewModel.Counterparty,
                    ViewModel.UoW, MainClass.MainWin.NavigationManager, ViewModel.AutofacScope);

            var viewModel = builder.ForProperty(x => x.MainContact)
                .UseViewModelJournalAndAutocompleter<CounterpartyJournalViewModel>()
                .Finish();

            entityentry1.ViewModel = viewModel;
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
