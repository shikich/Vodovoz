using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Services;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Services;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class DeliveryOrderInfoPanelViewModel : OrderInfoPanelViewModelBase
    {
        public DeliveryOrderInfoPanelViewModel(
            ICommonServices commonServices,
            IOrderRepository orderRepository,
            IOrderParametersProvider orderParametersProvider,
            DeliveryOrder order) : base(commonServices, orderRepository, orderParametersProvider)
        {
            Order = order;
            Order.PropertyChanged += OrderOnPropertyChanged;
            
            UoW = UnitOfWorkFactory.CreateWithoutRoot();
            PaymentFromList = FillPaymentFromList();
            UpdateState();
        }
        
        public event Action<bool> UpdatePaymentTypeListForNaturalCounterparty;
        public event Action<object[]> UpdatePaymentTypeListForStopDelivery;
        public event Action<PaymentType> UpdateSelectedPaymentType;
        
        public DeliveryOrder Order { get; set; }

        private IEnumerable FillPaymentFromList()
        {
            var excludedPaymentFromId = orderParametersProvider.PaymentByCardFromSmsId;

            if (Order.PaymentByCardFrom?.Id != excludedPaymentFromId) {
                return UoW.Session.QueryOver<PaymentFrom>().Where(x => x.Id != excludedPaymentFromId).List();
            }
            
            return UoW.GetAll<PaymentFrom>();
        }

        private void OrderOnPropertyChanged(object sender, PropertyChangedEventArgs e)
        {
            switch (e.PropertyName)
            {
                case nameof(Order.PaymentType):
                    UpdateParamsDependOnPaymentType();
                    //TODO обработать заполнение основания подписи доков
                    //Order.SetProxyForOrder();
                    break;
                case nameof(Order.Status):
                    UpdateParamsDependOnOrderStatus();
                    break;
                case nameof(Order.Counterparty):
                    UpdateParamsDependOnCounterparty();
                    break;
            }
        }

        private void UpdateParamsDependOnPaymentType()
        {
            if (Order.PaymentType != PaymentType.ByCard && Order.PaymentType != PaymentType.Terminal) 
            {
                ChangeOrderNumberFromOnlineStore();
                ChangePaymentByCardFrom();
            }
            
            if (Order.PaymentType != PaymentType.cash) 
            {
                IsPaymentBySMSVisible = Order.IsPaymentBySms = false;
            }
            else 
            {
                IsPaymentBySMSVisible = true;
            }

            IsOrderNumberFromOnlineStoreVisible = IsPaymentByCardFromVisible = IsByCardPaymentType();
            IsBillDateVisible = IsDefaultDocumentTypeVisible = Order.PaymentType == PaymentType.cashless;
        }

        private void ChangeOrderNumberFromOnlineStore() => Order.OrderNumberFromOnlineStore = null;
        
        private void ChangePaymentByCardFrom() => Order.PaymentByCardFrom = null;

        private void UpdateParamsDependOnOrderStatus()
        {
            bool canEdit = Order.CanEditOrder;
            IsBillDateSensitive = IsPaymentTypeSensitive = IsOrderNumberFromOnlineStoreSensitive = IsPaymentFromSensitive = 
                IsNeedAddCertificatesSensitive = IsContactlessDeliverySensitive = IsPaymentBySMSSensitive = 
                    IsCounterpartySensitive = IsDefaultDocumentTypeSensitive = canEdit;
        }

        private void UpdateParamsDependOnCounterparty()
        {
            if (Order.Counterparty != null)
            {
                if(Order.Counterparty.PersonType == PersonType.natural) {
                    UpdatePaymentTypeListForNaturalCounterparty?.Invoke(false);
                } else {
                    UpdatePaymentTypeListForNaturalCounterparty?.Invoke(true);
                }
                
                if(Order.Id == 0 || HidePaymentTypesForNaturalCounterparty.Contains(Order.PaymentType)) {
                    Order.PaymentType = Order.Counterparty.PaymentMethod;
                    UpdateParamsDependOnPaymentType();
                } else {
                    UpdateSelectedPaymentType?.Invoke(Order.PaymentType);
                }
            }
        }
        
        private void UpdateState()
        {
            UpdateParamsDependOnPaymentType();
            UpdateParamsDependOnOrderStatus();
        }

        private bool IsByCardPaymentType() => Order.PaymentType == PaymentType.ByCard;
        
        //TODO Должно выполняться в EntryCounterpartyChangedByUser
        public void CheckForStopDelivery()
        {
            if (Order?.Counterparty != null && Order.Counterparty.IsDeliveriesClosed)
            {
                string message = "Стоп отгрузки!!!" + Environment.NewLine + "Комментарий от фин.отдела: " + Order.Counterparty.CloseDeliveryComment;
                CommonServices.InteractiveService.ShowMessage(ImportanceLevel.Info, message);
                
                UpdatePaymentTypeListForStopDelivery?.Invoke(HidePaymentTypesForStopDelivery);
            }
        }

        //TODO Должно выполняться в EntryCounterpartyChangedByUser
        public void UpdateClientDefaultParam() => Order.UpdateClientDefaultParam();
        
        public void PickerDeliveryDateDateOnChangedByUser(object sender, EventArgs e)
        {
            if(Order.DeliveryDate.HasValue) {
                if(Order.DeliveryDate.Value.Date != DateTime.Today.Date 
                   || CommonServices.InteractiveService.Question("Доставка сегодня? Вы уверены?", "Подтвердите дату доставки")) {
                    CheckSameOrders();
                    return;
                }
                Order.DeliveryDate = null;
            }
        }
        
        private void CheckSameOrders()
        {
            if(!Order.DeliveryDate.HasValue || Order.DeliveryPoint == null) return;

            var sameOrder = orderRepository.GetOrderOnDateAndDeliveryPoint(UoW, Order.DeliveryDate.Value, Order.DeliveryPoint);
            
            if(sameOrder != null && templateOrder == null) {
                CommonServices.InteractiveService.ShowMessage(
                    ImportanceLevel.Warning,"На выбранную дату и точку доставки уже есть созданный заказ!");
            }
        }
    }
}
