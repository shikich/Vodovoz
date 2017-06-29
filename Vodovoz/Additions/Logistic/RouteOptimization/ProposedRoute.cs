﻿﻿using System.Collections.Generic;
using System.Linq;
using Vodovoz.Domain.Logistic;
using Vodovoz.Domain.Orders;

namespace Vodovoz.Additions.Logistic.RouteOptimization
{
	public class ProposedRoute
	{
		public List<Order> Orders = new List<Order>();
		public AtWorkDriver Driver;

		public List<FreeOrders> PossibleOrders;

		public Car Car {
			get {
				return Driver.Car;
			}
		}

		int? bottlesCache;

		public int CurrentBottles{
			get{
				if(bottlesCache == null)
					bottlesCache = Orders.SelectMany(x => x.OrderItems)
							 .Where(x => x.Nomenclature.Category == Domain.Goods.NomenclatureCategory.water)
							 .Sum(x => x.Count);
				return bottlesCache.Value;
			}
		}

		double? weightCache;

		public double CurrentWeight {
			get {
				if(weightCache == null)
					weightCache = Orders.SelectMany(x => x.OrderItems)
							 .Sum(x => x.Nomenclature.Weight * x.Count);
				return weightCache.Value;
			}
		}

		double? volumeCache;

		public double CurrentVolume {
			get {
				if(volumeCache == null)
					volumeCache = Orders.SelectMany(x => x.OrderItems)
							 .Sum(x => x.Nomenclature.Volume * x.Count);
				return volumeCache.Value;
			}
		}

		public ProposedRoute(AtWorkDriver driver)
		{
			Driver = driver;
		}

		public bool CanAdd(Order order)
		{
			if(Orders.Count >= Car.MaxRouteAddresses)
				return false;

			var bottles = CurrentBottles + order.OrderItems.Where(x => x.Nomenclature.Category == Domain.Goods.NomenclatureCategory.water)
							 .Sum(x => x.Count);
			if(bottles > Car.MaxBottles)
				return false;

			var weight = CurrentWeight + order.OrderItems.Sum(x => x.Nomenclature.Weight * x.Count);
			if(weight > Car.MaxWeight)
				return false;

			var volume = CurrentVolume + order.OrderItems.Sum(x => x.Nomenclature.Volume * x.Count);
			if(volume > Car.MaxVolume)
				return false;

			return true;
		}

		/// <summary>
		/// Возвращает стоимость добавления адреса.
		/// </summary>
		public double AddOrder(Order order)
		{
			var cost = Orders.Count == 0
				? DistanceCalculator.GetDistanceFromBase(order.DeliveryPoint)
			                 : DistanceCalculator.GetDistance(Orders.Last().DeliveryPoint, order.DeliveryPoint);
			Orders.Add(order);
			RemoveFromPossible(order);
			CleanCache();

			return cost;
		}

		public void RemoveFromPossible(Order order)
		{
			PossibleOrders.ForEach(x => x.Orders.Remove(order));
		}

		public ProposedRoute Clone()
		{
			var propose = new ProposedRoute(Driver);
			propose.Orders = Orders.ToList();
			propose.PossibleOrders = PossibleOrders.Select(x => x.Clone()).ToList();

			return propose;
		}

		private void CleanCache()
		{
			bottlesCache = null;
			weightCache = null;
			volumeCache = null;
		}
	}
}
