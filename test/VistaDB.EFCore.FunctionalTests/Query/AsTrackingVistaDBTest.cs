using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsTrackingVistaDBTest : AsTrackingTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public AsTrackingVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}