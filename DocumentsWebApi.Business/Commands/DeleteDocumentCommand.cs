using DocumentsWebApi.Models;
using MediatR;

namespace DocumentsWebApi.Business.Commands
{
    public class UpdateDocumentCommand : IRequest<Document>
    {
        public int Id { get; init; }

        public string Title { get; init; } = string.Empty;
    }
}
