namespace Bymyslf.Validation
{
    public static class ValidationExtensions
    {
        public static bool IsValid<T>(this T entity)
            where T : IValidatable
        {
            return IsValidImpl(entity);
        }

        public static ValidationResult Validate<T>(this T entity)
            where T : IValidatable
        {
            return ValidateImpl(entity);
        }

        private static bool IsValidImpl<T>(T entity)
        {
            var result = ValidateImpl(entity);
            return result == null ? false : result.IsValid;
        }

        private static ValidationResult ValidateImpl<T>(T entity)
        {
            var validation = ValidatorFactory.Current.GetValidator<T>();
            if (validation == null)
            {
                return null;
            }

            return validation.Validate(entity);
        }
    }
}