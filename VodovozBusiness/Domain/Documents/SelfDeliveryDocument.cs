﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Data.Bindings.Collections.Generic;
using System.Linq;
using Gamma.Utilities;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;
using QS.DomainModel.UoW;
using QS.HistoryLog;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.Orders;
using Vodovoz.Domain.Store;
using Vodovoz.EntityRepositories.Goods;
using Vodovoz.EntityRepositories.Operations;
using Vodovoz.Services;

namespace Vodovoz.Domain.Documents
{
	[Appellative(Gender = GrammaticalGender.Masculine,
		NominativePlural = "отпуски самовывоза",
		Nominative = "отпуск самовывоза")]
	[EntityPermission]
	[HistoryTrace]
	public class SelfDeliveryDocument : Document, IValidatableObject
	{
		public override DateTime TimeStamp {
			get { return base.TimeStamp; }
			set {
				base.TimeStamp = value;
				if(!NHibernate.NHibernateUtil.IsInitialized(Items))
					return;
				foreach(var item in Items) {
					if(item.WarehouseMovementOperation != null && item.WarehouseMovementOperation.OperationTime != TimeStamp)
						item.WarehouseMovementOperation.OperationTime = TimeStamp;
				}
			}
		}

		Order order;
		[Required(ErrorMessage = "Заказ должен быть указан.")]
		public virtual Order Order {
			get { return order; }
			set { SetField(ref order, value, () => Order); }
		}

		Warehouse warehouse;
		[Required(ErrorMessage = "Склад должен быть указан.")]
		public virtual Warehouse Warehouse {
			get { return warehouse; }
			set { SetField(ref warehouse, value, () => Warehouse); }
		}

		string comment;

		[Display(Name = "Комментарий")]
		public virtual string Comment {
			get { return comment; }
			set { SetField(ref comment, value, () => Comment); }
		}

		IList<SelfDeliveryDocumentItem> items = new List<SelfDeliveryDocumentItem>();

		[Display(Name = "Строки")]
		public virtual IList<SelfDeliveryDocumentItem> Items {
			get { return items; }
			set {
				SetField(ref items, value, () => Items);
				observableItems = null;
			}
		}

		GenericObservableList<SelfDeliveryDocumentItem> observableItems;
		//FIXME Кослыль пока не разберемся как научить hibernate работать с обновляемыми списками.
		public virtual GenericObservableList<SelfDeliveryDocumentItem> ObservableItems {
			get {
				if(observableItems == null)
					observableItems = new GenericObservableList<SelfDeliveryDocumentItem>(Items);
				return observableItems;
			}
		}

		IList<SelfDeliveryDocumentReturned> returnedItems = new List<SelfDeliveryDocumentReturned>();

		[Display(Name = "Строки возврата")]
		public virtual IList<SelfDeliveryDocumentReturned> ReturnedItems {
			get { return returnedItems; }
			set {
				SetField(ref returnedItems, value, () => ReturnedItems);
			}
		}

		#region Не сохраняемые

		public virtual string Title => string.Format("Самовывоз №{0} от {1:d}", Id, TimeStamp);

		#endregion

		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			foreach(var item in Items) {
				if(item.Amount > item.AmountInStock)
					yield return new ValidationResult(string.Format("На складе недостаточное количество <{0}>", item.Nomenclature.Name),
						new[] { this.GetPropertyName(o => o.Items) });
				if(item.Amount <= 0) {
					yield return new ValidationResult(string.Format("Введено не положительное количество <{0}>", item.Nomenclature.Name),
						new[] { this.GetPropertyName(o => o.Items) });
				}
			}
		}

		#region Функции

		public virtual void FillByOrder()
		{
			ObservableItems.Clear();
			if(Order == null)
				return;

			foreach(var orderItem in Order.OrderItems) {
				if(!Nomenclature.GetCategoriesForShipment().Contains(orderItem.Nomenclature.Category)) {
					continue;
				}

				if(Warehouse == null || Warehouse.Nomenclatures.Contains(orderItem.Nomenclature)) {
					if(!ObservableItems.Any(i => i.Nomenclature == orderItem.Nomenclature))
						ObservableItems.Add(
							new SelfDeliveryDocumentItem {
								Document = this,
								Nomenclature = orderItem.Nomenclature,
								OrderItem = orderItem,
								OrderEquipment = null,
								Amount = GetNomenclaturesCountInOrder(orderItem.Nomenclature)
							}
						);
				}
			}

			foreach(var orderEquipment in Order.OrderEquipments.Where(x => x.Direction == Direction.Deliver)) {
				if(Warehouse == null || Warehouse.Nomenclatures.Contains(orderEquipment.Nomenclature)) {
					if(!ObservableItems.Any(i => i.Nomenclature == orderEquipment.Nomenclature))
						ObservableItems.Add(
							new SelfDeliveryDocumentItem {
								Document = this,
								Nomenclature = orderEquipment.Nomenclature,
								OrderItem = null,
								OrderEquipment = orderEquipment,
								Amount = GetNomenclaturesCountInOrder(orderEquipment.Nomenclature)
							}
						);
				}
			}
		}

		public virtual decimal GetNomenclaturesCountInOrder(Nomenclature item)
		{
			decimal cnt = Order.OrderItems.Where(i => i.Nomenclature == item).Sum(i => i.Count);
			cnt += Order.OrderEquipments.Where(e => e.Nomenclature == item && e.Direction == Direction.Deliver).Sum(e => e.Count);
			return cnt;
		}

		public virtual void UpdateStockAmount(IUnitOfWork uow)
		{
			if(!Items.Any() || Warehouse == null)
				return;
			var nomenclatureIds = Items.Select(x => x.Nomenclature.Id).ToArray();
			var inStock = Repository.StockRepository.NomenclatureInStock(uow, Warehouse.Id, nomenclatureIds, TimeStamp);

			foreach(var item in Items) {
				item.AmountInStock = inStock[item.Nomenclature.Id];
			}
		}

		public virtual void UpdateAlreadyUnloaded(IUnitOfWork uow)
		{
			if(Items.Count == 0 || Order == null)
				return;

			var inUnloaded = Repository.Store.SelfDeliveryRepository.NomenclatureUnloaded(uow, Order, this);

			foreach(var item in Items) {
				if(inUnloaded.ContainsKey(item.Nomenclature.Id))
					item.AmountUnloaded = inUnloaded[item.Nomenclature.Id];
			}
		}

		public virtual void UpdateOperations(IUnitOfWork uow)
		{
			foreach(var item in Items) {
				if(item.Amount == 0 && item.WarehouseMovementOperation != null) {
					uow.Delete(item.WarehouseMovementOperation);
					item.WarehouseMovementOperation = null;
				}
				if(item.Amount != 0) {
					if(item.WarehouseMovementOperation != null) {
						item.UpdateOperation(Warehouse);
					} else {
						item.CreateOperation(Warehouse, TimeStamp);
					}
				}
			}
		}

		public virtual void UpdateReceptions(IUnitOfWork uow, IList<GoodsReceptionVMNode> bottlesReceptions, IList<GoodsReceptionVMNode> goodsReceptions, INomenclatureRepository nomenclatureRepository, IBottlesRepository bottlesRepository)
		{
			if(nomenclatureRepository == null)
				throw new ArgumentNullException(nameof(nomenclatureRepository));
			if(bottlesRepository == null)
				throw new ArgumentNullException(nameof(bottlesRepository));

			if(Warehouse != null && Warehouse.CanReceiveBottles) {
				int bottlesReturned = 0;
				foreach(GoodsReceptionVMNode item in bottlesReceptions) {
					UpdateReturnedOperation(uow, item.NomenclatureId, item.Amount);
					var defBottle = nomenclatureRepository.GetDefaultBottle(uow);
					if(item.NomenclatureId == defBottle.Id)
						bottlesReturned = item.Amount;
				}
				var emptyBottlesAlreadyReturned = bottlesRepository.GetEmptyBottlesFromClientByOrder(uow, nomenclatureRepository, Order, Id);
				Order.ReturnedTare = emptyBottlesAlreadyReturned + bottlesReturned;
			}

			if(Warehouse != null && Warehouse.CanReceiveEquipment)
				foreach(GoodsReceptionVMNode item in goodsReceptions) {
					UpdateReturnedOperation(uow, item.NomenclatureId, item.Amount);
				}
		}

		public virtual void UpdateReturnedOperations(IUnitOfWork uow, Dictionary<int, decimal> returnedNomenclatures)
		{
			foreach(var returned in returnedNomenclatures) {
				UpdateReturnedOperation(uow, returned.Key, returned.Value);
			}
		}

		void UpdateReturnedOperation(IUnitOfWork uow, int returnedNomenclaureId, decimal returnedNomenclaureQuantity)
		{
			var item = ReturnedItems.FirstOrDefault(x => x.Nomenclature.Id == returnedNomenclaureId);
			if(item == null && returnedNomenclaureQuantity != 0) {
				item = new SelfDeliveryDocumentReturned {
					Amount = returnedNomenclaureQuantity,
					Document = this,
					Nomenclature = uow.GetById<Nomenclature>(returnedNomenclaureId)
				};
				item.CreateOperation(Warehouse, TimeStamp);
				ReturnedItems.Add(item);
			} else if(item != null && returnedNomenclaureQuantity == 0) {
				ReturnedItems.Remove(item);
			} else if(item != null && returnedNomenclaureQuantity != 0) {
				item.Amount = returnedNomenclaureQuantity;
				item.UpdateOperation(Warehouse);
			}
		}

		public virtual bool FullyShiped(IUnitOfWork uow, IStandartNomenclatures standartNomenclatures)
		{
			//Проверка текущего документа
			return Order.TryCloseSelfDeliveryOrder(uow, standartNomenclatures, this);
		}

		#endregion
	}

	public class GoodsReceptionVMNode
	{
		public int NomenclatureId { get; set; }
		public string Name { get; set; }
		public int Amount { get; set; }
		public NomenclatureCategory Category { get; set; }
	}
}