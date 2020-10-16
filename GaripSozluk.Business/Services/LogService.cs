using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.ViewModels.Log;
using GaripSozluk.Data.Domain;
using GaripSozluk.Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Text;

namespace GaripSozluk.Business.Services
{
    public class LogService : ILogService
    {
        private readonly ILogRepository _logRepository;

        public LogService(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public List<LogVM> GetAll()
        {
            var model = new List<LogVM>();

            _logRepository.GetAll().ToList().ForEach(x =>
            {
                var logRow = new LogVM()
                {
                    Id = x.Id,
                    IPAddress = x.IPAddress,
                    RequestMethod = x.RequestMethod,
                    RequestPath = x.RequestPath,
                    ResponseStatusCode = x.ResponseStatusCode,
                    RoutePath = x.RoutePath,
                    TraceIdentifier = x.TraceIdentifier,
                    UserAgent = x.UserAgent,
                    CreateDate = x.CreateDate
                };

                model.Add(logRow);
            });

            return model;
        }

        public bool InsertLog(Log model)
        {
            _logRepository.Add(model);
            _logRepository.Save();

            return true;
        }
    }
}
