using DeepDive.Data;
using DeepDive.Repository;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using DeepDive.Services;

namespace DeepDive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddScoped<CartRepository>();
            builder.Services.AddScoped<ProductRepository>();

            // Weather API
            builder.Services.AddScoped<IWeatherHttpService, WeatherHttpService>();


            //builder.Services.AddHttpClient("DeepDiveApi", client => {
            //    client.BaseAddress = new Uri("localhostUrigoeshere");
            //});

            builder.Services.AddHttpClient("GeolocateClient", client =>
            {
                client.BaseAddress = new Uri("https://geocoding-api.open-meteo.com/v1/");
            });
            builder.Services.AddHttpClient("WeatherClient", client =>
            {
                client.BaseAddress = new Uri("https://api.open-meteo.com/v1/");
            });
            builder.Services.AddHttpClient("MarineClient", client =>
            {
                client.BaseAddress = new Uri("https://marine-api.open-meteo.com/v1/");
            });

            builder.Services.AddDbContext<DeepDiveContext>(options => options.UseSqlServer(builder.Configuration.GetConnectionString("default")));

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DeepDiveContext>();
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
