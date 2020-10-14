using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Interfaces
{
    public interface IPagination
    {
        int CurrentPage { get; set; }
        public int PageCount { get; set; }
    }
}
