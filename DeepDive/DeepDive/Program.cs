using DeepDive.Data;
using DeepDive.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;

namespace DeepDive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<BasketRepository>();
            builder.Services.AddScoped<BookingRepository>();
            builder.Services.AddScoped<ProductRepository>();

            builder.Services.AddDbContext<DeepDiveContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false).AddEntityFrameworkStores<DeepDiveContext>();
            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();
            app.MapControllerRoute
            (
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            app.Run();
        }
    }
}
