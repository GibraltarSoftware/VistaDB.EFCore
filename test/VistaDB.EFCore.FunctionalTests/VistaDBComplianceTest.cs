using System.Reflection;

namespace Microsoft.EntityFrameworkCore
{
    public class VistaDBComplianceTest : RelationalComplianceTestBase
    {
        protected override Assembly TargetAssembly { get; } = typeof(VistaDBComplianceTest).Assembly;
    }
}