using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Utilities;
using Microsoft.Extensions.DependencyInjection;

namespace VistaDB.EFCore.Infrastructure.Internal
{
    public class VistaDBOptionsExtension : RelationalOptionsExtension
    {
        private long? _serviceProviderHash;

        public VistaDBOptionsExtension()
        {
        }

        public VistaDBOptionsExtension([NotNull] VistaDBOptionsExtension copyFrom)
            : base(copyFrom)
        {
        }

        protected override RelationalOptionsExtension Clone()
            => new VistaDBOptionsExtension(this);

        public override bool ApplyServices(IServiceCollection services)
        {
            Check.NotNull(services, nameof(services));

            services.AddEntityFrameworkVistaDB();

            return true;
        }
    }
}
