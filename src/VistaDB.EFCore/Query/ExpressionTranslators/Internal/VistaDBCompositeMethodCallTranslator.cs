using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace VistaDB.EFCore.Query.ExpressionTranslators.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class VistaDBCompositeMethodCallTranslator : RelationalCompositeMethodCallTranslator
    {
        private static readonly IMethodCallTranslator[] _methodCallTranslators =
        {
            //TODO Implement and test!

            //new SqlServerContainsOptimizedTranslator(),
            //new SqlServerConvertTranslator(),
            //new SqlServerDateAddTranslator(),
            //new SqlServerDateDiffTranslator(),
            //new SqlServerEndsWithOptimizedTranslator(),
            //new SqlServerMathTranslator(),
            //new SqlServerNewGuidTranslator(),
            //new SqlServerObjectToStringTranslator(),
            //new SqlServerStartsWithOptimizedTranslator(),
            //new SqlServerStringIsNullOrWhiteSpaceTranslator(),
            //new SqlServerStringReplaceTranslator(),
            //new SqlServerStringSubstringTranslator(),
            //new SqlServerStringToLowerTranslator(),
            //new SqlServerStringToUpperTranslator(),
            //new SqlServerStringTrimEndTranslator(),
            //new SqlServerStringTrimStartTranslator(),
            //new SqlServerStringTrimTranslator(),
            //new SqlServerStringIndexOfTranslator()
        };

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public VistaDBCompositeMethodCallTranslator(
            [NotNull] RelationalCompositeMethodCallTranslatorDependencies dependencies)
            : base(dependencies)
        {
            // ReSharper disable once DoNotCallOverridableMethodsInConstructor
            AddTranslators(_methodCallTranslators);
        }
    }
}
