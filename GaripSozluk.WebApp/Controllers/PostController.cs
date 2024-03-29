﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.Extensions;
using GaripSozluk.Common.ViewModels.Header;
using GaripSozluk.Common.ViewModels.Post;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.Extensions.Logging;

namespace GaripSozluk.WebApp.Controllers
{

    //ToDo: OK! Tüm actionların üzerine authorize attribute eklemek yerine bir kere controller seviyesine ekleyebilirsin. 
    [Authorize]
    public class PostController : Controller
    {
        private readonly ILogger<PostController> _logger;
        private readonly ICategoryService _categoryService;
        private readonly IHeaderService _headerService;
        private readonly IPostService _postService;
        private readonly IUserService _userService;

        public PostController(ILogger<PostController> logger,
            ICategoryService categoryService,
            IHeaderService headerService,
            IPostService postService,
            IUserService userService)
        {
            _logger = logger;

            _categoryService = categoryService;
            _headerService = headerService;
            _postService = postService;

            _userService = userService;
        }

        public IActionResult AddHeader(string categoryCode)
        {
            ViewBag.CategorySelectItemList = _categoryService.GetCategoriesOptionList();

            var model = new NewHeaderVM();
            model.CategoryCode = categoryCode;

            return View(model);
        }

        [HttpPost]
        public IActionResult AddHeader(NewHeaderVM model)
        {
            if (ModelState.IsValid)
            {
                if (_headerService.AddNewHeader(HttpContext.User, model))
                {
                    return Redirect(this.Action<HomeController>(nameof(Index), new { categoryCode = model.CategoryCode, headerCode = model.HeaderCode }));
                }
            }

            ViewBag.CategorySelectItemList = _categoryService.GetCategoriesOptionList();

            return View(model);
        }

        public IActionResult AddPost(string CategoryCode, string headerCode)
        {
            var model = new NewPostVM();
            model.HeaderCode = headerCode;
            model.CategoryCode = CategoryCode;

            return View(model);
        }

        [HttpPost]
        public IActionResult AddPost(NewPostVM model)
        {
            if (ModelState.IsValid)
            {
                if (_headerService.AddNewPost(HttpContext.User, model))
                {
                    return Redirect(this.Action<HomeController>(nameof(Index), new { categoryCode = model.CategoryCode, headerCode = model.HeaderCode }));
                }
            }

            return View(model);
        }

        public IActionResult UpVote(string categoryCode, string headerCode, int postId, int pageNumber)
        {
            _postService.UpVote(HttpContext.User, postId);
            return Redirect(this.Action<HomeController>(nameof(Index), new { categoryCode = categoryCode, headerCode = headerCode, pageNumber = pageNumber }));
        }

        public IActionResult DownVote(string categoryCode, string headerCode, int postId, int pageNumber)
        {
            _postService.DownVote(HttpContext.User, postId);
            return Redirect(this.Action<HomeController>(nameof(Index), new { categoryCode = categoryCode, headerCode = headerCode, pageNumber = pageNumber }));
        }
    }
}
