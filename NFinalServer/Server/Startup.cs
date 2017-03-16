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
            NFinal.Middleware.Config.MiddlewareConfigOptions options = new NFinal.Middleware.Config.MiddlewareConfigOptions();
            options.plugs = new NFinal.Plugs.Plug[] {
                new NFinal.Plugs.Plug { filePath = System.Reflection.Assembly.GetEntryAssembly().Location ,subDomain="www"} };
            options.debug = true;
            options.debugUrl = "http://localhost:8083";
            options.customErrors = new NFinal.Middleware.Config.CustomErrors();
            options.customErrors.mode = NFinal.Middleware.Config.Mode.Off;
            options.defaultDocument = "Index.html";
            options.urlRouteRule = NFinal.Middleware.Config.UrlRouteRule.AreaControllerCustomActionUrl;
            appBuilder.Use<NFinal.Middleware.OwinMiddleware>(options);
            appBuilder.UseStaticFiles();
        }
    }
}
