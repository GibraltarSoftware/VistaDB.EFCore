﻿using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.Extensions.Logging;
using VistaDB.EntityFrameworkCore.Utilities;

namespace VistaDB.EntityFrameworkCore.Diagnostics
{
    /// <summary>
    ///		This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public static class VistaDBStrings
    {
        /// <summary>
        ///     VistaDB does not support this migration operation ('{operation}'). For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.
        /// </summary>
        public static string InvalidMigrationOperation([CanBeNull] object operation)
            => string.Format(
                GetString("VistaDB does not support this migration operation ('{operation}'). For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.", nameof(operation)),
                operation);

        /// <summary>
        ///     Generating idempotent scripts for migration is not currently supported by VistaDB. For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.
        /// </summary>
        public static string MigrationScriptGenerationNotSupported
            => GetString("Generating idempotent scripts for migration is not currently supported by VistaDB. For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.");

        /// <summary>
        ///     The entity type '{entityType}' is configured to use schema '{schema}'. VistaDB does not support schemas. This configuration will be ignored by the SQL Compact provider.
        /// </summary>
        public static readonly EventDefinition<string, string> LogSchemaConfigured
            = new EventDefinition<string, string>(
                VistaDBEventId.SchemaConfiguredWarning,
                LogLevel.Warning,
                LoggerMessage.Define<string, string>(
                    LogLevel.Warning,
                    VistaDBEventId.SchemaConfiguredWarning,
                    "The entity type '{entityType}' is configured to use schema '{schema}'. VistaDB does not support schemas. This configuration will be ignored by the SQL Compact provider"));

        /// <summary>
        ///     The model was configured with the database sequence '{sequence}'. VistaDB does not support sequences.
        /// </summary>
        public static readonly EventDefinition<string> LogSequenceConfigured
            = new EventDefinition<string>(
                VistaDBEventId.SequenceConfiguredWarning,
                LogLevel.Warning,
                LoggerMessage.Define<string>(
                    LogLevel.Warning,
                    VistaDBEventId.SequenceConfiguredWarning,
                    "The model was configured with the database sequence '{sequence}'. VistaDB does not support sequences."));

        /// <summary>
        ///     VistaDB does not support sequences. For more information, see http://go.microsoft.com/fwlink/?LinkId=723262.
        /// </summary>
        public static string SequencesNotSupported
            => "VistaDB does not support this migration operation ('{0}').";

        /// <summary>
        ///     VistaDB doesn't support schemas. The specified schema selection arguments will be ignored.
        /// </summary>
        public static readonly EventDefinition LogUsingSchemaSelectionsWarning
            = new EventDefinition(
                VistaDBEventId.SchemasNotSupportedWarning,
                LogLevel.Warning,
                LoggerMessage.Define(
                    LogLevel.Warning,
                    VistaDBEventId.SchemasNotSupportedWarning,
                    "VistaDB doesn't support schemas. The specified schema selection arguments will be ignored."));

        /// <summary>
        ///     Found column on table: {tableName}, column name: {columnName}, data type: {dataType}, not nullable: {isNotNullable}, default value: {defaultValue}.
        /// </summary>
        public static readonly EventDefinition<string, string, string, bool, string> LogFoundColumn
            = new EventDefinition<string, string, string, bool, string>(
                VistaDBEventId.ColumnFound,
                LogLevel.Debug,
                LoggerMessage.Define<string, string, string, bool, string>(
                    LogLevel.Debug,
                    VistaDBEventId.ColumnFound,
                    "Found column on table: {tableName}, column name: {columnName}, data type: {dataType}, not nullable: {isNotNullable}, default value: {defaultValue}."));

        /// <summary>
        ///     Found foreign key on table: {tableName}, id: {id}, principal table: {principalTableName}, delete action: {deleteAction}.
        /// </summary>
        public static readonly EventDefinition<string, long, string, string> LogFoundForeignKey
            = new EventDefinition<string, long, string, string>(
                VistaDBEventId.ForeignKeyFound,
                LogLevel.Debug,
                LoggerMessage.Define<string, long, string, string>(
                    LogLevel.Debug,
                    VistaDBEventId.ForeignKeyFound,
                    "Found foreign key on table: {tableName}, id: {id}, principal table: {principalTableName}, delete action: {deleteAction}."));

        /// <summary>
        ///     Could not scaffold the foreign key '{foreignKeyName}'. The referenced table could not be found. This most likely occurred because the referenced table was excluded from scaffolding.
        /// </summary>
        public static readonly EventDefinition<string> LogForeignKeyScaffoldErrorPrincipalTableNotFound
            = new EventDefinition<string>(
                VistaDBEventId.ForeignKeyReferencesMissingTableWarning,
                LogLevel.Warning,
                LoggerMessage.Define<string>(
                    LogLevel.Warning,
                    VistaDBEventId.ForeignKeyReferencesMissingTableWarning,
                    "Could not scaffold the foreign key '{foreignKeyName}'. The referenced table could not be found. This most likely occurred because the referenced table was excluded from scaffolding."));

        /// <summary>
        ///     Found table with name: {name}.
        /// </summary>
        public static readonly EventDefinition<string> LogFoundTable
            = new EventDefinition<string>(
                VistaDBEventId.TableFound,
                LogLevel.Debug,
                LoggerMessage.Define<string>(
                    LogLevel.Debug,
                    VistaDBEventId.TableFound,
                    "Found table with name: {name}."));

        /// <summary>
        ///     Unable to find a table in the database matching the selected table {table}.
        /// </summary>
        public static readonly EventDefinition<string> LogMissingTable
            = new EventDefinition<string>(
                VistaDBEventId.MissingTableWarning,
                LogLevel.Warning,
                LoggerMessage.Define<string>(
                    LogLevel.Warning,
                    VistaDBEventId.MissingTableWarning,
                    "Unable to find a table in the database matching the selected table {table}."));

        /// <summary>
        ///     For foreign key with identity {id} on table {tableName}, unable to find the column called {principalColumnName} on the foreign key's principal table, {principaltableName}. Skipping foreign key.
        /// </summary>
        public static readonly EventDefinition<string, string, string, string> LogPrincipalColumnNotFound
            = new EventDefinition<string, string, string, string>(
                VistaDBEventId.ForeignKeyPrincipalColumnMissingWarning,
                LogLevel.Warning,
                LoggerMessage.Define<string, string, string, string>(
                    LogLevel.Warning,
                    VistaDBEventId.ForeignKeyPrincipalColumnMissingWarning,
                    "For foreign key with identity {id} on table {tableName}, unable to find the column called {principalColumnName} on the foreign key's principal table, {principaltableName}. Skipping foreign key."));

        /// <summary>
        ///     Found index with name: {indexName}, table: {tableName}, is unique: {isUnique}.
        /// </summary>
        public static readonly EventDefinition<string, string, bool?> LogFoundIndex
            = new EventDefinition<string, string, bool?>(
                VistaDBEventId.IndexFound,
                LogLevel.Debug,
                LoggerMessage.Define<string, string, bool?>(
                    LogLevel.Debug,
                    VistaDBEventId.IndexFound,
                    "Found index with name: {indexName}, table: {tableName}, is unique: {isUnique}."));

        /// <summary>
        ///     Found primary key with name: {primaryKeyName}, table: {tableName}.
        /// </summary>
        public static readonly EventDefinition<string, string> LogFoundPrimaryKey
            = new EventDefinition<string, string>(
                VistaDBEventId.PrimaryKeyFound,
                LogLevel.Debug,
                LoggerMessage.Define<string, string>(
                    LogLevel.Debug,
                    VistaDBEventId.PrimaryKeyFound,
                    "Found primary key with name: {primaryKeyName}, table: {tableName}."));

        /// <summary>
        ///     Found unique constraint with name: {uniqueConstraintName}, table: {tableName}.
        /// </summary>
        public static readonly EventDefinition<string, string> LogFoundUniqueConstraint
            = new EventDefinition<string, string>(
                VistaDBEventId.UniqueConstraintFound,
                LogLevel.Debug,
                LoggerMessage.Define<string, string>(
                    LogLevel.Debug,
                    VistaDBEventId.UniqueConstraintFound,
                    "Found unique constraint with name: {uniqueConstraintName}, table: {tableName}."));

        private static string GetString(string value, params string[] formatterNames)
        {
            for (var i = 0; i < formatterNames.Length; i++)
            {
                value = value.Replace("{" + formatterNames[i] + "}", "{" + i + "}");
            }

            return value;
        }
    }
}
