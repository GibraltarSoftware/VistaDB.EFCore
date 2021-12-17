using System;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.EntityFrameworkCore.Internal;
using Xunit;

namespace Microsoft.EntityFrameworkCore
{
    public class VistaDBDatabaseFacadeExtensionsTest
    {
        [Fact]
        public void IsVistaDB_when_using_OnConfguring()
        {
            using (var context = new VistaDBOnConfiguringContext())
            {
                Assert.True(context.Database.IsVistaDB());
            }
        }

        [Fact]
        public void IsVistaDB_in_OnModelCreating_when_using_OnConfguring()
        {
            using (var context = new VistaDBOnModelContext())
            {
                var _ = context.Model; // Trigger context initialization
                Assert.True(context.IsVistaDBSet);
            }
        }

        [Fact]
        public void IsVistaDB_in_constructor_when_using_OnConfguring()
        {
            using (var context = new VistaDBConstructorContext())
            {
                var _ = context.Model; // Trigger context initialization
                Assert.True(context.IsVistaDBSet);
            }
        }

        [Fact]
        public void Cannot_use_IsVistaDB_in_OnConfguring()
        {
            using (var context = new VistaDBUseInOnConfiguringContext())
            {
                Assert.Equal(
                    CoreStrings.RecursiveOnConfiguring,
                    Assert.Throws<InvalidOperationException>(
                        () =>
                        {
                            var _ = context.Model; // Trigger context initialization
                        }).Message);
            }
        }

        [Fact]
        public void IsVistaDB_when_using_constructor()
        {
            using (var context = new ProviderContext(
                new DbContextOptionsBuilder().UseVistaDB("Database=Maltesers").Options))
            {
                Assert.True(context.Database.IsVistaDB());
            }
        }

        [Fact]
        public void IsVistaDB_in_OnModelCreating_when_using_constructor()
        {
            using (var context = new ProviderOnModelContext(
                new DbContextOptionsBuilder().UseVistaDB("Database=Maltesers").Options))
            {
                var _ = context.Model; // Trigger context initialization
                Assert.True(context.IsVistaDBSet);
            }
        }

        [Fact]
        public void IsVistaDB_in_constructor_when_using_constructor()
        {
            using (var context = new ProviderConstructorContext(
                new DbContextOptionsBuilder().UseVistaDB("Database=Maltesers").Options))
            {
                var _ = context.Model; // Trigger context initialization
                Assert.True(context.IsVistaDBSet);
            }
        }

        [Fact]
        public void Cannot_use_IsVistaDB_in_OnConfguring_with_constructor()
        {
            using (var context = new ProviderUseInOnConfiguringContext(
                new DbContextOptionsBuilder().UseVistaDB("Database=Maltesers").Options))
            {
                Assert.Equal(
                    CoreStrings.RecursiveOnConfiguring,
                    Assert.Throws<InvalidOperationException>(
                        () =>
                        {
                            var _ = context.Model; // Trigger context initialization
                        }).Message);
            }
        }

        [Fact(Skip = "wait for Functional tests reference")]
        public void Not_IsVistaDB_when_using_different_provider()
        {
            //using (var context = new ProviderContext(
            //    new DbContextOptionsBuilder().UseInMemoryDatabase("Maltesers").Options))
            //{
            //    Assert.False(context.Database.IsVistaDB());
            //}
        }

        private class ProviderContext : DbContext
        {
            protected ProviderContext()
            {
            }

            public ProviderContext(DbContextOptions options)
                : base(options)
            {
            }

            public bool? IsVistaDBSet { get; protected set; }
        }

        private class VistaDBOnConfiguringContext : ProviderContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => optionsBuilder.UseVistaDB("Database=Maltesers");
        }

        private class VistaDBOnModelContext : VistaDBOnConfiguringContext
        {
            protected override void OnModelCreating(ModelBuilder modelBuilder)
                => IsVistaDBSet = Database.IsVistaDB();
        }

        private class VistaDBConstructorContext : VistaDBOnConfiguringContext
        {
            public VistaDBConstructorContext()
                => IsVistaDBSet = Database.IsVistaDB();
        }

        private class VistaDBUseInOnConfiguringContext : VistaDBOnConfiguringContext
        {
            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                base.OnConfiguring(optionsBuilder);

                IsVistaDBSet = Database.IsVistaDB();
            }
        }

        private class ProviderOnModelContext : ProviderContext
        {
            public ProviderOnModelContext(DbContextOptions options)
                : base(options)
            {
            }

            protected override void OnModelCreating(ModelBuilder modelBuilder)
                => IsVistaDBSet = Database.IsVistaDB();
        }

        private class ProviderConstructorContext : ProviderContext
        {
            public ProviderConstructorContext(DbContextOptions options)
                : base(options)
                => IsVistaDBSet = Database.IsVistaDB();
        }

        private class ProviderUseInOnConfiguringContext : ProviderContext
        {
            public ProviderUseInOnConfiguringContext(DbContextOptions options)
                : base(options)
            {
            }

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
                => IsVistaDBSet = Database.IsVistaDB();
        }
    }
}
