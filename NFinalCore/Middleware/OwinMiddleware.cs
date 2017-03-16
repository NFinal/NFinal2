
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using NFinal.Owin;
using NFinal.Action;

namespace NFinal.Middleware
{
    public class OwinMiddleware : Middleware<IDictionary<string, object>,Owin.Request>
    {
        public OwinMiddleware(InvokeDelegate<IDictionary<string, object>> next,MiddlewareConfigOptions options) : base(next,options)
        {  
        }

        public override IAction<IDictionary<string, object>,Owin.Request> GetAction(IDictionary<string, object> context)
        {
            NFinal.Owin.Request request = context.GetRequest();
            NFinal.OwinAction<EmptyMasterPageModel, object> controller = new OwinAction<EmptyMasterPageModel, object>();
            controller.BaseInitialization(context,null);
            return controller;
        }

        public override NameValueCollection GetParameters(Request request)
        {
            return request.parameters;
        }

        public override Request GetRequest(IDictionary<string, object> context)
        {
            return context.GetRequest();
        }

        public override string GetRequestMethod(IDictionary<string, object> context)
        {
            return context.GetRequestMethod();
        }

        public override string GetRequestPath(IDictionary<string, object> context)
        {
            return context.GetRequestPath();
        }

        public override string GetSubDomain(IDictionary<string, object> context)
        {
            string subDomain = context.GetSubDomain();
            if (subDomain == null)
            {
                subDomain = defaultSubDomain;
            }
            return subDomain;
        }
    }
}