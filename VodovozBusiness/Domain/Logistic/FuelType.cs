﻿using System;
using QSOrmProject;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace Vodovoz.Domain.Logistic
{

	[OrmSubject (Gender = QSProjectsLib.GrammaticalGender.Masculine,
		NominativePlural = "виды топлива",
		Nominative = "вид топлива")]
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

		double cost;

		[Display (Name = "Название")]
		[Required (ErrorMessage = "Цена должна быть заполнена.")]
		public virtual double Cost {
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
	}
}

