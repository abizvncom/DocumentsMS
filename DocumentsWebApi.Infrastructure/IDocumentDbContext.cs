using DocumentsWebApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace DocumentsWebApi.Infrastructure
{
    public interface IDocumentDbContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);

        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;

        DbSet<Document> Documents { get; set; }
    }
}
