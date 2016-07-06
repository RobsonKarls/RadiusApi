using System.Data.Entity.ModelConfiguration;
using Freedom.Domain.Entities;

namespace Freedom.Infrastructure.DataAccess.Mappings
{
    public class GroupMap : EntityTypeConfiguration<Group>
    {
        public GroupMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Active).IsRequired().HasColumnType("bit");
            Property(x => x.Description).HasColumnType("nvarchar").HasMaxLength(200);
            Property(x => x.Name).HasColumnType("nvarchar").IsRequired().HasMaxLength(60);
            Property(x => x.Created).HasColumnType("DateTime");
            Property(x => x.Modified).HasColumnType("DateTime");

        }
    }
}