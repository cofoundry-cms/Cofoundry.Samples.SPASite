using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    /// <summary>
    /// Defining a user area allows us to 
    /// </summary>
    public class MemberUserArea : IUserArea
    {
        public static string AreaCode = "SPA";

        public bool AllowPasswordLogin
        {
            get { return true; }
        }

        public string Name
        {
            get { return "SPACat Members"; }
        }

        public bool UseEmailAsUsername
        {
            get { return true; }
        }

        public string UserAreaCode
        {
            get { return AreaCode; }
        }
    }
}
