using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Metadata;
using VistaDB.EntityFrameworkCore.Diagnostics;
using VistaDB.EntityFrameworkCore.Utilities;

namespace VistaDB.EntityFrameworkCore.Internal
{
    public static class VistaDBLoggerExtensions
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void SchemaConfiguredWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Model.Validation> diagnostics,
            [NotNull] IEntityType entityType,
            [NotNull] string schema)
        {
            var definition = VistaDBStrings.LogSchemaConfigured;

            var warningBehavior = definition.GetLogBehavior(diagnostics);
            // Checking for enabled here to avoid string formatting if not needed.
            if (warningBehavior != WarningBehavior.Ignore)
            {
                definition.Log(diagnostics, warningBehavior, entityType.Name, schema);
            }

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new EntityTypeSchemaEventData(
                        definition,
                        SchemaConfiguredWarning,
                        entityType,
                        schema));
            }
        }

        private static string SchemaConfiguredWarning(EventDefinitionBase definition, EventData payload)
        {
            var d = (EventDefinition<string, string>)definition;
            var p = (EntityTypeSchemaEventData)payload;
            return d.GenerateMessage(
                p.EntityType.Name,
                p.Schema);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void SequenceConfiguredWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Model.Validation> diagnostics,
            [NotNull] ISequence sequence)
        {
            var definition = VistaDBStrings.LogSequenceConfigured;

            var warningBehavior = definition.GetLogBehavior(diagnostics);

            definition.Log(diagnostics, warningBehavior, sequence.Name);

            if (diagnostics.DiagnosticSource.IsEnabled(definition.EventId.Name))
            {
                diagnostics.DiagnosticSource.Write(
                    definition.EventId.Name,
                    new SequenceEventData(
                        definition,
                        SequenceConfiguredWarning,
                        sequence));
            }
        }

        private static string SequenceConfiguredWarning(EventDefinitionBase definition, EventData payload)
        {
            var d = (EventDefinition<string>)definition;
            var p = (SequenceEventData)payload;
            return d.GenerateMessage(p.Sequence.Name);
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void ColumnFound(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string tableName,
            [CanBeNull] string columnName,
            [CanBeNull] string dataTypeName,
            bool notNull,
            [CanBeNull] string defaultValue)
            // No DiagnosticsSource events because these are purely design-time messages
            => VistaDBStrings.LogFoundColumn.Log(diagnostics, WarningBehavior.Log, tableName, columnName, dataTypeName, notNull, defaultValue);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void SchemasNotSupportedWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Scaffolding> diagnostics)
            // No DiagnosticsSource events because these are purely design-time messages
            => VistaDBStrings.LogUsingSchemaSelectionsWarning.Log(diagnostics, WarningBehavior.Log);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void ForeignKeyReferencesMissingTableWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string foreignKeyName)
            // No DiagnosticsSource events because these are purely design-time messages
            => VistaDBStrings.LogForeignKeyScaffoldErrorPrincipalTableNotFound.Log(diagnostics, WarningBehavior.Log, foreignKeyName);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void TableFound(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string tableName)
            // No DiagnosticsSource events because these are purely design-time messages
            => VistaDBStrings.LogFoundTable.Log(diagnostics,WarningBehavior.Log, tableName);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void MissingTableWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string tableName)
            // No DiagnosticsSource events because these are purely design-time messages
            => VistaDBStrings.LogMissingTable.Log(diagnostics, WarningBehavior.Log, tableName);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void ForeignKeyPrincipalColumnMissingWarning(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string foreignKeyName,
            [CanBeNull] string tableName,
            [CanBeNull] string principalColumnName,
            [CanBeNull] string principalTableName)
            // No DiagnosticsSource events because these are purely design-time messages
            => VistaDBStrings.LogPrincipalColumnNotFound.Log(diagnostics, WarningBehavior.Log, foreignKeyName, tableName, principalColumnName, principalTableName);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void IndexFound(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string indexName,
            [CanBeNull] string tableName,
            bool? unique)
            // No DiagnosticsSource events because these are purely design-time messages
            => VistaDBStrings.LogFoundIndex.Log(diagnostics, WarningBehavior.Log, indexName, tableName, unique);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void ForeignKeyFound(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string tableName,
            long id,
            [CanBeNull] string principalTableName,
            [CanBeNull] string deleteAction)
            // No DiagnosticsSource events because these are purely design-time messages
            => VistaDBStrings.LogFoundForeignKey.Log(diagnostics, WarningBehavior.Log, tableName, id, principalTableName, deleteAction);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void PrimaryKeyFound(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string primaryKeyName,
            [CanBeNull] string tableName)
            // No DiagnosticsSource events because these are purely design-time messages
            => VistaDBStrings.LogFoundPrimaryKey.Log(diagnostics, WarningBehavior.Log, primaryKeyName, tableName);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public static void UniqueConstraintFound(
            [NotNull] this IDiagnosticsLogger<DbLoggerCategory.Scaffolding> diagnostics,
            [CanBeNull] string uniqueConstraintName,
            [CanBeNull] string tableName)
            // No DiagnosticsSource events because these are purely design-time messages
            => VistaDBStrings.LogFoundUniqueConstraint.Log(diagnostics,  WarningBehavior.Log, uniqueConstraintName, tableName);
    }
}
