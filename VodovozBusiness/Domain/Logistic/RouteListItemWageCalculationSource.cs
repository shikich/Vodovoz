﻿using System;
using System.Collections.Generic;
using System.Linq;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Goods;
using Vodovoz.Domain.WageCalculation;
using Vodovoz.Domain.WageCalculation.CalculationServices.RouteList;

namespace Vodovoz.Domain.Logistic
{
	public class RouteListItemWageCalculationSource : IRouteListItemWageCalculationSource
	{
		readonly RouteListItem item;
		readonly EmployeeCategory employeeCategory;

		public RouteListItemWageCalculationSource(RouteListItem item, EmployeeCategory employeeCategory)
		{
			this.employeeCategory = employeeCategory;
			this.item = item ?? throw new ArgumentNullException(nameof(item));
		}

		#region IRouteListItemWageCalculationSource implementation

		public int FullBottle19LCount => item.Order.OrderItems.Where(item => item.Nomenclature.Category == NomenclatureCategory.water && item.Nomenclature.TareVolume == TareVolume.Vol19L)
															  .Sum(item => item.ActualCount ?? 0);

		public int EmptyBottle19LCount => item.BottlesReturned;

		public int Bottle6LCount => item.Order.OrderItems.Where(item => item.Nomenclature.Category == NomenclatureCategory.water && item.Nomenclature.TareVolume == TareVolume.Vol6L)
														 .Sum(item => item.ActualCount ?? 0);

		public int Bottle600mlCount => item.Order.OrderItems.Where(i => i.Nomenclature.TareVolume == TareVolume.Vol600ml)
													   		.Sum(i => i.ActualCount ?? 0);
		public int Bottle1500mlCount => item.Order.OrderItems.Where(i => i.Nomenclature.TareVolume == TareVolume.Vol1500ml)
															.Sum(i => i.ActualCount ?? 0);

		public bool ContractCancelation => false;

		public IEnumerable<IOrderItemWageCalculationSource> OrderItemsSource => item.Order.OrderItems;

		public IEnumerable<IOrderDepositItemWageCalculationSource> OrderDepositItemsSource => item.Order.OrderDepositItems;

		public bool HasFirstOrderForDeliveryPoint {
			get {

				var sameAddress = item.RouteList.Addresses.Where(a => a.IsDelivered())
											   .Select(i => i.Order)
											   .FirstOrDefault(o => o.DeliveryPoint?.Id == item.Order.DeliveryPoint?.Id);
				if(sameAddress == null) {
					return false;
				}

				return sameAddress.Id == item.Order.Id;
			}
		}

		public WageDistrict WageDistrictOfAddress => item.Order.DeliveryPoint.District?.WageDistrict ?? throw new InvalidOperationException("Точке доставки не присвоен логистический или зарплатный район!");

		public bool WasVisitedByForwarder => item.WithForwarder;

		public bool NeedTakeOrDeliverEquipment {
			get {
				bool result = item.Order.OrderItems.Any(i => i.CurrentCount > 0 && i.Nomenclature.Category == NomenclatureCategory.equipment);
				result |= item.Order.OrderEquipments.Any(i => i.CurrentCount > 0 && i.Nomenclature.Category == NomenclatureCategory.equipment);
				return result;
			}
		}

		#region Для старого расчета оплаты за оборудование

		/// <summary>
		/// Текущая зарплата которая записана в МЛ
		/// </summary>
		public decimal CurrentWage {
			get {
				switch(employeeCategory) {
					case EmployeeCategory.driver:
						return item.DriverWage;
					case EmployeeCategory.forwarder:
						return item.ForwarderWage;
					case EmployeeCategory.office:
					default:
						throw new NotSupportedException();
				}
			}
		}

		#endregion Для старого расчета оплаты за оборудование


		public WageDistrictLevelRate WageCalculationMethodic {
			get {
				switch(employeeCategory) {
					case EmployeeCategory.driver:
						return item.DriverWageCalculationMethodic;
					case EmployeeCategory.forwarder:
						return item.ForwarderWageCalculationMethodic;
					case EmployeeCategory.office:
					default:
						throw new NotSupportedException();
				}
			}
		}

		public decimal DriverWageSurcharge => item.DriverWageSurcharge;

		public bool IsDelivered => item.IsDelivered() && item.Status != RouteListItemStatus.Transfered;

		public (TimeSpan, TimeSpan) DeliverySchedule => (item.Order.DeliverySchedule.From, item.Order.DeliverySchedule.To);

		#endregion IRouteListItemWageCalculationSource implementation
	}
}