using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class AsNoTrackingVistaDBTest : AsNoTrackingTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public AsNoTrackingVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}