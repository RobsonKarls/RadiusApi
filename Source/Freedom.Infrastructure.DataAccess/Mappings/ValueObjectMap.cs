using System.Data.Entity.ModelConfiguration;
using Freedom.Domain.Core;

namespace Freedom.Infrastructure.DataAccess.Mappings
{
    public abstract class ValueObjectConfiguration<T> : EntityTypeConfiguration<T> where T : ValueObject
    {
        protected ValueObjectConfiguration()
        {
            Map(a => a.MapInheritedProperties());
            HasKey(p => p.Id);
            ToTable(typeof(T).Name, "ValueObjects");
        }
    }
}
