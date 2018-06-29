using JetBrains.Annotations;
using VistaDB.EFCore.Infrastructure.Internal;

namespace Microsoft.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     <para>
    ///         Allows VistaDB specific configuration to be performed on <see cref="DbContextOptions" />.
    ///     </para>
    ///     <para>
    ///         Instances of this class are returned from a call to
    ///         <see
    ///             cref="VistaDBDbContextOptionsBuilderExtensions.UseVistaDB(DbContextOptionsBuilder, string, System.Action{VistaDBDbContextOptionsBuilder})" />
    ///         and it is not designed to be directly constructed in your application code.
    ///     </para>
    /// </summary>
    public class VistaDBDbContextOptionsBuilder : RelationalDbContextOptionsBuilder<VistaDBDbContextOptionsBuilder, VistaDBOptionsExtension>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="SqliteDbContextOptionsBuilder" /> class.
        /// </summary>
        /// <param name="optionsBuilder"> The options builder. </param>
        public VistaDBDbContextOptionsBuilder([NotNull] DbContextOptionsBuilder optionsBuilder)
            : base(optionsBuilder)
        {
        }
    }
}