﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Compile.Template.Startup
{
    public struct OwinRouterModel
    {
        public string project;
        public List<ControllerFileData> controllerFileDataList;
        public ReWriteData rewriteData;
    }
}
