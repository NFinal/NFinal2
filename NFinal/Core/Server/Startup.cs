using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owin;
namespace NFinal
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            NFinal.Middleware.MiddlewareConfigOptions options = new NFinal.Middleware.MiddlewareConfigOptions();
            options.plugs = new NFinal.Middleware.Plug[] {
                new NFinal.Middleware.Plug { filePath = @"E:\work\NFinal2\NFinal2\CoreWebTest\bin\CoreWebTest.dll" ,subDomain="www"} };
            options.debug = true;
            options.customErrors = new NFinal.Middleware.CustomErrors();
            options.customErrors.mode = NFinal.Middleware.Mode.Off;
            options.defaultDocument = "Index.html";
            options.urlRouteRule = NFinal.Middleware.UrlRouteRule.AreaControllerCustomActionUrl;
            appBuilder.Use<NFinal.Middleware.OwinMiddleware>(options);
            //appBuilder.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}
