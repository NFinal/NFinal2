using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Owin;
using NFinal.Owin;

namespace NFinal.Middleware
{
    public class ContextMiddleware : Middleware<IOwinContext,IOwinRequest>
    {
        public ContextMiddleware(InvokeDelegate<IOwinContext> next, MiddlewareConfigOptions options) : base(next, options)
        {
        }

        public override IAction<IOwinContext,IOwinRequest> GetAction(IOwinContext context)
        {
            ContextAction<object,object> controller= new ContextAction<object, object>();
            controller.BaseInitialization(context);
            return controller;
        }

        public override NameValueCollection GetParameters(IOwinRequest request)
        {
            return null;
        }

        public override IOwinRequest GetRequest(IOwinContext context)
        {
            return context.Request;
        }

        public override string GetRequestMethod(IOwinContext context)
        {
            throw new NotImplementedException();
        }

        public override string GetRequestPath(IOwinContext context)
        {
            return context.Request.Path.Value;
        }

        public override string GetSubDomain(IOwinContext context)
        {
            throw new NotImplementedException();
        }
    }
}
