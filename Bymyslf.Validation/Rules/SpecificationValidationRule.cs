namespace Bymyslf.Validation.Rules
{
    using System;
    using Bymyslf.Validation.Utils;

    public class SpecificationValidationRule<TEntity> : AbstractValidationRule<TEntity>
    {
        private readonly IValidationSpecification<TEntity> specification;

        public SpecificationValidationRule(IValidationSpecification<TEntity> specification)
            : this(specification, ValidationMessages.SpecificationError)
        {
        }

        public SpecificationValidationRule(IValidationSpecification<TEntity> specificationRule, string errorMessage)
            : base(errorMessage)
        {
            Guard.Against<ArgumentNullException>(specificationRule.IsNull(), "specificationRule cannot be null");

            this.specification = specificationRule;
            this.ErrorMessage = errorMessage;
        }

        public override bool Valid(TEntity entity)
        {
            bool isValid = this.specification.IsSatisfiedBy(entity);
            if (!isValid && !this.specification.ErrorMessage.IsNullOrEmpty())
            {
                this.ErrorMessage = this.specification.ErrorMessage;
            }

            return isValid;
        }
    }
}