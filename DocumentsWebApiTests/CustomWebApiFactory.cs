using DocumentsWebApi;
using DocumentsWebApi.Data;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Respawn;
using System.Data.Common;
using Testcontainers.MsSql;

namespace DocumentsWebApiTests
{
    public class CustomWebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private DbConnection _dbConnection = null!;
        private Respawner _respawner = null!;
        private MsSqlContainer _dbContainer = null!;
        private string _connectionString = null!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("ConnectionStrings:SqlServerConnection", _connectionString);
        }

        public async Task InitializeAsync()
        {
            await UseContainerDb();
            await MigrateDb();
            await InitializeRespawnerAsync();
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }

        public new async Task DisposeAsync()
        {
            await _dbConnection.DisposeAsync();
            await _dbContainer.DisposeAsync();
        }

        private async Task InitializeRespawnerAsync()
        {
            _respawner = await Respawner.CreateAsync(_dbConnection);
        }

        private async Task UseContainerDb()
        {
            _dbContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("ComplexPassword@123")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .Build();

            await _dbContainer.StartAsync();

            _connectionString = _dbContainer.GetConnectionString();
            _dbConnection = new SqlConnection(_connectionString);
            await _dbConnection.OpenAsync();
        }

        private async Task MigrateDb()
        {
            var contextOptions = new DbContextOptionsBuilder<DocumentDbContext>()
                .UseSqlServer(_connectionString).Options;
            var dbContext = new DocumentDbContext(contextOptions);

            await dbContext.Database.MigrateAsync();
        }
    }
}