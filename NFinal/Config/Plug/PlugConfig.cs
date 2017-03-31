using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Newtonsoft.Json;

namespace NFinal.Config.Plug
{
    public class PlugConfig
    {
        [JsonIgnore]
        public SimpleJSON.JSONObject JsonObject;

        public Dictionary<string, string> appSettings;
        public Dictionary<string, ConnectionString> connectionStrings;
        public string[] verbs;
        public SessionState sessionState;
        public Url url;
        public Cookie cookie;
        public string defaultSkin;
        public CustomErrors customErrors;
    }
}
