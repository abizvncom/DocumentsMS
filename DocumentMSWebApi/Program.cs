
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

            //builder.Services.AddDbContext<DocumentDbContext>(opt => opt.UseInMemoryDatabase("DocumentsDb"));
            builder.Services.AddDbContext<DocumentDbContext>(opt => opt.UseSqlServer(builder.Configuration.GetConnectionString("SqlServerConnection")));

            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}
