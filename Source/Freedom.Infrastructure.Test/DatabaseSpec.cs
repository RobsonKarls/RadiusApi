using System.Data;
using System.Data.Common;
using System.Data.Entity.Infrastructure;

using Freedom.Infrastructure.DataAccess;
using Freedom.Infrastructure.DataAccess.Factories;

using Machine.Specifications;



namespace Freedom.Infrastructure.Test
{
    public abstract class DatabaseSpec
    {
        protected static DataContextFactory ContextFactory;
        protected static UnitOfWork UnitOfWork;

        static DbTransaction transaction;

        Establish context = () =>
        {
            ContextFactory = new DataContextFactory("MadeNaRoca");
            var context = ContextFactory.GetContext();
            UnitOfWork = new UnitOfWork(context);

            IObjectContextAdapter adapter = context;

            if ((adapter.ObjectContext.Connection.State & ConnectionState.Open) != ConnectionState.Open)
            {
                adapter.ObjectContext.Connection.Open();
            }

            transaction = adapter.ObjectContext
                .Connection
                .BeginTransaction(IsolationLevel.ReadCommitted);
        };

        Cleanup on_exit = () =>
        {
            transaction.Commit();
            //transaction.Rollback();

            transaction.Dispose();

            ContextFactory.Dispose();
        };
    }
}
