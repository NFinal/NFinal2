using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace NFinalCoreServer
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var host = new WebHostBuilder()
               .UseKestrel()
               .UseStartup<Startup>()
               //.UseContentRoot(Directory.GetCurrentDirectory())
               .Build();

            host.Run();
        }
    }
}