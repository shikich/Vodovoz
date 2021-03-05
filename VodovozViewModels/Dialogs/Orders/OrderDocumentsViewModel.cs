using System;
using QS.DomainModel.UoW;
using QS.Commands;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders.Documents.OrderContract;
using Vodovoz.Domain.Orders.Documents.OrderM2Proxy;
using System.Linq;
using QS.Print;
using Gamma.Utilities;
using QS.Report;
using System.Collections.Generic;
using QS.Dialog;
using QS.Navigation;
using QS.Project.Domain;
using QS.Services;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Dialogs.Email;
using Vodovoz.Infrastructure.Print;
using Vodovoz.ViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Counterparties;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderDocumentsViewModel : UoWWidgetViewModelBase
    {
        public OrderBase Order { get; set; }
        public SendDocumentByEmailViewModel SendDocumentByEmailViewModel { get; }
        public DialogViewModelBase ParentTab { get; set; }

        public object[] SelectedDocs { get; set; }

        #region Команды
        
        private DelegateCommand viewDocCommand;
        public DelegateCommand ViewDocCommand => viewDocCommand ?? (
            viewDocCommand = new DelegateCommand(
                () => 
                {
                    if (!SelectedDocs.Any())
                        return;

                    var rdlDocs = SelectedDocs.OfType<PrintableOrderDocument>()
                                               .Where(d => d.PrintType == PrinterType.RDL)
                                               .ToList();

                    if (rdlDocs.Any())
                    {
                        string whatToPrint = rdlDocs.ToList().Count > 1
                                            ? "документов"
                                            : $"документа \"{rdlDocs.Cast<OrderDocument>().First().Type.GetEnumTitle()}\"";
                        
                        if (uow.HasChanges && commonMessages.SaveBeforePrint(typeof(Order), whatToPrint)) uow.Save();
                        
                        rdlDocs.ForEach(doc => {
                                if (doc is IPrintableRDLDocument printableRdlDocument)
                                {
                                    rdlPreviewOpener.OpenRldDocument(doc.GetType(), printableRdlDocument);
                                }
                            }
                        );
                    }

                    var odtDocs = SelectedDocs.OfType<PrintableOrderDocument>()
                                               .Where(d => d.PrintType == PrinterType.ODT)
                                               .ToList();
                    if (odtDocs.Any())
                        foreach (var doc in odtDocs)
                        {
                            if (doc is OrderContract contract)
                            {
                                compatibilityNavigation
                                    .OpenViewModel<CounterpartyContractViewModel, int, INavigationManager>(ParentTab,
                                        contract.Contract.Id, compatibilityNavigation);
                                
                                //NavigationManager.OpenViewModel(
                                        //DialogHelper.GenerateDialogHashName<CounterpartyContract>(contract.Contract.Id),
                                        //() =>
                                        //{
                                        //    var dialog = OrmMain.CreateObjectDialog(contract.Contract);
                                        //    
                                        //    if (dialog != null)
                                        //        (dialog as IEditableDialog).IsEditable = false;
                                        //    
                                        //    return dialog;
                                        //}
                                    //);
                            }
                            else if (doc is OrderM2Proxy m2Proxy)
                            {
                                if (doc.Id == 0)
                                {
                                    commonServices.InteractiveService.ShowMessage(
                                        ImportanceLevel.Info,
                                        "Перед просмотром документа необходимо сохранить заказ");
                                    
                                    return;
                                }
                                
                                compatibilityNavigation.OpenViewModel<
                                    M2ProxyDocumentViewModel, 
                                    int, 
                                    INavigationManager, 
                                    ICommonServices>(ParentTab, m2Proxy.M2Proxy.Id, compatibilityNavigation, commonServices);
                            }
                        }

                },
                () => true
            )
        );

        private DelegateCommand openPrintDlgCommand;
        public DelegateCommand OpenPrintDlgCommand => openPrintDlgCommand ?? (
            openPrintDlgCommand = new DelegateCommand(
                () => throw new NotImplementedException(),
                () => true
            )
        );

        private DelegateCommand printSelectedDocsCommand;
        public DelegateCommand PrintSelectedDocsCommand => printSelectedDocsCommand ?? (
            printSelectedDocsCommand = new DelegateCommand(
                () => throw new NotImplementedException(),
                () => true
            )
        );

        private DelegateCommand addExistingDocCommand;
        public DelegateCommand AddExistingDocCommand => addExistingDocCommand ?? (
            addExistingDocCommand = new DelegateCommand(
                () => 
                {
                    if (Order.Counterparty == null)
                    {
                        commonServices.InteractiveService.ShowMessage(
                            ImportanceLevel.Warning,
                            "Для добавления дополнительных документов должен быть выбран клиент.");
                        
                        return;
                    }

                    compatibilityNavigation.OpenViewModel<AddExistingDocumentsViewModel, IUnitOfWork, OrderBase, INavigationManager>(
                        ParentTab, uow, Order, compatibilityNavigation);
                },
                () => true
            )
        );
        
        private DelegateCommand addM2ProxyCommand;
        public DelegateCommand AddM2ProxyCommand => addM2ProxyCommand ?? (
            addM2ProxyCommand = new DelegateCommand(
                () => 
                /*{
                    
                    if (!new QSValidator<Order>(Order, new Dictionary<object, object>{
                        //индикатор того, что заказ - копия, созданная из недовозов
                        { "IsCopiedFromUndelivery", templateOrder != null }})
                        .RunDlgIfNotValid((Window)this.Toplevel) && SaveOrderBeforeContinue<M2ProxyDocument>())
                   */
                    {
                        var childUoW = EntityUoWBuilder.ForCreateInChildUoW(uow);
                        var uowFactory = UnitOfWorkFactory.GetDefaultFactory;

                        Type[] types = new[]
                        {
                            typeof(IEntityUoWBuilder),
                            typeof(IUnitOfWorkFactory),
                            typeof(INavigationManager),
                            typeof(ICommonServices)
                        };

                        object[] m2ProxyViewModelCtorObjs = new[]
                        {
                            childUoW,
                            (object) uowFactory,
                            compatibilityNavigation,
                            commonServices
                        };
                    
                        compatibilityNavigation.OpenViewModelTypedArgs<M2ProxyDocumentViewModel>(ParentTab, types, m2ProxyViewModelCtorObjs);
                        
                    //}

                },
                () => true
            )
        );
        
        private DelegateCommand addCurrentContractCommand;
        public DelegateCommand AddCurrentContractCommand => addCurrentContractCommand ?? (
            addCurrentContractCommand = new DelegateCommand(
                () => AddContractDocument(Order.Contract),
                () => true
            )
        );

        private DelegateCommand<IEnumerable<OrderDocument>> removeExistingDocCommand;
        public DelegateCommand<IEnumerable<OrderDocument>> RemoveExistingDocCommand => removeExistingDocCommand ?? (
            removeExistingDocCommand = new DelegateCommand<IEnumerable<OrderDocument>>(
                docs => 
                {
                    if (!commonServices.InteractiveService.Question("Вы уверены, что хотите удалить выделенные документы?")) return;
                    
                    //var documents = treeDocuments.GetSelectedObjects<OrderDocument>();
                    var notDeletedDocs = RemoveAdditionalDocuments(docs);
                    
                    if (notDeletedDocs != null && notDeletedDocs.Any())
                    {
                        string strDocuments = "";
                        
                        foreach (OrderDocument doc in notDeletedDocs)
                        {
                            strDocuments += $"\n\t{doc.Name}";
                        }
                        
                        commonServices.InteractiveService.ShowMessage(
                            ImportanceLevel.Warning,
                            $"Документы{strDocuments}\nудалены не были, так как относятся к текущему заказу.");
                    }
                },
                docs => docs.Any()
            )
        );
        
        #endregion
        
        private readonly IUnitOfWork uow = UnitOfWorkFactory.CreateWithoutRoot();
        private readonly ICommonServices commonServices;
        private readonly CommonMessages commonMessages;
        private readonly ITdiCompatibilityNavigation compatibilityNavigation;
        private readonly IRDLPreviewOpener rdlPreviewOpener;

        public OrderDocumentsViewModel(
            //IUnitOfWork uow,
            OrderBase order,
            ITdiCompatibilityNavigation compatibilityNavigation,
            ICommonServices commonServices,
            IRDLPreviewOpener rdlPreviewOpener,
            CommonMessages commonMessages,
            SendDocumentByEmailViewModel sendDocumentByEmailViewModel)
        {
            //this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            Order = order;
            this.commonServices = commonServices ?? throw new ArgumentException(nameof(commonServices));
            this.commonMessages = commonMessages ?? throw new ArgumentNullException(nameof(commonMessages));
            this.compatibilityNavigation = compatibilityNavigation;
            this.rdlPreviewOpener = rdlPreviewOpener;
            SendDocumentByEmailViewModel = sendDocumentByEmailViewModel;
        }
        
        private IEnumerable<OrderDocument> RemoveAdditionalDocuments(IEnumerable<OrderDocument> docs)
        {
            throw new NotImplementedException();
        }

        private void AddContractDocument(CounterpartyContract contract)
        {
            
        }
    }
}
