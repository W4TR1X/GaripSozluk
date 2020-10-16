using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaripSozluk.Business.Interfaces;
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
            var logs = _logService.GetAll();
            return View(logs);
        }
    }
}
