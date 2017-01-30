using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cofoundry.Samples.SPASite
{
    /// <summary>
    /// Our SPA requires a couple of properties in the viewmodel so
    /// it can startup correctly and make API requests. This data is
    /// output as js in the main view.
    /// </summary>
    public class HomeViewModel
    {
        public bool IsLoggedIn { get; set; }

        public string XSRFToken { get; set; }
    }
}