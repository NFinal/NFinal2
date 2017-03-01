using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Json
{
    public struct JilJsonSerialize : IJsonSerialize
    {
        public object DeserializeObject(string json)
        {
            return null;
        }

        public T DeserializeObject<T>(string json)
        {
            throw new NotImplementedException();
        }

        public string SerializeObject(object obj)
        {
            throw new NotImplementedException();
        }
    }
}
