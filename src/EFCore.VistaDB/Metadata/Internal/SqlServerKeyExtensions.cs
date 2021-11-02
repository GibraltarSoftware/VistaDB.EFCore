// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.SqlServer.Internal;

namespace Microsoft.EntityFrameworkCore.Metadata.Internal
{
    /// <summary>
    ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
    ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
    ///     any release. You should only use it directly in your code with extreme caution and knowing that
    ///     doing so can result in application failures when updating to a new Entity Framework Core release.
    /// </summary>
    public static class SqlServerKeyExtensions
    {
        /// <summary>
        ///     This is an internal API that supports the Entity Framework Core infrastructure and not subject to
        ///     the same compatibility standards as public APIs. It may be changed or removed without notice in
        ///     any release. You should only use it directly in your code with extreme caution and knowing that
        ///     doing so can result in application failures when updating to a new Entity Framework Core release.
        /// </summary>
        public static bool AreCompatibleForSqlServer(
            [NotNull] this IKey key,
            [NotNull] IKey duplicateKey,
            in StoreObjectIdentifier storeObject,
            bool shouldThrow)
        {
            if (key.IsClustered(storeObject)
                != duplicateKey.IsClustered(storeObject))
            {
                if (shouldThrow)
                {
                    throw new InvalidOperationException(
                        SqlServerStrings.DuplicateKeyMismatchedClustering(
                            key.Properties.Format(),
                            key.DeclaringEntityType.DisplayName(),
                            duplicateKey.Properties.Format(),
                            duplicateKey.DeclaringEntityType.DisplayName(),
                            storeObject.DisplayName(),
                            key.GetName(storeObject)));
                }

                return false;
            }

            return true;
        }
    }
}
