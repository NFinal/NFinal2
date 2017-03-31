using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Hosting;

namespace NFinalCoreWebSample
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string a = NFinal.IO.Path.GetApplicationPath("/a");
            string b = NFinal.IO.Path.GetProjectPath("/b");
            string c = Directory.GetCurrentDirectory();
            var host = new WebHostBuilder()
                .UseKestrel()
                .UseUrls("localhost:8083")
                .UseContentRoot(Directory.GetCurrentDirectory())
                .UseIISIntegration()
                .UseStartup<Startup>()
                .UseApplicationInsights()
                .Build();

            host.Run();
        }
    }
}
