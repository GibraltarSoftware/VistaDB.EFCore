using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.Logging;
using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class QueryNoClientEvalVistaDBFixture : NorthwindQueryVistaDBFixture<NoopModelCustomizer>
    {
        /* QueryClientEvaluationWarning has no property provided for it, so this override does not seem to be possible.
        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            => base.AddOptions(builder).ConfigureWarnings(c => c.Throw(RelationalEventId.QueryClientEvaluationWarning));
        */
    }
}