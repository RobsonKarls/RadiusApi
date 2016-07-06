using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using Freedom.Infrastructure.DataAccess.Factories;
using System.Data.Entity.Validation;
using System;
using Freedom.Infrastructure.DataAccess.Base;

namespace Freedom.Infrastructure.DataAccess
{
    public class UnitOfWork
    {
        public UnitOfWork(IDbContext context)
        {

            Context = context;
        }

        protected IDbContext Context { get; private set; }

        public virtual void Commit()
        {
            if (Context.ChangeTracker.Entries().Any(HasChanged))
            {

                try
                {
                    Context.SaveChanges();
                }
                catch (DbEntityValidationException e)
                {
                    foreach (var eve in e.EntityValidationErrors)
                    {
                        Console.WriteLine("Entity of type \"{0}\" in state \"{1}\" has the following validation errors:",
                            eve.Entry.Entity.GetType().Name, eve.Entry.State);
                        foreach (var ve in eve.ValidationErrors)
                        {
                            Console.WriteLine("- Property: \"{0}\", Error: \"{1}\"",
                                ve.PropertyName, ve.ErrorMessage);
                        }
                    }
                    throw;
                }
            }
        }

        private static bool HasChanged(DbEntityEntry entity)
        {
            return IsState(entity, EntityState.Added) ||
                   IsState(entity, EntityState.Deleted) ||
                   IsState(entity, EntityState.Modified);
        }

        private static bool IsState(DbEntityEntry entity, EntityState state)
        {
            return (entity.State & state) == state;
        }
    }
}