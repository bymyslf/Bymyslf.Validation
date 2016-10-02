namespace Bymyslf.Validation.Rules
{
    using System;
    using Bymyslf.Validation.Utils;

    public abstract class AbstractValidationRule<TEntity> : IValidationRule<TEntity>
    {
        public AbstractValidationRule(string errorMessage)
        {
            Guard.Against<ArgumentNullException>(errorMessage.IsNull(), "errorMessage cannot be null");
            this.ErrorMessage = errorMessage;
        }

        public string ErrorMessage { get; protected set; }

        public abstract bool Valid(TEntity entity);

        bool IValidationRule.Valid(object entity)
        {
            return this.Valid((TEntity)entity);
        }
    }
}