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
using System.ComponentModel.DataAnnotations;
using QS.Dialog;
using QS.DocTemplates;
using QS.Navigation;
using QS.Project.Domain;
using QS.Services;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Dialogs.Email;
using Vodovoz.Domain.Contacts;
using Vodovoz.Domain.Employees;
using Vodovoz.Infrastructure.Print;
using Vodovoz.ViewModels.Employees;
using Vodovoz.ViewModels.ViewModels.Counterparties;
using Vodovoz.ViewModels.ViewModels.Orders;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderDocumentsViewModel : UoWWidgetViewModelBase
    {
        private readonly ValidationContext ValidationContext;
        private OrderBase TemplateOrder { get; set;}
        public OrderBase Order { get; set; }
        public SendDocumentByEmailViewModel SendDocumentByEmailViewModel { get; }
        public DialogViewModelBase ParentTab { get; set; }

        #region Команды
        
        private DelegateCommand<object[]> viewDocCommand;
        public DelegateCommand<object[]> ViewDocCommand => viewDocCommand ?? (
            viewDocCommand = new DelegateCommand<object[]>(
                docs => 
                {
                    var rdlDocs = docs.OfType<PrintableOrderDocument>()
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

                    var odtDocs = docs.OfType<PrintableOrderDocument>()
                                               .Where(d => d.PrintType == PrinterType.ODT)
                                               .ToList();
                    if (odtDocs.Any())
                        foreach (var doc in odtDocs)
                        {
                            if (doc is OrderContract contract)
                            {
                                compatibilityNavigation
                                    .OpenViewModel<CounterpartyContractViewModel, int, INavigationManager>(ParentTab,
                                        contract.Contract.Id, compatibilityNavigation)
                                    .ViewModel.IsEditable = false;
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

                                var uowBuilder = EntityUoWBuilder.ForOpen(m2Proxy.M2Proxy.Id);
                                var uowFactory = UnitOfWorkFactory.GetDefaultFactory;

                                Type[] types =
                                {
                                    typeof(IEntityUoWBuilder),
                                    typeof(IUnitOfWorkFactory),
                                    typeof(INavigationManager),
                                    typeof(ICommonServices)
                                };

                                object[] m2ProxyViewModelCtorObjs =
                                {
                                    uowBuilder,
                                    uowFactory,
                                    compatibilityNavigation,
                                    commonServices
                                };
                                
                                compatibilityNavigation.OpenViewModelTypedArgs<M2ProxyDocumentViewModel>(
                                        ParentTab, types, m2ProxyViewModelCtorObjs)
                                    .ViewModel.IsEditable = false;
                            }
                        }

                },
                docs => docs.Any()
            )
        );

        private DelegateCommand openPrintDlgCommand;
        public DelegateCommand OpenPrintDlgCommand => openPrintDlgCommand ?? (
            openPrintDlgCommand = new DelegateCommand(
                () =>
                {
                    //TODO Переписать на MVVM DocumentsPrinterDlg
                    /*if(Order.OrderDocuments.OfType<PrintableOrderDocument>().Any(
                        doc => doc.PrintType == PrinterType.RDL || doc.PrintType == PrinterType.ODT))
                        TabParent.AddSlaveTab(this, new DocumentsPrinterDlg(Order));*/
                },
                () => true
            )
        );

        private DelegateCommand<object[]> printSelectedDocsCommand;
        public DelegateCommand<object[]> PrintSelectedDocsCommand => printSelectedDocsCommand ?? (
            printSelectedDocsCommand = new DelegateCommand<object[]>(
                docs =>
                {
                    var selectedDocs = docs.Cast<OrderDocument>().ToList();
                    selectedDocs.OfType<ITemplateOdtDocument>().ToList().ForEach(x => x.PrepareTemplate(UoW));
                        
                    string whatToPrint = selectedDocs.Count() > 1
                        ? "документов"
                        : "документа \"" + selectedDocs.First().Type.GetEnumTitle() + "\"";
                    /*
                    if(UoWGeneric.HasChanges && commonMessages.SaveBeforePrint(typeof(Order), whatToPrint))
                        UoWGeneric.Save();
                    */
                    var selectedPrintableRDLDocuments = docs.OfType<PrintableOrderDocument>()
                        .Where(doc => doc.PrintType == PrinterType.RDL).ToList();
                    if(selectedPrintableRDLDocuments.Any()) {
                        documentPrinter.PrintAllDocuments(selectedPrintableRDLDocuments);
                    }

                    var selectedPrintableODTDocuments = docs.OfType<IPrintableOdtDocument>().ToList();
                    if (selectedPrintableODTDocuments.Any()) {
                        documentPrinter.PrintAllODTDocuments(selectedPrintableODTDocuments);
                    }
                },
                docs => docs.Any()
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
                {
                    ValidationContext.Items.Add("IsCopiedFromUndelivery", TemplateOrder != null);
                    if (Validate() && SaveOrderBeforeContinue<M2ProxyDocument>())
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
                    }
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

        private DelegateCommand<OrderDocument> updateSendDocumentViewModelCommand;
        public DelegateCommand<OrderDocument> UpdateSendDocumentViewModelCommand => updateSendDocumentViewModelCommand ?? (
            updateSendDocumentViewModelCommand = new DelegateCommand<OrderDocument>(
                doc =>
                {
                    string email = "";
            
                    if(!Order.Counterparty.Emails.Any()) {
                        email = "";
                    } else {
                        Email clientEmail = 
                            Order.Counterparty.Emails.FirstOrDefault(x => 
                                (x.EmailType?.EmailPurpose == EmailPurpose.ForBills) || x.EmailType == null);
                
                        if(clientEmail == null) {
                            clientEmail = Order.Counterparty.Emails.FirstOrDefault();
                        }
                
                        email = clientEmail?.Address;
                    }
            
                    SendDocumentByEmailViewModel.Update(doc, email);
                },
                doc => doc != null
            )     
        );
                                                                     
        
        #endregion
        
        private readonly IUnitOfWork uow = UnitOfWorkFactory.CreateWithoutRoot();
        private readonly ICommonServices commonServices;
        private readonly CommonMessages commonMessages;
        private readonly ITdiCompatibilityNavigation compatibilityNavigation;
        private readonly IRDLPreviewOpener rdlPreviewOpener;
        private readonly IDocumentPrinter documentPrinter;
        private readonly OrderDocumentsModel orderDocumentsModel;

        public OrderDocumentsViewModel(
            //IUnitOfWork uow,
            OrderBase order,
            ITdiCompatibilityNavigation compatibilityNavigation,
            ICommonServices commonServices,
            IRDLPreviewOpener rdlPreviewOpener,
            IDocumentPrinter documentPrinter,
            CommonMessages commonMessages,
            SendDocumentByEmailViewModel sendDocumentByEmailViewModel,
            OrderDocumentsModel orderDocumentsModel)
        {
            //this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            Order = order;
            this.commonServices = commonServices ?? throw new ArgumentException(nameof(commonServices));
            this.commonMessages = commonMessages ?? throw new ArgumentNullException(nameof(commonMessages));
            this.compatibilityNavigation = 
                compatibilityNavigation ?? throw new ArgumentNullException(nameof(compatibilityNavigation));
            this.rdlPreviewOpener = rdlPreviewOpener ?? throw new ArgumentNullException(nameof(rdlPreviewOpener));
            this.documentPrinter = documentPrinter ?? throw new ArgumentNullException(nameof(documentPrinter));
            this.orderDocumentsModel = orderDocumentsModel;
            SendDocumentByEmailViewModel = sendDocumentByEmailViewModel;

            ValidationContext = new ValidationContext(Order);
        }
        
        private IEnumerable<OrderDocument> RemoveAdditionalDocuments(IEnumerable<OrderDocument> docs)
        {
            var docsOfThisOrder = docs.Where(x => x.NewOrder.Id == Order.Id);
            var anotherDocs = docs.Where(x => x.NewOrder.Id != Order.Id);

            if (anotherDocs.Any()) {
                orderDocumentsModel.RemoveExistingDocuments(anotherDocs);
            }

            return docsOfThisOrder;
        }

        private void AddContractDocument(CounterpartyContract contract)
        {
            throw new NotImplementedException();
        }

        private bool Validate()
        {
            return commonServices.ValidationService.Validate(Order, ValidationContext);
        }
        
        private bool SaveOrderBeforeContinue<T>()
        {
            return true;
            /*
            if(UoWGeneric.IsNew) {
                if(CommonDialogs.SaveBeforeCreateSlaveEntity(EntityObject.GetType(), typeof(T))) {
                    if(!Save())
                        return false;
                } else
                    return false;
            }
            return true;
            */
        }
    }
}
