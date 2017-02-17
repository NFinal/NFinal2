using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Json
{
    public struct SimpleJsonSerialize : IJsonSerialize
    {
        public object DeserializeObject(string json)
        {
            return SimpleJson.DeserializeObject(json);
        }

        public T DeserializeObject<T>(string json)
        {
            return SimpleJson.DeserializeObject<T>(json);
        }

        public string SerializeObject(object obj)
        {
            return SimpleJson.SerializeObject(obj);
        }
    }
}
