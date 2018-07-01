namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class VistaDBNorthwindTestStoreFactory : VistaDBTestStoreFactory
    {
        public const string Name = "Northwind";
        public static readonly string NorthwindConnectionString = VistaDBTestStore.CreateConnectionString(Name);
        public new static VistaDBNorthwindTestStoreFactory Instance { get; } = new VistaDBNorthwindTestStoreFactory();

        protected VistaDBNorthwindTestStoreFactory()
        {
        }

        public override TestStore GetOrCreate(string storeName)
            => VistaDBTestStore.GetNorthwindStore();
    }
}
