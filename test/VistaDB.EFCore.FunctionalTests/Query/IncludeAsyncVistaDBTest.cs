using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class IncludeAsyncVistaDBTest : IncludeAsyncTestBase<IncludeVistaDBFixture>
    {
        public IncludeAsyncVistaDBTest(IncludeVistaDBFixture fixture)
            : base(fixture)
        {
        }
    }
}