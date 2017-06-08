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
            NFinal.Config.Configration.getSessionCache=()=>{
                return new NFinal.Cache.SimpleCache(30);
            };
            NFinal.Config.Configration.getSerializable = () =>
            {
                return new NFinal.ProtobufSerialize();
            };
            appBuilder.Use<NFinal.Middleware.OwinMiddleware>();
            appBuilder.UseStaticFiles();
            appBuilder.UseStageMarker(PipelineStage.Authenticate);
        }
    }
}