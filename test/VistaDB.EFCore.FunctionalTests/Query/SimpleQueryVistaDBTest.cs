using System;
using System.Linq;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Xunit;
using Xunit.Abstractions;

// ReSharper disable UnusedParameter.Local
// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore.Query
{
    public partial class SimpleQueryVistaDBTest : SimpleQueryTestBase<NorthwindQueryVistaDBFixture<NoopModelCustomizer>>
    {
        public SimpleQueryVistaDBTest(NorthwindQueryVistaDBFixture<NoopModelCustomizer> fixture, ITestOutputHelper testOutputHelper)
            : base(fixture)
        {
            Fixture.TestSqlLoggerFactory.Clear();
            //Fixture.TestSqlLoggerFactory.SetTestOutputHelper(testOutputHelper);
        }

        public override void Where_bitwise_and()
        {
  //          SELECT[c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
  //      FROM[Customers] AS[c]
  //WHERE(CASE
  //    WHEN [c].[CustomerID] = N'ALFKI'
  //    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
  //END & CASE
  //    WHEN[c].[CustomerID] = N'ANATR'
  //    THEN CAST(1 AS BIT) ELSE CAST(0 AS BIT)
  //END) = 1

            base.Where_bitwise_and();
        }

        public override void Where_datetimeoffset_now_component()
        {
            base.Where_datetimeoffset_now_component();

            // VistaDB does not implement SYSDATETIMEOFFSET(), forcing client eval
            AssertSql(
                @"SELECT [o].[OrderID], [o].[CustomerID], [o].[EmployeeID], [o].[OrderDate]
FROM [Orders] AS [o]");
        }

        public override void  OrderBy_empty_list_does_not_contains()
        {
  //          SELECT[c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]
  //      FROM[Customers] AS[c]
  //ORDER BY(SELECT 1)

            base.OrderBy_empty_list_does_not_contains();
        }

        public override void Environment_newline_is_funcletized()
        {
//            Executing DbCommand[Parameters =[@__NewLine_0 = '

//' (Size = 4000) (DbType = Object)], CommandType='Text', CommandTimeout='30']

//SELECT[c].[CustomerID], [c].[Address], [c].[City], [c].[CompanyName], [c].[ContactName], [c].[ContactTitle], [c].[Country], [c].[Fax], [c].[Phone], [c].[PostalCode], [c].[Region]

//FROM[Customers] AS[c]

//WHERE(CHARINDEX(@__NewLine_0, [c].[CustomerID]) > 0) OR(@__NewLine_0 = N'')

          base.Environment_newline_is_funcletized();
        }

        private void AssertSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected);

        private void AssertContainsSql(params string[] expected)
            => Fixture.TestSqlLoggerFactory.AssertBaseline(expected, assertOrder: false);

        protected override void ClearLog()
            => Fixture.TestSqlLoggerFactory.Clear();
    }
}
