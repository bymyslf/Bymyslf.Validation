namespace Bymyslf.Validation
{
    public interface IConditionalValidationRule<TEntity> : IValidationRule<TEntity>
    {
        bool CheckCondition(TEntity entity);

        IValidationRule<TEntity> InnerRule { get; }
    }
}