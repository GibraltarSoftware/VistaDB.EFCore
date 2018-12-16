using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class QueryNavigationsVistaDBTest : QueryNavigationsTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public QueryNavigationsVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}