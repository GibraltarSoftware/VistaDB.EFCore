﻿// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using VistaDB.EntityFrameworkCore.FunctionalTests.TestUtilities;

namespace VistaDB.EntityFrameworkCore.FunctionalTests
{
    public class VistaDBFixture : ServiceProviderFixtureBase
    {
        public static IServiceProvider DefaultServiceProvider { get; }
            = new ServiceCollection().AddEntityFrameworkVistaDB().BuildServiceProvider();

        public TestSqlLoggerFactory TestSqlLoggerFactory
            => (TestSqlLoggerFactory)ServiceProvider.GetRequiredService<ILoggerFactory>();

        protected override ITestStoreFactory TestStoreFactory
            => VistaDBTestStoreFactory.Instance;

        /*
        public override DbContextOptionsBuilder AddOptions(DbContextOptionsBuilder builder)
            => base.AddOptions(builder).ConfigureWarnings(
                w =>
                {
                    w.Log(SqlServerEventId.ByteIdentityColumnWarning);
                    w.Log(SqlServerEventId.DecimalTypeKeyWarning);
                });
        */
    }
}
