using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Utilities;
using VistaDB.EntityFrameworkCore.Utilities;

// ReSharper disable once CheckNamespace

namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     VistaDB specific extension methods for metadata.
    /// </summary>
    public static class VistaDBMetadataExtensions
    {
        /// <summary>
        ///     Gets the VistaDB specific metadata for an entity.
        /// </summary>
        /// <param name="entityType"> The entity to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the entity. </returns>
        public static IRelationalEntityTypeAnnotations VistaDB([NotNull] this IEntityType entityType)
            => new RelationalEntityTypeAnnotations(Check.NotNull(entityType, nameof(entityType)));

        /// <summary>
        ///     Gets the VistaDB specific metadata for an entity.
        /// </summary>
        /// <param name="entityType"> The entity to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the entity. </returns>
        public static RelationalEntityTypeAnnotations VistaDB([NotNull] this IMutableEntityType entityType)
            => (RelationalEntityTypeAnnotations)VistaDB((IEntityType)entityType);

        /// <summary>
        ///     Gets the VistaDB specific metadata for a foreign key.
        /// </summary>
        /// <param name="foreignKey"> The foreign key to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the foreign key. </returns>
        public static IRelationalForeignKeyAnnotations VistaDB([NotNull] this IForeignKey foreignKey)
            => new RelationalForeignKeyAnnotations(Check.NotNull(foreignKey, nameof(foreignKey)));

        /// <summary>
        ///     Gets the VistaDB specific metadata for a foreign key.
        /// </summary>
        /// <param name="foreignKey"> The foreign key to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the foreign key. </returns>
        public static RelationalForeignKeyAnnotations VistaDB([NotNull] this IMutableForeignKey foreignKey)
            => (RelationalForeignKeyAnnotations)VistaDB((IForeignKey)foreignKey);

        /// <summary>
        ///     Gets the VistaDB specific metadata for an index.
        /// </summary>
        /// <param name="index"> The index to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the index. </returns>
        public static IRelationalIndexAnnotations VistaDB([NotNull] this IIndex index)
            => new RelationalIndexAnnotations(Check.NotNull(index, nameof(index)));

        /// <summary>
        ///     Gets the VistaDB specific metadata for an index.
        /// </summary>
        /// <param name="index"> The index to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the index. </returns>
        public static RelationalIndexAnnotations VistaDB([NotNull] this IMutableIndex index)
            => (RelationalIndexAnnotations)VistaDB((IIndex)index);

        /// <summary>
        ///     Gets the VistaDB specific metadata for a key.
        /// </summary>
        /// <param name="key"> The key to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the key. </returns>
        public static IRelationalKeyAnnotations VistaDB([NotNull] this IKey key)
            => new RelationalKeyAnnotations(Check.NotNull(key, nameof(key)));

        /// <summary>
        ///     Gets the VistaDB specific metadata for a key.
        /// </summary>
        /// <param name="key"> The key to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the key. </returns>
        public static RelationalKeyAnnotations VistaDB([NotNull] this IMutableKey key)
            => (RelationalKeyAnnotations)VistaDB((IKey)key);

        /// <summary>
        ///     Gets the VistaDB specific metadata for a model.
        /// </summary>
        /// <param name="model"> The model to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the model. </returns>
        public static RelationalModelAnnotations VistaDB([NotNull] this IMutableModel model)
            => (RelationalModelAnnotations)VistaDB((IModel)model);

        /// <summary>
        ///     Gets the VistaDB specific metadata for a model.
        /// </summary>
        /// <param name="model"> The model to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the model. </returns>
        public static IRelationalModelAnnotations VistaDB([NotNull] this IModel model)
            => new RelationalModelAnnotations(Check.NotNull(model, nameof(model)));

        /// <summary>
        ///     Gets the VistaDB specific metadata for a property.
        /// </summary>
        /// <param name="property"> The property to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the property. </returns>
        public static IRelationalPropertyAnnotations VistaDB([NotNull] this IProperty property)
            => new RelationalPropertyAnnotations(Check.NotNull(property, nameof(property)));

        /// <summary>
        ///     Gets the VistaDB specific metadata for a property.
        /// </summary>
        /// <param name="property"> The property to get metadata for. </param>
        /// <returns> The VistaDB specific metadata for the property. </returns>
        public static RelationalPropertyAnnotations VistaDB([NotNull] this IMutableProperty property)
            => (RelationalPropertyAnnotations)VistaDB((IProperty)property);
    }
}
