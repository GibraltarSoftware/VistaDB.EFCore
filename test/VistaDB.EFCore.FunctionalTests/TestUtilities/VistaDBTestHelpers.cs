using Microsoft.Extensions.DependencyInjection;
using VistaDB.Provider;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class VistaDBTestHelpers : TestHelpers
    {
        protected VistaDBTestHelpers()
        {
        }

        public static VistaDBTestHelpers Instance { get; } = new VistaDBTestHelpers();

        public override IServiceCollection AddProviderServices(IServiceCollection services)
            => services.AddEntityFrameworkVistaDB();

        public override void UseProviderOptions(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseVistaDB(new VistaDBConnection("Database=DummyDatabase"));
    }
}
