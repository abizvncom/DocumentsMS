using Asp.Versioning;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsWebApi.v2.Controllers
{
    [Route("api/v{version:apiVersion}/documents")]
    [ApiController]
    [ApiVersion("2.0")]
    public class DocumentsController : ControllerBase
    {
        public DocumentsController() { }

        [HttpGet]
        public IActionResult GetDocuments()
        {
            // Simulate fetching documents from a database or service
            var documents = new List<string>
            {
                "V2 Documents"
            };
            return Ok(documents);
        }
    }
}
