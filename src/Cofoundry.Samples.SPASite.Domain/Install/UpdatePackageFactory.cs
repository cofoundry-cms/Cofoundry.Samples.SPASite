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
        public override string ModuleIdentifier
        {
            get { return "SPASite"; }
        }

        public override IEnumerable<string> DependentModules
        {
            get
            {
                yield return CofoundryModuleInfo.ModuleIdentifier;
            }
        }
    }
}
