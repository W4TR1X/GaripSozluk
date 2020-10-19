using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.ViewModels.Header;
using GaripSozluk.Data.Domain;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GaripSozluk.Api.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class HeaderController : ControllerBase
    {
        private readonly ILogger<HeaderController> _logger;
        private readonly IHeaderService _headerService;

        public HeaderController(ILogger<HeaderController> logger, IHeaderService headerService)
        {
            _logger = logger;
            _headerService = headerService;
        }

        [HttpGet]
        public IList<HeaderRowVM> GetAll()
        {
            return _headerService.GetAllHeaders();
        }
    }
}
