using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Internal;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using Microsoft.EntityFrameworkCore.Query.ExpressionVisitors;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Update;
using Microsoft.EntityFrameworkCore.Update.Internal;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.EntityFrameworkCore.ValueGeneration;
using VistaDB.EFCore.Infrastructure.Internal;
using VistaDB.EFCore.Storage.Internal;

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
                //.TryAdd<IRelationalTypeMappingSource, SqlServerTypeMappingSource>()
                //.TryAdd<ISqlGenerationHelper, SqlServerSqlGenerationHelper>()
                //.TryAdd<IMigrationsAnnotationProvider, SqlServerMigrationsAnnotationProvider>()
                //.TryAdd<IModelValidator, SqlServerModelValidator>()
                //.TryAdd<IConventionSetBuilder, SqlServerConventionSetBuilder>()
                //.TryAdd<IUpdateSqlGenerator>(p => p.GetService<ISqlServerUpdateSqlGenerator>())
                //.TryAdd<ISingletonUpdateSqlGenerator>(p => p.GetService<ISqlServerUpdateSqlGenerator>())
                //.TryAdd<IModificationCommandBatchFactory, SqlServerModificationCommandBatchFactory>()
                
                .TryAdd<IRelationalConnection, VistaDBRelationalConnection>();

                //.TryAdd<IMigrationsSqlGenerator, SqlServerMigrationsSqlGenerator>()
                //.TryAdd<IRelationalDatabaseCreator, VistaDBDatabaseCreator>()

                //.TryAdd<IHistoryRepository, SqlServerHistoryRepository>()
                //.TryAdd<IQueryCompilationContextFactory, SqlServerQueryCompilationContextFactory>()
                //.TryAdd<IMemberTranslator, SqlServerCompositeMemberTranslator>()
                //.TryAdd<ICompositeMethodCallTranslator, SqlServerCompositeMethodCallTranslator>()
                //.TryAdd<IQuerySqlGeneratorFactory, SqlServerQuerySqlGeneratorFactory>()
                //.TryAdd<ISqlTranslatingExpressionVisitorFactory, SqlServerSqlTranslatingExpressionVisitorFactory>()
                //.TryAddProviderSpecificServices(
                //    b => b
                ////        .TryAddSingleton<ISqlServerUpdateSqlGenerator, SqlServerUpdateSqlGenerator>()
                //        .TryAddScoped<IRelationalConnection, VistaDBRelationalConnection>()
                //        );

            builder.TryAddCoreServices();

            return serviceCollection;
        }
    }
}
