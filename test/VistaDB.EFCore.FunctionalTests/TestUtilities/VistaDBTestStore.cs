using System;
using System.Collections.Generic;
using System.Data.Common;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using VistaDB;
using VistaDB.DDA;
using VistaDB.EFCore.Storage.Internal;
using VistaDB.Provider;

namespace Microsoft.EntityFrameworkCore.TestUtilities
{
    public class VistaDBTestStore : RelationalTestStore
    {
        private const string NorthwindName = "Northwind";

        private static int _scratchCount;

        public static readonly string NorthwindConnectionString = CreateConnectionString(NorthwindName);

        public static VistaDBTestStore GetNorthwindStore()
            => GetOrCreateShared(NorthwindName, () => { });

        public static VistaDBTestStore GetOrCreateShared(string name, Action initializeDatabase) =>
            new VistaDBTestStore(name).CreateShared(initializeDatabase);

        public static VistaDBTestStore Create(string name)
            => VistaDBTestStore.CreateScratch(true);

        public static VistaDBTestStore CreateScratch(bool createDatabase)
        {
            string name;
            do
            {
                name = "scratch-" + Interlocked.Increment(ref _scratchCount);
            }
            while (File.Exists(name + ".vdb5"));

            return new VistaDBTestStore(name).CreateTransient(createDatabase);
        }

        public static Task<VistaDBTestStore> CreateScratchAsync(bool createDatabase = true)
        {
            return Task.FromResult(CreateScratch(createDatabase));
        }

        private bool _deleteDatabase;
        private string _name;

        private VistaDBTestStore(string name, bool shared = true) : base(name, shared)
        {
            _name = name;
        }

        protected override void Initialize(Func<DbContext> createContext, Action<DbContext> seed)
        {
            using (var context = createContext())
            {
                context.Database.EnsureCreated();
                seed(context);
            }
        }

        private VistaDBTestStore CreateShared(Action initializeDatabase)
        {
            ConnectionString = CreateConnectionString(_name);

            Connection = new VistaDBConnection(ConnectionString);

            if (!Exists())
            {
                initializeDatabase?.Invoke();
            }

            return this;
        }

        private VistaDBTestStore CreateTransient(bool createDatabase)
        {
            ConnectionString = CreateConnectionString(_name);

            Connection = new VistaDBConnection(ConnectionString);

            if (createDatabase)
            {
                ((VistaDBConnection)Connection).CreateEmptyDatabase();
                Connection.Open();
            }

            _deleteDatabase = true;

            return this;
        }

        public int ExecuteNonQuery(string sql, params object[] parameters)
        {
            using (var command = CreateCommand(sql, parameters))
            {
                return command.ExecuteNonQuery();
            }
        }

        public IEnumerable<T> Query<T>(string sql, params object[] parameters)
        {
            using (var command = CreateCommand(sql, parameters))
            {
                using (var dataReader = command.ExecuteReader())
                {
                    var results = Enumerable.Empty<T>();

                    while (dataReader.Read())
                    {
                        results = results.Concat(new[] { dataReader.GetFieldValue<T>(0) });
                    }

                    return results;
                }
            }
        }

        public IEnumerable<string> GetTableInfo()
        {
            var result = new List<string>();
            using (IVistaDBDDA DDAObj = VistaDBEngine.Connections.OpenDDA())
            {
                using (IVistaDBDatabase db1 = DDAObj.OpenDatabase((Connection as VistaDBConnection).DataSource, VistaDBDatabaseOpenMode.NonexclusiveReadWrite, null))
                {                    
                    return db1.GetTableNames();
                }
            }
        }

        public IEnumerable<string> GetColumnInfo()
        {
            var result = new List<string>();
            using (IVistaDBDDA DDAObj = VistaDBEngine.Connections.OpenDDA())
            {
                using (IVistaDBDatabase db = DDAObj.OpenDatabase((Connection as VistaDBConnection).DataSource, VistaDBDatabaseOpenMode.NonexclusiveReadWrite, null))
                {
                    foreach (var tableInfo in db.GetTableNames())
                    {
                        using (IVistaDBTableSchema tableSchema = db.TableSchema(tableInfo))
                        {
                            for (int i = 0; i < tableSchema.ColumnCount; i++)
                            {
                                var columnInfo = tableSchema[i];
                                result.Add($"{tableSchema.Name}.{columnInfo.Name} ({columnInfo.Type})");
                            }
                        }
                    }
                }
            }
            return result;
        }

        public bool Exists()
        {
            return ((VistaDBConnection)Connection).Exists();
        }

        private DbCommand CreateCommand(string commandText, object[] parameters)
        {
            var command = Connection.CreateCommand();

            command.CommandText = commandText;

            for (var i = 0; i < parameters.Length; i++)
            {
                command.Parameters.Add(new VistaDBParameter("p" + i, parameters[i]));
            }

            return command;
        }

        public override void Dispose()
        {
            if (_deleteDatabase)
            {
                ((VistaDBConnection)Connection).Drop(throwOnOpen: false);
            }
            Connection?.Dispose();
            base.Dispose();
        }

        public static string CreateConnectionString(string name)
        {
            return new VistaDBConnectionStringBuilder
            {
                DataSource = name + ".vdb5",
                OpenMode = VistaDB.VistaDBDatabaseOpenMode.NonexclusiveReadWrite
            }
            .ToString();
        }

        public override DbContextOptionsBuilder AddProviderOptions(DbContextOptionsBuilder builder)
            => builder.UseVistaDB(Connection);

        public override void Clean(DbContext context)
        {
        }
    }
}
