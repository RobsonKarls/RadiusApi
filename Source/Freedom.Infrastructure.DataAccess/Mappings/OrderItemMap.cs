using Freedom.Domain.Entities;
using System.Data.Entity.ModelConfiguration;

namespace Freedom.Infrastructure.DataAccess.Mappings
{
    public class OrderItemMap : EntityTypeConfiguration<OrderItem>
    {
        public OrderItemMap()
        {
            HasKey(x => x.Id);

            #region RELATIONSHIP 
            //BelongsTo
            HasRequired(x => x.Order);
            HasRequired(x => x.Product);
            #endregion
        }
    }
}
