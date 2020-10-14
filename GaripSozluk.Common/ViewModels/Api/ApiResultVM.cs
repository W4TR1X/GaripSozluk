using GaripSozluk.Common.Enums;
using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Api
{
    public class ApiResultVM
    {
        public string  QueryString { get; set; }
        public ApiSearchTypeEnum SearchType { get; set; }

        public OpenLibrarySearchJsonVM ResultModel { get; set; }
    }
}
