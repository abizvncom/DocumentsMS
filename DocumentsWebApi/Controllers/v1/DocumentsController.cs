using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsWebApi.Controllers.v1
{
    [Route("api/v{version:apiVersion}/Documents")]
    [ApiVersion(0.1, Deprecated = true)]
    [ApiVersion(0.2, Deprecated = true)]
    [ApiVersion(1.0, Deprecated = true)]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        [HttpGet]
        public IActionResult GetDocumentsV1(ApiVersion version)
        {
            // Simulate fetching documents from a database or service
            var documents = new List<string>
            {
                "GetDocuments",
                $"API Version: {version.ToString()}"
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
