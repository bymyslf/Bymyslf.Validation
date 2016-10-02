namespace Bymyslf.Validation.Rules
{
    using System;
    using Bymyslf.Validation.Utils;

    public class PredicateValidationRule<TEntity> : AbstractValidationRule<TEntity>
    {
        private readonly Predicate<TEntity> predicate;

        public PredicateValidationRule(Predicate<TEntity> predicate)
            : this(predicate, ValidationMessages.PredicateError)
        {
        }

        public PredicateValidationRule(Predicate<TEntity> predicate, string errorMessage)
            : base(errorMessage)
        {
            Guard.Against<ArgumentNullException>(predicate.IsNull(), "predicate cannot be null");

            this.predicate = predicate;
            this.ErrorMessage = errorMessage;
        }

        public override bool Valid(TEntity entity)
        {
            return this.predicate(entity);
        }
    }
}