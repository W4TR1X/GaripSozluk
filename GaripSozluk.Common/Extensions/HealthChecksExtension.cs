using GaripSozluk.Common.HealthChecks;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GaripSozluk.Common.Extensions
{
    public static class HealthCheckBuilder
    {
        public static IHealthChecksBuilder AddAppHealthChecks(this IServiceCollection services, string healthcheckConnectionString)
        {
            var connectionString = healthcheckConnectionString;//"HealthcheckDatabase"

            var builder = services.AddHealthChecks()
            .AddHangfire(setup =>
              {

              }, name: "Hangfire", tags: new[] { "Task", "Background", "Job" })

            .AddDiskStorageHealthCheck(setup =>
            {
                setup.AddDrive("C:\\");
            }, name: "C Drive", tags: new[] { "Storage" })

            .AddDiskStorageHealthCheck(setup =>
            {
                setup.AddDrive("D:\\");
            }, name: "D Drive", tags: new[] { "Storage" })


            .AddDiskStorageHealthCheck(setup =>
            {
                setup.AddDrive("Z:\\");
            }, name: "Z Drive", tags: new[] { "Storage" })

            .AddPrivateMemoryHealthCheck(336216064, tags: new[] { "Memory" })

            .AddProcessAllocatedMemoryHealthCheck(35, tags: new[] { "Memory" })

            .AddVirtualMemorySizeHealthCheck(2224155897856, tags: new[] { "Memory" })

            .AddWorkingSetHealthCheck(197386240, tags: new[] { "Memory" })

            .AddCheck<GaripSozlukDbContextHealthCheck>(name: "Garip Sözlük App Database", null, new[] { "Sql", "Database" })

            .AddCheck<GaripSozlukLogDbContextHealthCheck>(name: "Garip Sözlük Log Database", null, new[] { "Postgre", "Sql", "Database" })

            .AddDnsResolveHealthCheck(setup =>
            {
                setup.ResolveHost("openlibrary.org");
            }, name: "OpenLibrary", tags: new[] { "Api" })

            .AddDnsResolveHealthCheck(setup =>
            {
                setup.ResolveHost("localhost:51923/");
            }, name: "Garip Sözlük Web App", tags: new[] { "aspnet", "core", "main" });

            services.AddHealthChecksUI(setupSettings: setup =>
            {
                setup.AddHealthCheckEndpoint("Garip Sözlük API", "/hc");
            }).AddPostgreSqlStorage(connectionString);

            return builder;
        }
    }
}
