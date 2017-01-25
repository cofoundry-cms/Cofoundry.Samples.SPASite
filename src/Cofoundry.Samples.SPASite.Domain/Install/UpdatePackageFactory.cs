using Cofoundry.Core;
using Cofoundry.Core.AutoUpdate;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain.Install
{
    public class UpdatePackageFactory : BaseDbOnlyUpdatePackageFactory
    {
        /// <summary>
        /// The module identifier should be unique to this installation
        /// and usually indicates the application or plugin being updated
        /// </summary>
        public override string ModuleIdentifier
        {
            get { return "SPASite"; }
        }

        /// <summary>
        /// Here we can any modules that this installation is dependent
        /// on. In this case we are dependent on the Cofoundry installation
        /// being run before this one
        /// </summary>
        public override IEnumerable<string> DependentModules
        {
            get
            {
                yield return CofoundryModuleInfo.ModuleIdentifier;
            }
        }
    }
}
