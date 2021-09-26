using Microsoft.EntityFrameworkCore.Query.Expressions;
using Microsoft.EntityFrameworkCore.Query.Sql;
using Microsoft.EntityFrameworkCore.Utilities;
using VistaDB.EntityFrameworkCore.Utilities;

namespace VistaDB.EntityFrameworkCore.Query.Sql.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class VistaDBQuerySqlGeneratorFactory : QuerySqlGeneratorFactoryBase
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public VistaDBQuerySqlGeneratorFactory(
            [NotNull] QuerySqlGeneratorDependencies dependencies)
            : base(dependencies)
        {
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override IQuerySqlGenerator CreateDefault(SelectExpression selectExpression)
            => new VistaDBQuerySqlGenerator(
                Dependencies,
                Check.NotNull(selectExpression, nameof(selectExpression)));
    }
}
