using Freedom.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Freedom.Infrastructure.DataAccess.Mappings
{
    public class ProductMap : EntityTypeConfiguration<Product>
    {
        public ProductMap()
        {
            HasKey(x => x.Id);

            Property(x => x.Created).HasColumnType("DateTime");
            Property(x => x.Modified).HasColumnType("DateTime");
            Property(x => x.BidPrice).HasColumnType("Decimal");

            #region RELATIONSHIP
            //BelongsTo
            HasRequired(x => x.Farm);
            HasRequired(x => x.Category);

            HasMany(x => x.Images);
            #endregion

        }
    }
}
