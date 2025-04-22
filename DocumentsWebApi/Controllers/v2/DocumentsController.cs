using Asp.Versioning;
using DocumentsWebApi.Business.Commands;
using DocumentsWebApi.Business.Queries;
using DocumentsWebApi.Infrastructure;
using DocumentsWebApi.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsWebApi.Controllers.v2
{
    [Route("api/v{version:apiVersion}/Documents")]
    [ApiVersion(2.0)]
    [ApiController]
    public class DocumentsController : ApiControllerBase
    {
        private readonly IMediator _mediator;

        public DocumentsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        // GET: api/Documents
        [HttpGet]
        public async Task<ActionResult<PaginatedList<Document>>> GetDocuments()
        {
            var query = new GetDocumentsQuery(base.GetPageNumber(), base.GetPageSize());

            return await _mediator.Send(query);
        }

        // GET: api/Documents/5
        [HttpGet("{id}")]
        public async Task<ActionResult<Document>> GetDocument(int id)
        {
            var query = new GetDocumentByIdQuery(id);

            return await _mediator.Send(query);
        }

        // PUT: api/Documents/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<ActionResult<Document>> PutDocument(int id, UpdateDocumentCommand command)
        {
            return await _mediator.Send(command);
        }

        // POST: api/Documents
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<Document>> PostDocument(CreateDocumentCommand command)
        {
            var document = await _mediator.Send(command);

            return StatusCode(StatusCodes.Status201Created, document);
        }

        // DELETE: api/Documents/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteDocument(int id)
        {
            var command = new DeleteDocumentCommand(id);

            await _mediator.Send(command);

            return NoContent();
        }
    }
}
