using System.Data.Entity.ModelConfiguration;
using Freedom.Domain.Entities;

namespace Freedom.Infrastructure.DataAccess.Mappings
{
    public class LoginProviderMap : EntityTypeConfiguration<LoginProvider>
    {
        public LoginProviderMap()
        {
            HasKey(x => x.Id);
            Property(x => x.Provider).HasColumnType("smallint");
            Property(x => x.EncryptedToken).HasColumnType("nvarchar").HasMaxLength(64);

            #region RELATIOSHIP
            HasRequired(x => x.User);
            #endregion
        }
    }
}