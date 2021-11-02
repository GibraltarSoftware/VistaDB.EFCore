// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Data.Common;
using System.Threading;
using System.Threading.Tasks;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.SqlServer.Internal;
using Microsoft.EntityFrameworkCore.Storage;

namespace Microsoft.EntityFrameworkCore.SqlServer.Storage.Internal
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public class SqlServerTransaction : RelationalTransaction
    {
        private static readonly bool _useOldBehavior
            = AppContext.TryGetSwitch("Microsoft.EntityFrameworkCore.Issue23305", out var enabled) && enabled;

        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public SqlServerTransaction(
            [NotNull] IRelationalConnection connection,
            [NotNull] DbTransaction transaction,
            Guid transactionId,
            [NotNull] IDiagnosticsLogger<DbLoggerCategory.Database.Transaction> logger,
            bool transactionOwned)
            : base(connection, transaction, transactionId, logger, transactionOwned)
        {
        }

        /// <inheritdoc />
        protected override string GetCreateSavepointSql(string name)
            => "SAVE TRANSACTION " + name;

        /// <inheritdoc />
        protected override string GetRollbackToSavepointSql(string name)
            => "ROLLBACK TRANSACTION " + name;

        /// <inheritdoc />
        public override bool SupportsSavepoints
        {
            get
            {
                if (_useOldBehavior)
                {
                    return base.SupportsSavepoints;
                }

                if (Connection is ISqlServerConnection sqlServerConnection && sqlServerConnection.IsMultipleActiveResultSetsEnabled)
                {
                    Logger.SavepointsDisabledBecauseOfMARS();

                    return false;
                }

                return true;
            }
        }

        // SQL Server doesn't support releasing savepoints. Override to do nothing.

        /// <inheritdoc />
        public override void ReleaseSavepoint(string name) { }

        /// <inheritdoc />
        public override Task ReleaseSavepointAsync(string name, CancellationToken cancellationToken = default)
            => Task.CompletedTask;
    }
}
