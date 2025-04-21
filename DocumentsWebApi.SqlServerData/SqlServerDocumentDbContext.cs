using DocumentsWebApi.Infrastructure;
using DocumentsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentsWebApi.SqlServerData
{
    public class SqlServerDocumentDbContext : DbContext, IDocumentDbContext
    {
        public SqlServerDocumentDbContext(DbContextOptions<SqlServerDocumentDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; } 
    }
}
