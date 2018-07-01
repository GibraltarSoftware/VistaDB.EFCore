using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class VistaDBTestStoreFactory : ITestStoreFactory
    {
        public static VistaDBTestStoreFactory Instance { get; } = new VistaDBTestStoreFactory();

        protected VistaDBTestStoreFactory()
        {
        }

        public virtual TestStore Create(string storeName)
            => VistaDBTestStore.Create(storeName);

        public virtual TestStore GetOrCreate(string storeName)
            => VistaDBTestStore.CreateScratch(true);

        public virtual IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
            => serviceCollection.AddEntityFrameworkVistaDB()
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory());
    }
}
