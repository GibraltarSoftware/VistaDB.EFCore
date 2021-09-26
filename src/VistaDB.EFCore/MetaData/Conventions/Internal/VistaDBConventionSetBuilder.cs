using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using Microsoft.EntityFrameworkCore.Metadata.Conventions.Internal;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;
using VistaDB.EntityFrameworkCore.Utilities;

namespace VistaDB.EntityFrameworkCore.MetaData.Conventions.Internal
{
    public class VistaDBConventionSetBuilder : RelationalConventionSetBuilder
    {
        public VistaDBConventionSetBuilder(
            [NotNull] RelationalConventionSetBuilderDependencies dependencies,
            [NotNull] ISqlGenerationHelper sqlGenerationHelper)
            : base(dependencies)
        {
        }

        public override ConventionSet AddConventions(ConventionSet conventionSet)
        {
            Check.NotNull(conventionSet, nameof(conventionSet));

            base.AddConventions(conventionSet);

            conventionSet.ModelInitializedConventions.Add(new RelationalMaxIdentifierLengthConvention(128));

            return conventionSet;
        }

        public static ConventionSet Build()
        {
            var serviceProvider = new ServiceCollection()
                .AddEntityFrameworkVistaDB()
                .AddDbContext<DbContext>(o => o.UseVistaDB("Data Source=_.vdb"))
                .BuildServiceProvider();

            using (var serviceScope = serviceProvider.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                using (var context = serviceScope.ServiceProvider.GetService<DbContext>())
                {
                    return ConventionSet.CreateConventionSet(context);
                }
            }
        }
    }
}