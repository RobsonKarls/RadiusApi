using System.Data.Entity.ModelConfiguration;
using Freedom.Domain.Entities;

namespace Freedom.Infrastructure.DataAccess.Mappings
{
    public class AddressMap : EntityTypeConfiguration<Address>
    {
        public AddressMap()
        {
            HasKey(x => x.Id);
            Property(x => x.AddressLine1).IsRequired().HasColumnType("nvarchar");
            Property(x => x.ZipCode).IsRequired().HasColumnType("nvarchar").HasMaxLength(8);
            Property(x => x.City).IsRequired().HasColumnType("nvarchar");
            Property(x => x.Number).HasColumnType("nvarchar");
            Property(x => x.AddressLine2).HasColumnType("nvarchar");
            Property(x => x.Country).HasColumnType("nvarchar");
            Property(x => x.State).HasColumnType("nvarchar");

        }
    }
}
