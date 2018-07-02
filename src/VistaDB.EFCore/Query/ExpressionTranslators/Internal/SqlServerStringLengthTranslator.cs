using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.ExpressionTranslators;

namespace VistaDB.EFCore.Query.ExpressionTranslators.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class SqlServerStringLengthTranslator : IMemberTranslator
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public virtual Expression Translate(MemberExpression memberExpression)
            => memberExpression.Expression != null
               && memberExpression.Expression.Type == typeof(string)
               && memberExpression.Member.Name == nameof(string.Length)
                ? new ExplicitCastExpression(
                    new SqlFunctionExpression("LEN", memberExpression.Type, new[] { memberExpression.Expression }),
                    typeof(int))
                : null;
    }
}
