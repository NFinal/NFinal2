using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
#if NETCORE
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
#endif
#if NET461
using Owin;
using Microsoft.Owin;
#endif
namespace NFinal
{
    public class Startup
    {
#if NETCORE
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
        public void Configure(IApplicationBuilder appBuilder)
        {
#endif
#if NET461
        public void Configure(IAppBuilder appBuilder)
        {
#endif
            NFinal.Middleware.MiddlewareConfigOptions options = new NFinal.Middleware.MiddlewareConfigOptions();
            options.plugs = new NFinal.Middleware.Plug[] {
                new NFinal.Middleware.Plug { filePath = @"E:\work\NFinal2\NFinal2\CoreWebTest\bin\CoreWebTest.dll" ,subDomain="www"} };
            options.debug = true;
            options.customErrors = new NFinal.Middleware.CustomErrors();
            options.customErrors.mode = NFinal.Middleware.Mode.Off;
            options.defaultDocument = "Index.html";
            options.urlRouteRule = NFinal.Middleware.UrlRouteRule.AreaControllerCustomActionUrl;
#if NET461
            appBuilder.Use<NFinal.Middleware.OwinMiddleware>(options);
#endif
#if NETCORE
            appBuilder.UseMiddleware<NFinal.Middleware.OwinMiddleware>(options);
#endif
            //appBuilder.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}
