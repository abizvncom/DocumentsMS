using DocumentsWebApi.Models;
using MediatR;

namespace DocumentsWebApi.Business.Queries
{
    public class GetDocumentByIdQuery : IRequest<Document>
    {
        public GetDocumentByIdQuery(int id)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
