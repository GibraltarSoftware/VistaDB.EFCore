using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class DbFunctionsVistaDBTest : DbFunctionsTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public DbFunctionsVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
        }
    }
}