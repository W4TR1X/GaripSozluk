using GaripSozluk.Common.ViewModels.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Log
{
    public class LogVM : IVM
    {
        public int Id { get; set; }
        public string TraceIdentifier { get; set; }
        public int ResponseStatusCode { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string UserAgent { get; set; }
        public string RoutePath { get; set; }
        public string IPAddress { get; set; }

        public DateTime CreateDate { get; set; }
    }
}
