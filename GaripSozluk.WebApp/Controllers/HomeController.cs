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
        public IActionResult Index(int? categoryId = null, int? headerId = null, int pageNumber = 1)
        {
            _logger.LogDebug("Home>Index");

            if (headerId.HasValue)
            {
                return View(_headerService.GetHeaderPosts(HttpContext.User, headerId.Value, categoryId, pageNumber));
            }
            else if (categoryId.HasValue)
            {
                return View(_headerService.GetPopularHeaders(HttpContext.User, categoryId.Value));
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

            return Redirect(this.Action<HomeController>(nameof(Index), new { headerId = searchModel.HeaderId, categoryId = searchModel.CategoryId, pageNumber = searchModel.PageNumber }));
        }

        public IActionResult Random()
        {
            _logger.LogDebug("Home>Index");

            return Redirect(this.Action<HomeController>(nameof(Index), new { headerId = _headerService.GetRandomHeaderIndex(HttpContext.User) }));
        }
         

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }

}
