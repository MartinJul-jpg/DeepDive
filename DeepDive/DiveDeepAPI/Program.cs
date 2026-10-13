
using Shared.Data;
using DiveDeepAPI.Repository;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Scalar.AspNetCore;
using Shared.Services;
using System.Security.Claims;

namespace DiveDeepAPI
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddCors(options =>
            {
                options.AddPolicy("AllowDeepDiveClient", policy =>
                    policy.WithOrigins("https://localhost:7294")
                        .AllowAnyMethod()
                        .AllowAnyHeader());
            });

            builder.Services.AddControllers();

            builder.Services.AddIdentityCore<ApplicationUser>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<DiveDeepContext>()
                .AddApiEndpoints();

            builder.Services.AddAuthentication().AddBearerToken(IdentityConstants.BearerScheme);
            builder.Services.AddAuthorization();

            builder.Services.AddScoped<IProductRepository, ProductRepository>();
            builder.Services.AddScoped<ICartRepository, CartRepository>();
            builder.Services.AddScoped<IDTO_Service, DTO_Service>();

            builder.Services.AddDbContext<DiveDeepContext>(options =>
            {
                options.UseSqlServer(builder.Configuration.GetConnectionString("default"));
                options.UseSqlServer(builder => builder.MigrationsAssembly("DiveDeepAPI"));
            });

            // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
            builder.Services.AddOpenApi();

            var app = builder.Build();

            // Configure the HTTP request pipeline. 
            if (app.Environment.IsDevelopment())
            {
                app.MapOpenApi();
                app.MapScalarApiReference();
            }

            app.UseHttpsRedirection();

            app.UseCors("AllowDeepDiveClient");

            app.UseAuthentication();
            app.UseAuthorization();
            app.MapIdentityApi<ApplicationUser>();
            app.MapGet("/account/me", async (ClaimsPrincipal principal, UserManager<ApplicationUser> userManager) =>
            {
                ApplicationUser? applicationUser = await userManager.GetUserAsync(principal);

                string[] roles = Array.Empty<string>();
                if (applicationUser != null)
                {
                    string? userId = await userManager.GetUserIdAsync(applicationUser);
                    IList<string> assignedRoles = await userManager.GetRolesAsync(applicationUser);
                    roles = assignedRoles.ToArray();

                    return Results.Ok(new
                    {
                        UserId = userId,
                        Roles = roles
                    });
                }
                else
                {
                    return Results.NotFound();
                }
            }).RequireAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
