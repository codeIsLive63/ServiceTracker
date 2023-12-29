using Microsoft.EntityFrameworkCore;
using ServiceTracker.Domain;
using ServiceTracker.Domain.Entities;
using ServiceTracker.Domain.Repository.Abstractions;
using ServiceTracker.Domain.Repository.EntityFramework;
using System.Reflection;

namespace ServiceTracker;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        builder.Configuration.AddUserSecrets(Assembly.GetExecutingAssembly(), true);

        builder.Services.AddTransient<IRecordRepository, EFRecordRepository>();

        builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options.UseLazyLoadingProxies().UseSqlite(builder.Configuration["DbConnectionString"], b => b.MigrationsAssembly("ServiceTracker"))
        );

        builder.Services.AddDefaultIdentity<ApplicationUser>()
            .AddEntityFrameworkStores<ApplicationDbContext>();

        builder.Services.ConfigureApplicationCookie(options =>
        {
            options.ExpireTimeSpan = TimeSpan.FromDays(14);

            options.Cookie.SameSite = SameSiteMode.Strict;
            options.Cookie.SecurePolicy = CookieSecurePolicy.Always;
            options.Cookie.HttpOnly = true;

            options.LoginPath = "/Account/Login";
            options.LogoutPath = "/Account/Logout";
            options.AccessDeniedPath = "/Account/AccessDenied";

            options.SlidingExpiration = true;
        });

        builder.Services.AddControllersWithViews();

        var app = builder.Build();

        if (!app.Environment.IsDevelopment())
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
        }

        app.UseHttpsRedirection();
        app.UseStaticFiles();

        app.UseRouting();

        app.UseCookiePolicy();
        app.UseAuthentication();
        app.UseAuthorization();

        app.MapControllerRoute(
            name: "default",
            pattern: "{controller=Home}/{action=Index}/{id?}"
        );

        app.Run();
    }
}