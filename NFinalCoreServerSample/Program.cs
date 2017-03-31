using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using System.Reflection;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.StaticFiles;

namespace NFinalCoreServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string a = NFinal.IO.Path.GetApplicationPath("/a");
            string b = NFinal.IO.Path.GetProjectPath("/b");
            string c = NFinal.IO.Path.MapPath(NFinal.Config.Global.ProjectType.Console, "/c");
            bool debug = true;
            string url = null;
            if (debug)
            {
                url = "http://localhost:8083";
            }
            else
            {
                url = "http://localhost:80";
            }
            var host = new WebHostBuilder()
               .UseStartup<Startup>()
               .UseContentRoot(AppContext.BaseDirectory)
               .UseUrls(url)
               .UseKestrel()
               .Build();

            host.Run();
        }
    }
}