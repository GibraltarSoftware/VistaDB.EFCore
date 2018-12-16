using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsNoTrackingVistaDBTest : AsNoTrackingTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public AsNoTrackingVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}