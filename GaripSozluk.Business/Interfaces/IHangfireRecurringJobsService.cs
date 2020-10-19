using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Business.Interfaces
{
    public interface IHangfireRecurringJobs
    {
        void CreateYesterdayLogsHeader();
        void CreateYesterdayRequestGroupHeader();
    }
}
