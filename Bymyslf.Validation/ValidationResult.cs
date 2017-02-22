namespace Bymyslf.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class ValidationResult
    {
        private readonly List<ValidationError> errors;

        public ValidationResult()
        {
            this.errors = new List<ValidationError>();
        }

        private string Message { get; set; }

        public bool IsValid
        {
            get
            {
                return !this.errors.Any();
            }
        }

        public IEnumerable<ValidationError> Errors
        {
            get
            {
                return this.errors;
            }
        }

        public ValidationResult Add(string errorMessage)
        {
            this.errors.Add(new ValidationError(errorMessage));
            return this;
        }

        public ValidationResult Add(ValidationError error)
        {
            this.errors.Add(error);
            return this;
        }

        public ValidationResult Add(params ValidationResult[] validationResults)
        {
            if (validationResults == null)
            {
                return this;
            }

            var results = validationResults.Where(r => r != null);
            foreach (var result in results)
            {
                this.errors.AddRange(result.Errors);
            }

            return this;
        }

        public ValidationResult Remove(ValidationError error)
        {
            if (this.errors.Contains(error))
            {
                this.errors.Remove(error);
            }

            return this;
        }

        public override string ToString()
        {
            return String.Join(", ", this.errors.Select(err => err.Message));
        }
        
        public static implicit operator bool(ValidationResult validationResult)
        {
            return validationResult.IsValid;
        }
    }
}
