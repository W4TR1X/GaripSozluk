using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Log
{
  public   class LogResultVM
    {
        public List<LogVM> Logs { get; set; }

        public List<FilteredLogVM> FilteredLogs { get; set; }

        public DateTime? FirstDate { get; set; }
        public DateTime? LastDate { get; set; }
    }
}
