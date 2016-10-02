namespace Bymyslf.Validation.Rules
{
    using System;
    using Bymyslf.Validation.Utils;

    public class ChildValidationRule<TEntity, TProperty> : AbstractValidationRule<TEntity>
        where TProperty : IValidatable
    {
        private readonly Func<TEntity, TProperty> propertyFunc;

        public ChildValidationRule(Func<TEntity, TProperty> propertyAccessor)
            : base(ValidationMessages.ChildError)
        {
            Guard.Against<ArgumentNullException>(propertyAccessor.IsNull(), "propertyAccessor cannot be null");
            this.propertyFunc = propertyAccessor;
        }

        public override bool Valid(TEntity entity)
        {
            var prop = this.propertyFunc(entity);
            return prop.IsNull() ? false : prop.IsValid();
        }
    }
}