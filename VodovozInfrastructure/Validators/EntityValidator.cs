using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace VodovozInfrastructure.Validators {
    public abstract class EntityValidator<TValidateParameters> : IValidatableObject
    {
        public abstract IEnumerable<ValidationResult> Validate();
        public abstract IEnumerable<ValidationResult> Validate(TValidateParameters validateParameters);
        public abstract IEnumerable<ValidationResult> Validate(ValidationContext validationContext);
    }
}