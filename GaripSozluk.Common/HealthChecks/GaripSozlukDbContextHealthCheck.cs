using GaripSozluk.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GaripSozluk.Common.HealthChecks
{
    public class GaripSozlukDbContextHealthCheck : IHealthCheck
    {
        private readonly GaripSozlukDbContext _context;

        public GaripSozlukDbContextHealthCheck(GaripSozlukDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                if (await _context.Database.CanConnectAsync(cancellationToken))
                {
                    return HealthCheckResult.Healthy("GaripSozlukDbContext could connect to database");
                }
                return HealthCheckResult.Unhealthy("GaripSozlukDbContext could not connect to database");
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy("Error when trying to check GaripSozlukDbContext. ", e);
            }
        }
    }
}