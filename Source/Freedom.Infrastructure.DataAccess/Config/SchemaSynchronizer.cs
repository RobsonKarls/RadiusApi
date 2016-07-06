using System;
using System.Data.Entity;
using Freedom.Infrastructure.DataAccess.Factories;

namespace Freedom.Infrastructure.DataAccess.Config
{
    public class SchemaSynchronizer
    {
        private readonly Func<bool> _debugMode;

        public SchemaSynchronizer(Func<bool> debugMode)
        {
            _debugMode = debugMode;
        }

        public void Execute()
        {
            var initializer = _debugMode() ? new CreateDatabaseIfNotExists<FreedomDbContext>() : new NullDatabaseInitializer<FreedomDbContext>() as IDatabaseInitializer<FreedomDbContext>;

            Database.SetInitializer(initializer);
        }
    }
}