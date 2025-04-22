using DocumentsWebApi.Business.Queries;
using DocumentsWebApi.Infrastructure;
using DocumentsWebApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentsWebApi.Business.Handlers
{
    public class GetDocumentByIdQueryHandler : IRequestHandler<GetDocumentByIdQuery, Document>
    {
        public IDocumentDbContext DbContext { get; }

        public GetDocumentByIdQueryHandler(IDocumentDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<Document> Handle(GetDocumentByIdQuery request, CancellationToken cancellationToken)
        {
            var entity = await DbContext.Documents
                .AsNoTracking()
                .FirstOrDefaultAsync(x => x.Id == request.Id, cancellationToken);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Document), $"Document with id {request.Id} not found");
            }

            return entity;
        }
    }
}
