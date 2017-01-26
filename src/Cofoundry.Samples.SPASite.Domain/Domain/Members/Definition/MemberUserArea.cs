using Cofoundry.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cofoundry.Samples.SPASite.Domain
{
    /// <summary>
    /// Defining a user area allows us to provide an enhanced
    /// to people who have signed up to the site.
    /// </summary>
    public class MemberUserArea : IUserArea
    {
        public static string AreaCode = "SPA";

        /// <summary>
        /// Indicates if users in this area can login using a password. If this is false
        /// the password field will be null and login will typically be via SSO or some 
        /// other method.
        /// </summary>
        public bool AllowPasswordLogin
        {
            get { return true; }
        }

        /// <summary>
        /// Display name of the area, used in the Cofoundry admin panel
        /// as the navigation link to manage your users
        /// </summary>
        public string Name
        {
            get { return "SPACat Members"; }
        }

        /// <summary>
        /// Indicates whether the user should login using thier email address as the username.
        /// Some SSO systems might provide only a username and not an email address so in
        /// this case the email address is allowed to be null.
        /// </summary>
        public bool UseEmailAsUsername
        {
            get { return true; }
        }

        /// <summary>
        /// A unique 3 letter code identifying this user area. The cofoundry 
        /// user are uses the code "COF" so you can pick anything else!
        /// </summary>
        public string UserAreaCode
        {
            get { return AreaCode; }
        }
    }
}
