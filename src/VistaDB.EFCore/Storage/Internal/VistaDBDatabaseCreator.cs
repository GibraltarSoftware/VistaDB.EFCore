using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;
using VistaDB.DDA;
using VistaDB.Provider;

namespace VistaDB.EFCore.Storage.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class VistaDBDatabaseCreator : RelationalDatabaseCreator
    {
        private readonly IVistaDBRelationalConnection _connection;
        private readonly IRawSqlCommandBuilder _rawSqlCommandBuilder;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public VistaDBDatabaseCreator(
            [NotNull] RelationalDatabaseCreatorDependencies dependencies,
            [NotNull] IVistaDBRelationalConnection connection,
            [NotNull] IRawSqlCommandBuilder rawSqlCommandBuilder)
            : base(dependencies)
        {
            _connection = connection;
            _rawSqlCommandBuilder = rawSqlCommandBuilder;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override void Create()
        {
            Check.NotNull(_connection, nameof(_connection));
            var connection = _connection.DbConnection as VistaDBConnection;
            connection?.CreateEmptyDatabase();
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override bool Exists()
        {
            var connection = _connection.DbConnection as VistaDBConnection;
            return (connection != null) && connection.Exists();
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override bool HasTables()
        {
            using (IVistaDBDDA DDAObj = VistaDBEngine.Connections.OpenDDA())
            {
                using (IVistaDBDatabase db1 = DDAObj.OpenDatabase((_connection.DbConnection as VistaDBConnection).DataSource, VistaDBDatabaseOpenMode.ExclusiveReadWrite, null))
                {
                    return db1.GetTableNames().Count > 0;
                }
            }
        }
        //=> (int)CreateHasTablesCommand().ExecuteScalar(_connection) != 0;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default(CancellationToken))
        {
            using (IVistaDBDDA DDAObj = VistaDBEngine.Connections.OpenDDA())
            {
                using (IVistaDBDatabase db1 = DDAObj.OpenDatabase((_connection.DbConnection as VistaDBConnection).DataSource, VistaDBDatabaseOpenMode.NonexclusiveReadWrite, null))
                {
                    return db1.GetTableNames().Count > 0;
                }
            }
        }
        //=> (int)await CreateHasTablesCommand().ExecuteScalarAsync(_connection, cancellationToken: cancellationToken) != 0;

        private IRelationalCommand CreateHasTablesCommand()
            => _rawSqlCommandBuilder
                .Build("SELECT COUNT(*) FROM INFORMATION_SCHEMA.TABLES WHERE TABLE_TYPE <> N'SYSTEM TABLE';");
        
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override void Delete()
        {
            var connection = _connection.DbConnection as VistaDBConnection;
            connection?.Drop();
        }
    }
}
