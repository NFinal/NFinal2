using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Config
{
    public class KeyValueConfig
    {
        private static IDictionary<string,string> keyValue=null;
        public KeyValueConfig(string config)
        {
            if (keyValue == null)
            {
                keyValue = new Dictionary<string, string>();
            }
        }
        public string GetString(string key)
        {
            if (keyValue.ContainsKey(key))
            {
                return keyValue[key];
            }
            return null;
        }
    }
}
