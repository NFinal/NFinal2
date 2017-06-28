using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Owin;

namespace $safeprojectname$
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use<NFinal.Middleware.OwinMiddleware>();
            appBuilder.UseStaticFiles();
        }
    }
}
