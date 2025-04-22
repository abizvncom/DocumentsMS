using MediatR;

namespace DocumentsWebApi.Business.Commands
{
    public class DeleteDocumentCommand : IRequest
    {
        public DeleteDocumentCommand(int id)
        {
            Id = id;
        }

        public int Id { get; init; }
    }
}
