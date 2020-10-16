using GaripSozluk.Common.ViewModels;
using GaripSozluk.Common.ViewModels.Account;
using GaripSozluk.Common.ViewModels.User;
using GaripSozluk.Data.Domain;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;

namespace GaripSozluk.Business.Interfaces
{
    public interface ILogService
    {
        bool InsertLog(Log model);
    }
}
