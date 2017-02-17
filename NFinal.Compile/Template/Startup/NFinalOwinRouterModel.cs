using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile.Template.Startup
{
    public struct NFinalOwinRouterModel
    {
        public string project;
        public List<ControllerFileData> controllerFileDataList;
        public ReWriteData rewriteData;
    }
}
