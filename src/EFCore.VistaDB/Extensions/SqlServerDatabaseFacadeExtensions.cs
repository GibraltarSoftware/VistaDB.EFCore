// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.SqlServer.Infrastructure.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     SQL Server specific extension methods for <see cref="DbContext.Database" />.
    /// </summary>
    public static class SqlServerDatabaseFacadeExtensions
    {
        /// <summary>
        ///     <para>
        ///         Returns <see langword="true" /> if the database provider currently in use is the SQL Server provider.
        ///     </para>
        ///     <para>
        ///         This method can only be used after the <see cref="DbContext" /> has been configured because
        ///         it is only then that the provider is known. This means that this method cannot be used
        ///         in <see cref="DbContext.OnConfiguring" /> because this is where application code sets the
        ///         provider to use as part of configuring the context.
        ///     </para>
        /// </summary>
        /// <param name="database"> The facade from <see cref="DbContext.Database" />. </param>
        /// <returns> <see langword="true" /> if SQL Server is being used; <see langword="false" /> otherwise. </returns>
        public static bool IsSqlServer([NotNull] this DatabaseFacade database)
            => database.ProviderName.Equals(
                typeof(SqlServerOptionsExtension).Assembly.GetName().Name,
                StringComparison.Ordinal);
    }
}
