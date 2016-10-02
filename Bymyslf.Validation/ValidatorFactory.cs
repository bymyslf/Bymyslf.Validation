namespace Bymyslf.Validation
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Reflection;

    public class ValidatorFactory : IValidatorFactory
    {
        private readonly Dictionary<Type, Func<object>> validators = new Dictionary<Type, Func<object>>();

        public ValidatorFactory()
        {
            this.Register();
        }

        private static IValidatorFactory current;

        public static IValidatorFactory Current
        {
            get { return current ?? (current = new ValidatorFactory()); }
        }

        public IValidator<T> GetValidator<T>()
        {
            var type = typeof(T);
            if (validators.ContainsKey(type))
            {
                return this.validators[type]() as IValidator<T>;
            }

            return null;
        }

        public void Register()
        {
            var types = AppDomain.CurrentDomain.GetAssemblies()
                                   .Where(a => !a.IsDynamic)
                                   .SelectMany(a => a.GetTypes())
                                   .Where(t => !t.IsInterface && !t.IsAbstract);

            foreach (var type in types)
            {
                foreach (var interfaceType in type.GetInterfaces())
                {
                    if (!interfaceType.IsGenericType)
                    {
                        continue;
                    }

                    if (interfaceType.GetGenericTypeDefinition() == typeof(IValidator<>))
                    {
                        var entityType = interfaceType.GetGenericArguments()[0];
                        if (!this.validators.ContainsKey(entityType))
                        {
                            this.validators.Add(entityType, () => Activator.CreateInstance(type));
                        }
                    }
                }
            }
        }
    }
}