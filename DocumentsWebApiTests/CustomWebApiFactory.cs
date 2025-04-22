using DocumentsWebApi;
using DocumentsWebApi.PostgresqlData;
using DocumentsWebApi.SqlServerData;
using DotNet.Testcontainers.Containers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc.Testing;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Npgsql;
using Respawn;
using System.Data.Common;
using Testcontainers.MsSql;
using Testcontainers.PostgreSql;

namespace DocumentsWebApiTests
{
    public class CustomWebApiFactory : WebApplicationFactory<Program>, IAsyncLifetime
    {
        private DbConnection _dbConnection = null!;
        private Respawner _respawner = null!;

        private IDatabaseContainer _dbContainer = null!;

        private string _connectionString = null!;
        private string _dbProvider = null!;

        protected override void ConfigureWebHost(IWebHostBuilder builder)
        {
            Environment.SetEnvironmentVariable("ASPNETCORE_ENVIRONMENT", "IntegrationTest");
            Environment.SetEnvironmentVariable("ConnectionStrings:SqlServerConnection", _connectionString);
            Environment.SetEnvironmentVariable("ConnectionStrings:PostgresConnection", _connectionString);
            Environment.SetEnvironmentVariable("ConnectionStrings:DatabaseProvider", _dbProvider);
        }

        public async Task InitializeAsync()
        {
            //await UseContainerDbMsSql();
            await UseContainerDbPostgresql();
            await InitializeRespawnerAsync();
        }

        public async Task ResetDatabaseAsync()
        {
            await _respawner.ResetAsync(_dbConnection);
        }

        public new async Task DisposeAsync()
        {
            await _dbConnection.CloseAsync();
            await _dbConnection.DisposeAsync();

            await _dbContainer.StopAsync();
            await _dbContainer.DisposeAsync();
        }

        private async Task InitializeRespawnerAsync()
        {
            if (_dbProvider == "postgresql")
            {
                _respawner = await Respawner.CreateAsync(_dbConnection, new RespawnerOptions
                {
                    DbAdapter = DbAdapter.Postgres,
                });
            }
            else
            {
                _respawner = await Respawner.CreateAsync(_dbConnection);
            }
        }

        private async Task UseContainerDbMsSql()
        {
            _dbContainer = new MsSqlBuilder()
                .WithImage("mcr.microsoft.com/mssql/server:2022-latest")
                .WithPassword("ComplexPassword@123")
                .WithEnvironment("ACCEPT_EULA", "Y")
                .Build();

            await _dbContainer.StartAsync();

            _dbProvider = "sqlserver";
            _connectionString = _dbContainer.GetConnectionString();

            // Initialize db connection
            _dbConnection = new SqlConnection(_connectionString);
            await _dbConnection.OpenAsync();

            // Migrate the database
            var contextOptions = new DbContextOptionsBuilder<SqlServerDocumentDbContext>()
                .UseSqlServer(_connectionString).Options;
            var dbContext = new SqlServerDocumentDbContext(contextOptions);

            await dbContext.Database.MigrateAsync();
        }

        private async Task UseContainerDbPostgresql()
        {
            _dbContainer = new PostgreSqlBuilder()
                .WithDatabase("testdb")
                .WithUsername("testuser")
                .WithPassword("TestPassword@123")
                .Build();

            await _dbContainer.StartAsync();

            _dbProvider = "postgresql";
            _connectionString = _dbContainer.GetConnectionString();

            // Initialize db connection
            _dbConnection = new NpgsqlConnection(_connectionString);
            await _dbConnection.OpenAsync();

            // Migrate the database
            var contextOptions = new DbContextOptionsBuilder<PostgresqlDocumentDbContext>()
                .UseNpgsql(_connectionString)
                .ConfigureWarnings(w => w.Ignore(RelationalEventId.PendingModelChangesWarning));
            var dbContext = new PostgresqlDocumentDbContext(contextOptions.Options);

            await dbContext.Database.MigrateAsync();
        }
    }
}
