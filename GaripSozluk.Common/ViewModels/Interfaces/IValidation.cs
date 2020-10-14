using System;
using System.Collections.Generic;
using System.Text;

namespace GaripSozluk.Common.ViewModels.Interfaces
{
    public interface IValidation : IDisposable
    {
        public Dictionary<string, string> ValidationErrors { get; set; }

        public VMStatus Status { get; set; }
    }
}
