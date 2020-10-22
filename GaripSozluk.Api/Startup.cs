using System;
using System.Collections.Generic;
using System.Diagnostics;
using GaripSozluk.Business.Interfaces;
using GaripSozluk.Business.Services;
using GaripSozluk.Common.Extensions;
using GaripSozluk.Common.HealthChecks;
using GaripSozluk.Data;
using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using GaripSozluk.Data.Repositories;
using Hangfire;
using Hangfire.SqlServer;
using HealthChecks.UI.Client;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Diagnostics.HealthChecks;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace GaripSozluk.Api
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
            var hangfireConnectionString = Configuration.GetConnectionString("HangfireDatabase");
            var healthchecksConnectionString = Configuration.GetConnectionString("HealthchecksDatabase");

            services.AddAppHealthChecks(healthchecksConnectionString);
       
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
            services.AddScoped<ILogRepository, LogRepository>();

            //Services
            services.AddScoped<IUserService, UserService>();
            services.AddScoped<ICategoryService, CategoryService>();
            services.AddScoped<IHeaderService, HeaderService>();
            services.AddScoped<IPostService, PostService>();
            services.AddScoped<IOpenLibraryApiService, OpenLibraryApiService>();

            // Add Hangfire services.
            services.AddHangfire(configuration => configuration
                .SetDataCompatibilityLevel(CompatibilityLevel.Version_170)
                .UseSimpleAssemblyNameTypeSerializer()
                .UseRecommendedSerializerSettings()
                .UseSqlServerStorage(hangfireConnectionString, new SqlServerStorageOptions
                {
                    CommandBatchMaxTimeout = TimeSpan.FromMinutes(5),
                    SlidingInvisibilityTimeout = TimeSpan.FromMinutes(5),
                    QueuePollInterval = TimeSpan.Zero,
                    UseRecommendedIsolationLevel = true,
                    DisableGlobalLocks = true
                }));

            // Add the processing server as IHostedService
            services.AddHangfireServer();

            services.AddScoped<IHangfireRecurringJobs, HangfireRecurringJobsService>();

            // Register the Swagger generator, defining 1 or more Swagger documents
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo { Title = "Garip Sözlük Api Service", Version = "v1" });
            });

            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder.WithOrigins("http://localhost:51923",
                                        "http://localhost:6025");
                });
            });

            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors();

            app.UseRouting();

            app.UseAuthorization();

            app.UseSwagger()
              .UseSwaggerUI(c =>
              {
                  c.SwaggerEndpoint($"/swagger/v1/swagger.json", "Garip Sözlük Api Service V1");
              });

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
                endpoints.MapHangfireDashboard();

                endpoints.MapHealthChecks("/hc", new HealthCheckOptions()
                {
                    Predicate = _ => true,
                    ResponseWriter = UIResponseWriter.WriteHealthCheckUIResponse
                });
                endpoints.MapHealthChecksUI();
            });

            app.UseHangfireDashboard();

            //The Cron time string is five values separated by spaces, based on the following information:
            //Character Descriptor     Acceptable values
            // 1   Minute               0 to 59, or * (no specific value)
            // 2   Hour                 0 to 23, or * for any value. All times UTC.
            // 3   Day of the month     1 to 31, or * (no specific value)
            // 4   Month                1 to 12, or * (no specific value)
            // 5   Day of the week      0 to 7(0 and 7 both represent Sunday), or * (no specific value)

            RecurringJob.AddOrUpdate<IHangfireRecurringJobs>(x => x.CreateYesterdayLogsHeader(), "00 01 * * *", TimeZoneInfo.Local);
            RecurringJob.AddOrUpdate<IHangfireRecurringJobs>(x => x.CreateYesterdayRequestGroupHeader(), "10 01 * * *", TimeZoneInfo.Local);
        }
    }
}
