using GaripSozluk.Business.Interfaces;
using GaripSozluk.Business.Middleware;
using GaripSozluk.Business.Services;
using GaripSozluk.Data;
using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using GaripSozluk.Data.Repositories;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GaripSozluk.WebApp
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            var appConnectionString = Configuration.GetConnectionString("AppDatabase");
            var logConnectionString = Configuration.GetConnectionString("LogDatabase");

            services.AddDbContext<GaripSozlukDbContext>(options => options.UseSqlServer(appConnectionString))
                    .AddDbContext<GaripSozlukLogDbContext>(options => options.UseNpgsql(logConnectionString));

            services.AddIdentity<AppUser, AppRole>()
               .AddRoles<AppRole>()
               .AddRoleManager<RoleManager<AppRole>>()
               .AddEntityFrameworkStores<GaripSozlukDbContext>();

            //Repositories 
            services.AddScoped<ICategoryRepository, CategoryRepository>();
            services.AddScoped<IHeaderRepository, HeaderRepository>();
            services.AddScoped<IPostRepository, PostRepository>();
            services.AddScoped<IPostRatingRepository, PostRatingRepository>();
            services.AddScoped<IBlockedUserRepository, BlockedUserRepository>();

            //Log Repository
            services.AddScoped<ILogRepository, LogRepository>();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IHeaderService, HeaderService>();
            services.AddScoped<IPostService, PostService>();

            //Log Service
            services.AddScoped<ILogService, LogService>();

            //OpenLibrary Api Service
            services.AddScoped<IOpenLibraryApiService, OpenLibraryApiService>();

            services.AddHealthChecks();
            //services.AddHealthChecksUI();

            services.AddControllersWithViews();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseStatusCodePages();

            app.UseStaticFiles();

            app.UseMiddleware<RequestLoggerMiddleware>();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{categoryCode?}/{headerCode?}/{pageNumber?}");

                //endpoints.MapControllerRoute(
                //    name: "default",
                //    pattern: "{categoryCode?}/{headerCode?}/{pageNumber?}",
                //    defaults: new { controller = "Home", action = "Index" });
            });
        }
    }
}
