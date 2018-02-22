using Cofoundry.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Data
{
    /// <summary>
    /// An IDependencyRegistration class allows us to automatically register 
    /// services with the DI container in a modular way.
    /// 
    /// See https://github.com/cofoundry-cms/cofoundry/wiki/Dependency-Injection
    /// </summary>
    public class DataDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container.RegisterScoped<SPASiteDbContext>();
        }
    }
}
