using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata.Internal;

namespace EFCore.SqlCe.Metadata.Internal
{
    public static class VistaDBInternalMetadataBuilderExtensions
    {
        public static RelationalModelBuilderAnnotations VistaDB(
            [NotNull] this InternalModelBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalModelBuilderAnnotations(builder, configurationSource);

        public static RelationalPropertyBuilderAnnotations VistaDB(
            [NotNull] this InternalPropertyBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalPropertyBuilderAnnotations(builder, configurationSource);

        public static RelationalEntityTypeBuilderAnnotations VistaDB(
            [NotNull] this InternalEntityTypeBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalEntityTypeBuilderAnnotations(builder, configurationSource);

        public static RelationalKeyBuilderAnnotations VistaDB(
            [NotNull] this InternalKeyBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalKeyBuilderAnnotations(builder, configurationSource);

        public static RelationalIndexBuilderAnnotations VistaDB(
            [NotNull] this InternalIndexBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalIndexBuilderAnnotations(builder, configurationSource);

        public static RelationalForeignKeyBuilderAnnotations VistaDB(
            [NotNull] this InternalRelationshipBuilder builder,
            ConfigurationSource configurationSource)
            => new RelationalForeignKeyBuilderAnnotations(builder, configurationSource);
    }
}
