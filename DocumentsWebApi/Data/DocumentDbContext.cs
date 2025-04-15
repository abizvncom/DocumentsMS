using DocumentsWebApi.Models;
using Microsoft.EntityFrameworkCore;

namespace DocumentsWebApi.Data
{
    public class DocumentDbContext : DbContext
    {
        public DocumentDbContext(DbContextOptions<DocumentDbContext> options) : base(options)
        {
        }

        public DbSet<Document> Documents { get; set; } 
    }
}
