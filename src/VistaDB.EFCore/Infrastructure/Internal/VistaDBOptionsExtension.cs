using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;
using VistaDB.EntityFrameworkCore.Utilities;

namespace VistaDB.EntityFrameworkCore.Infrastructure.Internal
{
    /// <summary>
    ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
    ///     directly from your code. This API may change or be removed in future releases.
    /// </summary>
    public class VistaDBOptionsExtension : RelationalOptionsExtension
    {
        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public VistaDBOptionsExtension()
        {
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public VistaDBOptionsExtension([NotNull] VistaDBOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        protected override RelationalOptionsExtension Clone()
            => new VistaDBOptionsExtension(this);

        /// <summary>
        ///     This API supports the Entity Framework Core infrastructure and is not intended to be used
        ///     directly from your code. This API may change or be removed in future releases.
        /// </summary>
        public override bool ApplyServices(IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            services.AddEntityFrameworkVistaDB();

            return true;
        }
    }
}
