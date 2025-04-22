using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;

namespace DocumentsWebApi.Infrastructure
{
    public class GlobalExceptionHandler : IExceptionHandler
    {
        private readonly ILogger<GlobalExceptionHandler> _logger;
        public GlobalExceptionHandler(ILogger<GlobalExceptionHandler> logger)
        {
            _logger = logger;
        }

        public async ValueTask<bool> TryHandleAsync(HttpContext httpContext, Exception exception, CancellationToken cancellationToken)
        {
            var problemDetails = new ProblemDetails();
            problemDetails.Instance = httpContext.Request.Path;

            if (exception is NotFoundException notFoundEx)
            {
                problemDetails.Title = "Not Found";
                problemDetails.Detail = notFoundEx.Message;

                httpContext.Response.StatusCode = StatusCodes.Status404NotFound;
            }
            else
            {
                problemDetails.Title = "An unexpected error occurred";
                problemDetails.Detail = exception.Message;

                _logger.LogError("{UnexpectedException}", exception);
            }

            problemDetails.Status = httpContext.Response.StatusCode;

            httpContext.Response.ContentType = "application/json";

            await httpContext.Response.WriteAsJsonAsync(problemDetails, cancellationToken);

            return true;
        }
    }
}
