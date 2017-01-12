using Cofoundry.Domain;
using Cofoundry.Web;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Cofoundry.Samples.SPASite.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILoginService _loginService;
        private readonly IUserContextService _userContextService;
        private readonly IAntiCSRFService _antiCSRFService;

        public HomeController(
            ILoginService loginService,
            IUserContextService userContextService,
            IAntiCSRFService antiCSRFService
            )
        {
            _loginService = loginService;
            _userContextService = userContextService;
            _antiCSRFService = antiCSRFService;
        }

        public ActionResult Index()
        {
            var currentUser = _userContextService.GetCurrentContext();

            var vm = new HomeViewModel();
            vm.IsLoggedIn = currentUser.UserId.HasValue;
            vm.XSRFToken = _antiCSRFService.GetToken();

            return View(vm);
        }

        [HttpPost]
        public ActionResult Logout()
        {
            _loginService.SignOut();

            return Redirect("/");
        }
    }
}