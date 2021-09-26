using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;
using VistaDB.EntityFrameworkCore.Utilities;

namespace VistaDB.EntityFrameworkCore.Query.ExpressionTranslators.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class VistaDBCompositeMemberTranslator : RelationalCompositeMemberTranslator
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public VistaDBCompositeMemberTranslator([NotNull] RelationalCompositeMemberTranslatorDependencies dependencies)
            : base(dependencies)
        {
            var sqlServerTranslators = new List<IMemberTranslator>
            {
                new SqlServerStringLengthTranslator(),
                new SqlServerDateTimeMemberTranslator()
            };

            AddTranslators(sqlServerTranslators);
        }
    }
}
