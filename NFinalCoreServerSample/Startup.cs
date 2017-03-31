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
            string filePath = System.Reflection.Assembly.GetEntryAssembly().Location;
            app.UseMiddleware<NFinal.Middleware.CoreMiddleware>();
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
