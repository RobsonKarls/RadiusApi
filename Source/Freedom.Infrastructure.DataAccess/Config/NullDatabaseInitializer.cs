using System.Data.Entity;

namespace Freedom.Infrastructure.DataAccess.Config
{
    public class NullDatabaseInitializer<TContext> : IDatabaseInitializer<TContext> where TContext : DbContext
    {
        public void InitializeDatabase(TContext context)
        {
        }
    }
}