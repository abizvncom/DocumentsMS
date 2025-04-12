using DocumentMSWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentMSWebApi.Data
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; } 
    }
}
