using DocumentsWebApi.Business.Commands;
using DocumentsWebApi.Infrastructure;
using DocumentsWebApi.Models;
using MediatR;

namespace DocumentsWebApi.Business.Handlers
{
    public class UpdateDocumentCommandHandler : IRequestHandler<UpdateDocumentCommand, Document>
    {
        public IDocumentDbContext DbContext { get; }

        public UpdateDocumentCommandHandler(IDocumentDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Document> Handle(UpdateDocumentCommand request, CancellationToken cancellationToken)
        {
            var entity = await DbContext.Documents.FindAsync(request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Document), $"Document with id {request.Id} not found");
            }

            entity.Title = request.Title;
            entity.UpdatedAt = DateTime.Now;

            await DbContext.SaveChangesAsync(cancellationToken);

            return entity;
        }
    }
}
