using Freedom.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Freedom.Infrastructure.DataAccess.Mappings
{
    public class OrderMap : EntityTypeConfiguration<Order>
    {
        public OrderMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Created).HasColumnType("Datetime2");
            Property(x => x.Modified).HasColumnType("Datetime2");
            Property(x => x.PaymentType).IsRequired();

            #region RELATIONSHIP 
            //belongsTo
            HasRequired(x => x.User);
            HasRequired(x => x.AggregatorType);

            //HasMany
            HasMany(x => x.OrderItems);
            #endregion
        }
    }
}
