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

            //new stuff
            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
                options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            });

            builder.Services.AddHttpContextAccessor();
            //end of new stuff

            builder.Services.AddScoped<AuthState>();

            builder.Services.AddScoped<IIdentityApiService, IdentityApiService>();
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

            var app = builder.Build();

            app.UseStaticFiles();
            app.UseRouting();

            //new stuff
            app.UseSession();
            //end of new stuff

            app.MapControllerRoute
            (
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"
            );

            app.UseAuthentication();
            app.UseAuthorization();

            app.Run();
        }
    }
}
