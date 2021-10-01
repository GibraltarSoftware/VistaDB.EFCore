using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.TestModels.Northwind;
using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.EntityFrameworkCore.FunctionalTests;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Query
{
    [Collection(SetupFixture.CollectionName)]
    public class NorthwindQueryVistaDBFixture<TModelCustomizer> : NorthwindQueryRelationalFixture<TModelCustomizer>
        where TModelCustomizer : IModelCustomizer, new()
    {
        protected override ITestStoreFactory TestStoreFactory => VistaDBNorthwindTestStoreFactory.Instance;

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            modelBuilder.Entity<Customer>()
                .Property(c => c.CustomerID)
                .HasColumnType("nchar(5)");

            modelBuilder.Entity<Employee>(
                b =>
                    {
                        b.Property(c => c.EmployeeID).HasColumnType("int");
                        b.Property(c => c.ReportsTo).HasColumnType("int");
                    });

            modelBuilder.Entity<Order>(
                b =>
                {
                    b.Property(o => o.EmployeeID).HasColumnType("int");
                    b.Property(o => o.OrderDate).HasColumnType("datetime");
                });

            modelBuilder.Entity<OrderDetail>()
                .Property(od => od.UnitPrice)
                .HasColumnType("money");

            modelBuilder.Entity<Product>(
                b =>
                    {
                        b.Property(p => p.UnitPrice).HasColumnType("money");
                        b.Property(p => p.UnitsInStock).HasColumnType("smallint");
                    });

            modelBuilder.Entity<MostExpensiveProduct>()
                .Property(p => p.UnitPrice)
                .HasColumnType("money");
        }

        protected override void Seed(NorthwindContext context)
        {
            //base.Seed(context);
        }
    }
}
