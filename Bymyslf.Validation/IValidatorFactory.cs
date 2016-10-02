namespace Bymyslf.Validation
{
    public interface IValidatorFactory
    {
        /// <summary>
        /// Gets the validator for the specified entity type.
        /// </summary>
        IValidator<T> GetValidator<T>();
    }
}