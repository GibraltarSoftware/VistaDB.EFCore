// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore.Migrations
{
    /// <summary>
    ///     SQL Server specific extension methods for <see cref="MigrationBuilder" />.
    /// </summary>
    public static class SqlServerMigrationBuilderExtensions
    {
        /// <summary>
        ///     <para>
        ///         Returns <see langword="true" /> if the database provider currently in use is the SQL Server provider.
        ///     </para>
        /// </summary>
        /// <param name="migrationBuilder">
        ///     The migrationBuilder from the parameters on <see cref="Migration.Up(MigrationBuilder)" /> or
        ///     <see cref="Migration.Down(MigrationBuilder)" />.
        /// </param>
        /// <returns> <see langword="true" /> if SQL Server is being used; <see langword="false" /> otherwise. </returns>
        public static bool IsSqlServer([NotNull] this MigrationBuilder migrationBuilder)
            => string.Equals(
                migrationBuilder.ActiveProvider,
                typeof(SqlServerOptionsExtension).Assembly.GetName().Name,
                StringComparison.Ordinal);
    }
}
