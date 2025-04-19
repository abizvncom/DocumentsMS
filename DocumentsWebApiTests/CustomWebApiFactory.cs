using DocumentsWebApi;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using Respawn;
using System.Data.Common;

namespace DocumentsWebApiTests
{
    public class CustomWebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private DbConnection _dbConnection = null!;
        private Respawner _respawner = null!;
        private IConfiguration _config = null!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            builder.ConfigureAppConfiguration(config =>
            {
                config.AddConfiguration(_config);
            });
        }

        public async Task InitializeAsync()
        {
            _config = new ConfigurationBuilder()
                .AddJsonFile("integrationsettings.json")
                .Build();

            _dbConnection = new SqlConnection(_config.GetConnectionString("SqlServerConnection"));

            await _dbConnection.OpenAsync();
            await InitializeRespawnerAsync();
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }

        public new async Task DisposeAsync()
        {
            await _dbConnection.DisposeAsync();
        }

        private async Task InitializeRespawnerAsync()
        {
            _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
            {
                SchemasToInclude = ["dbo"],
                DbAdapter = DbAdapter.SqlServer
            });
        }
    }
}
