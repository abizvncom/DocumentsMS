using DocumentsWebApi.Infrastructure;
using DocumentsWebApi.PostgresqlData;
using DocumentsWebApi.SqlServerData;
using Microsoft.EntityFrameworkCore;

namespace DocumentsWebApi
{
    public static class ConfigureDbContext
    {
        public static void ConfigureDbContextServices(this IHostApplicationBuilder builder)
        {
            // Configure the database
            var databaseProvider = Environment.GetEnvironmentVariable("ConnectionStrings_DatabaseProvider");
            if (string.IsNullOrEmpty(databaseProvider))
            {
                databaseProvider = builder.Configuration.GetConnectionString("DatabaseProvider");
            }

            switch (databaseProvider?.ToLowerInvariant())
            {
                case "sqlserver":
                    builder.Services.AddSingleton<IDocumentDbContext, SqlServerDocumentDbContext>(f =>
                    {
                        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings_SqlServerConnection");
                        if (string.IsNullOrEmpty(connectionString))
                        {
                            connectionString = builder.Configuration.GetConnectionString("SqlServerConnection");
                        }

                        var options = new DbContextOptionsBuilder<SqlServerDocumentDbContext>()
                            .UseSqlServer(connectionString)
                            .Options;

                        var dbContext = new SqlServerDocumentDbContext(options);

                        if (RequiresMigration())
                        {
                            dbContext.Database.Migrate();
                        }

                        return dbContext;
                    });
                    break;

                case "postgresql":
                    builder.Services.AddSingleton<IDocumentDbContext, PostgresqlDocumentDbContext>(f =>
                    {
                        var connectionString = Environment.GetEnvironmentVariable("ConnectionStrings_PostgresConnection");
                        if (string.IsNullOrEmpty(connectionString))
                        {
                            connectionString = builder.Configuration.GetConnectionString("PostgresConnection");
                        }

                        var options = new DbContextOptionsBuilder<PostgresqlDocumentDbContext>()
                            .UseNpgsql(connectionString)
                            .Options;

                        var dbContext = new PostgresqlDocumentDbContext(options);

                        if (RequiresMigration())
                        {
                            dbContext.Database.Migrate();
                        }

                        return dbContext;
                    });
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported database provider: {databaseProvider}");
            }
        }

        private static bool RequiresMigration()
        {
            var configuredEnv = Environment.GetEnvironmentVariable("ASPNETCORE_ENVIRONMENT");
            return string.Equals(configuredEnv, "IntegrationTest", StringComparison.InvariantCultureIgnoreCase) == false;
        }
    }
}