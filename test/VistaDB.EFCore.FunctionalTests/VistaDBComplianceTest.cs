using System.Reflection;
using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    [Collection(SetupFixture.CollectionName)]
    public class VistaDBComplianceTest : RelationalComplianceTestBase
    {
        protected override Assembly TargetAssembly { get; } = typeof(VistaDBComplianceTest).Assembly;
    }
}