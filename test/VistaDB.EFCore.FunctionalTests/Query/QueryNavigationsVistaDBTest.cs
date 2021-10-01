using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class QueryNavigationsVistaDBTest : QueryNavigationsTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public QueryNavigationsVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}