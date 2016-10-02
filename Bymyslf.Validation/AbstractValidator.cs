namespace Bymyslf.Validation
{
    using System;
    using System.Collections.Generic;

    public abstract class AbstractValidator<TEntity> : IValidator<TEntity>
        where TEntity : class
    {
        private readonly Dictionary<string, IValidationRule<TEntity>> validationsRules;

        public AbstractValidator()
        {
            this.validationsRules = new Dictionary<string, IValidationRule<TEntity>>();
        }

        protected virtual string AddRule(IValidationRule<TEntity> validationRule)
        {
            var ruleName = string.Concat(validationRule.GetType(), Guid.NewGuid().ToString("D"));
            this.validationsRules.Add(ruleName, validationRule);

            return ruleName;
        }

        protected virtual void RemoveRule(string ruleName)
        {
            this.validationsRules.Remove(ruleName);
        }

        public virtual ValidationResult Validate(TEntity entity)
        {
            var result = new ValidationResult();
            foreach (var key in this.validationsRules.Keys)
            {
                var rule = this.validationsRules[key];
                if (!rule.Valid(entity))
                {
                    result.Add(new ValidationError(rule.ErrorMessage));
                }
            }

            return result;
        }
    }
}