using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using GaripSozluk.WebApp.Models;
using GaripSozluk.Business.Interfaces;
using Microsoft.AspNetCore.Authorization;
using GaripSozluk.Common.Extensions;
using GaripSozluk.Common.ViewModels.Header;
using GaripSozluk.Common.ViewModels.Api;
using GaripSozluk.Common.Enums;

namespace GaripSozluk.WebApp.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IHeaderService _headerService;
        private readonly ICategoryService _categoryService;

        public HomeController(ILogger<HomeController> logger,
            ICategoryService categoryService,
            IHeaderService headerService)
        {
            _logger = logger;
            _categoryService = categoryService;
            _headerService = headerService;
        }

        [AllowAnonymous]
        public IActionResult Index(string categoryCode = "", string headerCode = "", int pageNumber = 1)
        {
            _logger.LogDebug("Home>Index");

            if (headerCode != "")
            {
                return View(_headerService.GetHeaderPosts(HttpContext.User, headerCode, categoryCode, pageNumber));
            }
            else if (categoryCode != "")
            {
                return View(_headerService.GetPopularHeaders(HttpContext.User, categoryCode));
            }
            else
            {
                return View(_headerService.GetPopularHeaders(HttpContext.User));
            }
        }

        [HttpPost]
        [AllowAnonymous]
        public IActionResult Index(HeaderSearchVM searchModel)
        {
            if (ModelState.IsValid)
            {
                return View(_headerService.Search(HttpContext.User, searchModel));
            }

            return Redirect(this.Action<HomeController>(nameof(Index), new { headerCode = searchModel.HeaderCode, categoryCode = searchModel.CategoryCode, pageNumber = searchModel.PageNumber }));
        }

        public IActionResult Random(string categoryCode = "")
        {
            return Redirect(this.Action<HomeController>(nameof(Index), new { categoryCode = categoryCode, headerCode = _headerService.GetRandomHeaderIdCode(HttpContext.User) }));
        }

        [AllowAnonymous]
        public IActionResult ApiHeaderList()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
