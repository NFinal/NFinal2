using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace NFinal.Icon
{
    public class Favicon
    {
        public static bool hasInit = false;
        private static System.IO.FileStream fs = null;
        public static void Init<TContext,TResquest>(List<KeyValuePair<string,NFinal.Middleware.ActionData<TContext, TResquest>>> actionDataList)
        {
            //hasInit = true;
            //string iconPath = NFinal.Utility.MapPath("/favicon.ico");
            //if (System.IO.File.Exists(iconPath))
            //{
            //    fs = new FileStream(iconPath, FileMode.Open);
            //    NFinal.Middleware.ActionData<TContext, TResquest> actionData = new Middleware.ActionData<TContext, TResquest>();
            //    actionData.actionExecute = Favicon.Execute;
            //    actionDataList.Add(new KeyValuePair<string, NFinal.Middleware.ActionData<TContext, TResquest>>("/favicon.ico", actionData));
            //}
        }
        public static void Execute<TContext,TResquest>(TContext context,NFinal.Middleware.ActionData<TContext, TResquest> actionData,NFinal.Owin.Request request)
        {
            var environment = context as IDictionary<string,object>;
            if (environment != null)
            {
                var headers = environment.GetResponseHeaders();
                headers.Add("Content-Type", new string[] { "image/x-icon" });
                headers.Add("Cache-Control", new string[] { "public" });
                environment.SetResponseStatusCode(200);
                Stream stream = environment.GetResponseBody();
                fs.Seek(0, SeekOrigin.Begin);
                fs.CopyTo(stream);
                fs.Flush();
            }
        }
    }
}
