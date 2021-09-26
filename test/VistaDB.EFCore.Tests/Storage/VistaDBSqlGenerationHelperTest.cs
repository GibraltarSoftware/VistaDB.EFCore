using System;
using VistaDB.EntityFrameworkCore.Storage.Internal;
using Xunit;

namespace Microsoft.EntityFrameworkCore.Storage
{
    public class VistaDBSqlGenerationHelperTest : SqlGenerationHelperTestBase
    {
        public override void BatchSeparator_returns_separator()
        {
            Assert.Equal("GO" + Environment.NewLine + Environment.NewLine, CreateSqlGenerationHelper().BatchTerminator);
        }

        protected override ISqlGenerationHelper CreateSqlGenerationHelper()
            => new VistaDBSqlGenerationHelper(new RelationalSqlGenerationHelperDependencies());
    }
}
