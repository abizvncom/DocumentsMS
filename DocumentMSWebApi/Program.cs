
using DocumentMSWebApi.Data;
using Microsoft.EntityFrameworkCore;

namespace DocumentMSWebApi
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();
            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            //builder.Services.AddDbContext<DocumentDbContext>(opt => opt.UseInMemoryDatabase("DocumentsDb"));
            builder.Services.AddDbContext<DocumentDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
