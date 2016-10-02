namespace Bymyslf.Validation.Rules
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using Bymyslf.Validation.Utils;

    public class CollectionValidationRule<TEntity, TCollectionElement> : AbstractValidationRule<TEntity>
        where TCollectionElement : IValidatable
    {
        private readonly Func<TEntity, IEnumerable<TCollectionElement>> collectionFunc;

        public CollectionValidationRule(Func<TEntity, IEnumerable<TCollectionElement>> collectionAccessor)
            : base(ValidationMessages.CollectionError)
        {
            Guard.Against<ArgumentNullException>(collectionAccessor.IsNull(), "collectionProperty cannot be null");
            this.collectionFunc = collectionAccessor;
        }

        public override bool Valid(TEntity entity)
        {
            var collection = this.collectionFunc(entity);

            return collection.IsNull()
                ? false
                : (collection.Any()
                    && !(collection.Select(line => this.ValidateLine(line))
                                        .SelectMany(errors => errors).Any()));
        }

        private IEnumerable<ValidationError> ValidateLine(TCollectionElement line)
        {
            var errors = line.Validate().Errors;
            this.ErrorMessage = String.Concat(this.ErrorMessage, String.Join(Environment.NewLine, errors.Select(err => err.Message)));
            return errors;
        }
    }
}