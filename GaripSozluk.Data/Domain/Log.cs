﻿using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Data.Domain
{
    public class Log : BaseEntity
    {
        public string TraceIdentifier { get; set; }
        public int ResponseStatusCode { get; set; }
        public string RequestMethod { get; set; }
        public string RequestPath { get; set; }
        public string UserAgent { get; set; }
        public string RoutePath { get; set; }
        public string IPAddress { get; set; }

        //Id,
        //TraceIdentifier,
        //ResponseStatusCode, 
        //RequestMethod, 
        //RequestPath,
        //UserAgent,
        //RoutePath,
        //IPAddress
    }
}
