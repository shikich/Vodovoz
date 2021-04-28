using System;
using System.Collections.Generic;
using System.Linq;
using Autofac;
using Autofac.Core;
using QS.Commands;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Report;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Orders.Documents;
using Vodovoz.Domain.Orders.Documents.OrderContract;
using Vodovoz.Infrastructure.Print;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class AddExistingDocumentsViewModel : DialogViewModelBase, IAutofacScopeHolder
    {
        private readonly IUnitOfWork uow;
        private readonly OrderBase orderBase;

        private OrderDocumentsModel orderDocumentsModel;
        private OrderDocumentsModel OrderDocumentsModel 
        {
            get
            {
                if (orderDocumentsModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(OrderBase), orderBase),
                        new TypedParameter(typeof(IOrderDocumentUpdatersFactory),
                            AutofacScope.Resolve<IOrderDocumentUpdatersFactory>())
                    };
                    orderDocumentsModel = AutofacScope.Resolve<OrderDocumentsModel>(parameters);
                }

                return orderDocumentsModel;
            }
        } 

        private IRDLPreviewOpener rDLPreviewOpener;
        private IRDLPreviewOpener RDLPreviewOpener
        {
            get
            {
                if (rDLPreviewOpener == null)
                {
                    rDLPreviewOpener = AutofacScope.Resolve<IRDLPreviewOpener>();
                }

                return rDLPreviewOpener;
            }
        }
        
        private BaseOrdersDocumentsViewModel ordersDocumentsViewModel;
        public BaseOrdersDocumentsViewModel OrdersDocumentsViewModel
        {
            get
            {
                if (ordersDocumentsViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(IUnitOfWork), uow),
                        new TypedParameter(typeof(Vodovoz.Domain.Client.Counterparty), orderBase.Counterparty)
                    };
                    ordersDocumentsViewModel = AutofacScope.Resolve<BaseOrdersDocumentsViewModel>(parameters);
                    ordersDocumentsViewModel.OrderActivated += OnOrderSelectedViewModelOrderActivated;
                    ordersDocumentsViewModel.AutofacScope = AutofacScope;
                }

                return ordersDocumentsViewModel;
            }
        }

        private CounterpartyDocumentsViewModel counterpartyDocumentsViewModel;
        public CounterpartyDocumentsViewModel CounterpartyDocumentsViewModel
        {
            get
            {
                if (counterpartyDocumentsViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(IUnitOfWork), uow),
                        new TypedParameter(typeof(Vodovoz.Domain.Client.Counterparty), orderBase.Counterparty),
                        new TypedParameter(typeof(bool), true)
                    };
                    counterpartyDocumentsViewModel = AutofacScope.Resolve<CounterpartyDocumentsViewModel>(parameters);
                }

                return counterpartyDocumentsViewModel;
            }
        }
        
        public ILifetimeScope AutofacScope { get; set; }

        private DelegateCommand addSelectedDocsCommand;
        public DelegateCommand AddSelectedDocsCommand => addSelectedDocsCommand ?? (
            addSelectedDocsCommand = new DelegateCommand(
                () =>
                {
                    var counterpartyDocuments = CounterpartyDocumentsViewModel.GetSelectedDocuments();
                    var orderDocuments = OrdersDocumentsViewModel.GetSelectedDocuments();

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

                    OrderDocumentsModel.AddExistingDocuments(resultList);

                    Close(false, CloseSource.Self);
                },
                () => CounterpartyDocumentsViewModel.GetSelectedDocuments().Any() 
                    || OrdersDocumentsViewModel.GetSelectedDocuments().Any()
            )
        );

        public AddExistingDocumentsViewModel(
            IUnitOfWork uow,
            OrderBase orderBase,
            INavigationManager navigation) : base(navigation)
        {
            this.uow = uow ?? throw new ArgumentNullException(nameof(uow));
            this.orderBase = orderBase;
            Title = "Добавление документов";
        }

        void OnOrderSelectedViewModelOrderActivated(object sender, int e)
        {
            if(uow.GetById<OrderDocument>(e) is IPrintableRDLDocument printableRDLDocument)
            {
                RDLPreviewOpener.OpenRldDocument(printableRDLDocument.GetType(), printableRDLDocument);
            }
        }
    }
}
