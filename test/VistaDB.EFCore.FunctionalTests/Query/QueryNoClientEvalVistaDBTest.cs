using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class QueryNoClientEvalVistaDBTest : QueryNoClientEvalTestBase<QueryNoClientEvalVistaDBFixture>
    {
        public QueryNoClientEvalVistaDBTest(QueryNoClientEvalVistaDBFixture fixture)
            : base(fixture)
        {
        }
    }
}