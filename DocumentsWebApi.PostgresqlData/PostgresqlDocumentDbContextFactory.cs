using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DocumentsWebApi.PostgresqlData
{
    public class PostgresqlDocumentDbContextFactory : IDesignTimeDbContextFactory<PostgresqlDocumentDbContext>
    {
        public PostgresqlDocumentDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("PostgresConnection");
           
            var builder = new DbContextOptionsBuilder<PostgresqlDocumentDbContext>().UseNpgsql(connectionString);

            return new PostgresqlDocumentDbContext(builder.Options);
        }
    }
}
