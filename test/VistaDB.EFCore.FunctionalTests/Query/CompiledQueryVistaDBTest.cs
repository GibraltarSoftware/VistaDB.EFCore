using Microsoft.EntityFrameworkCore.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class CompiledQueryVistaDBTest : CompiledQueryTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public CompiledQueryVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }
    }
}