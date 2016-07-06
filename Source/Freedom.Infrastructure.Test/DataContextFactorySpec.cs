using System.Collections.Generic;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Reflection;
using Freedom.Domain.Core;
using Freedom.Infrastructure.DataAccess.Factories;
using Machine.Specifications;

namespace Freedom.Infrastructure.Test
{
    [Subject(typeof(DataContextFactory))]
    public class BuscandoOContext : DatabaseSpec
    {
        private static FreedomDbContext _context;

        private It _deveRegistrarAsClassesMappingsDisponiveis = () =>
        {
            List<string> availableMappings = Assembly.Load("Freedom.Domain").GetExportedTypes().Where(t =>
                t.IsClass &&
                !t.IsAbstract &&
                // the ValueObject is inherating from IEntity so this  condition is useless
                // but better keep just in case
                //(typeof(IEntity).IsAssignableFrom(t) || typeof(ValueObject).IsAssignableFrom(t)))
                (typeof(IEntity).IsAssignableFrom(t)))
                .Select(t => t.Name)
                .ToList();

            IObjectContextAdapter adapter = _context;

            List<string> registeredMappings = adapter.ObjectContext
                .MetadataWorkspace.GetItems(DataSpace.OSpace)
                .OfType<EntityType>()
                .Select(e => e.Name)
                .Concat(adapter.ObjectContext
                    .MetadataWorkspace
                    .GetItems(DataSpace.OSpace)
                    .OfType<ComplexType>()
                    .Select(e => e.Name))
                .Where(e => !e.Equals("EdmMetadata"))
                .ToList();

            registeredMappings.Count.ShouldEqual(availableMappings.Count);
            registeredMappings.ShouldContain(availableMappings);
        };

        private It _deveSerDataContextDoTipo = () => _context.ShouldBeOfExactType<FreedomDbContext>();
        private It _naoDeveSerNull = () => _context.ShouldNotBeNull();
        private Because of = () => _context = ContextFactory.GetContext();
    }
}