using System;
using Loupe.Agent.Core.Services;
using Loupe.Agent.EntityFrameworkCore;
using Loupe.Extensions.Logging;
using Microsoft.Extensions.Hosting;
using Xunit;

namespace VistaDB.EntityFrameworkCore.FunctionalTests
{
    public class SetupFixture: IDisposable
    {
        public const string CollectionName = "VistaDB collection";

        private readonly IHost _host;
        public SetupFixture()
        {
            var hostSetup = Host.CreateDefaultBuilder()
                .AddLoupe(builder => builder.AddEntityFrameworkCoreDiagnostics())
                .AddLoupeLogging();

            _host = hostSetup.Build();

            var task = _host.StartAsync();
            var results = task.Status;

            VistaDB.Loupe.Logger.Register();
        }

        /// <inheritdoc />
        public void Dispose()
        {
            _host.StopAsync(TimeSpan.FromSeconds(5));
            _host.Dispose();
        }
    }

    [CollectionDefinition(SetupFixture.CollectionName)]
    public class TestCollection : ICollectionFixture<SetupFixture>
    {
        //this is an xunit thing to bind a collection definition name to a fixture type.
    }
}
