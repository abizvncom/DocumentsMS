
using Asp.Versioning;
using DocumentsWebApi.Data;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;

namespace DocumentsWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                    // Disable camel case for JSON serialization
                    options.JsonSerializerOptions.PropertyNamingPolicy = null; 
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
            //builder.Services.AddDbContext<DocumentDbContext>(opt => opt.UseInMemoryDatabase("DocumentsDb"));
            builder.Services.AddDbContext<DocumentDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));


            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi("v1");
            builder.Services.AddOpenApi("v2");

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            // Swagger UI configuration
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/openapi/v1.json", "Documents API V1.0");
                c.SwaggerEndpoint("/openapi/v2.json", "Documents API V2.0");
            });

            app.UseHttpsRedirection();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
