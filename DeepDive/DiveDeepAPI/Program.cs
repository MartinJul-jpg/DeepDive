
using DiveDeepAPI.Data;
using DiveDeepAPI.Repository;
using Microsoft.EntityFrameworkCore;

namespace DiveDeepAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();

            builder.Services.AddDbContext<DiveDeepContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi


            var app = builder.Build();

            // Configure the HTTP request pipeline.       

            app.UseHttpsRedirection();

            app.UseAuthentication();

            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
