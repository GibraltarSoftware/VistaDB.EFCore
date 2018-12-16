using System.Data.Common;
using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.Provider;

namespace Microsoft.EntityFrameworkCore.Query
{
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