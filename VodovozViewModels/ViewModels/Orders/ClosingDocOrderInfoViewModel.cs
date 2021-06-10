using System;
using Autofac;
using Autofac.Core;
using QS.Services;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Cash;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Factories;
using Vodovoz.Services;
using Vodovoz.Tools.CallTasks;
using Vodovoz.ViewModels.Dialogs.Orders;

namespace Vodovoz.ViewModels.ViewModels.Orders
{
    public class ClosingDocOrderInfoViewModel : OrderInfoViewModelBase
    {
        private readonly IRouteListItemRepository _routeListItemRepository;
        private readonly IStandartNomenclatures _standartNomenclatures;
        private readonly ICashRepository _cashRepository;

        public ClosingDocOrder ClosingDocOrder => Order as ClosingDocOrder;
        public CallTaskWorker CallTaskWorker { get; set; }

        private ClosingDocOrderInfoPanelViewModel closingDocOrderInfoPanelViewModel;
        public ClosingDocOrderInfoPanelViewModel ClosingDocOrderInfoPanelViewModel
        {
            get
            {
                if (closingDocOrderInfoPanelViewModel == null)
                {
                    Parameter[] parameters = {
                        new TypedParameter(typeof(ClosingDocOrder), ClosingDocOrder),
                        new TypedParameter(typeof(ICommonServices), CommonServices),
                        new TypedParameter(typeof(IOrderRepository), AutofacScope.Resolve<IOrderRepository>()),
                        new TypedParameter(typeof(IOrderParametersProvider), AutofacScope.Resolve<IOrderParametersProvider>())
                    };
                    closingDocOrderInfoPanelViewModel = AutofacScope.Resolve<ClosingDocOrderInfoPanelViewModel>(parameters);
                    closingDocOrderInfoPanelViewModel.AutofacScope = AutofacScope;
                    closingDocOrderInfoPanelViewModel.ParentTab = ParentTab;
                }

                return closingDocOrderInfoPanelViewModel;
            }
        }

        public ClosingDocOrderInfoViewModel(
            ClosingDocOrder closingDocOrder,
            OrderInfoExpandedPanelViewModel expandedPanelViewModel,
            INomenclaturesJournalViewModelFactory nomenclaturesJournalViewModelFactory,
            ICommonServices commonServices,
            IRouteListItemRepository routeListItemRepository,
            IStandartNomenclatures standartNomenclatures,
            ICashRepository cashRepository)
            : base(closingDocOrder, expandedPanelViewModel, nomenclaturesJournalViewModelFactory, commonServices)
        {
            _routeListItemRepository = routeListItemRepository ?? throw new ArgumentNullException(nameof(routeListItemRepository));
            _standartNomenclatures = standartNomenclatures ?? throw new ArgumentNullException(nameof(standartNomenclatures));
            _cashRepository = cashRepository ?? throw new ArgumentNullException(nameof(cashRepository));
        }

        public event Action UpdateState;

        public bool CanCloseOrder => Order.Status == OrderStatus.Accepted
                                     && CommonServices.CurrentPermissionService.ValidatePresetPermission("can_close_orders");
        public bool CanReturnOrderToAccepted => Order.Status == OrderStatus.Closed && CanBeMovedFromClosedToAcepted;
        private bool CanBeMovedFromClosedToAcepted => 
            _routeListItemRepository.WasOrderInAnyRouteList(UoW, Order)
            && CommonServices.CurrentPermissionService.ValidatePresetPermission("can_move_order_from_closed_to_acepted");
        
        /// <summary>
        /// Возврат в принят из ручного закрытия
        /// </summary>
        public void OnButtonReturnToAcceptedClicked(object sender, EventArgs e)
        {
            if(Order.Status == OrderStatus.Closed && CanBeMovedFromClosedToAcepted) 
            {
                if(!CommonServices.InteractiveService.Question("Вы уверены, что хотите вернуть заказ в статус \"Принят\"?")) 
                {
                    return;
                }
                
                ChangeStatusAndCreateTasks(OrderStatus.Accepted, CallTaskWorker);
            }
            UpdateUIState();
        }
        
        /// <summary>
        /// Ручное закрытие заказа
        /// </summary>
        public void OnButtonCloseOrderClicked(object sender, EventArgs e)
        {
            if(Order.Status == OrderStatus.Accepted && CommonServices.CurrentPermissionService.ValidatePresetPermission("can_close_orders")) 
            {
                if(!CommonServices.InteractiveService.Question("Вы уверены, что хотите закрыть заказ?")) 
                {
                    return;
                }
                //TODO реализовать методы
                //Entity.UpdateBottlesMovementOperationWithoutDelivery(UoW, _standartNomenclatures, _routeListItemRepository, _cashRepository);
                //Entity.UpdateDepositOperations(UoW);

                ChangeStatusAndCreateTasks(OrderStatus.Closed, CallTaskWorker);
                
                foreach(OrderItem i in Order.ObservableOrderItems) 
                {
                    i.ActualCount = i.Count;
                }
            }
            
            UpdateUIState();
        }

        private void UpdateUIState()
        {
            throw new NotImplementedException();
        }

        //TODO реализовать и перенести метод в нужное место
        private void ChangeStatusAndCreateTasks(OrderStatus newStatus, CallTaskWorker callTaskWorker)
        {
            throw new NotImplementedException();
        }
    }
}
