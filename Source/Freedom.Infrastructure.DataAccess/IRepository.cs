using System;
using System.Linq;
using System.Linq.Expressions;
using Freedom.Domain.Core;

namespace Freedom.Infrastructure.DataAccess
{
    /// <summary>
    /// Repository main interface
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public interface IRepository<TEntity>  where TEntity : IAggregateRoot
    {
        /// <summary>
        /// Get a specific entity by id
        /// </summary>
        /// <returns>Requested entity</returns>
        TEntity Find(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes);

        /// <summary>
        /// Add an item to repository
        /// </summary>
        /// <param name="instance">instance do add</param>
        void Save(TEntity instance);

        /// <summary>
        /// remove an item from the repository
        /// </summary>
        /// <param name="instance">instance to remove</param>
        void Remove(TEntity instance);

        IQueryable<TEntity> All(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes);


        bool Exists(Expression<Func<TEntity, bool>> predicate = null);


        int Count(Expression<Func<TEntity, bool>> predicate = null);
    }
}
