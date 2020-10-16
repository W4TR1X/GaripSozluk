using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.Enums;
using GaripSozluk.Common.Extensions;
using GaripSozluk.Common.ViewModels.Api;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace GaripSozluk.WebApp.Controllers
{
    public class ApiController : Controller
    {
        private readonly ILogger<ApiController> _logger;

        private readonly IHeaderService _headerService;

        private readonly IOpenLibraryApiService _openLibraryApiService;

        public ApiController(ILogger<ApiController> logger,
            IHeaderService headerService,
            IOpenLibraryApiService openLibraryApiService)
        {
            _logger = logger;
            _headerService = headerService;
            _openLibraryApiService = openLibraryApiService;
        }

        public IActionResult ApiSearch()
        {
            return View();
        }

        [HttpPost]
        public IActionResult ApiSearch(ApiSearchVM model)
        {
            if (ModelState.IsValid)
            {
                return Redirect(this.Action<ApiController>(nameof(ApiResult), new { queryText = model.QueryText, searchType = model.SearchType }));
            }

            return View(model);
        }

        public IActionResult ApiResult(string queryText, ApiSearchTypeEnum searchType)
        {
            if (queryText.Length == 0)
            {
                return Redirect(this.Action<ApiController>(nameof(ApiSearch)));
            }

            // API
            var result = _openLibraryApiService.Search(queryText, searchType);
            if (result.ResultModel.Docs.Count > 0)
            {
                return View(result);
            }
            else
            {
                return Redirect(this.Action<ApiController>(nameof(ApiSearch)));
            }
        }

        [HttpPost]
        [Authorize]
        public IActionResult InsertTitles(List<string> titles)
        {
            var statusText = "{0} adet başlıktan {1} adet başlığın eklenmesi başarıyla tamamlandı.";

            try
            {
                var insertedTitleCount = _headerService.BulkInsert(HttpContext.User, titles);

                if (insertedTitleCount > 0)
                {
                    if (titles.Count == insertedTitleCount)
                    {
                        statusText = "Tüm başlıklar başarıyla eklendi.";
                    }
                    else
                    {
                        statusText = string.Format(statusText, titles.Count, insertedTitleCount);
                    }
                }
                else
                {
                    statusText = "Seçilen başlıklar daha önce eklenmiş.";
                }

                return Content(JsonConvert.SerializeObject(new
                {
                    status = "success",
                    message = statusText,
                    returnUrl = this.Action<HomeController>(nameof(Index))
                }));
            }
            catch (Exception ex)
            {
                return Content(JsonConvert.SerializeObject(new { status = "error", message = ex.Message }));
            }
        }
    }
}
