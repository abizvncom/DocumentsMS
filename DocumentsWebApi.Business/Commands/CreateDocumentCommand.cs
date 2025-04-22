using DocumentsWebApi.Models;
using MediatR;

namespace DocumentsWebApi.Business.Commands
{
    public class CreateDocumentCommand : IRequest<Document>
    {
        public string Title { get; set; } = string.Empty;
    }
}
