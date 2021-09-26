using System.IO;
using Microsoft.EntityFrameworkCore.Utilities;
using VistaDB.EntityFrameworkCore.Utilities;
using VistaDB.Provider;

namespace VistaDB.EntityFrameworkCore.Storage.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static class VistaDBConnectionExtensions
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void CreateEmptyDatabase([NotNull] this VistaDBConnection connection)
        {
            Check.NotNull(connection, nameof(connection));

            using (var command = new VistaDBCommand())
            {
                command.Connection = connection;
                command.CommandText = $"CREATE DATABASE '{connection.DataSource}'";
                command.ExecuteNonQuery();
            }
            connection.Close();
            VistaDBConnection.ClearAllPools();
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static bool Exists([NotNull] this VistaDBConnection connection)
        {
            Check.NotNull(connection, nameof(connection));
            return File.Exists(connection.DataSource);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void Drop([NotNull] this VistaDBConnection connection, bool throwOnOpen = true)
        {
            Check.NotNull(connection, nameof(connection));

            if (throwOnOpen)
            {
                connection.Open();
            }
            connection.Close();
            VistaDBConnection.ClearAllPools();
            var path = connection.DataSource;
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }
    }
}
