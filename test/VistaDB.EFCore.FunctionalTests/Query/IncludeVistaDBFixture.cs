using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class IncludeVistaDBFixture : NorthwindQueryVistaDBFixture<NoopModelCustomizer>
    {
        /* IncludeIgnoredWarning is apparently now obsolete and no property is provided for it,
         * so this override is probably no longer needed.
        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            => base.AddOptions(builder).ConfigureWarnings(c => c.Log(CoreEventId.IncludeIgnoredWarning));
        */
    }
}