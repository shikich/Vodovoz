using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using QS.DomainModel.Entity;
using QS.DomainModel.UoW;
using QS.HistoryLog;
using QS.Tools;
using Vodovoz.Domain.Client;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.WageCalculation;
using Vodovoz.Domain.WageCalculation.CalculationServices.RouteList;
using Vodovoz.EntityRepositories.Logistic;
using Vodovoz.Tools.CallTasks;
using Vodovoz.Tools.Logistic;

namespace Vodovoz.Domain.Logistic
{

	[Appellative(Gender = GrammaticalGender.Masculine,
		NominativePlural = "адреса маршрутного листа",
		Nominative = "адрес маршрутного листа")]
	[HistoryTrace]
	public class RouteListItem : PropertyChangedBase, IDomainObject, IValidatableObject
	{
		private static NLog.Logger _logger = NLog.LogManager.GetCurrentClassLogger();

		#region Свойства

		public virtual int Id { get; set; }

		DateTime _version;
		[Display(Name = "Версия")]
		public virtual DateTime Version
		{
			get => _version;
			set => SetField(ref _version, value);
		}

		private Order _order;
		[Display(Name = "Заказ")]
		public virtual Order Order
		{
			get => _order;
			set => SetField(ref _order, value);
		}

		private RouteList _routeList;
		[Display(Name = "Маршрутный лист")]
		public virtual RouteList RouteList
		{
			get => _routeList;
			set => SetField(ref _routeList, value);
		}

		private RouteListItemStatus _status;
		[Display(Name = "Статус адреса")]
		public virtual RouteListItemStatus Status
		{
			get => _status;
			protected set => SetField(ref _status, value);
		}

		private DateTime? _statusLastUpdate;
		[Display(Name = "Время изменения статуса")]
		public virtual DateTime? StatusLastUpdate
		{
			get => _statusLastUpdate;
			set => SetField(ref _statusLastUpdate, value);
		}

		private RouteListItem _transferedTo;
		[Display(Name = "Перенесен в другой маршрутный лист")]
		public virtual RouteListItem TransferedTo
		{
			get => _transferedTo;
			set
			{
				if(SetField(ref _transferedTo, value) && value != null)
				{
					Status = RouteListItemStatus.Transfered;
				}
			}
		}

		private bool _needToReload;
		[Display(Name = "Необходима повторная загрузка")]
		public virtual bool NeedToReload
		{
			get => _needToReload;
			set => SetField(ref _needToReload, value);
		}

		private bool _wasTransfered;
		[Display(Name = "Был перенесен")]
		public virtual bool WasTransfered
		{
			get => _wasTransfered;
			set => SetField(ref _wasTransfered, value);
		}

		private string _cashierComment;
		[Display(Name = "Комментарий кассира")]
		public virtual string CashierComment
		{
			get => _cashierComment;
			set => SetField(ref _cashierComment, value);
		}

		private DateTime? _cashierCommentCreateDate;
		[Display(Name = "Дата создания комментария кассира")]
		public virtual DateTime? CashierCommentCreateDate
		{
			get => _cashierCommentCreateDate;
			set => SetField(ref _cashierCommentCreateDate, value);
		}

		private DateTime? _cashierCommentLastUpdate;
		[Display(Name = "Дата обновления комментария кассира")]
		public virtual DateTime? CashierCommentLastUpdate
		{
			get => _cashierCommentLastUpdate;
			set => SetField(ref _cashierCommentLastUpdate, value);
		}

		private Employee _cashierCommentAuthor;
		[Display(Name = "Автор комментария")]
		public virtual Employee CashierCommentAuthor
		{
			get => _cashierCommentAuthor;
			set => SetField(ref _cashierCommentAuthor, value);
		}

		private string _comment;
		[Display(Name = "Комментарий")]
		public virtual string Comment
		{
			get => _comment;
			set => SetField(ref _comment, value);
		}

		private bool _withForwarder;
		[Display(Name = "С экспедитором")]
		public virtual bool WithForwarder
		{
			get => _withForwarder;
			set => SetField(ref _withForwarder, value);
		}

		private int _indexInRoute;
		[Display(Name = "Порядковый номер в МЛ")]
		public virtual int IndexInRoute
		{
			get => _indexInRoute;
			set => SetField(ref _indexInRoute, value);
		}

		private int _bottlesReturned;
		[Display(Name = "Возвращено бутылей")]
		public virtual int BottlesReturned
		{
			get => _bottlesReturned;
			set => SetField(ref _bottlesReturned, value);
		}

		private int? _driverBottlesReturned;
		[Display(Name = "Возвращено бутылей - водитель")]
		public virtual int? DriverBottlesReturned
		{
			get => _driverBottlesReturned;
			set => SetField(ref _driverBottlesReturned, value);
		}

		private decimal _oldBottleDepositsCollected;
		/// <summary>
		/// Устаревший залог за бутыли. Который раньше вводился пользователем вручную при закрытии МЛ
		/// </summary>
		[Display(Name = "Старый залог за бутыли")]
		public virtual decimal OldBottleDepositsCollected
		{
			get => _oldBottleDepositsCollected;
			set => SetField(ref _oldBottleDepositsCollected, value);
		}

		public virtual decimal BottleDepositsCollected
		{
			get
			{
				if(Order.PaymentType == PaymentType.ContractDoc || Order.PaymentType == PaymentType.cashless)
				{
					return 0;
				}

				if(_oldBottleDepositsCollected != 0m)
				{
					return _oldBottleDepositsCollected;
				}

				return 0 - Order.BottleDepositSum;
			}
		}

		private decimal _oldEquipmentDepositsCollected;
		/// <summary>
		/// Устаревший залог за оборудование. Который раньше вводился пользователем вручную при закрытии МЛ
		/// </summary>
		[Display(Name = "Старый залог за оборудование")]
		public virtual decimal OldEquipmentDepositsCollected
		{
			get => _oldEquipmentDepositsCollected;
			set => SetField(ref _oldEquipmentDepositsCollected, value);
		}

		public virtual decimal EquipmentDepositsCollected
		{
			get
			{
				if(Order.PaymentType == PaymentType.ContractDoc || Order.PaymentType == PaymentType.cashless)
				{
					return 0;
				}

				if(_oldEquipmentDepositsCollected != 0m)
				{
					return _oldEquipmentDepositsCollected;
				}

				return 0 - Order.EquipmentDepositSum;
			}
		}

		public virtual decimal AddressCashSum
		{
			get
			{
				if(!IsDelivered())
				{
					return 0;
				}
				if(Order.PaymentType != Client.PaymentType.cash && Order.PaymentType != Client.PaymentType.BeveragesWorld)
				{
					return 0;
				}
				return Order.OrderCashSum + OldBottleDepositsCollected + OldEquipmentDepositsCollected + ExtraCash;
			}
		}

		private decimal _totalCash;
		[Display(Name = "Всего наличных")]
		public virtual decimal TotalCash
		{
			get => _totalCash;
			set => SetField(ref _totalCash, value);
		}

		private decimal _extraCash;
		[Display(Name = "Дополнительно наличных")]
		public virtual decimal ExtraCash
		{
			get => _extraCash;
			set => SetField(ref _extraCash, value);
		}

		private decimal _driverWage;
		[Display(Name = "ЗП водителя")]
		public virtual decimal DriverWage
		{
			get => _driverWage;
			set => SetField(ref _driverWage, value);
		}

		private decimal _driverWageSurcharge;
		[Display(Name = "Надбавка к ЗП водителя")]
		public virtual decimal DriverWageSurcharge
		{
			get => _driverWageSurcharge;
			set => SetField(ref _driverWageSurcharge, value);
		}

		private decimal _forwarderWage;
		[Display(Name = "ЗП экспедитора")]
		//Зарплана с уже включенной надбавкой ForwarderWageSurcharge
		public virtual decimal ForwarderWage
		{
			get => _forwarderWage;
			set => SetField(ref _forwarderWage, value);
		}

		[Display(Name = "Оповещение за 30 минут")]
		[IgnoreHistoryTrace]
		public virtual bool Notified30Minutes { get; set; }

		[Display(Name = "Время оповещения прошло")]
		[IgnoreHistoryTrace]
		public virtual bool NotifiedTimeout { get; set; }

		private TimeSpan? _planTimeStart;
		[Display(Name = "Запланированное время приезда min")]
		public virtual TimeSpan? PlanTimeStart
		{
			get => _planTimeStart;
			set => SetField(ref _planTimeStart, value);
		}

		private TimeSpan? _planTimeEnd;
		[Display(Name = "Запланированное время приезда max")]
		public virtual TimeSpan? PlanTimeEnd
		{
			get => _planTimeEnd;
			set => SetField(ref _planTimeEnd, value);
		}

		private WageDistrictLevelRate _forwarderWageCalulationMethodic;
		[Display(Name = "Методика расчёта ЗП экспедитора")]
		public virtual WageDistrictLevelRate ForwarderWageCalculationMethodic
		{
			get => _forwarderWageCalulationMethodic;
			set => SetField(ref _forwarderWageCalulationMethodic, value);
		}

		private WageDistrictLevelRate _driverWageCalulationMethodic;
		[Display(Name = "Методика расчёта ЗП водителя")]
		public virtual WageDistrictLevelRate DriverWageCalculationMethodic
		{
			get => _driverWageCalulationMethodic;
			set => SetField(ref _driverWageCalulationMethodic, value);
		}

		private LateArrivalReason _lateArrivalReason;
		[Display(Name = "Причина опоздания водителя")]
		public virtual LateArrivalReason LateArrivalReason
		{
			get => _lateArrivalReason;
			set => SetField(ref _lateArrivalReason, value);
		}

		private Employee _lateArrivalReasonAuthor;
		[Display(Name = "Автор причины опоздания водителя")]
		public virtual Employee LateArrivalReasonAuthor
		{
			get => _lateArrivalReasonAuthor;
			set => SetField(ref _lateArrivalReasonAuthor, value);
		}

		private string _commentForFine;
		[Display(Name = "Комментарий по штрафу")]
		public virtual string CommentForFine
		{
			get => _commentForFine;
			set => SetField(ref _commentForFine, value);
		}

		private Employee _commentForFineAuthor;
		[Display(Name = "Последний редактор комментария по штрафу")]
		public virtual Employee CommentForFineAuthor
		{
			get => _commentForFineAuthor;
			set
			{
				if(_commentForFineAuthor != value)
				{
					SetField(ref _commentForFineAuthor, value);
				}
			}
		}

		private IList<Fine> _fines = new List<Fine>();
		[Display(Name = "Штрафы")]
		public virtual IList<Fine> Fines
		{
			get => _fines;
			set => SetField(ref _fines, value);
		}

		private bool _isDriverForeignDistrict;

		[Display(Name = "Чужой район для водителя")]
		public virtual bool IsDriverForeignDistrict
		{
			get => _isDriverForeignDistrict;
			set => SetField(ref _isDriverForeignDistrict, value);
		}

		GenericObservableList<Fine> _observableFines;
		//FIXME Костыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<Fine> ObservableFines
		{
			get
			{
				if(_observableFines == null)
				{
					_observableFines = new GenericObservableList<Fine>(Fines);
				}
				return _observableFines;
			}
		}

		#endregion

		#region Runtime свойства (не мапятся)

		public virtual bool AddressIsValid { get; set; } = true;

		#endregion

		#region Расчетные

		public virtual string Title => $"Адрес в МЛ №{RouteList.Id} - {Order.DeliveryPoint.CompiledAddress}";

		//FIXME запуск оборудования - временный фикс
		public virtual int CoolersToClient
		{
			get
			{
				return Order.OrderEquipments.Where(item => item.Direction == Direction.Deliver)
					.Where(item => item.Confirmed)
							.Count(item => item.Equipment != null ? item.Equipment.Nomenclature.Category == NomenclatureCategory.equipment
								   : (item.Nomenclature.Category == NomenclatureCategory.equipment));
			}
		}

		public virtual int PlannedCoolersToClient
		{
			get
			{
				return Order.OrderEquipments.Where(item => item.Direction == Direction.Deliver).Where(item => item.Equipment != null)
					.Count(item => item.Equipment.Nomenclature.Kind.WarrantyCardType == WarrantyCardType.CoolerWarranty);
			}
		}

		public virtual int PumpsToClient
		{
			get
			{
				return Order.OrderEquipments.Where(item => item.Direction == Direction.Deliver).Where(item => item.Equipment != null)
					.Where(item => item.Confirmed)
					.Count(item => item.Equipment.Nomenclature.Kind.WarrantyCardType == WarrantyCardType.PumpWarranty);
			}
		}

		public virtual int PlannedPumpsToClient
		{
			get
			{
				return Order.OrderEquipments.Where(item => item.Direction == Direction.Deliver).Where(item => item.Equipment != null)
					.Count(item => item.Equipment.Nomenclature.Kind.WarrantyCardType == WarrantyCardType.PumpWarranty);
			}
		}

		public virtual int UncategorisedEquipmentToClient
		{
			get
			{
				return Order.OrderEquipments.Where(item => item.Direction == Direction.Deliver).Where(item => item.Equipment != null)
					.Where(item => item.Confirmed)
					.Count(item => item.Equipment.Nomenclature.Kind.WarrantyCardType == WarrantyCardType.WithoutCard);
			}
		}

		public virtual int PlannedUncategorisedEquipmentToClient
		{
			get
			{
				return Order.OrderEquipments.Where(item => item.Direction == Direction.Deliver).Where(item => item.Equipment != null)
					.Count(item => item.Equipment.Nomenclature.Kind.WarrantyCardType == WarrantyCardType.WithoutCard);
			}
		}

		//FIXME запуск оборудования - временный фикс
		public virtual int CoolersFromClient
		{
			get
			{
				return Order.OrderEquipments.Where(item => item.Direction == Direction.PickUp)
					.Where(item => item.Confirmed).Where(item => item.Equipment != null)
							.Count(item => item.Equipment.Nomenclature.Category == NomenclatureCategory.equipment);
			}
		}

		public virtual int PumpsFromClient
		{
			get
			{
				return Order.OrderEquipments.Where(item => item.Direction == Direction.PickUp).Where(item => item.Equipment != null)
					.Where(item => item.Confirmed)
					.Count(item => item.Equipment.Nomenclature.Kind.WarrantyCardType == WarrantyCardType.PumpWarranty);
			}
		}

		public virtual int PlannedCoolersFromClient
		{
			get
			{
				return Order.OrderEquipments.Where(item => item.Direction == Direction.PickUp)
					.Where(item => item.Equipment != null)
					.Count(item => item.Equipment.Nomenclature.Kind.WarrantyCardType == WarrantyCardType.CoolerWarranty);
			}
		}

		public virtual int PlannedPumpsFromClient
		{
			get
			{
				return Order.OrderEquipments.Where(item => item.Direction == Direction.PickUp)
					.Where(item => item.Equipment != null)
					.Count(item => item.Equipment.Nomenclature.Kind.WarrantyCardType == WarrantyCardType.PumpWarranty);
			}
		}

		public virtual string EquipmentsToClientText
		{
			get
			{
				//Если это старый заказ со старой записью оборудования в виде строки, то выводим только его
				if(!string.IsNullOrWhiteSpace(Order.ToClientText))
				{
					return Order.ToClientText;
				}

				var orderEquipment = string.Join(
								Environment.NewLine,
								Order.OrderEquipments
									.Where(x => x.Direction == Direction.Deliver)
									.Select(x => $"{x.NameString}: {x.Count:N0}")
				);

				var orderItemEquipment = string.Join(
								Environment.NewLine,
								Order.OrderItems
									.Where(x => x.Nomenclature.Category == NomenclatureCategory.equipment)
									.Select(x => $"{x.Nomenclature.Name}: {x.Count:N0}")
				);

				if(string.IsNullOrWhiteSpace(orderItemEquipment))
				{
					return orderEquipment;
				}

				return $"{orderEquipment}{Environment.NewLine}{orderItemEquipment}";
			}
		}

		public virtual string EquipmentsFromClientText
		{
			get
			{
				//Если это старый заказ со старой записью оборудования в виде строки, то выводим только его
				if(!string.IsNullOrWhiteSpace(Order.FromClientText))
				{
					return Order.FromClientText;
				}

				return string.Join("\n", Order.OrderEquipments
													.Where(x => x.Direction == Direction.PickUp)
													.Select(x => $"{x.NameString}: {x.Count}"));
			}
		}

		public virtual WageDistrictLevelRate DriverWageCalcMethodicTemporaryStore { get; set; }

		public virtual WageDistrictLevelRate ForwarderWageCalcMethodicTemporaryStore { get; set; }

		public virtual bool NeedToLoad => Order.HasItemsNeededToLoad;

		#endregion

		public RouteListItem() { }

		//Конструктор создания новой строки
		public RouteListItem(RouteList routeList, Order order, RouteListItemStatus status)
		{
			_routeList = routeList;
			if(order.OrderStatus == OrderStatus.Accepted)
			{
				order.OrderStatus = OrderStatus.InTravelList;
			}
			Order = order;
			_status = status;
		}

		#region Функции

		public virtual void UpdateStatusAndCreateTask(IUnitOfWork uow, RouteListItemStatus status, CallTaskWorker callTaskWorker)
		{
			if(Status == status)
			{
				return;
			}

			Status = status;
			StatusLastUpdate = DateTime.Now;

			switch(Status)
			{
				case RouteListItemStatus.Canceled:
					Order.ChangeStatusAndCreateTasks(OrderStatus.DeliveryCanceled, callTaskWorker);
					FillCountsOnCanceled();
					break;
				case RouteListItemStatus.Completed:
					Order.ChangeStatusAndCreateTasks(OrderStatus.Shipped, callTaskWorker);
					if(Order.TimeDelivered == null)
					{
						Order.TimeDelivered = DateTime.Now;
					}
					RestoreOrder();
					break;
				case RouteListItemStatus.EnRoute:
					Order.ChangeStatusAndCreateTasks(OrderStatus.OnTheWay, callTaskWorker);
					RestoreOrder();
					break;
				case RouteListItemStatus.Overdue:
					Order.ChangeStatusAndCreateTasks(OrderStatus.NotDelivered, callTaskWorker);
					FillCountsOnCanceled();
					break;
			}
			uow.Save(Order);
		}

		public virtual void UpdateStatus(IUnitOfWork uow, RouteListItemStatus status)
		{
			if(Status == status)
			{
				return;
			}

			Status = status;
			StatusLastUpdate = DateTime.Now;

			switch(Status)
			{
				case RouteListItemStatus.Canceled:
					Order.ChangeStatus(OrderStatus.DeliveryCanceled);
					FillCountsOnCanceled();
					break;
				case RouteListItemStatus.Completed:
					Order.ChangeStatus(OrderStatus.Shipped);
					if(Order.TimeDelivered == null)
					{
						Order.TimeDelivered = DateTime.Now;
					}
					RestoreOrder();
					break;
				case RouteListItemStatus.EnRoute:
					Order.ChangeStatus(OrderStatus.OnTheWay);
					RestoreOrder();
					break;
				case RouteListItemStatus.Overdue:
					Order.ChangeStatus(OrderStatus.NotDelivered);
					FillCountsOnCanceled();
					break;
			}
			uow.Save(Order);
		}

		public virtual void RecalculateTotalCash() => TotalCash = CalculateTotalCash();

		public virtual decimal CalculateTotalCash() => IsDelivered() ? AddressCashSum : 0;

		public virtual bool RouteListIsUnloaded() =>
			new[] { RouteListStatus.OnClosing, RouteListStatus.Closed }.Contains(RouteList.Status);

		public virtual bool IsDelivered()
		{
			return Status == RouteListItemStatus.Completed || Status == RouteListItemStatus.EnRoute && RouteListIsUnloaded();
		}

		public virtual bool IsValidForWageCalculation()
		{
			return !GetNotDeliveredStatuses().Contains(Status);
		}

		public virtual int GetFullBottlesDeliveredCount()
		{
			return (int)Order.OrderItems.Where(item => item.Nomenclature.Category == NomenclatureCategory.water && item.Nomenclature.TareVolume == TareVolume.Vol19L)
								   .Sum(item => item.ActualCount ?? 0);
		}

		public virtual int GetFullBottlesToDeliverCount()
		{
			return (int)Order.OrderItems.Where(item => item.Nomenclature.Category == NomenclatureCategory.water && item.Nomenclature.TareVolume == TareVolume.Vol19L)
								   .Sum(item => item.Count);
		}

		/// <summary>
		/// Функция вызывается при переходе адреса в закрытие.
		/// Если адрес в пути, при закрытии МЛ он считается автоматически доставленным.
		/// </summary>
		/// <param name="uow">Uow.</param>
		public virtual void FirstFillClosing(IUnitOfWork uow, WageParameterService wageParameterService)
		{
			//В этом месте изменяем статус для подстраховки.
			if(Status == RouteListItemStatus.EnRoute)
			{
				Status = RouteListItemStatus.Completed;
			}

			foreach(var item in Order.OrderItems)
			{
				item.ActualCount = IsDelivered() ? item.Count : 0;
			}

			foreach(var equip in Order.OrderEquipments)
			{
				equip.ActualCount = IsDelivered() ? equip.Count : 0;
			}

			foreach(var deposit in Order.OrderDepositItems)
			{
				deposit.ActualCount = IsDelivered() ? deposit.Count : 0;
			}

			Order.BottlesByStockActualCount = Order.BottlesByStockCount;

			PerformanceHelper.AddTimePoint(_logger, "Обработали номенклатуры");
			BottlesReturned = IsDelivered() ? (DriverBottlesReturned ?? Order.BottlesReturn ?? 0) : 0;
			RecalculateTotalCash();
			RouteList.RecalculateWagesForRouteListItem(this, wageParameterService);
		}

		/// <summary>
		/// Обнуляет фактическое количетво
		/// Использовать если заказ отменен или полностью не доставлен
		/// </summary>
		public virtual void FillCountsOnCanceled()
		{
			foreach(var item in Order.OrderItems)
			{
				if(!item.OriginalDiscountMoney.HasValue || !item.OriginalDiscount.HasValue)
				{
					item.OriginalDiscountMoney = item.DiscountMoney > 0 ? (decimal?)item.DiscountMoney : null;
					item.OriginalDiscount = item.Discount > 0 ? (decimal?)item.Discount : null;
					item.OriginalDiscountReason = (item.DiscountMoney > 0 || item.Discount > 0) ? item.DiscountReason : null;
				}
				item.ActualCount = 0m;
				BottlesReturned = 0;
			}
			foreach(var equip in Order.OrderEquipments)
			{
				equip.ActualCount = 0;
			}

			foreach(var deposit in Order.OrderDepositItems)
			{
				deposit.ActualCount = 0;
			}
		}

		public virtual void RestoreOrder()
		{
			foreach(var item in Order.OrderItems)
			{
				if(item.OriginalDiscountMoney.HasValue || item.OriginalDiscount.HasValue)
				{
					item.DiscountMoney = item.OriginalDiscountMoney ?? 0;
					item.DiscountReason = item.OriginalDiscountReason;
					item.Discount = item.OriginalDiscount ?? 0;
					item.OriginalDiscountMoney = null;
					item.OriginalDiscountReason = null;
					item.OriginalDiscount = null;
				}
				item.ActualCount = item.Count;
			}
			foreach(var equip in Order.OrderEquipments)
			{
				equip.ActualCount = equip.Count;
			}

			foreach(var deposit in Order.OrderDepositItems)
			{
				deposit.ActualCount = deposit.Count;
			}
		}

		private Dictionary<int, decimal> _goodsByRouteColumns;

		public virtual Dictionary<int, decimal> GoodsByRouteColumns
		{
			get
			{
				if(_goodsByRouteColumns == null)
				{
					_goodsByRouteColumns = Order.OrderItems.Where(i => i.Nomenclature.RouteListColumn != null)
						.GroupBy(i => i.Nomenclature.RouteListColumn.Id, i => i.Count)
						.ToDictionary(g => g.Key, g => g.Sum());
				}
				return _goodsByRouteColumns;
			}
		}

		public virtual decimal GetGoodsAmountForColumn(int columnId) => GoodsByRouteColumns.ContainsKey(columnId) ? GoodsByRouteColumns[columnId] : 0;

		public virtual decimal GetGoodsActualAmountForColumn(int columnId)
		{
			if(Status == RouteListItemStatus.Transfered)
			{
				return 0;
			}

			return Order.OrderItems.Where(i => i.Nomenclature.RouteListColumn != null && i.Nomenclature.RouteListColumn.Id == columnId)
								   .Sum(i => i.ActualCount ?? 0);
		}

		public virtual void RemovedFromRoute() => Order.OrderStatus = OrderStatus.Accepted;

		public virtual void SetStatusWithoutOrderChange(RouteListItemStatus status) => Status = status;

		// Скопировано из RouteListClosingItemsView, отображает передавшего и принявшего адрес.
		public virtual string GetTransferText(RouteListItem item)
		{
			if(item.Status == RouteListItemStatus.Transfered)
			{
				if(item.TransferedTo != null)
				{
					return $"Заказ был перенесен в МЛ №{item.TransferedTo.RouteList.Id} водителя {item.TransferedTo.RouteList.Driver.ShortName}.";
				}
				else
				{
					return "ОШИБКА! Адрес имеет статус перенесенного в другой МЛ, но куда он перенесен не указано.";
				}
			}
			if(item.WasTransfered)
			{
				var transferedFrom = new RouteListItemRepository().GetTransferedFrom(RouteList.UoW, item);
				if(transferedFrom != null)
				{
					return $"Заказ из МЛ №{transferedFrom.RouteList.Id} водителя {transferedFrom.RouteList.Driver.ShortName}.";
				}
				else
				{
					return "ОШИБКА! Адрес помечен как перенесенный из другого МЛ, но строка откуда он был перенесен не найдена.";
				}
			}
			return null;
		}

		public virtual void AddFine(Fine fine)
		{
			if(!ObservableFines.Contains(fine))
			{
				ObservableFines.Add(fine);
			}
		}

		public virtual void RemoveAllFines()
		{
			if(ObservableFines.Any())
			{
				ObservableFines.Clear();
			}
		}

		public virtual void RemoveFine(Fine fine)
		{
			if(ObservableFines.Any() && ObservableFines.Contains(fine))
			{
				ObservableFines.Remove(fine);
			}
		}

		public virtual string GetAllFines()
		{
			return ObservableFines.Any() ?
				string.Join("\n", ObservableFines.SelectMany(x => x.ObservableItems)
					.Select(x => x.Title))
				: string.Empty;
		}

		#endregion

		#region Для расчетов в логистике

		/// <summary>
		/// Время разгрузки на адресе в секундах.
		/// </summary>
		public virtual int TimeOnPoint => Order.CalculateTimeOnPoint(RouteList.Forwarder != null);

		public virtual TimeSpan CalculatePlanedTime(RouteGeometryCalculator sputnikCache)
		{
			DateTime time = default(DateTime);

			for(int ix = 0; ix < RouteList.Addresses.Count; ix++)
			{
				var address = RouteList.Addresses[ix];
				if(ix == 0)
				{
					time = time.Add(RouteList.Addresses[ix].Order.DeliverySchedule.From);
				}
				else
				{
					time = time.AddSeconds(sputnikCache.TimeSec(RouteList.Addresses[ix - 1].Order.DeliveryPoint, RouteList.Addresses[ix].Order.DeliveryPoint));
				}

				if(address == this)
				{
					break;
				}

				time = time.AddSeconds(RouteList.Addresses[ix].TimeOnPoint);
			}
			sputnikCache?.Dispose();
			return time.TimeOfDay;
		}

		public virtual TimeSpan? CalculateTimeLateArrival()
		{
			if(StatusLastUpdate.HasValue)
			{
				var late = StatusLastUpdate.Value.TimeOfDay - Order.DeliverySchedule.To;

				if(late.TotalSeconds > 0)
				{
					return late;
				}
			}

			return null;
		}

		#endregion

		#region Зарплата

		public virtual IRouteListItemWageCalculationSource DriverWageCalculationSrc => new RouteListItemWageCalculationSource(this, EmployeeCategory.driver);

		public virtual IRouteListItemWageCalculationSource ForwarderWageCalculationSrc => new RouteListItemWageCalculationSource(this, EmployeeCategory.forwarder);

		public virtual void SaveWageCalculationMethodics()
		{
			DriverWageCalculationMethodic = DriverWageCalcMethodicTemporaryStore;
			ForwarderWageCalculationMethodic = ForwarderWageCalcMethodicTemporaryStore;
		}

		#endregion Зарплата

		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if(CommentForFine?.Length > 1000)
			{
				yield return new ValidationResult($"В адресе: '{Title}' превышена максимально допустимая длина комментария по штрафу ({CommentForFine.Length}/1000)");
			}

			if(CashierComment?.Length > 255)
			{
				yield return new ValidationResult(
					$"В адресе: '{Title}' превышена максимально допустимая длина комментария кассира ({CashierComment.Length}/255)");
			}
		}

		public static RouteListItemStatus[] GetUndeliveryStatuses()
		{
			return new RouteListItemStatus[]
				{
					RouteListItemStatus.Canceled,
					RouteListItemStatus.Overdue
				};
		}

		/// <summary>
		/// Возвращает все возможные конечные статусы <see cref="RouteListItem"/>, при которых <see cref="RouteListItem"/> не был довезён
		/// </summary>
		/// <returns></returns>
		public static RouteListItemStatus[] GetNotDeliveredStatuses()
		{
			return new RouteListItemStatus[]
			{
				RouteListItemStatus.Canceled,
				RouteListItemStatus.Overdue,
				RouteListItemStatus.Transfered
			};
		}
	}

	public enum RouteListItemStatus
	{
		[Display(Name = "В пути")]
		EnRoute,
		[Display(Name = "Выполнен")]
		Completed,
		[Display(Name = "Доставка отменена")]
		Canceled,
		[Display(Name = "Недовоз")]
		Overdue,
		[Display(Name = "Передан")]
		Transfered
	}

	public class RouteListItemStatusStringType : NHibernate.Type.EnumStringType
	{
		public RouteListItemStatusStringType() : base(typeof(RouteListItemStatus)) { }
	}
}
