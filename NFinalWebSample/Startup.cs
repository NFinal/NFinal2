using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin.Extensions;

namespace NFinalWebSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            NFinal.Middleware.Config.MiddlewareConfigOptions options = new NFinal.Middleware.Config.MiddlewareConfigOptions();
            options.plugs =  new NFinal.Plugs.Plug[] {
                new NFinal.Plugs.Plug { filePath = @"E:\work\NFinal2\NFinal2\NFinalWebSample\bin\NFinalWebSample.dll" ,subDomain="www"} };
            options.debug = true;
            options.customErrors = new NFinal.Middleware.Config.CustomErrors();
            options.customErrors.mode = NFinal.Middleware.Config.Mode.Off;
            options.defaultDocument = "Index.html";
            options.debugUrl = null;
            options.urlRouteRule = NFinal.Middleware.Config.UrlRouteRule.AreaControllerCustomActionUrl;
            appBuilder.Use<SimpleMiddleware>(options);
            appBuilder.UseStaticFiles();
            appBuilder.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}