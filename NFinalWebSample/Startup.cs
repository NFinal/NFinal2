using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Owin;
using Microsoft.Owin.Extensions;

namespace NFinalWebSample
{
    public class Startup
    {
        public void Configuration(IAppBuilder appBuilder)
        {
            appBuilder.Use<SimpleMiddleware>();
            appBuilder.UseStaticFiles();
            appBuilder.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}