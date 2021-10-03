using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;
using Vodovoz.Domain.Employees;

namespace Vodovoz.Domain.Orders
{
	[Appellative(Gender = GrammaticalGender.Neuter,
		NominativePlural = "основания для скидок",
		Nominative = "основание для скидки")]
	[EntityPermission]
	public class DiscountReason : PropertyChangedBase, IDomainObject, IValidatableObject
	{
		public virtual int Id { get; set; }

		private string _name;
		[Display(Name = "Название скидки")]
		public virtual string Name 
		{
			get => _name;
			set => SetField(ref _name, value);
		}

		private bool _isArchive;
		[Display(Name = "В архиве")]
		public virtual bool IsArchive 
		{
			get => _isArchive;
			set => SetField(ref _isArchive, value);
		}

		private DiscountTargetType _discountTargetType;
		[Display(Name = "Тип объекта применения скидки")]
		public virtual DiscountTargetType DiscountTargetType
		{
			get => _discountTargetType;
			set => SetField(ref _discountTargetType, value);
		}

		//FEDOS Отдел - права по идее
		private Employee _employee;
		public virtual Employee Employee
		{
			get => _employee;
			set => SetField(ref _employee, value);
		}

		private Discount _discountRow;
		public virtual Discount DiscountRow
		{
			get => _discountRow;
			set => SetField(ref _discountRow, value);
		}

		private IList<DiscountForObjectRow> _discountForObjectRows;
		[Display(Name = "Скидки на ТМЦ")]
		public virtual IList<DiscountForObjectRow> DiscountForObjectRows
		{
			get => _discountForObjectRows;
			set => SetField(ref _discountForObjectRows, value);
		}

		private DiscountUsage _discountUsage;
		[Display(Name = "Время - Условие применения скидки")]
		public virtual DiscountUsage DiscountUsage
		{
			get => _discountUsage;
			set => SetField(ref _discountUsage, value);
		}

		//Контрагенты
		private DiscountCounterpartyType _discountCounterpartyType;
		public virtual DiscountCounterpartyType DiscountCounterpartyType
		{
			get => _discountCounterpartyType;
			set => SetField(ref _discountCounterpartyType, value);
		}


		//PLACEHOLDER Для текстового поля с контрагентами или точками доставки


		private bool _forSelfdelivery;
		[Display(Name = "Только для самовывоза")]
		public virtual bool ForSelfdelivery
		{
			get => _forSelfdelivery;
			set => SetField(ref _forSelfdelivery, value);
		}

		private bool _forPromotionalSet;
		[Display(Name = "Для промонабора")]
		public virtual bool ForPromotionalSet
		{
			get => _forPromotionalSet;
			set => SetField(ref _forPromotionalSet, value);
		}

		private bool _includeReclamation;
		[Display(Name = "С обязательной рекламацией")]
		public virtual bool IncludeReclamation
		{
			get => _includeReclamation;
			set => SetField(ref _includeReclamation, value);
		}

		private bool _useWithOtherDiscountReasons;
		[Display(Name = "Взаимодействует с другими скидками")]
		public virtual bool UseWithOtherDiscountReasons
		{
			get => _useWithOtherDiscountReasons;
			set => SetField(ref _useWithOtherDiscountReasons, value);
		}


		public virtual string Title => $"Основание для скидки \"{Name}\"";

		public virtual IEnumerable<ValidationResult> Validate(ValidationContext validationContext)
		{
			if(Id == 0 && IsArchive)
			{
				yield return new ValidationResult("Нельзя создать новое архивное основание", new[] { nameof(IsArchive) });
			}
			
			if(string.IsNullOrEmpty(Name))
			{
				yield return new ValidationResult("Название должно быть заполнено", new[] { nameof(Name) });
			}
			
			if(Name?.Length > 45)
			{
				yield return new ValidationResult($"Превышена длина названия ({Name.Length}/45)", new[] { nameof(Name) });
			}
		}
	}

	//FEDOS Предварительные приколы, потом перенести в отдельный файл

	//Пункт Д - Скидка
	public class Discount : PropertyChangedBase
	{
		private int _discountMinValue;
		[Display(Name = "Минимальный размер скидки")]
		public virtual int DiscountMinValue
		{
			get => _discountMinValue;
			set => SetField(ref _discountMinValue, value);
		}

		private int _discountMaxValue;
		[Display(Name = "Максимальный размер скидки")]
		public virtual int DiscountMaxValue
		{
			get => _discountMaxValue;
			set => SetField(ref _discountMaxValue, value);
		}

		private DiscountValueType _discountValueType;
		[Display(Name = "Тип скидки - рубли или проценты")]
		public virtual DiscountValueType DiscountValueType
		{
			get => _discountValueType;
			set => SetField(ref _discountValueType, value);
		}
		

	}

	//Пункт Е - ТМЦ
	public class DiscountForObjectRow : PropertyChangedBase
	{
		private DiscountForObjectType _type;
		public virtual DiscountForObjectType Type
		{
			get => _type;
			set => SetField(ref _type, value);
		}

		private Discount _discountRow;
		public virtual Discount DiscountRow
		{
			get => _discountRow;
			set => SetField(ref _discountRow, value);
		}

		private int _discountMinValue;
		[Display(Name = "Минимальный размер скидки")]
		public virtual int DiscountMinValue
		{
			get => _discountMinValue;
			set => SetField(ref _discountMinValue, value);
		}

		private int _discountMaxValue;
		[Display(Name = "Максимальный размер скидки")]
		public virtual int DiscountMaxValue
		{
			get => _discountMaxValue;
			set => SetField(ref _discountMaxValue, value);
		}
	}


	//Тип объекта скидки
	public enum DiscountTargetType
	{
		//Скидка относится к товару
		Goods,
		//Скидка относится к заказу
		Orders
	}

	//Значение скидки (в процентах или в рублях)
	public enum DiscountValueType
	{
		Roubles,
		Percents
	}

	//Тип конкретного объекта применения скидки
	public enum DiscountForObjectType
	{
		//Скидка на конкретный товар
		Nomenclature,
		//Скидка на группу товаров
		NomenclatureGroup,
		//Скидка на тип товаров
		NomenclatureType
	}

	//Пункт F - условие применения скидки
	public enum DiscountUsage
	{
		[Display(Name = "Один раз по контрагенту")]
		OnceForCounterparty,
		[Display(Name = "Один раз по точке доставки")]
		OnceForDeliveryPoint,
		[Display(Name = "N заказ за день")]
		OrderNumberForDay
	}

	public enum DiscountCounterpartyType
	{
		[Display(Name = "Физическое лицо")]
		PhysicalCounterparty,
		[Display(Name = "Юридическое лицо")]
		LegalCounterparty,
		[Display(Name = "Конкретный контрагент")]
		SpecificCounterparty,
		[Display(Name = "Конкретная точка доставки")]
		SpecificDeliveryPoint
	}

}
