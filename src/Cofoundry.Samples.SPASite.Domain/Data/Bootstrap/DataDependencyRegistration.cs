using Cofoundry.Core.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Data
{
    public class DataDependencyRegistration : IDependencyRegistration
    {
        public void Register(IContainerRegister container)
        {
            container.RegisterDatabase<SPASiteDbContext>();
        }
    }
}
