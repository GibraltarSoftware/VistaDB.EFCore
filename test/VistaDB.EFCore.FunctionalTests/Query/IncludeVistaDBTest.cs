using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class IncludeVistaDBTest : IncludeTestBase<IncludeVistaDBFixture>
    {
        public IncludeVistaDBTest(IncludeVistaDBFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            //TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
        }
    }
}