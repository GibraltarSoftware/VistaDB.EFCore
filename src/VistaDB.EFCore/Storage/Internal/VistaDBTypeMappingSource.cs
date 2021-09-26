using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Storage;
using VistaDB.EntityFrameworkCore.Utilities;

namespace VistaDB.EntityFrameworkCore.Storage.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class VistaDBTypeMappingSource : RelationalTypeMappingSource
    {
        private readonly FloatTypeMapping _real
            = new VistaDBFloatTypeMapping("real");

        private readonly ByteTypeMapping _byte
            = new VistaDBByteTypeMapping("tinyint", DbType.Byte);

        private readonly ShortTypeMapping _short
            = new VistaDBShortTypeMapping("smallint", DbType.Int16);

        private readonly LongTypeMapping _long
            = new VistaDBLongTypeMapping("bigint", DbType.Int64);

        private readonly VistaDBByteArrayTypeMapping _rowversion
            = new VistaDBByteArrayTypeMapping(
                "rowversion",
                DbType.Binary,
                size: 8,
                comparer: new ValueComparer<byte[]>(
                    (v1, v2) => StructuralComparisons.StructuralEqualityComparer.Equals(v1, v2),
                    v => StructuralComparisons.StructuralEqualityComparer.GetHashCode(v),
                    v => v == null ? null : v.ToArray()),
                storeTypePostfix: StoreTypePostfix.None);

        private readonly IntTypeMapping _int
            = new IntTypeMapping("int", DbType.Int32);

        private readonly BoolTypeMapping _bool
            = new BoolTypeMapping("bit");

        private readonly VistaDBStringTypeMapping _fixedLengthUnicodeString
            = new VistaDBStringTypeMapping("nchar", dbType: DbType.String, unicode: true, fixedLength: true);

        private readonly VistaDBStringTypeMapping _variableLengthUnicodeString
            = new VistaDBStringTypeMapping("nvarchar", dbType: null, unicode: true);

        private readonly VistaDBStringTypeMapping _fixedLengthAnsiString
            = new VistaDBStringTypeMapping("char", dbType: DbType.AnsiString, fixedLength: true);

        private readonly VistaDBStringTypeMapping _variableLengthAnsiString
            = new VistaDBStringTypeMapping("varchar", dbType: DbType.AnsiString);

        private readonly VistaDBByteArrayTypeMapping _variableLengthBinary
            = new VistaDBByteArrayTypeMapping("varbinary");

        private readonly VistaDBByteArrayTypeMapping _fixedLengthBinary
            = new VistaDBByteArrayTypeMapping("binary", fixedLength: true);

        private readonly VistaDBDateTimeTypeMapping _date
            = new VistaDBDateTimeTypeMapping("date", dbType: DbType.Date);

        private readonly VistaDBDateTimeTypeMapping _datetime
            = new VistaDBDateTimeTypeMapping("datetime", dbType: DbType.DateTime);

        private readonly VistaDBDateTimeTypeMapping _datetime2
            = new VistaDBDateTimeTypeMapping("datetime2", dbType: DbType.DateTime2);

        private readonly DoubleTypeMapping _double
            = new VistaDBDoubleTypeMapping("float");

        private readonly VistaDBDateTimeOffsetTypeMapping _datetimeoffset
            = new VistaDBDateTimeOffsetTypeMapping("datetimeoffset");

        private readonly GuidTypeMapping _uniqueidentifier
            = new GuidTypeMapping("uniqueidentifier", DbType.Guid);

        private readonly DecimalTypeMapping _decimal
            = new VistaDBDecimalTypeMapping("decimal(18, 2)", null, 18, 2);

        private readonly TimeSpanTypeMapping _time
            = new VistaDBTimeSpanTypeMapping("time");

        private readonly VistaDBStringTypeMapping _xml
            = new VistaDBStringTypeMapping("xml", dbType: null, unicode: true);

        private readonly Dictionary<Type, RelationalTypeMapping> _clrTypeMappings;

        private readonly Dictionary<string, RelationalTypeMapping> _storeTypeMappings;

        // These are disallowed only if specified without any kind of length specified in parenthesis.
        private readonly HashSet<string> _disallowedMappings
            = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
            {
                "binary",
                "binary varying",
                "varbinary",
                "char",
                "character",
                "char varying",
                "character varying",
                "varchar",
                "national char",
                "national character",
                "nchar",
                "national char varying",
                "national character varying",
                "nvarchar"
            };

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public VistaDBTypeMappingSource(
            [NotNull] TypeMappingSourceDependencies dependencies,
            [NotNull] RelationalTypeMappingSourceDependencies relationalDependencies)
            : base(dependencies, relationalDependencies)
        {
            _clrTypeMappings
                = new Dictionary<Type, RelationalTypeMapping>
                {
                    { typeof(int), _int },
                    { typeof(long), _long },
                    { typeof(DateTime), _datetime2 },
                    { typeof(Guid), _uniqueidentifier },
                    { typeof(bool), _bool },
                    { typeof(byte), _byte },
                    { typeof(double), _double },
                    { typeof(DateTimeOffset), _datetimeoffset },
                    { typeof(short), _short },
                    { typeof(float), _real },
                    { typeof(decimal), _decimal },
                    { typeof(TimeSpan), _time }
                };

            _storeTypeMappings
                = new Dictionary<string, RelationalTypeMapping>(StringComparer.OrdinalIgnoreCase)
                {
                    { "bigint", _long },
                    { "binary varying", _variableLengthBinary },
                    { "binary", _fixedLengthBinary },
                    { "bit", _bool },
                    { "char varying", _variableLengthAnsiString },
                    { "char", _fixedLengthAnsiString },
                    { "character varying", _variableLengthAnsiString },
                    { "character", _fixedLengthAnsiString },
                    { "date", _date },
                    { "datetime", _datetime },
                    { "datetime2", _datetime2 },
                    { "datetimeoffset", _datetimeoffset },
                    { "dec", _decimal },
                    { "decimal", _decimal },
                    { "double precision", _double },
                    { "float", _double },
                    { "image", _variableLengthBinary },
                    { "int", _int },
                    { "money", _decimal },
                    { "national char varying", _variableLengthUnicodeString },
                    { "national character varying", _variableLengthUnicodeString },
                    { "national character", _fixedLengthUnicodeString },
                    { "nchar", _fixedLengthUnicodeString },
                    { "ntext", _variableLengthUnicodeString },
                    { "numeric", _decimal },
                    { "nvarchar", _variableLengthUnicodeString },
                    { "real", _real },
                    { "rowversion", _rowversion },
                    { "smalldatetime", _datetime },
                    { "smallint", _short },
                    { "smallmoney", _decimal },
                    { "text", _variableLengthAnsiString },
                    { "time", _time },
                    { "timestamp", _rowversion },
                    { "tinyint", _byte },
                    { "uniqueidentifier", _uniqueidentifier },
                    { "varbinary", _variableLengthBinary },
                    { "varchar", _variableLengthAnsiString },
                    { "xml", _xml }
                };
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override void ValidateMapping(CoreTypeMapping mapping, IProperty property)
        {
            var relationalMapping = mapping as RelationalTypeMapping;

            if (_disallowedMappings.Contains(relationalMapping?.StoreType))
            {
                if (property == null)
                {
                    throw new ArgumentException($"Unqualified data type {relationalMapping.StoreType}");
                }

                throw new ArgumentException($"Unqualified data type {relationalMapping.StoreType} on property {property.Name}");
            }
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override RelationalTypeMapping FindMapping(in RelationalTypeMappingInfo mappingInfo)
            => FindRawMapping(mappingInfo)?.Clone(mappingInfo);

        private RelationalTypeMapping FindRawMapping(RelationalTypeMappingInfo mappingInfo)
        {
            var clrType = mappingInfo.ClrType;
            var storeTypeName = mappingInfo.StoreTypeName;
            var storeTypeNameBase = mappingInfo.StoreTypeNameBase;

            if (storeTypeName != null)
            {
                if (clrType == typeof(float)
                    && mappingInfo.Size != null
                    && mappingInfo.Size <= 24
                    && (storeTypeNameBase.Equals("float", StringComparison.OrdinalIgnoreCase)
                        || storeTypeNameBase.Equals("double precision", StringComparison.OrdinalIgnoreCase)))
                {
                    return _real;
                }

                if (_storeTypeMappings.TryGetValue(storeTypeName, out var mapping)
                    || _storeTypeMappings.TryGetValue(storeTypeNameBase, out mapping))
                {
                    return clrType == null
                           || mapping.ClrType == clrType
                        ? mapping
                        : null;
                }
            }

            if (clrType != null)
            {
                if (_clrTypeMappings.TryGetValue(clrType, out var mapping))
                {
                    return mapping;
                }

                if (clrType == typeof(string))
                {
                    var isAnsi = mappingInfo.IsUnicode == false;
                    var isFixedLength = mappingInfo.IsFixedLength == true;
                    var baseName = (isAnsi ? "" : "n") + (isFixedLength ? "char" : "varchar");
                    var maxSize = isAnsi ? 8000 : 4000;

                    var size = mappingInfo.Size ?? (mappingInfo.IsKeyOrIndex ? (int?)(isAnsi ? 900 : 450) : null);
                    if (size > maxSize)
                    {
                        size = null;
                    }

                    var dbType = isAnsi
                        ? (isFixedLength ? DbType.AnsiStringFixedLength : DbType.AnsiString)
                        : (isFixedLength ? DbType.StringFixedLength : (DbType?)null);

                    return new VistaDBStringTypeMapping(
                        baseName + "(" + (size == null ? "max" : size.ToString()) + ")",
                        dbType,
                        !isAnsi,
                        size,
                        isFixedLength);
                }

                if (clrType == typeof(byte[]))
                {
                    if (mappingInfo.IsRowVersion == true)
                    {
                        return _rowversion;
                    }

                    var size = mappingInfo.Size ?? (mappingInfo.IsKeyOrIndex ? (int?)900 : null);
                    if (size > 8000)
                    {
                        size = null;
                    }

                    var isFixedLength = mappingInfo.IsFixedLength == true;

                    return new VistaDBByteArrayTypeMapping(
                        (isFixedLength ? "binary(" : "varbinary(") + (size == null ? "max" : size.ToString()) + ")",
                        DbType.Binary,
                        size);
                }
            }

            return null;
        }
    }
}
