using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;
using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class IncludeVistaDBTest : IncludeTestBase<IncludeVistaDBFixture>
    {
        public IncludeVistaDBTest(IncludeVistaDBFixture fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            //TestSqlLoggerFactory.CaptureOutput(testOutputHelper);
        }
    }
}