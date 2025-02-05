﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NHibernate.Type;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.HistoryLog;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Orders;
using Vodovoz.EntityRepositories.Cash;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.EntityRepositories.Store;
using Vodovoz.Services;

namespace Vodovoz.Domain.FastPayments
{
	[Appellative(
		Gender = GrammaticalGender.Masculine,
		NominativePlural = "быстрые платежи",
		Nominative = "быстрый платеж")]
	[HistoryTrace]
	public class FastPayment : PropertyChangedBase, IDomainObject
	{
		private string _ticket;
		private string _qrPngBase64;
		private string _phoneNumber;
		private Order _order;
		private DateTime _creationDate;
		private DateTime? _paidDate;
		private FastPaymentStatus _fastPaymentStatus;
		private decimal _amount;
		private int _externalId;

		public virtual int Id { get; set; }
        
		[Display(Name = "Статус оплаты")]
		public virtual FastPaymentStatus FastPaymentStatus
		{
			get => _fastPaymentStatus;
			protected set => SetField(ref _fastPaymentStatus, value);
		}

		[Display(Name = "Сумма платежа")]
		public virtual decimal Amount
		{
			get => _amount;
			set => SetField(ref _amount, value);
		}

		[Display(Name = "Заказ")]
		public virtual Order Order
		{
			get => _order;
			set => SetField(ref _order, value);
		}
		
		[Display(Name = "Сессия оплаты")]
		public virtual string Ticket
		{
			get => _ticket;
			set => SetField(ref _ticket, value);
		}
		
		[IgnoreHistoryTrace]
		[Display(Name = "Qr-код")]
		public virtual string QRPngBase64
		{
			get => _qrPngBase64;
			set => SetField(ref _qrPngBase64, value);
		}
		
		[Display(Name = "Дата создания")]
		public virtual DateTime CreationDate
		{
			get => _creationDate;
			set => SetField(ref _creationDate, value);
		}
        
		[Display(Name = "Дата оплаты")]
		public virtual DateTime? PaidDate
		{
			get => _paidDate;
			set => SetField(ref _paidDate, value);
		}
		
		[Display(Name = "Внешний ID")]
		public virtual int ExternalId {
			get => _externalId;
			set => SetField(ref _externalId, value);
		}
		
		[Display(Name = "Номер телефона")]
		public virtual string PhoneNumber
		{
			get => _phoneNumber;
			set => SetField(ref _phoneNumber, value);
		}

		public virtual void SetProcessingStatus()
		{
			FastPaymentStatus = FastPaymentStatus.Processing;
		}
		
		public virtual void SetPerformedStatus(
			IUnitOfWork uow,
			DateTime paidDate,
			PaymentFrom paymentByCardFromQrId,
			IStandartNomenclatures standartNomenclatures,
			IRouteListItemRepository routeListItemRepository,
			ISelfDeliveryRepository selfDeliveryRepository,
			ICashRepository cashRepository)
		{
			FastPaymentStatus = FastPaymentStatus.Performed;

			if (Order.PaymentType == PaymentType.cash
				&& Order.SelfDelivery
				&& Order.OrderStatus == OrderStatus.WaitForPayment
				&& Order.PayAfterShipment)
			{
				Order.TryCloseSelfDeliveryOrder(
					uow,
					standartNomenclatures,
					routeListItemRepository,
					selfDeliveryRepository,
					cashRepository);
				Order.IsSelfDeliveryPaid = true;
			}

			if (Order.PaymentType == PaymentType.cash
				&& Order.SelfDelivery
				&& Order.OrderStatus == OrderStatus.WaitForPayment
				&& !Order.PayAfterShipment)
			{
				Order.ChangeStatus(OrderStatus.OnLoading);
				Order.IsSelfDeliveryPaid = true;
			}
            
			PaidDate = paidDate;
			Order.OnlineOrder = ExternalId;
			Order.PaymentType = PaymentType.ByCard;
			Order.PaymentByCardFrom = paymentByCardFromQrId;
			Order.ForceUpdateContract();

			foreach (var routeListItem in routeListItemRepository.GetRouteListItemsForOrder(uow, Order.Id))
			{
				routeListItem.RecalculateTotalCash();
				uow.Save(routeListItem);
			}
		}
		
		public virtual void SetRejectedStatus()
		{
			FastPaymentStatus = FastPaymentStatus.Rejected;
		}
	}

	public class FastPaymentStatusStringType : EnumStringType<FastPaymentStatus> { }
}
