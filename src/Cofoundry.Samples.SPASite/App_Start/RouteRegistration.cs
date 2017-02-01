using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Cofoundry.Web.ModularMvc;

namespace Cofoundry.Samples.SPASite.App_Start
{
    /// <summary>
    /// Routes can be registered either using the standard MVC attribute routing or if you want to
    /// use rule based routing you can register an IRouteRegistration class, which will automatically 
    /// be picked up by the Dependency Injector system and injected into the Cofoundry route registration 
    /// at the right time.
    /// </summary>
    public class RouteRegistration : IRouteRegistration
    {
        public void RegisterRoutes(RouteCollection routes)
        {
            // For this application most of the routing is done by the backbone router so
            // we just need to point routes back to the home controller.

            routes.MapRoute(
                name: "Home",
                url: "",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Login",
                url: "login",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Register",
                url: "register",
                defaults: new { controller = "Home", action = "Index" }
            );

            routes.MapRoute(
                name: "Cats",
                url: "cat/{id}",
                defaults: new { controller = "Home", action = "Index" }
            );

            // The signout route is the only route that is slightly different
            routes.MapRoute(
                name: "SignOut",
                url: "sign-out",
                defaults: new { controller = "Home", action = "SignOut" }
            );
        }
    }
}