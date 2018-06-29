using System.Data.Common;
using JetBrains.Annotations;
using Microsoft.EntityFrameworkCore.Storage;
using VistaDB.Provider;

namespace VistaDB.EFCore.Storage.Internal
{
    public class VistaDBRelationalConnection : RelationalConnection
    {
        public VistaDBRelationalConnection([NotNull] RelationalConnectionDependencies dependencies)
            : base(dependencies)
        {
        }

        protected override DbConnection CreateDbConnection() => new  VistaDBConnection(ConnectionString);

        public override bool IsMultipleActiveResultSetsEnabled => true;
    }
}
