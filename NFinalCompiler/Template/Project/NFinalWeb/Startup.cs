using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin.Extensions;

namespace $safeprojectname$
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use<NFinal.Middleware.OwinMiddleware>();
            //appBuilder.UseStaticFiles();
            appBuilder.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}