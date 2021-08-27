﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using NetTopologySuite.Geometries;
using Newtonsoft.Json;
using QS.DomainModel.Entity;
using QS.HistoryLog;
using Vodovoz.Domain.Employees;
using Vodovoz.Domain.Sale;
using Vodovoz.Domain.WageCalculation;

namespace Vodovoz.Domain.Sectors
{
	[HistoryTrace]
	public class SectorVersion : PropertyChangedBase, IDomainObject, ICloneable, IValidatableObject
	{
		public SectorVersion()
		{
			Status = SectorsSetStatus.Draft;
		}
		public virtual int Id { get; set; }

		private DateTime? _startDate;
		[Display(Name = "Время активации")]
		public virtual DateTime? StartDate
		{
			get => _startDate;
			set => SetField(ref _startDate, value);
		}

		private DateTime? _endDate;
		[Display(Name = "Время закрытия")]
		public virtual DateTime? EndDate
		{
			get => _endDate;
			set => SetField(ref _endDate, value);
		}

		private Sector _sector;
		public virtual Sector Sector
		{
			get => _sector;
			set => SetField(ref _sector, value);
		}
		
		private string _sectorName;
		[Display(Name = "Название района")]
		public virtual string SectorName {
			get => _sectorName;
			set => SetField(ref _sectorName, value);
		}
		
		private TariffZone tariffZone;
		[Display(Name = "Тарифная зона")]
		public virtual TariffZone TariffZone {
			get => tariffZone;
			set => SetField(ref tariffZone, value);
		}

		private Geometry _polygon;
		[Display(Name = "Граница")]
		public virtual Geometry Polygon {
			get => _polygon;
			set => SetField(ref _polygon, value);
		}
		
		private WageSector _wageSector;
		[Display(Name = "Группа района для расчёта ЗП")]
		public virtual WageSector WageSector {
			get => _wageSector;
			set => SetField(ref _wageSector, value);
		}
		
		private GeographicGroup _geographicGroup;
		[Display(Name = "Часть города")]
		public virtual GeographicGroup GeographicGroup {
			get => _geographicGroup;
			set => SetField(ref _geographicGroup, value);
		}

		private SectorsSetStatus _status;
		public virtual SectorsSetStatus Status
		{
			get => _status;
			set => SetField(ref _status, value);
		}
		
		private int _minBottles;
		[Display(Name = "Минимальное количество бутылей")]
		public virtual int MinBottles {
			get => _minBottles;
			set => SetField(ref _minBottles, value);
		}

		private decimal _waterPrice;
		[Display(Name = "Цена на воду")]
		public virtual decimal WaterPrice {
			get => _waterPrice;
			set => SetField(ref _waterPrice, value);
		}

		private SectorWaterPrice _priceType;
		[Display(Name = "Вид цены")]
		public virtual SectorWaterPrice PriceType {
			get => _priceType;
			set {
				SetField(ref _priceType, value);
				if(WaterPrice != 0 && PriceType != SectorWaterPrice.FixForDistrict)
					WaterPrice = 0;
			}
		}

		public virtual object Clone()
		{
			var polygonClone = Polygon.Copy();

			return new SectorVersion
			{
				Status = SectorsSetStatus.Draft,
				SectorName = SectorName,
				WageSector = WageSector,
				GeographicGroup = GeographicGroup,
				Polygon = polygonClone,
				TariffZone = TariffZone,
				Sector = Sector,
				MinBottles = MinBottles,
				WaterPrice = WaterPrice,
				PriceType = PriceType
			};
		}

		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if(string.IsNullOrWhiteSpace(SectorName))
			{
				yield return new ValidationResult(
					$"Необходимо заполнить имя района",
					new[] {nameof(SectorName)});
			}
			if(GeographicGroup == null)
			{
				yield return new ValidationResult(
					$"Для района \"{SectorName}\" необходимо указать часть города, содержащую этот район доставки",
					new[] {nameof(GeographicGroup)});
			}
			if(Polygon == null)
			{
				yield return new ValidationResult(
					$"Для района \"{SectorName}\" необходимо нарисовать границы на карте", new[] {nameof(Polygon)});
			}
			if(WageSector == null)
			{
				yield return new ValidationResult(
					$"Для района \"{SectorName}\" необходимо выбрать зарплатную группу", new[] {nameof(WageSector)});
			}
			if(StartDate.HasValue == false)
			{
				yield return new ValidationResult($"Необходимо поставить дату активации", new[] {nameof(StartDate)});
			}
		}
	}
}