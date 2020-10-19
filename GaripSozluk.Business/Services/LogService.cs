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

            _logRepository
                .GetAll()
                .ToList()
                .ForEach(x =>
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

        public List<FilteredLogVM> GetAllByDateFilter(DateTime? startDate, DateTime? endDate)
        {
            var model = new List<FilteredLogVM>();

            if (!startDate.HasValue && !endDate.HasValue)
            {
                return model;
            }

            var query = _logRepository.GetAll();

            if (startDate.HasValue /*&& !endDate.HasValue*/)
            {
                query = query
                    .Where(x => x.CreateDate.Date >= startDate.Value.Date);
            }
            /*else*/ if (endDate.HasValue/* && !startDate.HasValue*/)
            {
                query = query
                    .Where(x => x.CreateDate.Date <= endDate.Value.Date);
            }
            //else
            //{
            //    query = query
            //        .Where(x => x.CreateDate.Date >= startDate.Value.Date && x.CreateDate.Date <= endDate.Value.Date);
            //}

            query.ToList()
                .OrderByDescending(x => x.CreateDate)
                .GroupBy(x => x.RequestPath)
                .Take(10)
                .ToList()
                .ForEach(x =>
               {
                   var logRow = new FilteredLogVM()
                   {
                       RequestPath = x.Key,
                       RequestCount = x.Count()
                   };

                   //logRow.Logs = new List<LogVM>();

                   //foreach (var item in query.ToList().Take(10))
                   //{
                   //    var log = new LogVM();
                   //    log.CreateDate = item.CreateDate;
                   //    log.Id = item.Id;
                   //    log.IPAddress = item.IPAddress;
                   //    log.RequestMethod = item.RequestMethod;
                   //    log.RequestPath = item.RequestPath;
                   //    log.ResponseStatusCode = item.ResponseStatusCode;
                   //    log.RoutePath = item.RoutePath;
                   //    log.TraceIdentifier = item.TraceIdentifier;
                   //    log.UserAgent = item.UserAgent;
                   //    logRow.Logs.Add(log);
                   //}

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
