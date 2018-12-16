using System;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;

// ReSharper disable InconsistentNaming
#pragma warning disable 1998
namespace Microsoft.EntityFrameworkCore.Query
{
    public class AsyncSimpleQueryVistaDBTest : AsyncSimpleQueryTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public AsyncSimpleQueryVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }

        public override Task Query_backed_by_database_view()
        {
            // Not present on VistaDB
            return Task.CompletedTask;
        }

        [Fact]
        public async Task Single_Predicate_Cancellation()
        {
            await Assert.ThrowsAnyAsync<OperationCanceledException>(
                async () =>
                    await Single_Predicate_Cancellation_test(Fixture.TestSqlLoggerFactory.CancelQuery()));
        }
    }
}