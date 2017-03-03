using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Server.Kestrel;
using Microsoft.Extensions.FileProviders;

namespace NFinalCoreServer
{
    public class Startup
    {
        public void Configure(IApplicationBuilder app)
        {
            NFinal.Middleware.MiddlewareConfigOptions options = new NFinal.Middleware.MiddlewareConfigOptions();
            options.plugs = new NFinal.Middleware.Plug[] {
                new NFinal.Middleware.Plug { filePath = System.Reflection.Assembly.GetEntryAssembly().Location ,subDomain="www"} };
            options.debug = true;
            options.debugUrl = "http://localhost:8083";
            options.customErrors = new NFinal.Middleware.CustomErrors();
            options.customErrors.mode = NFinal.Middleware.Mode.Off;
            options.defaultDocument = "Index.html";
            options.urlRouteRule = NFinal.Middleware.UrlRouteRule.AreaControllerCustomActionUrl;
            app.UseMiddleware<NFinal.Middleware.CoreMiddleware>(options);
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            string root = AppContext.BaseDirectory;
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(root),
            });
        }
    }
}
