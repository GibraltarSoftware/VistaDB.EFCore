using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsyncFromSqlQueryVistaDBTest : AsyncFromSqlQueryTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public AsyncFromSqlQueryVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}