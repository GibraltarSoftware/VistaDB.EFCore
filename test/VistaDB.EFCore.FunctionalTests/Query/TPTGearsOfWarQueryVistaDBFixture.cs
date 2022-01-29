// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestModels.GearsOfWarModel;
using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.EntityFrameworkCore.FunctionalTests.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class TPTGearsOfWarQueryVistaDBFixture : TPTGearsOfWarQueryRelationalFixture
    {
        protected override ITestStoreFactory TestStoreFactory
            => VistaDBTestStoreFactory.Instance;

        protected override void OnModelCreating(ModelBuilder modelBuilder, DbContext context)
        {
            base.OnModelCreating(modelBuilder, context);

            modelBuilder.Entity<City>().Property(g => g.Location).HasColumnType("varchar(100)");
        }
    }
}
