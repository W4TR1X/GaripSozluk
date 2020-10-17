using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.ViewModels.Log;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GaripSozluk.WebApp.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogger<LogController> _logger;
        private readonly ILogService _logService;

        public LogController(ILogger<LogController> logger,
            ILogService logService)
        {
            _logger = logger;
            _logService = logService;
        }

        public IActionResult LogList()
        {
            var model = new LogResultVM();
            model.Logs = _logService.GetAll();

            return View(model);
        }

        [HttpPost]
        public IActionResult LogList(LogResultVM model)
        {
            model.Logs = _logService.GetAll();

            if (model.FirstDate.HasValue || model.LastDate.HasValue)
            {
                //Do filter
                model.FilteredLogs = _logService.GetAllByDateFilter(model.FirstDate, model.LastDate);
            }

            return View(model);
        }
    }
}
