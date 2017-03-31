using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using System.Reflection;

namespace NFinal.Plugs
{
    public class PlugInfo
    {
        public bool loadSuccess;
        public Assembly assembly;
        public bool enable;
        public string name;
        public string urlPrefix;
        public string description;
        public string assemblyPath;
        public string configPath;
        public NFinal.Config.Plug.PlugConfig config;
    }
}
