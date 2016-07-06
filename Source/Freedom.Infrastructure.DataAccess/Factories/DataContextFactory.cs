using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Data.Entity.ModelConfiguration.Configuration;
using System.Linq;
using System.Reflection;
using Freedom.Infrastructure.DataAccess.Base;

namespace Freedom.Infrastructure.DataAccess.Factories
{
    public class DataContextFactory : Disposable
    {
        private static readonly Type EntityType = typeof(EntityTypeConfiguration<>);

        private static readonly Type ComplexType = typeof(ComplexTypeConfiguration<>);

        private readonly ConcurrentDictionary<string, IDictionary<MethodInfo, object>> _mappingConfigurations = new ConcurrentDictionary<string, IDictionary<MethodInfo, object>>();

        private readonly string _nameOrConnectionString;

        static Type _dataContextType = typeof(FreedomDbContext);

        private FreedomDbContext Context { get; set; }

        public DataContextFactory(string nameOrConnectionString)
        {
            if (String.IsNullOrWhiteSpace(nameOrConnectionString))
                throw new ArgumentNullException("nameOrConnectionString");

            _nameOrConnectionString = nameOrConnectionString;
        }

        public static Type DataContextType
        {
            get
            {
                return _dataContextType;
            }

            set
            {
                if (value == null)
                    throw new ArgumentNullException("value");

                if (!typeof(DbContext).IsAssignableFrom(value))
                    throw new ArgumentException("value");

                _dataContextType = value;
            }
        }

        public virtual FreedomDbContext GetContext()
        {
            return Context ?? (Context = CreateContext());
        }

        protected override void DisposeCore()
        {
            if (Context != null)
            {
                Context.Dispose();
            }
        }

        private static IDictionary<MethodInfo, object> BuildConfigurations()
        {
            var addMethods = typeof(ConfigurationRegistrar).GetMethods()
                .Where(m => m.Name.Equals("Add"))
                .ToList();

            var entityTypeMethod = addMethods.First(m =>
                m.GetParameters()
                    .First()
                    .ParameterType
                    .GetGenericTypeDefinition()
                    .IsAssignableFrom(EntityType));

            var complexTypeMethod = addMethods.First(m =>
                m.GetParameters().First()
                    .ParameterType
                    .GetGenericTypeDefinition()
                    .IsAssignableFrom(ComplexType));

            var configurations = new Dictionary<MethodInfo, object>();

            var types = typeof(DataContextFactory).Assembly
                .GetExportedTypes()
                .Where(IsMappingType)
                .ToList();

            foreach (var type in types)
            {
                MethodInfo typedMethod;
                Type modelType;

                if (IsMatching(
                    type, out modelType, t => EntityType.IsAssignableFrom(t)))
                {
                    typedMethod = entityTypeMethod.MakeGenericMethod(
                        modelType);
                }
                else if (IsMatching(
                    type, out modelType, t => ComplexType.IsAssignableFrom(t)))
                {
                    typedMethod = complexTypeMethod.MakeGenericMethod(
                        modelType);
                }
                else
                {
                    continue;
                }

                configurations.Add(
                    typedMethod, Activator.CreateInstance(type));
            }

            return configurations;
        }

        private static bool IsMappingType(Type matchingType)
        {
            if (!matchingType.IsClass ||
                matchingType.IsAbstract)
            {
                return false;
            }

            Type temp;

            return IsMatching(
                matchingType,
                out temp,
                t =>
                    EntityType.IsAssignableFrom(t) ||
                    ComplexType.IsAssignableFrom(t));
        }

        private static bool IsMatching(
            Type matchingType,
            out Type modelType,
            Predicate<Type> matcher)
        {
            modelType = null;

            while (matchingType != null)
            {
                if (matchingType.IsGenericType)
                {
                    var definationType = matchingType
                        .GetGenericTypeDefinition();

                    if (matcher(definationType))
                    {
                        modelType = matchingType.GetGenericArguments().First();
                        return true;
                    }
                }

                matchingType = matchingType.BaseType;
            }

            return false;
        }

        private FreedomDbContext CreateContext()
        {
            var configurtions = _mappingConfigurations.GetOrAdd(
                _nameOrConnectionString,
                key => BuildConfigurations());

            var localContext = (FreedomDbContext)Activator.CreateInstance(
                DataContextType,
                new object[] { _nameOrConnectionString, configurtions });

            return localContext;
        }

    }
}
