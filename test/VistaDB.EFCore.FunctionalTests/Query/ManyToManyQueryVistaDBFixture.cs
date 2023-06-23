// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestUtilities;
using System.Threading.Tasks;
using VistaDB.EntityFrameworkCore.FunctionalTests.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class ManyToManyQueryVistaDBFixture : ManyToManyQueryRelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory
            => VistaDBTestStoreFactory.Instance;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }
    }
}
