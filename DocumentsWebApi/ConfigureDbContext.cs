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
            var databaseProvider = builder.Configuration.GetConnectionString("DatabaseProvider");

            switch (databaseProvider?.ToLowerInvariant())
            {
                case "sqlserver":
                    builder.Services.AddSingleton<IDocumentDbContext, SqlServerDocumentDbContext>(f =>
                    {
                        var options = new DbContextOptionsBuilder<SqlServerDocumentDbContext>()
                            .UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection"))
                            .Options;
                        return new SqlServerDocumentDbContext(options);
                    });
                    break;

                case "postgresql":
                    builder.Services.AddSingleton<IDocumentDbContext, PostgresqlDocumentDbContext>(f =>
                    {
                        var options = new DbContextOptionsBuilder<PostgresqlDocumentDbContext>()
                            .UseNpgsql(builder.Configuration.GetConnectionString("PostgresConnection"))
                            .Options;
                        return new PostgresqlDocumentDbContext(options);
                    });
                    break;

                default:
                    throw new InvalidOperationException($"Unsupported database provider: {databaseProvider}");
            }
        }
    }
}