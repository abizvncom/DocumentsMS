using DocumentsWebApi.Business.Commands;
using DocumentsWebApi.Infrastructure;
using DocumentsWebApi.Models;
using MediatR;

namespace DocumentsWebApi.Business.Handlers
{
    public class DeleteDocumentCommandHandler : IRequestHandler<DeleteDocumentCommand>
    {
        public IDocumentDbContext DbContext { get; }

        public DeleteDocumentCommandHandler(IDocumentDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task Handle(DeleteDocumentCommand request, CancellationToken cancellationToken)
        {
            var entity = await DbContext.Documents.FindAsync(request.Id, cancellationToken);
            if (entity == null)
            {
                throw new NotFoundException(nameof(Document), $"Document with id {request.Id} not found");
            }

            DbContext.Documents.Remove(entity);
            await DbContext.SaveChangesAsync(cancellationToken);
        }
    }
}
