using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinalServer
{
    class Program
    {
        public static void Main(string[] args)
        {
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
            using (Microsoft.Owin.Hosting.WebApp.Start<NFinalServer.Startup>(url))
            {
                Console.WriteLine("服务器已经启动");
                Console.ReadKey();
            }
        }
    }
}
