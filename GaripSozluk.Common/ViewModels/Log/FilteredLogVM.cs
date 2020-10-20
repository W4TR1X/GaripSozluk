using GaripSozluk.Common.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Log
{
    public class FilteredLogVM : IVM
    {
        public string RequestPath { get; set; }
        public int RequestCount { get; set; }
        //public List<LogVM> Logs { get; set; }
    }
}
