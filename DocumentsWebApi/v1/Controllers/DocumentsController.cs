using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsWebApi.v1.Controllers
{
    [Route("api/v{version:apiVersion}/documents")]
    [ApiController]
    [ApiVersion("1.0")]
    public class DocumentsController : ControllerBase
    {
        public DocumentsController() { }

        [HttpGet]
        public IActionResult GetDocuments()
        {
            // Simulate fetching documents from a database or service
            var documents = new List<string>
            {
                "V1 documents"
            };
            return Ok(documents);
        }

        [HttpGet("{id}")]
        public IActionResult GetDocumentById(int id)
        {
            // Simulate fetching a document by ID from a database or service
            var document = $"Document {id}";
            return Ok(document);
        }
    }
}
