using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VodovozInfrastructure.Validators {
    public abstract class EntityValidator : IValidatableObject
    {
        public abstract IEnumerable<ValidationResult> Validate();
        public abstract IEnumerable<ValidationResult> Validate(object validateParameters);
        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
    
    public abstract class EntityValidator<TValidateParameters> : EntityValidator
    {
        public sealed override IEnumerable<ValidationResult> Validate(object validateParameters) {
            if(!(validateParameters is TValidateParameters))
                throw new ArgumentException("Неверный параметр.");

            return Validate((TValidateParameters)validateParameters);
        }
        public abstract IEnumerable<ValidationResult> Validate(TValidateParameters validateParameters);
    }
}