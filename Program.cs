using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MVCCoreAppWithIdentity.Models;

namespace MVCCoreAppWithIdentity
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddControllersWithViews();
            builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<ApplicationDbContext>();
            var connectionString =
                builder.Configuration.GetConnectionString("DefaultConnectionString") ?? throw new InvalidOperationException("Connection string not found");
            builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(connectionString));

            // Configure the Application Cookie settings
            builder.Services.ConfigureApplicationCookie(options =>
            {
                // If the LoginPath isn't set, ASP.NET Core defaults the path to /Account/Login.
                options.LoginPath = "/Account/Login"; // Set your login path here
            });
            //Configuration Identity Services
            //builder.Services.AddIdentity<ApplicationUser, IdentityRole>(
            //    options =>
            //    {
            //        // Password settings
            //        options.Password.RequireDigit = true;
            //        options.Password.RequiredLength = 8;
            //        options.Password.RequireNonAlphanumeric = true;
            //        options.Password.RequireUppercase = true;
            //        options.Password.RequireLowercase = true;
            //        options.Password.RequiredUniqueChars = 4;
            //        // Other settings can be configured here
            //    })
            //    .AddEntityFrameworkStores<ApplicationDbContext>();

            //AppID :561640756751768
            //AppSecret: b8cb1ba0a23e86ac6e4a338e920a636d
            builder.Services.AddAuthentication().AddFacebook(options =>
            {
                options.AppId = "561640756751768";
                options.AppSecret = "b8cb1ba0a23e86ac6e4a338e920a636d";
            }).AddGitHub(options =>
            {
                options.ClientId = "Iv23li7QqNxn7Kipy772";
                options.ClientSecret = "2c538367730f47e0096f881508a5e7c71cb0a6a5";
            });
            //AppId :1090570
            //clientSecret :2c538367730f47e0096f881508a5e7c71cb0a6a5


            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            
            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");

            app.Run();
        }
    }
}
