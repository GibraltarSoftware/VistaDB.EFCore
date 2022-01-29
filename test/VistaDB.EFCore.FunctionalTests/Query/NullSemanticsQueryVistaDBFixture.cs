// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using Microsoft.EntityFrameworkCore.TestUtilities;
using VistaDB.EntityFrameworkCore.FunctionalTests.TestUtilities;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class NullSemanticsQueryVistaDBFixture : NullSemanticsQueryFixtureBase
    {
        protected override ITestStoreFactory TestStoreFactory
            => VistaDBTestStoreFactory.Instance;
    }
}
