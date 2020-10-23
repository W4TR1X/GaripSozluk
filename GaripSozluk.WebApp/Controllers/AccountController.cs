using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using GaripSozluk.Business.Interfaces;
using GaripSozluk.Common.ViewModels;
using GaripSozluk.Common.Extensions;
using GaripSozluk.Common.ViewModels.Account;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace GaripSozluk.WebApp.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> _logger;

        private readonly IUserService _userService;

        public AccountController(ILogger<AccountController> logger,
            IUserService userService)
        {
            _logger = logger;
            _userService = userService;
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Login(LoginVM loginModel)
        {
            if (ModelState.IsValid)
            {
                //ToDo: OK! servise yaptığımız istekleri bir üst satırda yapıp sonucunu bir değişkene almaya özen gösterelim. Bu bizim için kod okunaklığını arttıracaktır.
                var loginState = _userService.Login(loginModel);
                if (loginState)
                {
                    return Redirect(this.Action<HomeController>(nameof(Index)));
                }
            }

            return View(loginModel);
        }

        public IActionResult Register()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Register(RegisterVM registerModel)
        {
            if (ModelState.IsValid)
            {
                if (registerModel.Password == registerModel.Password2)
                {
                    if (_userService.Register(registerModel))
                    {
                        return Redirect(this.Action<AccountController>(nameof(Login)));
                    }
                }
            }

            return View(registerModel);
        }

        [Authorize]
        public IActionResult BlockUser(int blockedUserId, string headerCode)
        {

            var contextUser = HttpContext.User;
            _userService.BlockUser(contextUser, blockedUserId);

            return Redirect(this.Action<HomeController>(nameof(Index), new { headerCode = headerCode }));
        }

        public IActionResult LostPassword()
        {
            //Not implemented yet
            return Redirect(this.Action<HomeController>(nameof(Index)));
        }

        [Authorize]
        public IActionResult Logout()
        {
            _userService.Logout();

            return Redirect(this.Action<HomeController>(nameof(Index)));
        }

        [Authorize]
        public IActionResult GetBlockedUsersList()
        {
            return View(_userService.GetBlockedUsers(HttpContext.User));
        }

        [Authorize]
        public IActionResult RemoveBlockedUser(int blockedUserId)
        {
            _userService.RemoveBlockedUser(HttpContext.User, blockedUserId);
            return Redirect(this.Action<AccountController>(nameof(AccountController.GetBlockedUsersList)));
        }
    }
}
