using DeepDive.Data;
using Microsoft.EntityFrameworkCore;

namespace DeepDive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddDbContext<DeepDiveContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute
            (
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );
            app.Run();
        }
    }
}
