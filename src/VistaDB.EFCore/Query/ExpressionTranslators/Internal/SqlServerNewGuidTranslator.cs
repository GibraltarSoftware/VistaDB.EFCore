using System;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace VistaDB.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlServerNewGuidTranslator : SingleOverloadStaticMethodCallTranslator
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public SqlServerNewGuidTranslator()
            : base(typeof(Guid), nameof(Guid.NewGuid), "NEWID")
        {
        }
    }
}
