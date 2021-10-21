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

		private DiscountPermissionType _discountPermissionType;
		public virtual DiscountPermissionType DiscountPermissionType
		{
			get => _discountPermissionType;
			set => SetField(ref _discountPermissionType, value);
		}

		private Subdivision _subdivisionPermission;
		[Display(Name = "Право на отдел")]
		public virtual Subdivision SubdivisionPermission
		{
			get => _subdivisionPermission;
			set => SetField(ref _subdivisionPermission, value);
		}

		private Employee _employeePermission;
		[Display(Name = "Право на сотрудника")]
		public virtual Employee EmployeePermission
		{
			get => _employeePermission;
			set => SetField(ref _employeePermission, value);
		}

		private EmployeePost _postPermission;
		[Display(Name = "Право на должность")]
		public virtual EmployeePost PostPermission
		{
			get => _postPermission;
			set => SetField(ref _postPermission, value);
		}

		private int? _skillLevel;
		[Display(Name = "Уровень квалификации")]
		public virtual int? SkillLevel
		{
			get => _skillLevel;
			set => SetField(ref _skillLevel, value);
		}

		private string _discountMinValue;
		[Display(Name = "Минимальный размер скидки")]
		public virtual string DiscountMinValue
		{
			get => _discountMinValue;
			set => SetField(ref _discountMinValue, value);
		}

		private string _discountMaxValue;
		[Display(Name = "Максимальный размер скидки")]
		public virtual string DiscountMaxValue
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

		private IList<Discount> _discounts;
		[Display(Name = "Скидки на ТМЦ")]
		public virtual IList<Discount> Discounts
		{
			get => _discounts;
			set => SetField(ref _discounts, value);
		}

		private DiscountUsage _discountUsage;
		[Display(Name = "Время - Условие применения скидки")]
		public virtual DiscountUsage DiscountUsage
		{
			get => _discountUsage;
			set => SetField(ref _discountUsage, value);
		}

		private DiscountCounterpartyType _discountCounterpartyType;
		[Display(Name = "Тип контрагента")]
		public virtual DiscountCounterpartyType DiscountCounterpartyType
		{
			get => _discountCounterpartyType;
			set => SetField(ref _discountCounterpartyType, value);
		}

		private string _counterpartyOrDPName;
		public virtual string CounterpartyOrDPName
		{
			get => _counterpartyOrDPName;
			set => SetField(ref _counterpartyOrDPName, value);
		}

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

		private DiscountWithOtherDiscounts _discountWithOtherDiscounts;
		[Display(Name = "Другиме скидки")]
		public virtual DiscountWithOtherDiscounts DiscountWithOtherDiscounts
		{
			get => _discountWithOtherDiscounts;
			set => SetField(ref _discountWithOtherDiscounts, value);
		}

		public virtual List<int> GetSkillLevels() => new List<int> { 0, 1, 2, 3, 4, 5 };

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

	//Пункт Е - ТМЦ
	public class Discount : PropertyChangedBase
	{
		public virtual int Id { get; set; }

		private DiscountRowType _type;
		public virtual DiscountRowType Type
		{
			get => _type;
			set => SetField(ref _type, value);
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

		private DiscountValueType _discountValueType;
		[Display(Name = "Тип скидки - рубли или проценты")]
		public virtual DiscountValueType DiscountValueType
		{
			get => _discountValueType;
			set => SetField(ref _discountValueType, value);
		}

		private int _discountMinQuantity;
		[Display(Name = "Минимальный размер скидки")]
		public virtual int DiscountMinQuantity
		{
			get => _discountMinQuantity;
			set => SetField(ref _discountMinQuantity, value);
		}

		private int _discountMaxQuantity;
		[Display(Name = "Максимальный размер скидки")]
		public virtual int DiscountMaxQuantity
		{
			get => _discountMaxQuantity;
			set => SetField(ref _discountMaxQuantity, value);
		}

		private int _andBlockNum;
		[Display(Name = "Номер блока И")]
		public virtual int AndBlockNum
		{
			get => _andBlockNum;
			set => SetField(ref _andBlockNum, value);
		}

		private DiscountReason _discountReason;
		public virtual DiscountReason DiscountReason
		{
			get => _discountReason;
			set => SetField(ref _discountReason, value);
		}

		public virtual Discount Init()
		{
			Type = DiscountRowType.Nomenclature;
			DiscountMinValue = 0;
			DiscountMaxValue = 0;
			DiscountValueType = DiscountValueType.Percents;
			return this;
		}
	}

	//Тип объекта скидки
	public enum DiscountTargetType
	{
		[Display(Name = "На товары")]
		Goods,
		[Display(Name = "На заказы")]
		Orders
	}

	//Тип права 
	public enum DiscountPermissionType
	{
		[Display(Name = "Отдел")]
		Subdivision,
		[Display(Name = "Сотрудник")]
		Employee,
		[Display(Name = "Должность")]
		Post,
		[Display(Name = "Должность + квалификация")]
		PostWithSkillLevel

	}

	//Значение скидки (в процентах или в рублях)
	public enum DiscountValueType
	{
		[Display(Name = "Рубли")]
		Roubles,
		[Display(Name = "Проценты")]
		Percents
	}

	//Тип конкретного объекта применения скидки
	public enum DiscountRowType
	{
		[Display(Name = "На товар")]
		Nomenclature,
		[Display(Name = "На группу товаров")]
		NomenclatureGroup,
		[Display(Name = "На тип товаров")]
		NomenclatureType
	}

	//Условие применения скидки
	public enum DiscountUsage
	{
		[Display(Name = "Не указано")]
		None,
		[Display(Name = "Один раз по контрагенту")]
		OnceForCounterparty,
		[Display(Name = "Один раз по точке доставки")]
		OnceForDeliveryPoint,
		[Display(Name = "N заказ за день")]
		OrderNumberForDay
	}

	//Тип контрагента
	public enum DiscountCounterpartyType
	{
		[Display(Name = "Не указано")]
		None,
		[Display(Name = "Физическое лицо")]
		PhysicalCounterparty,
		[Display(Name = "Юридическое лицо")]
		LegalCounterparty,
		[Display(Name = "Конкретный контрагент")]
		SpecificCounterparty,
		[Display(Name = "Конкретная точка доставки")]
		SpecificDeliveryPoint
	}

	//С другими скидками
	public enum DiscountWithOtherDiscounts
	{
		[Display(Name = "Нет")]
		Never,
		[Display(Name = "Со всеми")]
		Anything,
		[Display(Name = "С некоторыми")]
		OnlySome
	}

	public class DiscoutTargetTypeStringType : NHibernate.Type.EnumStringType
	{
		public DiscoutTargetTypeStringType() : base(typeof(DiscountTargetType))
		{
		}
	}

	public class DiscountPermissionTypeStringType : NHibernate.Type.EnumStringType
	{
		public DiscountPermissionTypeStringType() : base(typeof(DiscountPermissionType))
		{
		}
	}

	public class DiscountRowTypeStringType : NHibernate.Type.EnumStringType
	{
		public DiscountRowTypeStringType() : base(typeof(DiscountRowType))
		{
		}
	}

	public class DiscountUsageStringType : NHibernate.Type.EnumStringType
	{
		public DiscountUsageStringType() : base(typeof(DiscountUsage))
		{
		}
	}

	public class DiscountCounterpartyTypeStringType : NHibernate.Type.EnumStringType
	{
		public DiscountCounterpartyTypeStringType() : base(typeof(DiscountCounterpartyType))
		{
		}
	}

	public class DiscountWithOtherDiscountsStringType : NHibernate.Type.EnumStringType
	{
		public DiscountWithOtherDiscountsStringType() : base(typeof(DiscountWithOtherDiscounts))
		{
		}
	}

	public class DiscountValueTypeStringType : NHibernate.Type.EnumStringType
	{
		public DiscountValueTypeStringType() : base(typeof(DiscountValueType))
		{
		}
	}

}
