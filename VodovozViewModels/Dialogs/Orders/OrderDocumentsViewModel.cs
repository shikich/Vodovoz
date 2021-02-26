﻿using System;
using QS.DomainModel.UoW;
using QS.Commands;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders.Documents.OrderContract;
using Vodovoz.Domain.Orders.Documents.OrderM2Proxy;
using Vodovoz.Domain.Employees;
using System.Linq;
using QS.Print;
using Gamma.Utilities;
using QS.Report;
using System.Collections.Generic;
using QS.Dialog;
using QS.Navigation;
using QS.Project.Domain;
using QS.Services;
using QS.Tdi;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Infrastructure.Print;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderDocumentsViewModel : UoWWidgetViewModelBase
    {
        public OrderBase Order { get; set; }

        public object[] SelectedDocs { get; set; }

        #region Команды
        /*
        private DelegateCommand viewDoc;
        public DelegateCommand ViewDoc => viewDoc ?? (
            viewDoc = new DelegateCommand(
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

                                NavigationManager.OpenViewModel(
                                        DialogHelper.GenerateDialogHashName<CounterpartyContract>(contract.Contract.Id),
                                        () =>
                                        {
                                            var dialog = OrmMain.CreateObjectDialog(contract.Contract);
                                            
                                            if (dialog != null)
                                                (dialog as IEditableDialog).IsEditable = false;
                                            
                                            return dialog;
                                        }
                                    );
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
                                
                                compatibilityNavigation.OpenTdiTab<ITdiTab>(
                                    DialogHelper.GenerateDialogHashName<M2ProxyDocument>(m2Proxy.M2Proxy.Id),
                                    
                                    () =>
                                    {
                                        var dialog = OrmMain.CreateObjectDialog(m2Proxy.M2Proxy);
                                        
                                        if (dialog != null)
                                            (dialog as IEditableDialog).IsEditable = false;
                                        
                                        return dialog;
                                    }
                                );
                            }
                        }

                },
                () => true
            )
        );

        private DelegateCommand openPrintDlg;
        public DelegateCommand OpenPrintDlg => openPrintDlg ?? (
            openPrintDlg = new DelegateCommand(
                () => throw new NotImplementedException(),
                () => true
            )
        );

        private DelegateCommand printSelectedDocs;
        public DelegateCommand PrintSelectedDocs => printSelectedDocs ?? (
            printSelectedDocs = new DelegateCommand(
                () => throw new NotImplementedException(),
                () => true
            )
        );

        private DelegateCommand addExistingDoc;
        public DelegateCommand AddExistingDoc => addExistingDoc ?? (
            addExistingDoc = new DelegateCommand(
                () => 
                {
                    if (Order.Counterparty == null)
                    {
                        commonServices.InteractiveService.ShowMessage(
                            ImportanceLevel.Warning,
                            "Для добавления дополнительных документов должен быть выбран клиент.");
                        
                        return;
                    }

                    TabParent.OpenTab(
                        TdiTabBase.GenerateHashName<AddExistingDocumentsDlg>(),
                        () => new AddExistingDocumentsDlg(uow, Order.Counterparty)
                    );
                },
                () => true
            )
        );

        private DelegateCommand addM2Proxy;
        public DelegateCommand AddM2Proxy => addM2Proxy ?? (
            addM2Proxy = new DelegateCommand(
                () => 
                {
                    if (!new QSValidator<Order>(Order, new Dictionary<object, object>{
                        //индикатор того, что заказ - копия, созданная из недовозов
                        { "IsCopiedFromUndelivery", templateOrder != null }})
                        .RunDlgIfNotValid((Window)this.Toplevel) && SaveOrderBeforeContinue<M2ProxyDocument>())
                    {
                        TabParent.OpenTab(
                            DialogHelper.GenerateDialogHashName<M2ProxyDocument>(0),
                            () => OrmMain.CreateObjectDialog(
                                typeof(M2ProxyDocument), 
                                EntityUoWBuilder.ForCreateInChildUoW(uow), 
                                UnitOfWorkFactory.GetDefaultFactory)
                        );
                    }

                },
                () => true
            )
        );

        private DelegateCommand addCurrentContract;
        public DelegateCommand AddCurrentContract => addCurrentContract ?? (
            addCurrentContract = new DelegateCommand(
                () => Order.AddContractDocument(Order.Contract),
                () => true
            )
        );

        private DelegateCommand removeExistingDoc;
        public DelegateCommand RemoveExistingDoc => removeExistingDoc ?? (
            removeExistingDoc = new DelegateCommand(
                () => 
                {
                    if (!commonServices.InteractiveService.Question("Вы уверены, что хотите удалить выделенные документы?")) return;
                    
                    var documents = treeDocuments.GetSelectedObjects<OrderDocument>();
                    var notDeletedDocs = Order.RemoveAdditionalDocuments(documents);
                    
                    if (notDeletedDocs != null && notDeletedDocs.Any())
                    {
                        string strDocuments = "";
                        
                        foreach (OrderDocument doc in notDeletedDocs)
                        {
                            strDocuments += string.Format("\n\t{0}", doc.Name);
                        }
                        
                        commonServices.InteractiveService.ShowMessage(
                            ImportanceLevel.Warning,
                            $"Документы{strDocuments}\nудалены не были, так как относятся к текущему заказу.");
                    }
                },
                () => true
            )
        );
        */
        #endregion
        
        private readonly IUnitOfWork uow;
        private readonly ICommonServices commonServices;
        private readonly CommonMessages commonMessages;
        private readonly ITdiCompatibilityNavigation compatibilityNavigation;
        private readonly IRDLPreviewOpener rdlPreviewOpener;

        public OrderDocumentsViewModel(
            IUnitOfWork uow,
            ITdiCompatibilityNavigation compatibilityNavigation,
            ICommonServices commonServices,
            IRDLPreviewOpener rdlPreviewOpener,
            CommonMessages commonMessages)
            : base()
        {
            //this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.commonServices = commonServices ?? throw new ArgumentException(nameof(commonServices));
            this.commonMessages = commonMessages ?? throw new ArgumentNullException(nameof(commonMessages));
            this.compatibilityNavigation = compatibilityNavigation;
            this.rdlPreviewOpener = rdlPreviewOpener;
        }
    }
}
