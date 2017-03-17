using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.FileProviders;

namespace NFinalCoreWebSample
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, ILoggerFactory loggerFactory)
        {
            NFinal.Middleware.Config.MiddlewareConfigOptions options = new NFinal.Middleware.Config.MiddlewareConfigOptions();
            options.plugs = new NFinal.Plugs.Plug[] {
                new NFinal.Plugs.Plug { filePath = System.Reflection.Assembly.GetEntryAssembly().Location ,subDomain="www"} };
            options.debug = true;
            options.debugUrl = "http://localhost:59893/";
            options.customErrors = new NFinal.Middleware.Config.CustomErrors();
            options.customErrors.mode = NFinal.Middleware.Config.Mode.Off;
            options.defaultDocument = "Index.html";
            options.urlRouteRule = NFinal.Middleware.Config.UrlRouteRule.AreaControllerCustomActionUrl;
            app.UseMiddleware<NFinal.Middleware.CoreMiddleware>(options);
            //app.Run(async (context) =>
            //{
            //    await context.Response.WriteAsync("Hello World!");
            //});
            string root = System.IO.Directory.GetCurrentDirectory();
            app.UseStaticFiles(new StaticFileOptions()
            {
                FileProvider = new PhysicalFileProvider(root),
            });
        }
    }
}
