using System;
using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.EntityFrameworkCore.Diagnostics;
using VistaDB.Provider;
using VistaDB.EFCore.Infrastructure.Internal;

// ReSharper disable CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     SQL Server Compact specific extension methods for <see cref="DbContextOptionsBuilder"/>.
    /// </summary>
    public static class VistaDBDbContextOptionsExtensions
    {
        /// <summary>
        ///     Configures the context to connect to a VistaDB database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connectionString"> The connection string of the database to connect to. </param>
        /// <param name="vsiatDBOptionsAction">An optional action to allow additional SQL Server Compact specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseVistaDB(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] string connectionString,
            [CanBeNull] Action<VistaDBDbContextOptionsBuilder> vistaDBOptionsAction = null)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotEmpty(connectionString, nameof(connectionString));

            var extension = GetOrCreateExtension(optionsBuilder)
            .WithConnectionString(connectionString);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ConfigureWarnings(optionsBuilder);

            vistaDBOptionsAction?.Invoke(new VistaDBDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        ///     Configures the context to connect to a VistaDB database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connectionStringBuilder"> A connection string builder with the connection string of the database to connect to. </param>
        /// <param name="sqlCeOptionsAction">An optional action to allow additional SQL Server Compact specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseVistaDB(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] VistaDBConnectionStringBuilder connectionStringBuilder,
            [CanBeNull] Action<VistaDBDbContextOptionsBuilder> vistaDBOptionsAction = null)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotNull(connectionStringBuilder, nameof(connectionStringBuilder));

            var extension = GetOrCreateExtension(optionsBuilder)
            .WithConnectionString(connectionStringBuilder.ConnectionString);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ConfigureWarnings(optionsBuilder);

            vistaDBOptionsAction?.Invoke(new VistaDBDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        ///     Configures the context to connect to a VistaDB database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connectionStringBuilder"> A connection string builder with the connection string of the database to connect to. </param>
        /// <param name="vistaDBOptionsAction">An optional action to allow additional SQL Server Compact specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseVistaDB<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] VistaDBConnectionStringBuilder connectionStringBuilder,
            [CanBeNull] Action<VistaDBDbContextOptionsBuilder> vistaDBOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseVistaDB(
                (DbContextOptionsBuilder)optionsBuilder, connectionStringBuilder, vistaDBOptionsAction);
      
        /// <summary>
        ///     Configures the context to connect to a VistaDB database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connection">
        ///     An existing <see cref="DbConnection" /> to be used to connect to the database. If the connection is
        ///     in the open state then EF will not open or close the connection. If the connection is in the closed
        ///     state then EF will open and close the connection as needed.
        /// </param>
        /// <param name="vistaDBOptionsAction">An optional action to allow additional SQL Server Compact specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder UseVistaDB(
            [NotNull] this DbContextOptionsBuilder optionsBuilder,
            [NotNull] DbConnection connection,
            [CanBeNull] Action<VistaDBDbContextOptionsBuilder> vistaDBOptionsAction = null)
        {
            Check.NotNull(optionsBuilder, nameof(optionsBuilder));
            Check.NotNull(connection, nameof(connection));

            var extension = GetOrCreateExtension(optionsBuilder)
            .WithConnection(connection);
            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(extension);

            ConfigureWarnings(optionsBuilder);

            vistaDBOptionsAction?.Invoke(new VistaDBDbContextOptionsBuilder(optionsBuilder));

            return optionsBuilder;
        }

        /// <summary>
        ///     Configures the context to connect to a VistaDB database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connectionString"> The connection string of the database to connect to. </param>
        /// <param name="vistaDBOptionsAction">An optional action to allow additional SQL Server specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseVistaDB<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] string connectionString,
            [CanBeNull] Action<VistaDBDbContextOptionsBuilder> vistaDBOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseVistaDB(
                (DbContextOptionsBuilder)optionsBuilder, connectionString, vistaDBOptionsAction);

        /// <summary>
        ///     Configures the context to connect to a VistaDB database.
        /// </summary>
        /// <param name="optionsBuilder"> The builder being used to configure the context. </param>
        /// <param name="connection">
        ///     An existing <see cref="DbConnection" /> to be used to connect to the database. If the connection is
        ///     in the open state then EF will not open or close the connection. If the connection is in the closed
        ///     state then EF will open and close the connection as needed.
        /// </param>
        /// <param name="vistaDBOptionsAction">An optional action to allow additional SQL Server Compact specific configuration.</param>
        /// <returns> The options builder so that further configuration can be chained. </returns>
        public static DbContextOptionsBuilder<TContext> UseVistaDB<TContext>(
            [NotNull] this DbContextOptionsBuilder<TContext> optionsBuilder,
            [NotNull] DbConnection connection,
            [CanBeNull] Action<VistaDBDbContextOptionsBuilder> vistaDBOptionsAction = null)
            where TContext : DbContext
            => (DbContextOptionsBuilder<TContext>)UseVistaDB(
                (DbContextOptionsBuilder)optionsBuilder, connection, vistaDBOptionsAction);

        private static VistaDBOptionsExtension GetOrCreateExtension(DbContextOptionsBuilder optionsBuilder)
        {
            var existingExtension = optionsBuilder.Options.FindExtension<VistaDBOptionsExtension>();

            return existingExtension != null
                ? new VistaDBOptionsExtension(existingExtension)
                : new VistaDBOptionsExtension();
        }

        private static void ConfigureWarnings(DbContextOptionsBuilder optionsBuilder)
        {
            var coreOptionsExtension
                = optionsBuilder.Options.FindExtension<CoreOptionsExtension>()
                  ?? new CoreOptionsExtension();

            coreOptionsExtension = coreOptionsExtension.WithWarningsConfiguration(
                coreOptionsExtension.WarningsConfiguration.TryWithExplicit(
                    RelationalEventId.AmbientTransactionWarning, WarningBehavior.Throw));

            ((IDbContextOptionsBuilderInfrastructure)optionsBuilder).AddOrUpdateExtension(coreOptionsExtension);
        }
    }
}
