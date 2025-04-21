using DocumentsWebApi.Infrastructure;
using DocumentsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentsWebApi.PostgresqlData
{
    public class PostgresqlDocumentDbContext : DbContext, IDocumentDbContext
    {
        public PostgresqlDocumentDbContext(DbContextOptions<PostgresqlDocumentDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; }
    }
}
