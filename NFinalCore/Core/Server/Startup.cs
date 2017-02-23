﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
namespace NFinal
{
    public class Startup
    {
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMemoryCache();
        }
        public void Configure(IApplicationBuilder appBuilder)
        {
            NFinal.Middleware.MiddlewareConfigOptions options = new NFinal.Middleware.MiddlewareConfigOptions();
            options.plugs = new NFinal.Middleware.Plug[] {
                new NFinal.Middleware.Plug { filePath = @"E:\work\NFinal2\NFinal2\CoreWebTest\bin\CoreWebTest.dll" ,subDomain="www"} };
            options.debug = true;
            options.customErrors = new NFinal.Middleware.CustomErrors();
            options.customErrors.mode = NFinal.Middleware.Mode.Off;
            options.defaultDocument = "Index.html";
            options.urlRouteRule = NFinal.Middleware.UrlRouteRule.AreaControllerCustomActionUrl;
            //appBuilder.Use(options);
            //appBuilder.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}