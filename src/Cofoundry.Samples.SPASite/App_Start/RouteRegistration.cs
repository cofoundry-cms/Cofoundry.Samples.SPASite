using System;
using System.Collections.Generic;
using System.Linq;
using Cofoundry.Web;
using Microsoft.AspNetCore.Routing;
using Microsoft.AspNetCore.Builder;

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
        public void RegisterRoutes(IRouteBuilder routeBuilder)
        {
            // For this application most of the routing is done by the backbone router so
            // we just need to point routes back to the home controller.

            RouteToHome(routeBuilder, "Home", "");
            RouteToHome(routeBuilder, "Login", "login");
            RouteToHome(routeBuilder, "Register", "register");
            RouteToHome(routeBuilder, "Cats", "cat/{id}");

            // The signout route is the only route that is slightly different
            routeBuilder.MapRoute("SignOut", "sign-out", new { controller = "Home", action = "SignOut" });
        }

        private void RouteToHome(IRouteBuilder routeBuilder, string name, string path)
        {
            routeBuilder.MapRoute(name, path, new { controller = "Home", action = "Index" });
        }
    }
}