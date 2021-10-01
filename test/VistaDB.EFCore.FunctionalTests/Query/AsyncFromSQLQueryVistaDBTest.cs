using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class AsyncFromSqlQueryVistaDBTest : AsyncFromSqlQueryTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public AsyncFromSqlQueryVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}