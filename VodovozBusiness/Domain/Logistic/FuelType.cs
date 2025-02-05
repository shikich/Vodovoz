﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using QS.DomainModel.Entity;
using QS.DomainModel.Entity.EntityPermissions;
using QS.HistoryLog;

namespace Vodovoz.Domain.Logistic
{

	[Appellative (Gender = GrammaticalGender.Masculine,
		NominativePlural = "виды топлива",
		Nominative = "вид топлива")]
	[EntityPermission]
	[HistoryTrace]
	public class FuelType : PropertyChangedBase, IDomainObject, IValidatableObject
	{
		public virtual int Id { get; set; }

		string name;

		[Display (Name = "Название")]
		[Required (ErrorMessage = "Название должно быть заполнено.")]
		[StringLength(20)]
		public virtual string Name {
			get { return name; }
			set { SetField (ref name, value, () => Name); }
		}

		decimal cost;

		[Display (Name = "Цена")]
		[Required (ErrorMessage = "Цена должна быть заполнена.")]
		public virtual decimal Cost {
			get { return cost; }
			set { SetField (ref cost, value, () => Cost); }
		}
			

		public FuelType ()
		{
			Name = String.Empty;
		}

		public virtual IEnumerable<ValidationResult> Validate (ValidationContext validationContext)
		{
			if (Cost < 0)
				yield return new ValidationResult("Стоимость не может быть отрицательной",
					new[] {Gamma.Utilities.PropertyUtil.GetPropertyName(this, o=>o.Cost)});
		}

		public override bool Equals(object obj)
		{
			var type = obj as FuelType;
			return type != null &&
				   Id == type.Id;
		}

		public override int GetHashCode()
		{
			return 2108858624 + Id.GetHashCode();
		}
	}
}

