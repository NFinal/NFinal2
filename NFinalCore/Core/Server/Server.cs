//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;

//namespace NFinal
//{
//    public class Server
//    {
//        public static bool debug = false;
//        public static void Start(bool debug=true)
//        {
//            Server.debug = debug;
//            string url = null;
//            if (debug)
//            {
//                url = "http://localhost:8080";
//            }
//            else
//            {
//                url = "http://localhost:80";
//            }
//            using (Microsoft.Owin.Hosting.WebApp.Start<NFinal.Startup>(url))
//            {
//                Console.WriteLine("服务器已经启动");
//                Console.ReadKey();
//            }
//        }
//    }
//}
