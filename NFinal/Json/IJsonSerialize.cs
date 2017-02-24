using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Json
{
    public interface IJsonSerialize
    {
        string SerializeObject(object obj);
        T DeserializeObject<T>(string json);
        object DeserializeObject(string json);
    }
}
