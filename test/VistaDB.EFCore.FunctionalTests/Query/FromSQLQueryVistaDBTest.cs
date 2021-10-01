using System.Data.Common;
using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.EntityFrameworkCore.FunctionalTests;
using VistaDB.Provider;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class FromSqlQueryVistaDBTest : FromSqlQueryTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public FromSqlQueryVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture)
            : base(fixture)
        {
        }

        protected override DbParameter CreateDbParameter(string name, object value)
            => new VistaDBParameter
            {
                ParameterName = name,
                Value = value
            };
    }
}