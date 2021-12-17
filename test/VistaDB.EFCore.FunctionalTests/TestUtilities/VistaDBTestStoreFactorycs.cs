using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class VistaDBTestStoreFactory : RelationalTestStoreFactory
    {
        public static VistaDBTestStoreFactory Instance { get; } = new VistaDBTestStoreFactory();

        protected VistaDBTestStoreFactory()
        {
        }

        public override TestStore Create(string storeName)
            => VistaDBTestStore.Create(storeName);

        public override TestStore GetOrCreate(string storeName)
            => VistaDBTestStore.CreateScratch(true);

        public override IServiceCollection AddProviderServices(IServiceCollection serviceCollection)
            => serviceCollection.AddEntityFrameworkVistaDB()
                .AddSingleton<ILoggerFactory>(new TestSqlLoggerFactory());
    }
}
