using AngularUrlShortener.Models;
using AngularUrlShortener.Models.EF;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;

namespace AngularUrlShortener
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);
            ConfigurationManager configuration = builder.Configuration;

            // Add services to the container.
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("Application"));
            });

            builder.Services.AddDbContext<AppIdentityDbContext>(options =>
            {
                options.UseSqlite(configuration.GetConnectionString("Identity"));
            });

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                .AddEntityFrameworkStores<AppIdentityDbContext>();

            builder.Services.AddTransient<ISettingsRepository, EFSettingsRepository>();
            builder.Services.AddTransient<ILinksRepository, EFLinksRepository>();

            builder.Services.AddDistributedMemoryCache();
            builder.Services.AddSession();

            builder.Services.AddControllersWithViews();
            

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseSession();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            //app.MapControllerRoute(
            //    name: "api",
            //    pattern: "api/{controller}/{action=Index}/{id?}");

            //app.MapControllerRoute(
            //    name: "default",
            //    pattern: "{controller}/{action=Index}/{id?}");

            app.MapFallbackToFile("index.html");

            // Seed database.
            using (var scope = app.Services.CreateScope())
            {
                AppSeedData.EnsurePopulated(scope.ServiceProvider);
                IdentitySeedData.EnsurePopulated(scope.ServiceProvider).Wait();
            }

            app.Run();
        }
    }
}