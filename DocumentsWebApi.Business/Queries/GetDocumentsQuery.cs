using DocumentsWebApi.Infrastructure;
using DocumentsWebApi.Models;
using MediatR;

namespace DocumentsWebApi.Business.Queries
{
    public class GetDocumentsQuery : IRequest<PaginatedList<Document>>
    {
        public GetDocumentsQuery(int pageNumber, int pageSize)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
        }

        public int PageNumber { get; }

        public int PageSize { get; }
    }
}
