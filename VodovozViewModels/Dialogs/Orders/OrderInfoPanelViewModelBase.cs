using System;
using Autofac;
using QS.Navigation;
using QS.Services;
using QS.ViewModels;
using QS.ViewModels.Dialog;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Orders;
using Vodovoz.Services;

namespace Vodovoz.ViewModels.Dialogs.Orders
{
	public class OrderInfoPanelViewModelBase : UoWWidgetViewModelBase, IAutofacScopeHolder
	{
		protected readonly IOrderRepository orderRepository;
        protected readonly IOrderParametersProvider orderParametersProvider;
        protected OrderBase templateOrder;
        
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
        
        private bool isOrderNumberFromOnlineStoreVisible;
        public bool IsOrderNumberFromOnlineStoreVisible
        {
            get => isOrderNumberFromOnlineStoreVisible;
            set => SetField(ref isOrderNumberFromOnlineStoreVisible, value);
        }
        
        private bool isPaymentByCardFromVisible;
        public bool IsPaymentByCardFromVisible
        {
            get => isPaymentByCardFromVisible;
            set => SetField(ref isPaymentByCardFromVisible, value);
        }
        
        private bool isBillDateVisible;
        public bool IsBillDateVisible
        {
            get => isBillDateVisible;
            set => SetField(ref isBillDateVisible, value);
        }
        
        private bool isDefaultDocumentTypeVisible;
        public bool IsDefaultDocumentTypeVisible
        {
            get => isDefaultDocumentTypeVisible;
            set => SetField(ref isDefaultDocumentTypeVisible, value);
        }
        
        private bool isAuthorVisible;
        public bool IsAuthorVisible
        {
            get => isAuthorVisible;
            set => SetField(ref isAuthorVisible, value);
        }
        
        private bool isCreationDateVisible;
        public bool IsCreationDateVisible
        {
            get => isCreationDateVisible;
            set => SetField(ref isCreationDateVisible, value);
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
        
        private bool isOrderNumberFromOnlineStoreSensitive = true;
        public bool IsOrderNumberFromOnlineStoreSensitive
        {
            get => isOrderNumberFromOnlineStoreSensitive;
            set => SetField(ref isOrderNumberFromOnlineStoreSensitive, value);
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
        
        private bool isDefaultDocumentTypeSensitive = true;
        public bool IsDefaultDocumentTypeSensitive
        {
            get => isDefaultDocumentTypeSensitive;
            set => SetField(ref isDefaultDocumentTypeSensitive, value);
        }

        #endregion
        
        public ICommonServices CommonServices { get; }
        public DialogViewModelBase ParentTab { get; set; }
        public ILifetimeScope AutofacScope { get; set; }

        protected OrderInfoPanelViewModelBase(
            ICommonServices commonServices,
            IOrderRepository orderRepository,
            IOrderParametersProvider orderParametersProvider)
        {
            CommonServices = commonServices ?? throw new ArgumentNullException(nameof(commonServices));
            this.orderRepository = orderRepository ?? throw new ArgumentNullException(nameof(orderRepository));
            this.orderParametersProvider = orderParametersProvider ?? throw new ArgumentNullException(nameof(orderParametersProvider));
        }
	}
}