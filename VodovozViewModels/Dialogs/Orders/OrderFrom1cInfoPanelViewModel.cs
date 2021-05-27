using System;
using System.Collections;
using System.ComponentModel;
using System.Linq;
using Autofac;
using QS.Dialog;
using QS.DomainModel.UoW;
using QS.Navigation;
using QS.Services;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Services;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
    public class OrderFrom1cInfoPanelViewModel : UoWWidgetViewModelBase, IAutofacScopeHolder
    {
        private readonly IOrderRepository orderRepository;
        private readonly IOrderParametersProvider orderParametersProvider;
        private OrderBase templateOrder;

        #region Поля и свойства видимости элементов

        private bool isDeliveryScheduleVisible;
        public bool IsDeliveryScheduleVisible
        {
            get => isDeliveryScheduleVisible;
            set => SetField(ref isDeliveryScheduleVisible, value);
        }

        private bool isPaymentBySMSVisible;
        public bool IsPaymentBySMSVisible
        {
            get => isPaymentBySMSVisible;
            set => SetField(ref isPaymentBySMSVisible, value);
        }

        private bool isBillDateVisible;
        public bool IsBillDateVisible
        {
            get => isBillDateVisible;
            set => SetField(ref isBillDateVisible, value);
        }

        #endregion

        #region Поля и свойства чувствительности элементов

        private bool isBillDateSensitive = true;
        public bool IsBillDateSensitive
        {
            get => isBillDateSensitive;
            set => SetField(ref isBillDateSensitive, value);
        }

        private bool isPaymentTypeSensitive = true;
        public bool IsPaymentTypeSensitive
        {
            get => isPaymentTypeSensitive;
            set => SetField(ref isPaymentTypeSensitive, value);
        }

        private bool isPaymentFromSensitive = true;
        public bool IsPaymentFromSensitive
        {
            get => isPaymentFromSensitive;
            set => SetField(ref isPaymentFromSensitive, value);
        }

        private bool isNeedAddCertificatesSensitive = true;
        public bool IsNeedAddCertificatesSensitive
        {
            get => isNeedAddCertificatesSensitive;
            set => SetField(ref isNeedAddCertificatesSensitive, value);
        }

        private bool isContactlessDeliverySensitive = true;
        public bool IsContactlessDeliverySensitive
        {
            get => isContactlessDeliverySensitive;
            set => SetField(ref isContactlessDeliverySensitive, value);
        }

        private bool isPaymentBySMSSensitive = true;
        public bool IsPaymentBySMSSensitive
        {
            get => isPaymentBySMSSensitive;
            set => SetField(ref isPaymentBySMSSensitive, value);
        }

        private bool isCounterpartySensitive = true;
        public bool IsCounterpartySensitive
        {
            get => isCounterpartySensitive;
            set => SetField(ref isCounterpartySensitive, value);
        }

        #endregion

        public ICommonServices CommonServices { get; }
        public OrderFrom1c Order { get; set; }
        public DialogViewModelBase ParentTab { get; set; }
        public ILifetimeScope AutofacScope { get; set; }
        public object[] HidePaymentTypesForNaturalCounterparty { get; } = new object[] { PaymentType.cashless };
        public object[] HidePaymentTypesForStopDelivery { get; } =
            new object[] { PaymentType.barter, PaymentType.BeveragesWorld, PaymentType.ContractDoc, PaymentType.cashless };

        public IEnumerable PaymentFromList { get; }
        public event Action<bool> UpdatePaymentTypeListForNaturalCounterparty;
        public event Action<object[]> UpdatePaymentTypeListForStopDelivery;
        public event Action<PaymentType> UpdateSelectedPaymentType;

        public OrderFrom1cInfoPanelViewModel(
            ICommonServices commonServices,
            IOrderRepository orderRepository,
            IOrderParametersProvider orderParametersProvider,
            OrderFrom1c order)
        {
            CommonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.orderParametersProvider = orderParametersProvider ?? throw new ArgumentNullException(nameof(orderParametersProvider));
            Order = order;
            Order.PropertyChanged += OrderOnPropertyChanged;

            UoW = UnitOfWorkFactory.CreateWithoutRoot();
            PaymentFromList = FillPaymentFromList();
            UpdateState();
        }

        private IEnumerable FillPaymentFromList()
        {
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
            if (Order.PaymentType != PaymentType.cash)
            {
                IsPaymentBySMSVisible = Order.IsPaymentBySms = false;
            }
            else
            {
                IsPaymentBySMSVisible = true;
            }
        }

        private void UpdateParamsDependOnOrderStatus()
        {
            bool canEdit = Order.CanEditOrder;
            IsBillDateSensitive = IsPaymentTypeSensitive = IsPaymentFromSensitive = IsNeedAddCertificatesSensitive = 
                IsContactlessDeliverySensitive = IsPaymentBySMSSensitive = IsCounterpartySensitive = canEdit;
        }

        private void UpdateParamsDependOnCounterparty()
        {
            if (Order.Counterparty != null)
            {
                if (Order.Counterparty.PersonType == PersonType.natural)
                {
                    UpdatePaymentTypeListForNaturalCounterparty?.Invoke(false);
                }
                else
                {
                    UpdatePaymentTypeListForNaturalCounterparty?.Invoke(true);
                }

                if (Order.Id == 0 || HidePaymentTypesForNaturalCounterparty.Contains(Order.PaymentType))
                {
                    Order.PaymentType = Order.Counterparty.PaymentMethod;
                    UpdateParamsDependOnPaymentType();
                }
                else
                {
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
            if (Order.DeliveryDate.HasValue)
            {
                if (Order.DeliveryDate.Value.Date != DateTime.Today.Date
                   || CommonServices.InteractiveService.Question("Доставка сегодня? Вы уверены?", "Подтвердите дату доставки"))
                {
                    CheckSameOrders();
                    return;
                }
                Order.DeliveryDate = null;
            }
        }

        private void CheckSameOrders()
        {
            if (!Order.DeliveryDate.HasValue || Order.DeliveryPoint == null)
            {
                return;
            }

            var sameOrder = orderRepository.GetOrderOnDateAndDeliveryPoint(UoW, Order.DeliveryDate.Value, Order.DeliveryPoint);

            if (sameOrder != null && templateOrder == null)
            {
                CommonServices.InteractiveService.ShowMessage(
                    ImportanceLevel.Warning, "На выбранную дату и точку доставки уже есть созданный заказ!");
            }
        }
    }
}