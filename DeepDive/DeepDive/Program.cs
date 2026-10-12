using DeepDive.Services;
using DeepDive.Services.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Shared.Data;
using Shared.Services;

namespace DeepDive
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            builder.Services.AddControllersWithViews();

            builder.Services.AddSingleton<AuthState>();

            builder.Services.AddScoped<IDiveDeepApiService, DiveDeepApiService>();
            builder.Services.AddScoped<IDTO_Service, DTO_Service>();

            // Weather API
            builder.Services.AddScoped<IWeatherHttpService, WeatherHttpService>();

            builder.Services.AddScoped(sp => new HttpClient(new AuthHeaderHandler(sp.GetRequiredService<AuthState>()) { InnerHandler = new HttpClientHandler() })
            {
                BaseAddress = new Uri("https://localhost:7126/")
            });

            builder.Services.AddHttpClient("DeepDiveApi", client => {
                client.BaseAddress = new Uri("https://localhost:7126/");
            });

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

            builder.Services.AddDbContext<DiveDeepContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
            });

            builder.Services.AddDefaultIdentity<ApplicationUser>(options => options.SignIn.RequireConfirmedAccount = false)
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DiveDeepContext>();

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
