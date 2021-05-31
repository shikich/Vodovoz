using System.Linq;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using Gtk;
using QS.Views.GtkUI;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewWidgets.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderDocumentsView : WidgetViewBase<OrderDocumentsViewModel>
    {
        public OrderDocumentsView(OrderDocumentsViewModel viewModel) : base(viewModel)
        {
            this.Build();
            Configure();
        }

        private void Configure()
        {
            ConfigureTree();

            ybtnRemoveExistingDoc.Clicked +=
                (sender, args) => ViewModel.RemoveExistingDocCommand.Execute(ytreeDocuments.GetSelectedObjects<OrderDocument>());
            ybtnAddExistingDoc.Clicked += (sender, args) => ViewModel.AddExistingDocCommand.Execute();
            ybtnAddM2ProxyForThisOrder.Clicked += (sender, args) => ViewModel.AddM2ProxyCommand.Execute();
            ybtnPrintSelected.Clicked += (sender, args) => 
                ViewModel.PrintSelectedDocsCommand.Execute(ytreeDocuments.GetSelectedObjects());
            ybtnViewDoc.Clicked += (sender, args) => ViewModel.ViewDocCommand.Execute(ytreeDocuments.GetSelectedObjects());
            ybtnAddCurrentContract.Clicked += (sender, args) => ViewModel.AddCurrentContractCommand.Execute();
            ybtnOpenPrintDlg.Clicked += (sender, args) => ViewModel.OpenPrintDlgCommand.Execute();

            sendDocumentByEmailView.ViewModel = ViewModel.SendDocumentByEmailViewModel;
        }

        private void ConfigureTree()
        {
            var colorWhite = new Gdk.Color(0xff, 0xff, 0xff);
            var colorLightYellow = new Gdk.Color(0xe1, 0xd6, 0x70);
            
            ytreeDocuments.ColumnsConfig = FluentColumnsConfig<OrderDocument>.Create()
                .AddColumn("Документ")
                    .SetDataProperty(node => node.Name)
                .AddColumn("Дата документа")
                    .AddTextRenderer(node => node.DocumentDateText)
                .AddColumn("Заказ №")
                    .SetTag("OrderNumberColumn")
                    .AddTextRenderer(node => node.Order.Id != node.AttachedToOrder.Id ? node.Order.Id.ToString() : "")
                .AddColumn("Без рекламы")
                    .AddToggleRenderer(x => x is IAdvertisable && ((IAdvertisable) x).WithoutAdvertising)
                    .Editing()
                    .ChangeSetProperty(PropertyUtil.GetPropertyInfo<IAdvertisable>(x => x.WithoutAdvertising))
                    .AddSetter((c, n) => 
                        c.Visible = n.Type == OrderDocumentType.Invoice || n.Type == OrderDocumentType.InvoiceContractDoc)
                .AddColumn("Без подписей и печати")
                    .AddToggleRenderer(x => x is ISignableDocument && (x as ISignableDocument).HideSignature)
                    .Editing()
                    .ChangeSetProperty(PropertyUtil.GetPropertyInfo<ISignableDocument>(x => x.HideSignature))
                    .AddSetter((c, n) => c.Visible = n is ISignableDocument)
                    .AddSetter((toggle, document) =>
                    {
                        if (document.Type == OrderDocumentType.UPD)
                        {
                            toggle.Activatable =
                                ViewModel.commonServices.CurrentPermissionService.ValidatePresetPermission(
                                    "can_edit_seal_and_signature_UPD");
                        }
                        else
                        {
                            toggle.Activatable = true;  // по умолчанию Activatable false
                        }
					    
                    } ) // Сделать только для  ISignableDocument и UDP
                .AddColumn("")
                .RowCells()
                    .AddSetter<CellRenderer>((c, n) => {
                        c.CellBackgroundGdk = colorWhite;
                        if(n.Order.Id != n.AttachedToOrder.Id && !(c is CellRendererToggle)) {
                            c.CellBackgroundGdk = colorLightYellow;
                        }
                    })
                .Finish();
            
            ytreeDocuments.Selection.Mode = SelectionMode.Multiple;
            ytreeDocuments.ItemsDataSource = ViewModel.Order.ObservableOrderDocuments;
            ytreeDocuments.Selection.Changed += (sender, args) => 
                ViewModel.UpdateSendDocumentViewModelCommand.Execute(
                    ytreeDocuments.GetSelectedObjects().Cast<OrderDocument>().FirstOrDefault());
            ytreeDocuments.RowActivated += (o, args) => 
                ViewModel.ViewDocCommand.Execute(ytreeDocuments.GetSelectedObjects());
        }
    }
}
