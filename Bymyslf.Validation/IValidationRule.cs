namespace Bymyslf.Validation
{
    public interface IValidationRule
    {
        string ErrorMessage { get; }

        bool Valid(object entity);
    }

    public interface IValidationRule<in TEntity> : IValidationRule
    {
        bool Valid(TEntity entity);
    }
}