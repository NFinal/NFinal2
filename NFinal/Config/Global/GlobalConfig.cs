using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;

namespace NFinal.Config.Global
{
    
    public class GlobalConfig
    {
        public Plug[] plugs;
        public Debug debug;
        public string indexDocument;
        public ProjectType projectType;
        [JsonIgnore]
        public SimpleJSON.JSONObject JsonObject;
    }
}
