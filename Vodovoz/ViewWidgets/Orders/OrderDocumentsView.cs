using System;
using System.Linq;
using Gamma.ColumnConfig;
using Gamma.Utilities;
using Gtk;
using QS.Project.Domain;
using QS.Project.Services;
using QS.Views.GtkUI;
using Vodovoz.Dialogs.Email;
using Vodovoz.Dialogs.Employees;
using Vodovoz.Domain.Contacts;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewWidgets.Orders
{
    [System.ComponentModel.ToolboxItem(true)]
    public partial class OrderDocumentsView : WidgetViewBase<OrderDocumentsViewModel>
    {
        public OrderDocumentsView()
        {
            this.Build();
        }

        protected override void ConfigureWidget()
        {
            ConfigureTree();
            /*
            ybtnRemoveExistingDoc.Clicked +=
                (sender, args) =>
                    ViewModel.RemoveExistingDocCommand.Execute(ytreeDocuments.GetSelectedObjects<OrderDocument>());
            */
            ybtnAddM2ProxyForThisOrder.Clicked += YbtnAddM2ProxyForThisOrderOnClicked;
        }

        private void YbtnAddM2ProxyForThisOrderOnClicked(object sender, EventArgs e)
        {
            var childUoW = EntityUoWBuilder.ForCreateInChildUoW(ViewModel.UoW);
            ViewModel.compatibilityNavigation.OpenTdiTab<M2ProxyDlg, EntityUoWBuilder>(null, childUoW);
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
                                ServicesConfig.CommonServices.CurrentPermissionService.ValidatePresetPermission(
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
            ytreeDocuments.Selection.Changed += TreeDocumentsSelectionOnChanged;

            ytreeDocuments.RowActivated += (o, args) => OrderDocumentsOpener();
        }

        private void TreeDocumentsSelectionOnChanged(object sender, EventArgs e)
        {
            //buttonViewDocument.Sensitive = treeDocuments.Selection.CountSelectedRows() > 0;

            var selectedDoc = ytreeDocuments.GetSelectedObjects().Cast<OrderDocument>().FirstOrDefault();
            
            if(selectedDoc == null) {
                return;
            }
            
            string email = "";
            
            if(!ViewModel.Order.Counterparty.Emails.Any()) {
                email = "";
            } else {
                Email clientEmail = ViewModel.Order.Counterparty.Emails.FirstOrDefault(x => (x.EmailType?.EmailPurpose == EmailPurpose.ForBills) || x.EmailType == null);
                
                if(clientEmail == null) {
                    clientEmail = ViewModel.Order.Counterparty.Emails.FirstOrDefault();
                }
                
                email = clientEmail.Address;
            }
            
            //SendDocumentByEmailViewModel.Update(selectedDoc, email);
        }
    }
}
