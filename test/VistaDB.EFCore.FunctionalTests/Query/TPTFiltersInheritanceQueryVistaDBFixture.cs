// Copyright (c) .NET Foundation. All rights reserved.
// Licensed under the Apache License, Version 2.0. See License.txt in the project root for license information.

using System.Threading.Tasks;

namespace Microsoft.EntityFrameworkCore.Query
{
    public class TPTFiltersInheritanceQueryVistaDBFixture : TPTInheritanceQueryVistaDBFixture
    {
        protected override bool EnableFilters
            => true;

        public override async Task InitializeAsync()
        {
            await base.InitializeAsync();
        }
    }
}
