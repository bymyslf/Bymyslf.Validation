namespace Bymyslf.Validation.Rules
{
    using System;
    using Bymyslf.Validation.Utils;

    public class ConditionalValidationRule<TEntity> : AbstractValidationRule<TEntity>, IConditionalValidationRule<TEntity>
    {
        private readonly Predicate<TEntity> condition;

        public ConditionalValidationRule(Predicate<TEntity> condition, IValidationRule<TEntity> innerRule)
            : base(innerRule.ErrorMessage)
        {
            Guard.Against<ArgumentNullException>(condition.IsNull(), "condition cannot be null");
            Guard.Against<ArgumentNullException>(innerRule.IsNull(), "innerRule cannot be null");

            this.condition = condition;
            this.InnerRule = innerRule;
        }

        public IValidationRule<TEntity> InnerRule { get; private set; }

        public override bool Valid(TEntity entity)
        {
            if (this.condition(entity))
            {
                var isValid = this.InnerRule.Valid(entity);
                if (!isValid)
                {
                    this.ErrorMessage = this.InnerRule.ErrorMessage;
                }

                return isValid;
            }

            return true;
        }

        public bool CheckCondition(TEntity entity)
        {
            return this.condition(entity);
        }
    }
}