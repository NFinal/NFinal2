using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Middleware.Config
{
    public enum UrlRouteRule
    {
        /// <summary>
        /// /Area/Controller/CustomActionUrl;
        /// </summary>
        AreaControllerCustomActionUrl,
        /// <summary>
        /// /Area/Controller/Action/parameter1/parameter2/.....(.html|.php|.asp|.aspx|.cshtml)
        /// </summary>
        AreaControllerActionParameters
    }
}
