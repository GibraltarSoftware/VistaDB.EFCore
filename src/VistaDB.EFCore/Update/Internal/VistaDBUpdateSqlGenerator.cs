using System;
using System.Globalization;
using System.Text;
using Microsoft.EntityFrameworkCore.Update;
using VistaDB.EntityFrameworkCore.Utilities;

namespace VistaDB.EntityFrameworkCore.Update.Internal
{
    //TODO verify and test!

    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class VistaDBUpdateSqlGenerator : UpdateSqlGenerator
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public VistaDBUpdateSqlGenerator([NotNull] UpdateSqlGeneratorDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override ResultSetMapping AppendSelectAffectedCountCommand(StringBuilder commandStringBuilder, string name, string schema, int commandPosition)
        {
            commandStringBuilder
                .Append("SELECT @@ROWCOUNT")
                .Append(SqlGenerationHelper.StatementTerminator).AppendLine()
                .AppendLine();

            return ResultSetMapping.LastInResultSet;
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override void AppendBatchHeader(StringBuilder commandStringBuilder)
            => commandStringBuilder
                .Append("SET NOCOUNT ON")
                .Append(SqlGenerationHelper.StatementTerminator).AppendLine();

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override void AppendIdentityWhereCondition(StringBuilder commandStringBuilder, ColumnModification columnModification)
        {
            SqlGenerationHelper.DelimitIdentifier(commandStringBuilder, columnModification.ColumnName);
            commandStringBuilder.Append(" = ");

            commandStringBuilder.Append("SCOPE_IDENTITY()");
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override void AppendRowsAffectedWhereCondition(StringBuilder commandStringBuilder, int expectedRowsAffected)
            => commandStringBuilder
                .Append("@@ROWCOUNT = ")
                .Append(expectedRowsAffected.ToString(CultureInfo.InvariantCulture));

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override string GenerateNextSequenceValueOperation(string name, string schema)
        {
            throw new NotSupportedException("Sequences are not supported");
        }
    }
}
