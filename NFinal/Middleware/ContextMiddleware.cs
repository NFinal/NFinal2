#if (NET40 || NET451 || NET461)
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Owin;
using NFinal.Owin;
using NFinal.Action;

namespace NFinal.Middleware
{
    public class ContextMiddleware : Middleware<IOwinContext,IOwinRequest>
    {
        public ContextMiddleware(InvokeDelegate<IOwinContext> next) : base(next)
        {
            
        }
        
        public override IAction<IOwinContext,IOwinRequest> GetAction(IOwinContext context)
        {
            ContextAction<NFinal.User.User> controller= new ContextAction<NFinal.User.User>();
            controller.BaseInitialization(context,null);
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
#endif