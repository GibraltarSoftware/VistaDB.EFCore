using Xunit.Abstractions;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class FiltersVistaDBTest : FiltersTestBase<NorthwindQueryVistaDBFixture<NorthwindFiltersCustomizer>>
    {
        public FiltersVistaDBTest(NorthwindQueryVistaDBFixture<NorthwindFiltersCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            fixture.TestSqlLoggerFactory.Clear();
            //fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);
    }
}