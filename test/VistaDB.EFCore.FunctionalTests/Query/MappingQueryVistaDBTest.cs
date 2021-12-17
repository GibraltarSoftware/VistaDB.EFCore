using System;
using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class MappingQueryVistaDBTest : MappingQueryTestBase<MappingQueryVistaDBTest.MappingQueryVistaDBFixture>
    {
        public MappingQueryVistaDBTest(MappingQueryVistaDBFixture fixture)
            : base(fixture)
        {
        }

        private static readonly string EOL = Environment.NewLine;

        private string Sql => Fixture.TestSqlLoggerFactory.Sql;

        public class MappingQueryVistaDBFixture : MappingQueryFixtureBase
        {
            protected override ITestStoreFactory TestStoreFactory => VistaDBNorthwindTestStoreFactory.Instance;

            protected override string DatabaseSchema { get; } = null;

            protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
            {
                base.OnModelCreating(modelBuilder, context);

                modelBuilder.Entity<MappedCustomer>(
                    e =>
                    {
                        e.Property(c => c.CompanyName2).Metadata.SetColumnName("CompanyName");
                        e.Metadata.SetTableName("Customers");
                        e.Metadata.SetSchema("dbo");
                    });

                modelBuilder.Entity<MappedEmployee>()
                    .Property(c => c.EmployeeID)
                    .HasColumnType("int");
            }
        }
    }
}