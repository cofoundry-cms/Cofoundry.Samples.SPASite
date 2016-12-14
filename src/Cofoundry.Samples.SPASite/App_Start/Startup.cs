using System;
using System.Threading.Tasks;
using Microsoft.Owin;
using Owin;
using Cofoundry.Web;
using Cofoundry.Plugins.DependencyInjection.AutoFac.Web;

[assembly: OwinStartup(typeof(Cofoundry.Samples.SPASite.App_Start.Startup))]

namespace Cofoundry.Samples.SPASite.App_Start
{
    public class Startup
    {
        public void Configuration(IAppBuilder app)
        {
            // A DI integration package must be run before the main Cofoundry registration.
            // To add your own dependency registrations, you can implement an IDependencyRegistration
            // or use the overload to provide additional configuration
            app.UseCofoundryAutoFacIntegration();

            // Register Cofoundry into the pipeline. As part of this process it also initializes 
            // MVC/WebApi and runs additional startup tasks.
            app.UseCofoundry();
        }
    }
}
