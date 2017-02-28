#if NETCORE
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace NFinal.Middleware
{
    public class CoreMiddleware : Middleware<HttpContext, HttpRequest>
    {
        public CoreMiddleware(RequestDelegate next, MiddlewareConfigOptions options):base((context) => { return next.Invoke(context); },options)
        {
        }
        public override IAction<HttpContext, HttpRequest> GetAction(HttpContext context)
        {
            return new NFinal.CoreAction<object, EmptyMasterPageModel>();
        }

        public override NameValueCollection GetParameters(HttpRequest request)
        {
            var queryString= request.QueryString.Value.Split('&');
            NameValueCollection nvc = new NameValueCollection();
            string[] kv;
            foreach (var qs in queryString)
            {
                kv=qs.Split('=');
                nvc.Add(kv[0], kv[1]);
            }
            foreach (var form in request.Form)
            {
                nvc.Add(form.Key,form.Value);
            }
            return nvc;
        }

        public override HttpRequest GetRequest(HttpContext context)
        {
            return context.Request;
        }

        public override string GetRequestMethod(HttpContext context)
        {
            return context.Request.Method;
        }

        public override string GetRequestPath(HttpContext context)
        {
            return context.Request.Path;
        }

        public override string GetSubDomain(HttpContext context)
        {
            return context.Request.Host.Host==null?"www":context.Request.Host.Host.Split('.')[0];
        }
    }
}
#endif
