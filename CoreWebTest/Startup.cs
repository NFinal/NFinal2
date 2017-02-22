using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin.Extensions;

namespace CoreWebTest
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            NFinal.Middleware.MiddlewareConfigOptions options = new NFinal.Middleware.MiddlewareConfigOptions();
            options.plugs =  new NFinal.Middleware.Plug[] {
                new NFinal.Middleware.Plug { filePath = @"E:\work\NFinal2\NFinal2\CoreWebTest\bin\CoreWebTest.dll" ,subDomain="www"} };
            options.debug = true;
            options.customErrors = new NFinal.Middleware.CustomErrors();
            options.customErrors.mode = NFinal.Middleware.Mode.Off;
            options.defaultDocument = "Index.html";
            options.debugUrl = null;
            options.urlRouteRule = NFinal.Middleware.UrlRouteRule.AreaControllerCustomActionUrl;
            appBuilder.Use<SimpleMiddleware>(options);
            appBuilder.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}