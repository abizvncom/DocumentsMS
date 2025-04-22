using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace DocumentsWebApi.SqlServerData
{
    public class SqlServerDocumentDbContextFactory : IDesignTimeDbContextFactory<SqlServerDocumentDbContext>
    {
        public SqlServerDocumentDbContext CreateDbContext(string[] args)
        {
            IConfigurationRoot configuration = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = configuration.GetConnectionString("SqlServerConnection");
           
            var builder = new DbContextOptionsBuilder<SqlServerDocumentDbContext>().UseSqlServer(connectionString);

            return new SqlServerDocumentDbContext(builder.Options);
        }
    }
}
