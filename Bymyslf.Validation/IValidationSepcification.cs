namespace Bymyslf.Validation
{
    public interface IValidationSpecification<TEntity>
    {
        string ErrorMessage { get; }

        bool IsSatisfiedBy(TEntity entity);
    }
}