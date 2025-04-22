using DocumentsWebApi.Business.Commands;
using DocumentsWebApi.Infrastructure;
using DocumentsWebApi.Models;
using MediatR;

namespace DocumentsWebApi.Business.Handlers
{
    public class CreateDocumentCommandHandler : IRequestHandler<CreateDocumentCommand, Document>
    {
        public IDocumentDbContext DbContext { get; }

        public CreateDocumentCommandHandler(IDocumentDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Document> Handle(CreateDocumentCommand request, CancellationToken cancellationToken)
        {
            var entity = new Document
            {
                Title = request.Title,
                UpdatedAt = DateTime.UtcNow
            };

            DbContext.Documents.Add(entity);

            await DbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
