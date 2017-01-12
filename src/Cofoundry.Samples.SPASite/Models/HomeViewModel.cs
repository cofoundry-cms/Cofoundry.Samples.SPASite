using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cofoundry.Samples.SPASite
{
    public class HomeViewModel
    {
        public bool IsLoggedIn { get; set; }

        public string XSRFToken { get; set; }
    }
}