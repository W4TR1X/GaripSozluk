using GaripSozluk.Data;
using Microsoft.Extensions.Diagnostics.HealthChecks;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace GaripSozluk.Common.HealthChecks
{
    public class GaripSozlukLogDbContextHealthCheck : IHealthCheck
    {
        private readonly GaripSozlukLogDbContext _context;

        public GaripSozlukLogDbContextHealthCheck(GaripSozlukLogDbContext context)
        {
            _context = context;
        }

        public async Task<HealthCheckResult> CheckHealthAsync(HealthCheckContext context, CancellationToken cancellationToken = new CancellationToken())
        {
            try
            {
                if (await _context.Database.CanConnectAsync(cancellationToken))
                {
                    return HealthCheckResult.Healthy("GaripSozlukLogDbContext could connect to database");
                }
                return HealthCheckResult.Unhealthy("GaripSozlukLogDbContext could not connect to database");
            }
            catch (Exception e)
            {
                return HealthCheckResult.Unhealthy("Error when trying to check GaripSozlukLogDbContext. ", e);
            }
        }
    }
}