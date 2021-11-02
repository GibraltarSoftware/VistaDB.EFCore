// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using System.Collections.Generic;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.SqlServer.Metadata.Internal;

// ReSharper disable once CheckNamespace
namespace Microsoft.EntityFrameworkCore
{
    /// <summary>
    ///     Extension methods for <see cref="IIndex" /> for SQL Server-specific metadata.
    /// </summary>
    public static class SqlServerIndexExtensions
    {
        /// <summary>
        ///     Returns a value indicating whether the index is clustered.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> <see langword="true" /> if the index is clustered. </returns>
        public static bool? IsClustered([NotNull] this IIndex index)
            => (bool?)index[SqlServerAnnotationNames.Clustered];

        /// <summary>
        ///     Returns a value indicating whether the index is clustered.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <param name="storeObject"> The identifier of the store object. </param>
        /// <returns> <see langword="true" /> if the index is clustered. </returns>
        public static bool? IsClustered([NotNull] this IIndex index, in StoreObjectIdentifier storeObject)
        {
            var annotation = index.FindAnnotation(SqlServerAnnotationNames.Clustered);
            if (annotation != null)
            {
                return (bool?)annotation.Value;
            }

            return GetDefaultIsClustered(index, storeObject);
        }

        private static bool? GetDefaultIsClustered([NotNull] IIndex index, in StoreObjectIdentifier storeObject)
        {
            var sharedTableRootIndex = index.FindSharedObjectRootIndex(storeObject);
            return sharedTableRootIndex?.IsClustered(storeObject);
        }

        /// <summary>
        ///     Sets a value indicating whether the index is clustered.
        /// </summary>
        /// <param name="value"> The value to set. </param>
        /// <param name="index"> The index. </param>
        public static void SetIsClustered([NotNull] this IMutableIndex index, bool? value)
            => index.SetOrRemoveAnnotation(
                SqlServerAnnotationNames.Clustered,
                value);

        /// <summary>
        ///     Sets a value indicating whether the index is clustered.
        /// </summary>
        /// <param name="value"> The value to set. </param>
        /// <param name="index"> The index. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        /// <returns> The configured value. </returns>
        public static bool? SetIsClustered(
            [NotNull] this IConventionIndex index,
            bool? value,
            bool fromDataAnnotation = false)
        {
            index.SetOrRemoveAnnotation(
                SqlServerAnnotationNames.Clustered,
                value,
                fromDataAnnotation);

            return value;
        }

        /// <summary>
        ///     Returns the <see cref="ConfigurationSource" /> for whether the index is clustered.
        /// </summary>
        /// <param name="property"> The property. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for whether the index is clustered. </returns>
        public static ConfigurationSource? GetIsClusteredConfigurationSource([NotNull] this IConventionIndex property)
            => property.FindAnnotation(SqlServerAnnotationNames.Clustered)?.GetConfigurationSource();

        /// <summary>
        ///     Returns included property names, or <see langword="null" /> if they have not been specified.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> The included property names, or <see langword="null" /> if they have not been specified. </returns>
        public static IReadOnlyList<string> GetIncludeProperties([NotNull] this IIndex index)
            => (string[])index[SqlServerAnnotationNames.Include];

        /// <summary>
        ///     Sets included property names.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <param name="properties"> The value to set. </param>
        public static void SetIncludeProperties([NotNull] this IMutableIndex index, [NotNull] IReadOnlyList<string> properties)
            => index.SetOrRemoveAnnotation(
                SqlServerAnnotationNames.Include,
                properties);

        /// <summary>
        ///     Sets included property names.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        /// <param name="properties"> The value to set. </param>
        /// <returns> The configured property names. </returns>
        public static IReadOnlyList<string> SetIncludeProperties(
            [NotNull] this IConventionIndex index,
            [NotNull] IReadOnlyList<string> properties,
            bool fromDataAnnotation = false)
        {
            index.SetOrRemoveAnnotation(
                SqlServerAnnotationNames.Include,
                properties,
                fromDataAnnotation);

            return properties;
        }

        /// <summary>
        ///     Returns the <see cref="ConfigurationSource" /> for the included property names.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for the included property names. </returns>
        public static ConfigurationSource? GetIncludePropertiesConfigurationSource([NotNull] this IConventionIndex index)
            => index.FindAnnotation(SqlServerAnnotationNames.Include)?.GetConfigurationSource();

        /// <summary>
        ///     Returns a value indicating whether the index is online.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> <see langword="true" /> if the index is online. </returns>
        public static bool? IsCreatedOnline([NotNull] this IIndex index)
            => (bool?)index[SqlServerAnnotationNames.CreatedOnline];

        /// <summary>
        ///     Sets a value indicating whether the index is online.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <param name="createdOnline"> The value to set. </param>
        public static void SetIsCreatedOnline([NotNull] this IMutableIndex index, bool? createdOnline)
            => index.SetOrRemoveAnnotation(
                SqlServerAnnotationNames.CreatedOnline,
                createdOnline);

        /// <summary>
        ///     Sets a value indicating whether the index is online.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <param name="createdOnline"> The value to set. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        /// <returns> The configured value. </returns>
        public static bool? SetIsCreatedOnline(
            [NotNull] this IConventionIndex index,
            bool? createdOnline,
            bool fromDataAnnotation = false)
        {
            index.SetOrRemoveAnnotation(
                SqlServerAnnotationNames.CreatedOnline,
                createdOnline,
                fromDataAnnotation);

            return createdOnline;
        }

        /// <summary>
        ///     Returns the <see cref="ConfigurationSource" /> for whether the index is online.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for whether the index is online. </returns>
        public static ConfigurationSource? GetIsCreatedOnlineConfigurationSource([NotNull] this IConventionIndex index)
            => index.FindAnnotation(SqlServerAnnotationNames.CreatedOnline)?.GetConfigurationSource();

        /// <summary>
        ///     Returns a value indicating whether the index uses the fill factor.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> <see langword="true" /> if the index is online. </returns>
        public static int? GetFillFactor([NotNull] this IIndex index)
            => (int?)index[SqlServerAnnotationNames.FillFactor];

        /// <summary>
        ///     Sets a value indicating whether the index uses the fill factor.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <param name="fillFactor"> The value to set. </param>
        public static void SetFillFactor([NotNull] this IMutableIndex index, int? fillFactor)
        {
            if (fillFactor != null && (fillFactor <= 0 || fillFactor > 100))
            {
                throw new ArgumentOutOfRangeException(nameof(fillFactor));
            }

            index.SetOrRemoveAnnotation(
                SqlServerAnnotationNames.FillFactor,
                fillFactor);
        }

        /// <summary>
        ///     Defines a value indicating whether the index uses the fill factor.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <param name="fillFactor"> The value to set. </param>
        /// <param name="fromDataAnnotation"> Indicates whether the configuration was specified using a data annotation. </param>
        /// <returns> The configured value. </returns>
        public static int? SetFillFactor(
            [NotNull] this IConventionIndex index,
            int? fillFactor,
            bool fromDataAnnotation = false)
        {
            if (fillFactor != null && (fillFactor <= 0 || fillFactor > 100))
            {
                throw new ArgumentOutOfRangeException(nameof(fillFactor));
            }

            index.SetOrRemoveAnnotation(
                SqlServerAnnotationNames.FillFactor,
                fillFactor,
                fromDataAnnotation);

            return fillFactor;
        }

        /// <summary>
        ///     Returns the <see cref="ConfigurationSource" /> for whether the index uses the fill factor.
        /// </summary>
        /// <param name="index"> The index. </param>
        /// <returns> The <see cref="ConfigurationSource" /> for whether the index uses the fill factor. </returns>
        public static ConfigurationSource? GetFillFactorConfigurationSource([NotNull] this IConventionIndex index)
            => index.FindAnnotation(SqlServerAnnotationNames.FillFactor)?.GetConfigurationSource();
    }
}
