using Cofoundry.Domain;
using Microsoft.AspNetCore.Antiforgery;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Controllers
{
    /// <summary>
    /// Most of the routing is handled by the backbone router, so the home
    /// controller is the entry point for most routes in our application.
    /// </summary>
    public class HomeController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IUserContextService _userContextService;
        private readonly IAntiforgery _antiforgery;

        public HomeController(
            ILoginService loginService,
            IUserContextService userContextService,
            IAntiforgery antiforgery
            )
        {
            _loginService = loginService;
            _userContextService = userContextService;
            _antiforgery = antiforgery;
        }

        public async Task<ActionResult> Index()
        {
            var currentUser = await _userContextService.GetCurrentContextAsync();
            var token = _antiforgery.GetAndStoreTokens(HttpContext);

            var vm = new HomeViewModel();
            vm.IsLoggedIn = currentUser.UserId.HasValue;
            vm.XSRFToken = token.RequestToken;

            return View(vm);
        }

        [HttpPost]
        public async Task<ActionResult> SignOut()
        {
            await _loginService.SignOutAsync();

            return Redirect("/");
        }
    }
}