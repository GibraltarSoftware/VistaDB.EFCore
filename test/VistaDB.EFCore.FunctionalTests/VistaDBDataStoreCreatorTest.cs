﻿using System;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore.TestUtilities;
using Microsoft.Extensions.DependencyInjection;
using VistaDB.Diagnostic;
using VistaDB.EFCore.Storage.Internal;
using Xunit;

// ReSharper disable InconsistentNaming
namespace Microsoft.EntityFrameworkCore
{
    public class VistaDBDataStoreCreatorTest
    {
        [Fact]
        public async Task Exists_returns_false_when_database_doesnt_exist()
        {
            await Exists_returns_false_when_database_doesnt_exist_test(async: false);
        }

        [Fact]
        public async Task ExistsAsync_returns_false_when_database_doesnt_exist()
        {
            await Exists_returns_false_when_database_doesnt_exist_test(async: true);
        }

        private static async Task Exists_returns_false_when_database_doesnt_exist_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: false))
            {
                var creator = GetDatabaseCreator(testDatabase);

                Assert.False(async ? await creator.ExistsAsync() : creator.Exists());
            }
        }

        [Fact]
        public async Task Exists_returns_true_when_database_exists()
        {
            await Exists_returns_true_when_database_exists_test(async: false);
        }

        [Fact]
        public async Task ExistsAsync_returns_true_when_database_exists()
        {
            await Exists_returns_true_when_database_exists_test(async: true);
        }

        private static async Task Exists_returns_true_when_database_exists_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: true))
            {
                var creator = GetDatabaseCreator(testDatabase);

                Assert.True(async ? await creator.ExistsAsync() : creator.Exists());
            }
        }

        [Fact]
        public async Task HasTables_throws_when_database_doesnt_exist()
        {
            await HasTables_throws_when_database_doesnt_exist_test(async: false);
        }

        [Fact]
        public async Task HasTablesAsync_throws_when_database_doesnt_exist()
        {
            await HasTables_throws_when_database_doesnt_exist_test(async: true);
        }

        private static async Task HasTables_throws_when_database_doesnt_exist_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: false))
            {
                var creator = GetDatabaseCreator(testDatabase);

                var errorNumber = async
                    ? (await Assert.ThrowsAsync<VistaDBException>(() => ((TestDatabaseCreator)creator).HasTablesAsyncBase())).ErrorId
                    : Assert.Throws<VistaDBException>(() => ((TestDatabaseCreator)creator).HasTablesBase()).ErrorId;

                Assert.Equal(
                    117, // Cannot open database
                    errorNumber);
            }
        }

        [Fact]
        public async Task HasTables_returns_false_when_database_exists_but_has_no_tables()
        {
            await HasTables_returns_false_when_database_exists_but_has_no_tables_test(async: false);
        }

        [Fact]
        public async Task HasTablesAsync_returns_false_when_database_exists_but_has_no_tables()
        {
            await HasTables_returns_false_when_database_exists_but_has_no_tables_test(async: true);
        }

        private static async Task HasTables_returns_false_when_database_exists_but_has_no_tables_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: true))
            {
                var creator = GetDatabaseCreator(testDatabase);

                Assert.False(async ? await ((TestDatabaseCreator)creator).HasTablesAsyncBase() : ((TestDatabaseCreator)creator).HasTablesBase());
            }
        }

        [Fact]
        public async Task HasTables_returns_true_when_database_exists_and_has_any_tables()
        {
            await HasTables_returns_true_when_database_exists_and_has_any_tables_test(async: false);
        }

        [Fact]
        public async Task HasTablesAsync_returns_true_when_database_exists_and_has_any_tables()
        {
            await HasTables_returns_true_when_database_exists_and_has_any_tables_test(async: true);
        }

        private static async Task HasTables_returns_true_when_database_exists_and_has_any_tables_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: true))
            {
                testDatabase.ExecuteNonQuery("CREATE TABLE SomeTable (Id uniqueidentifier)");

                var creator = GetDatabaseCreator(testDatabase);

                Assert.True(async ? await ((TestDatabaseCreator)creator).HasTablesAsyncBase() : ((TestDatabaseCreator)creator).HasTablesBase());
            }
        }

        [Fact]
        public async Task Delete_will_delete_database()
        {
            await Delete_will_delete_database_test(async: false);
        }

        [Fact]
        public async Task DeleteAsync_will_delete_database()
        {
            await Delete_will_delete_database_test(async: true);
        }

        private static async Task Delete_will_delete_database_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: true))
            {
                testDatabase.CloseConnection();

                var creator = GetDatabaseCreator(testDatabase);

                Assert.True(async ? await creator.ExistsAsync() : creator.Exists());

                if (async)
                {
                    await creator.DeleteAsync();
                }
                else
                {
                    creator.Delete();
                }

                Assert.False(async ? await creator.ExistsAsync() : creator.Exists());
            }
        }

        [Fact]
        public async Task Delete_throws_when_database_doesnt_exist()
        {
            await Delete_throws_when_database_doesnt_exist_test(async: false);
        }

        [Fact]
        public async Task DeleteAsync_throws_when_database_doesnt_exist()
        {
            await Delete_throws_when_database_doesnt_exist_test(async: true);
        }

        private static async Task Delete_throws_when_database_doesnt_exist_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: false))
            {
                var creator = GetDatabaseCreator(testDatabase);

                if (async)
                {
                    await Assert.ThrowsAsync<VistaDBException>(() => creator.DeleteAsync());
                }
                else
                {
                    Assert.Throws<VistaDBException>(() => creator.Delete());
                }
            }
        }

        [Fact]
        public async Task CreateTables_creates_schema_in_existing_database()
        {
            await CreateTables_creates_schema_in_existing_database_test(async: false);
        }

        [Fact]
        public async Task CreateTablesAsync_creates_schema_in_existing_database()
        {
            await CreateTables_creates_schema_in_existing_database_test(async: true);
        }

        private static async Task CreateTables_creates_schema_in_existing_database_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: true))
            {
                var serviceCollection = new ServiceCollection();
                serviceCollection
                    .AddEntityFrameworkVistaDB();

                var serviceProvider = serviceCollection.BuildServiceProvider();

                var optionsBuilder = new DbContextOptionsBuilder();
                optionsBuilder.UseVistaDB(testDatabase.ConnectionString);

                using (var context = new BloggingContext(optionsBuilder.Options))
                {
                    var creator = (RelationalDatabaseCreator)context.GetService<IDatabaseCreator>();

                    if (async)
                    {
                        await creator.CreateTablesAsync();
                    }
                    else
                    {
                        creator.CreateTables();
                    }

                    if (testDatabase.ConnectionState != ConnectionState.Open)
                    {
                        await testDatabase.OpenConnectionAsync();
                    }

                    var tables = testDatabase.GetTableInfo();
                    Assert.Equal(1, tables.Count());
                    Assert.Equal("Blogs", tables.Single());

                    var columns = testDatabase.GetColumnInfo();
                    Assert.Equal(2, columns.Count());
                    Assert.True(columns.Any(c => c == "Blogs.Id (Int)"));
                    Assert.True(columns.Any(c => c == "Blogs.Name (NText)"));
                }
            }
        }

        [Fact]
        public async Task CreateTables_throws_if_database_does_not_exist()
        {
            await CreateTables_throws_if_database_does_not_exist_test(async: false);
        }

        [Fact]
        public async Task CreateTablesAsync_throws_if_database_does_not_exist()
        {
            await CreateTables_throws_if_database_does_not_exist_test(async: true);
        }

        private static async Task CreateTables_throws_if_database_does_not_exist_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: false))
            {
                var creator = GetDatabaseCreator(testDatabase);

                var errorNumber
                    = async
                        ? (await Assert.ThrowsAsync<VistaDBException>(() => creator.CreateTablesAsync())).ErrorId
                        : Assert.Throws<VistaDBException>(() => creator.CreateTables()).ErrorId;

                Assert.Equal(
                    117, // The database file cannot be found. Check the path to the database.
                    errorNumber);
            }
        }

        [Fact]
        public async Task Create_creates_physical_database_but_not_tables()
        {
            await Create_creates_physical_database_but_not_tables_test(async: false);
        }

        [Fact]
        public async Task CreateAsync_creates_physical_database_but_not_tables()
        {
            await Create_creates_physical_database_but_not_tables_test(async: true);
        }

        private static async Task Create_creates_physical_database_but_not_tables_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: false))
            {
                var creator = GetDatabaseCreator(testDatabase);

                Assert.False(creator.Exists());

                if (async)
                {
                    await creator.CreateAsync();
                }
                else
                {
                    creator.Create();
                }

                Assert.True(creator.Exists());

                if (testDatabase.ConnectionState != ConnectionState.Open)
                {
                    await testDatabase.OpenConnectionAsync();
                }

                Assert.Equal(0, testDatabase.GetColumnInfo().Count());

                Assert.True(testDatabase.Exists());
            }
        }

        [Fact]
        public async Task Create_throws_if_database_already_exists()
        {
            await Create_throws_if_database_already_exists_test(async: false);
        }

        [Fact]
        public async Task CreateAsync_throws_if_database_already_exists()
        {
            await Create_throws_if_database_already_exists_test(async: true);
        }

        private static async Task Create_throws_if_database_already_exists_test(bool async)
        {
            using (var testDatabase = VistaDBTestStore.CreateScratch(createDatabase: true))
            {
                var creator = GetDatabaseCreator(testDatabase);

                var errorNumber =
                        async
                        ? (await Assert.ThrowsAsync<VistaDBException>(() => creator.CreateAsync())).ErrorId
                        : Assert.Throws<VistaDBException>(() => creator.Create()).ErrorId;

                Assert.Equal(
                    113, // File already exists. 
                    errorNumber);
            }
        }

        private static IServiceProvider CreateContextServices(VistaDBTestStore testStore)
            => ((IInfrastructure<IServiceProvider>)new BloggingContext(
                new DbContextOptionsBuilder()
                    .UseVistaDB(testStore.ConnectionString)
                    .UseInternalServiceProvider(new ServiceCollection()
                        .AddEntityFrameworkVistaDB()
                        .AddScoped<IRelationalDatabaseCreator, TestDatabaseCreator>().BuildServiceProvider()).Options))
                .Instance;

        private static IRelationalDatabaseCreator GetDatabaseCreator(VistaDBTestStore testStore)
           => CreateContextServices(testStore).GetRequiredService<IRelationalDatabaseCreator>();

        private class BloggingContext : DbContext
        {
            public BloggingContext(DbContextOptions options)
                : base(options)
            {
            }

            public DbSet<Blog> Blogs { get; set; }

            public class Blog
            {
                public int Id { get; set; }
                public string Name { get; set; }
            }
        }

        public class TestDatabaseCreator : VistaDBDatabaseCreator
        {
            public TestDatabaseCreator(
                RelationalDatabaseCreatorDependencies dependencies,
                IVistaDBRelationalConnection connection)
                : base(dependencies, connection)
            {
            }

            public bool HasTablesBase() => HasTables();

            public Task<bool> HasTablesAsyncBase(CancellationToken cancellationToken = default(CancellationToken))
                => HasTablesAsync(cancellationToken);

            public IExecutionStrategyFactory ExecutionStrategyFactory => Dependencies.ExecutionStrategyFactory;
        }
    }
}
