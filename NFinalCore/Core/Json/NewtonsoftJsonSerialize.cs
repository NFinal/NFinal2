using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Json
{
    public struct NewtonsoftJsonSerialize : IJsonSerialize
    {
        public object DeserializeObject(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        }

        public T DeserializeObject<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }

        public string SerializeObject(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
