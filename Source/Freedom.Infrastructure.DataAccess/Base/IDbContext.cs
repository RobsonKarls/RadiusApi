using System.Data.Entity;
using System.Data.Entity.Infrastructure;

namespace Freedom.Infrastructure.DataAccess.Base
{
    public interface IDbContext
    {
        IDbSet<TEntity> CreateSet<TEntity>() where TEntity : class;
        void MarkAsModified<TEntity>(TEntity instance) where TEntity : class;
        int SaveChanges();
        DbChangeTracker ChangeTracker {get;}
    }
}
