using GaripSozluk.Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Business.Services
{
    public class HangfireRecurringJobsService : IHangfireRecurringJobs
    {
        public readonly IHeaderService _headerService;
        public HangfireRecurringJobsService(IHeaderService headerService)
        {
            _headerService = headerService;
        }

        public void CreateYesterdayLogsHeader()
        {
            _headerService.AddYesterdaysLogs();
        }

        public void CreateYesterdayRequestGroupHeader()
        {
            _headerService.AddYesterdaysMostRequestedPathLogs();
        }

    }
}
