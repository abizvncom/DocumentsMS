namespace DocumentsWebApi
{
    public static class ConfigOpenApi
    {
        public static void ConfigureOpenApiDocs(this WebApplication app)
        {
            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }
            else
            {
                app.MapOpenApi();
            }

            // Swagger UI configuration
            app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/openapi/v1.json", "Documents API V1.0");
                    c.SwaggerEndpoint("/openapi/v2.json", "Documents API V2.0");
                });
        }
    }
}