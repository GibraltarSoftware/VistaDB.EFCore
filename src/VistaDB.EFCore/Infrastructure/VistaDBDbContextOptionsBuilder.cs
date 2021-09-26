using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using VistaDB.EntityFrameworkCore.Infrastructure.Internal;
using VistaDB.EntityFrameworkCore.Utilities;

namespace VistaDB.EntityFrameworkCore.Infrastructure
{
    /// <summary>
    ///     <para>
    ///         Allows VistaDB specific configuration to be performed on <see cref="DbContextOptions" />.
    ///     </para>
    ///     <para>
    ///         Instances of this class are returned from a call to
    ///         <see
    ///             cref="VistaDBDbContextOptionsExtensions.UseVistaDB(DbContextOptionsBuilder, string, System.Action{VistaDBDbContextOptionsBuilder})" />
    ///         and it is not designed to be directly constructed in your application code.
    ///     </para>
    /// </summary>
    public class VistaDBDbContextOptionsBuilder : RelationalDbContextOptionsBuilder<VistaDBDbContextOptionsBuilder, VistaDBOptionsExtension>
    {
        /// <summary>
        ///     Initializes a new instance of the <see cref="VistaDBDbContextOptionsBuilder" /> class.
        /// </summary>
        /// <param name="optionsBuilder"> The options builder. </param>
        public VistaDBDbContextOptionsBuilder([NotNull] DbContextOptionsBuilder optionsBuilder)
            : base(optionsBuilder)
        {
        }
    }
}