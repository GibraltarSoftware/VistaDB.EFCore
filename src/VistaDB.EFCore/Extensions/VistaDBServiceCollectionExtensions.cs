using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore.Utilities;
using VistaDB.EFCore.Infrastructure.Internal;
using VistaDB.EFCore.Internal;
using VistaDB.EFCore.Metadata.Conventions.Internal;
using VistaDB.EFCore.Query.ExpressionTranslators.Internal;
using VistaDB.EFCore.Query.Sql.Internal;
using VistaDB.EFCore.Storage.Internal;
using VistaDB.EFCore.Update.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.Extensions.DependencyInjection
{
    /// <summary>
    ///     SQL Server specific extension methods for <see cref="IServiceCollection" />.
    /// </summary>
    public static class VistaDBServiceCollectionExtensions
    {
        /// <summary>
        ///     <para>
        ///         Adds the services required by the VistaDB database provider for Entity Framework
        ///         to an <see cref="IServiceCollection" />. You use this method when using dependency injection
        ///         in your application, such as with ASP.NET. For more information on setting up dependency
        ///         injection, see http://go.microsoft.com/fwlink/?LinkId=526890.
        ///     </para>
        ///     <para>
        ///         You only need to use this functionality when you want Entity Framework to resolve the services it uses
        ///         from an external dependency injection container. If you are not using an external
        ///         dependency injection container, Entity Framework will take care of creating the services it requires.
        ///     </para>
        /// </summary>
        /// <example>
        ///     <code>
        ///            public void ConfigureServices(IServiceCollection services)
        ///            {
        ///                var connectionString = "connection string to database";
        /// 
        ///                services
        ///                    .AddEntityFrameworkVistaDB()
        ///                    .AddDbContext&lt;MyContext&gt;((serviceProvider, options) =>
        ///                        options.UseVistaDB(connectionString)
        ///                               .UseInternalServiceProvider(serviceProvider));
        ///            }
        ///        </code>
        /// </example>
        /// <param name="serviceCollection"> The <see cref="IServiceCollection" /> to add services to. </param>
        /// <returns>
        ///     The same service collection so that multiple calls can be chained.
        /// </returns>
        public static IServiceCollection AddEntityFrameworkVistaDB([NotNull] this IServiceCollection serviceCollection)
        {
            Check.NotNull(serviceCollection, nameof(serviceCollection));

            var builder = new EntityFrameworkRelationalServicesBuilder(serviceCollection)
                .TryAdd<IDatabaseProvider, DatabaseProvider<VistaDBOptionsExtension>>()
                .TryAdd<IRelationalTypeMappingSource, VistaDBTypeMappingSource>()
                .TryAdd<ISqlGenerationHelper, VistaDBSqlGenerationHelper>()
                .TryAdd<IUpdateSqlGenerator, VistaDBUpdateSqlGenerator>()
                .TryAdd<ISingletonUpdateSqlGenerator, VistaDBUpdateSqlGenerator>()
                .TryAdd<IModificationCommandBatchFactory, VistaDBModificationCommandBatchFactory>()
                .TryAdd<IRelationalConnection>(p => p.GetService<IVistaDBRelationalConnection>())
                .TryAdd<ICompositeMethodCallTranslator, VistaDBCompositeMethodCallTranslator>()
                .TryAdd<IMemberTranslator, VistaDBCompositeMemberTranslator>()
                .TryAdd<IQuerySqlGeneratorFactory, VistaDBQuerySqlGeneratorFactory>()
                .TryAdd<IRelationalDatabaseCreator, VistaDBDatabaseCreator>()

                .TryAdd<IModelValidator, VistaDBModelValidator>()
                .TryAdd<IConventionSetBuilder, VistaDBConventionSetBuilder>()

                //.TryAdd<IMigrationsAnnotationProvider, SqlServerMigrationsAnnotationProvider>()
                //.TryAdd<IMigrationsSqlGenerator, SqlServerMigrationsSqlGenerator>()
                //.TryAdd<IHistoryRepository, SqlServerHistoryRepository>()
                
                .TryAddProviderSpecificServices(b => b
                    .TryAddScoped<IVistaDBRelationalConnection, VistaDBRelationalConnection>());

            builder.TryAddCoreServices();

            return serviceCollection;
        }
    }
}
