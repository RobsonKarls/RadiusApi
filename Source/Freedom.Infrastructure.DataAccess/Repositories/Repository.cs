using System;
using System.Collections.Generic;
using System.Linq;
using Freedom.Domain.Core;
using Freedom.Infrastructure.DataAccess.Factories;
using System.Linq.Expressions;
using System.Data.Entity;
using Freedom.Infrastructure.DataAccess.Base;

namespace Freedom.Infrastructure.DataAccess.Repositories
{
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity, IAggregateRoot
    {
        public Repository(IDbContext context)
        {
            Context = context;
        }

        protected IDbContext Context { get; private set; }

        public virtual void Save(TEntity instance)
        {
            if (instance.Id.Equals(0))
            {
                CreateSet().Add(instance);
            }
            else
            {
                Context.MarkAsModified(instance);
            }
        }

        public virtual void Remove(TEntity instance)
        {
            CreateSet().Remove(instance);
        }

        public virtual TEntity Find(Expression<Func<TEntity, bool>> predicate = null, params Expression<Func<TEntity, object>>[] includes)
        {
            var set = CreateIncludedSet(includes);

            return (predicate == null) ?
                   set.FirstOrDefault() :
                   set.FirstOrDefault(predicate);
        }

        public virtual IQueryable<TEntity> All(Expression<Func<TEntity, bool>> predicate = null,params Expression<Func<TEntity, object>>[] includes)
        {
            var set = CreateIncludedSet(includes);

            return (predicate == null) ? set : set.Where(predicate);
        }

        public virtual bool Exists(Expression<Func<TEntity, bool>> predicate = null)
        {
            var set = CreateSet();

            return (predicate == null) ? set.Any() : set.Any(predicate);
        }

        public virtual int Count(Expression<Func<TEntity, bool>> predicate = null)
        {
            var set = CreateSet();

            return (predicate == null) ? set.Count() : set.Count(predicate);
        }

        private IDbSet<TEntity> CreateIncludedSet(IEnumerable<Expression<Func<TEntity, object>>> includes)
        {
            var set = CreateSet();

            if (includes != null)
            {
                foreach (var include in includes)
                {
                    set.Include(include);
                }
            }

            return set;
        }

        private IDbSet<TEntity> CreateSet()
        {
            return Context.CreateSet<TEntity>();
        }
    }
}
