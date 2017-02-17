using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owin;
namespace NFinal
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            NFinal.Middleware.OwinMiddlewareConfigOptions options = new NFinal.Middleware.OwinMiddlewareConfigOptions();
            options.plugs = new string[] {};
            options.debug = Server.debug;
            appBuilder.Use(typeof(NFinal.Middleware.NFinalOwinMiddleware), options);
        }
    }
}
