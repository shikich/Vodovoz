using System;
using System.ComponentModel;
using Autofac;
using Autofac.Core;
using QS.Services;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Factories;
using Vodovoz.Infrastructure.Services;
using Vodovoz.Services;
using Vodovoz.Tools.CallTasks;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class SelfDeliveryOrderInfoViewModel : OrderInfoViewModelBase
    {
        private readonly IEmployeeService _employeeService;
        
        //TODO перенести в нужный класс, как станет понятно где он должен находиться
        public CallTaskWorker CallTaskWorker { get; set; }

        private SelfDeliveryOrderInfoPanelViewModel selfDeliveryOrderInfoPanelViewModel;
        public SelfDeliveryOrderInfoPanelViewModel SelfDeliveryOrderInfoPanelViewModel
        {
            get
            {
                if (selfDeliveryOrderInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(SelfDeliveryOrder), SelfDeliveryOrder),
                        new TypedParameter(typeof(ICommonServices), CommonServices),
                        new TypedParameter(typeof(IOrderRepository), AutofacScope.Resolve<IOrderRepository>()),
                        new TypedParameter(typeof(IOrderParametersProvider), AutofacScope.Resolve<IOrderParametersProvider>())
                    };
                    selfDeliveryOrderInfoPanelViewModel = AutofacScope.Resolve<SelfDeliveryOrderInfoPanelViewModel>(parameters);
                    selfDeliveryOrderInfoPanelViewModel.AutofacScope = AutofacScope;
                    selfDeliveryOrderInfoPanelViewModel.ParentTab = ParentTab;
                }

                return selfDeliveryOrderInfoPanelViewModel;
            }
        }

        public SelfDeliveryOrderInfoViewModel(
            SelfDeliveryOrder selfDeliveryOrder,
            OrderInfoExpandedPanelViewModel expandedPanelViewModel,
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory,
            ICommonServices commonServices,
            IEmployeeService employeeService)
            : base(selfDeliveryOrder, expandedPanelViewModel, nomenclaturesJournalViewModelFactory, commonServices)
        {
            _employeeService = employeeService ?? throw new ArgumentNullException(nameof(employeeService));
            Order.PropertyChanged += OrderOnPropertyChanged;
        }

        private void OrderOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch(e.PropertyName)
            {
                case nameof(Order.PaymentType):
                case nameof(Order.Status):
                    UpdateState?.Invoke();
                    break;
            }
        }

        public event Action UpdateState;

        public SelfDeliveryOrder SelfDeliveryOrder => Order as SelfDeliveryOrder;

        public bool CanAcceptPaymentSelfDelivery => 
            (Order.PaymentType == PaymentType.cashless || Order.PaymentType == PaymentType.ByCard)
            && Order.Status == OrderStatus.WaitForPayment
            && CommonServices.CurrentPermissionService.ValidatePresetPermission("accept_cashless_paid_selfdelivery");
        
        public bool CanSendForLoadingSelfDelivery => 
            Order.Status == OrderStatus.Accepted 
            && CommonServices.CurrentPermissionService.ValidatePresetPermission("allow_load_selfdelivery");
        
        public void OnButtonSendForLoadingSelfDeliveryClicked(object sender, EventArgs e)
        {
            SendForLoadingSelfDelivery(CommonServices.CurrentPermissionService, CallTaskWorker);
            //UpdateUIState();
        }
        
        //TODO Метод был в модели (Order)
        private void SendForLoadingSelfDelivery(ICurrentPermissionService permissionService, CallTaskWorker callTaskWorker)
        {
            if(Order.Status == OrderStatus.Accepted && permissionService.ValidatePresetPermission("allow_load_selfdelivery")) 
            {
                ChangeStatusAndCreateTasks(OrderStatus.OnLoading, callTaskWorker);
                SelfDeliveryOrder.LoadAllowedBy = _employeeService.GetEmployeeForUser(UoW, CommonServices.UserService.CurrentUserId);
            }
        }

        public void OnButtonAcceptPaymentSelfDeliveryClicked(object sender, EventArgs e)
        {
            SelfDeliveryAcceptCashlessPaid(CallTaskWorker);
            //UpdateUIState();
        }
        
        //TODO Метод был в модели (Order)
        private void SelfDeliveryAcceptCashlessPaid(CallTaskWorker callTaskWorker)
        {
            if(Order.PaymentType != PaymentType.cashless && Order.PaymentType != PaymentType.ByCard)
            {
                return;
            }

            if(Order.Status != OrderStatus.WaitForPayment)
            {
                return;
            }

            if(!CommonServices.CurrentPermissionService.ValidatePresetPermission("accept_cashless_paid_selfdelivery"))
            {
                return;
            }

            ChangeStatusAndCreateTasks(SelfDeliveryOrder.PayAfterShipment ? OrderStatus.Closed : OrderStatus.Accepted, callTaskWorker);
        }
        
        //TODO здесь ли будет этот метод?
        private void ChangeStatusAndCreateTasks(OrderStatus onLoading, CallTaskWorker callTaskWorker)
        {
            throw new NotImplementedException();
        }
    }
}
