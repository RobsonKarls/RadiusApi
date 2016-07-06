using Machine.Specifications;
using Freedom.Infrastructure.DataAccess.Config;

namespace Freedom.Infrastructure.Test
{
    public class Bootstrapper : IAssemblyContext
    {
        public void OnAssemblyStart()
        {
            new SchemaSynchronizer(() => true).Execute();
        }

        public void OnAssemblyComplete()
        {
            // Do nothing just sleep
        }
    }
}