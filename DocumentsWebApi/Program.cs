
using Asp.Versioning;
using DocumentsWebApi.Business.Commands;
using DocumentsWebApi.Infrastructure;
using System.Text.Json;
using static System.Net.WebRequestMethods;

namespace DocumentsWebApi
{
    public partial class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Disable camel case for JSON serialization
                    options.JsonSerializerOptions.PropertyNamingPolicy = JsonNamingPolicy.CamelCase;
                });


            // Configure API versioning
            builder.Services.AddApiVersioning(options =>
                {
                    options.ApiVersionReader = new UrlSegmentApiVersionReader();
                    options.AssumeDefaultVersionWhenUnspecified = true;
                    options.DefaultApiVersion = new ApiVersion(1, 0);
                    options.ReportApiVersions = true;
                })
                .AddApiExplorer(options =>
                {
                    options.GroupNameFormat = "'v'VVV";
                    options.SubstituteApiVersionInUrl = true;
                })
                .AddMvc();


            // Configure the database
            builder.ConfigureDbContextServices();

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi("v1");
            builder.Services.AddOpenApi("v2");

            // Register MediatR for CQRS
            builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssemblyContaining<CreateDocumentCommand>());

            // Add the custom exception handling middleware as a scoped service
            builder.Services.AddExceptionHandler<GlobalExceptionHandler>();
            builder.Services.AddProblemDetails();

            var app = builder.Build();

            app.ConfigureOpenApiDocs();

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.UseExceptionHandler();

            app.Run();
        }
    }
}
