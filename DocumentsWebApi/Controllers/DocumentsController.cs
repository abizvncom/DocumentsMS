using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsWebApi.Controllers
{
    [Route("api/v{version:apiVersion}/documents")]
    [ApiVersion(0.1, Deprecated = true)]
    [ApiVersion(0.2, Deprecated = true)]
    [ApiVersion(1.0)]
    [ApiVersion(2.0)]
    [ApiController]
    public class DocumentsController : ControllerBase
    {
        [HttpGet]
        [MapToApiVersion(1.0)]
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

        [HttpGet]
        [MapToApiVersion(2.0)]
        public IActionResult GetDocumentsV2(ApiVersion version)
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
