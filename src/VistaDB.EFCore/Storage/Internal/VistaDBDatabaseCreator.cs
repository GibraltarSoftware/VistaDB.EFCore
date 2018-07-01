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

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public VistaDBDatabaseCreator(
            [NotNull] RelationalDatabaseCreatorDependencies dependencies,
            [NotNull] IVistaDBRelationalConnection connection)
            : base(dependencies)
        {
            _connection = connection;
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
            => GetTableCount() > 0;

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override async Task<bool> HasTablesAsync(CancellationToken cancellationToken = default(CancellationToken))
            => GetTableCount() > 0;

        private int GetTableCount()
        {
            using (var DDAObj = VistaDBEngine.Connections.OpenDDA())
            {
                using (var db = DDAObj.OpenDatabase((_connection.DbConnection as VistaDBConnection).DataSource, VistaDBDatabaseOpenMode.NonexclusiveReadWrite, null))
                {
                    return db.GetTableNames().Count;
                }
            }
        }
        
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
