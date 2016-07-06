using System.Data.Entity.ModelConfiguration;
using Freedom.Domain.Entities;

namespace Freedom.Infrastructure.DataAccess.Mappings
{
    public class FarmMap : EntityTypeConfiguration<Farm>
    {
        public FarmMap()
        {
            HasKey(x => x.Id);
        }
    }
}
