using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Threading.Tasks;
using NFinal;
using NFinal.Middleware;

namespace NFinalWebSample
{
    public class SimpleMiddleware : NFinal.Middleware.OwinMiddleware
    {
        public SimpleMiddleware(NFinal.Middleware.InvokeDelegate<IDictionary<string, object>> next, NFinal.Middleware.Config.MiddlewareConfigOptions options) : base(next, options)
        {
        }
    }
}
