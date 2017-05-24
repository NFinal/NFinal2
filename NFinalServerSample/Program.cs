using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal;

namespace NFinalServerSample
{
    public class Program
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
            using (Microsoft.Owin.Hosting.WebApp.Start<NFinalServerSample.Startup>(url))
            {
                Console.WriteLine("服务器已经启动");
                Console.ReadKey();
            }
        }
        public static void MM()
        {
            StringContainer str = "";
            int num = str;
        }
    }
}
