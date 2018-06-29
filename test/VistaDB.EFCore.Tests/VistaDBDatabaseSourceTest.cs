using System.Reflection;
using Microsoft.EntityFrameworkCore.Storage;
using VistaDB.EFCore.Infrastructure.Internal;
using VistaDB.EFCore.Storage.Internal;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Tests
{
    public class VistaDBDatabaseSourceTest
    {
        [Fact]
        public void Is_configured_when_configuration_contains_associated_extension()
        {
            var optionsBuilder = new DbContextOptionsBuilder();
            optionsBuilder.UseVistaDB("Data Source=Crunchie");

            Assert.True(new DatabaseProvider<VistaDBOptionsExtension>(new DatabaseProviderDependencies()).IsConfigured(optionsBuilder.Options));
        }

        [Fact]
        public void Is_not_configured_when_configuration_does_not_contain_associated_extension()
        {
            var optionsBuilder = new DbContextOptionsBuilder();

            Assert.False(new DatabaseProvider<VistaDBOptionsExtension>(new DatabaseProviderDependencies()).IsConfigured(optionsBuilder.Options));
        }

        [Fact]
        public void Returns_appropriate_name()
        {
            Assert.Equal(
                typeof(VistaDBRelationalConnection).GetTypeInfo().Assembly.GetName().Name,
                new DatabaseProvider<VistaDBOptionsExtension>(new DatabaseProviderDependencies()).Name);
        }
    }
}
