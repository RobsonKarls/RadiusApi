using System.Data.Entity;

namespace Freedom.Infrastructure.DataAccess.Base
{
    interface IDataContextFactory<T> where T : DbContext
    {
         T Context { get; set; }
    }
}
