using Microsoft.EntityFrameworkCore.TestUtilities;

namespace VistaDB.EntityFrameworkCore.FunctionalTests.TestUtilities
{
    public class VistaDBNorthwindTestStoreFactory : VistaDBTestStoreFactory
    {
        public const string Name = "Northwind";
        public static readonly string NorthwindConnectionString = VistaDBNewTestStore.CreateConnectionString(Name);
        public new static VistaDBNorthwindTestStoreFactory Instance { get; } = new VistaDBNorthwindTestStoreFactory();

        protected VistaDBNorthwindTestStoreFactory()
        {
        }

        public override TestStore GetOrCreate(string storeName)
            => VistaDBNewTestStore.GetNorthwindStore();
    }
}
