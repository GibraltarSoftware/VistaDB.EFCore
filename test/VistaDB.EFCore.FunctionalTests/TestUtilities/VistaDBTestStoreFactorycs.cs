using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace VistaDB.EntityFrameworkCore.FunctionalTests.TestUtilities
{
    public class VistaDBTestStoreFactory : RelationalTestStoreFactory
    {
        public static VistaDBTestStoreFactory Instance { get; } = new VistaDBTestStoreFactory();

        protected VistaDBTestStoreFactory()
        {
        }

        public override TestStore Create(string storeName)
            => VistaDBNewTestStore.Create(storeName);

        public override TestStore GetOrCreate(string storeName)
            => VistaDBNewTestStore.CreateScratch(true);

        public override IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
            => serviceCollection.AddEntityFrameworkVistaDB()
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory());
    }
}
