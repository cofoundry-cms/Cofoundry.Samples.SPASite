using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cofoundry.Web.ModularMvc;

namespace Cofoundry.Samples.SPASite.App_Start
{
    public class RouteRegistration : IRouteRegistration
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            routes.MapRoute(
                name: "SignOut",
                url: "sign-out",
                defaults: new { controller = "Home", action = "SignOut" }
            );

            routes.MapRoute(
                name: "CatchAll", 
                url: "{*url}", 
                defaults: new { controller = "Home", action = "Index" }
            );
        }
    }
}