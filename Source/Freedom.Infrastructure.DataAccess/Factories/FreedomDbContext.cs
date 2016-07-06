using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Core.Objects;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using System.Reflection;
using Freedom.Domain.Core;
using Freedom.Domain.Entities;
using Freedom.Infrastructure.DataAccess.Base;
using Freedom.Infrastructure.DataAccess.Conventions;
using Freedom.Labs.Components;

namespace Freedom.Infrastructure.DataAccess.Factories
{
    [DbConfigurationType(typeof(CodeConfig))]
    public class FreedomDbContext : DbContext, IDbContext
    {
        readonly IDictionary<MethodInfo, object> _configurations;
        public DbSet<User> Users { get; set; }
        public DbSet<LoginProvider> LoginProviders { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Farm> Farms { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }

        public FreedomDbContext(string nameOrConnectionString, IDictionary<MethodInfo, object> configurations) : base(nameOrConnectionString)
        {
            if (configurations == null)
                throw new ArgumentNullException("configurations");

            _configurations = configurations;

            var obejctContext = ((IObjectContextAdapter)this).ObjectContext;

            obejctContext.SavingChanges += (sender, args) =>
            {

                foreach (var entry in ChangeTracker.Entries<User>())
                {
                    var entity = entry.Entity;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.Password = Password.CreateHashFrom(entity.Password);
                            break;
                    }
                }


                var now = DateTime.Now;

                foreach (var entry in ChangeTracker.Entries<IAggregateRoot>())
                {
                    var entity = entry.Entity;

                    switch (entry.State)
                    {
                        case EntityState.Added:
                            entity.Created = now;
                            entity.Modified = now;
                            break;
                        case EntityState.Modified:
                            entity.Modified = now;
                            break;
                    }
                }
                ChangeTracker.DetectChanges();
            };
        }

        public virtual void MarkAsModified<TEntity>(TEntity instance)
            where TEntity : class
        {
            Entry(instance).State = EntityState.Modified;
        }

        public virtual IDbSet<TEntity> CreateSet<TEntity>()
            where TEntity : class
        {
            return Set<TEntity>();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            if (modelBuilder == null)
            {
                throw new ArgumentNullException("modelBuilder");
            }

            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Conventions.Add(new FirstCharLowerCaseConvention());

            foreach (var config in _configurations)
            {
                config.Key.Invoke(
                    modelBuilder.Configurations,
                    new[] { config.Value });
            }

            base.OnModelCreating(modelBuilder);
        }
    }

    public class CodeConfig : DbConfiguration
    {
        public CodeConfig()
        {
            SetProviderServices("System.Data.SqlClient",
            System.Data.Entity.SqlServer.SqlProviderServices.Instance);
        }
    }
}
