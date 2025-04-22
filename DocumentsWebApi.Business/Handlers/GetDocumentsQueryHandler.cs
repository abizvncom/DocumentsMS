using DocumentsWebApi.Business.Queries;
using DocumentsWebApi.Infrastructure;
using DocumentsWebApi.Models;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace DocumentsWebApi.Business.Handlers
{
    public class GetDocumentsQueryHandler : IRequestHandler<GetDocumentsQuery, PaginatedList<Document>>
    {
        public IDocumentDbContext DbContext { get; }

        public GetDocumentsQueryHandler(IDocumentDbContext dbContext)
        {
            DbContext = dbContext;
        }

        public async Task<PaginatedList<Document>> Handle(GetDocumentsQuery request, CancellationToken cancellationToken)
        {
            var sources = DbContext.Documents.AsNoTracking().OrderByDescending(d => d.Id);
            return await PaginatedList<Document>.CreateAsync(sources, request.PageNumber, request.PageSize);
        }
    }
}
