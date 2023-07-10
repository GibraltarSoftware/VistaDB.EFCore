using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Storage.Internal;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
using VistaDB.EntityFrameworkCore.Provider.Storage.Internal;
using VistaDB.Provider;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Tests
{
    public class VistaDBRelationalConnectionTest
    {
        [Fact]
        public void Creates_VistaDB_connection()
        {
            using (var connection = new VistaDBRelationalConnection(CreateDependencies()))
            {
                Assert.IsType<VistaDBConnection>(connection.DbConnection);
            }
        }

        public static RelationalConnectionDependencies CreateDependencies(DbContextOptions options = null)
        {
            options = options
                      ?? new DbContextOptionsBuilder()
                          .UseVistaDB(@"Data Source=dummy.vdb;")
                          .Options;

            return new RelationalConnectionDependencies(
                options,
                new DiagnosticsLogger<DbLoggerCategory.Database.Transaction>(
                    new LoggerFactory(),
                    new LoggingOptions(),
                    new DiagnosticListener("FakeDiagnosticListener")),
                new DiagnosticsLogger<DbLoggerCategory.Database.Connection>(
                    new LoggerFactory(),
                    new LoggingOptions(),
                    new DiagnosticListener("FakeDiagnosticListener")),
                new NamedConnectionStringResolver(options),
                new RelationalTransactionFactory(new RelationalTransactionFactoryDependencies()));
        }
    }
}
