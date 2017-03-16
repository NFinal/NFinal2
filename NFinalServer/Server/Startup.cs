using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owin;
namespace NFinalServer
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            NFinal.Middleware.MiddlewareConfigOptions options = new NFinal.Middleware.MiddlewareConfigOptions();
            options.plugs = new NFinal.Plugs.Plug[] {
                new NFinal.Plugs.Plug { filePath = System.Reflection.Assembly.GetEntryAssembly().Location ,subDomain="www"} };
            options.debug = true;
            options.debugUrl = "http://localhost:8083";
            options.customErrors = new NFinal.Middleware.CustomErrors();
            options.customErrors.mode = NFinal.Middleware.Mode.Off;
            options.defaultDocument = "Index.html";
            options.urlRouteRule = NFinal.Middleware.UrlRouteRule.AreaControllerCustomActionUrl;
            appBuilder.Use<NFinal.Middleware.OwinMiddleware>(options);
            appBuilder.UseStaticFiles();
        }
    }
}
