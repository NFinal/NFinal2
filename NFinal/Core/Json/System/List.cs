using System;
using System.Collections.Generic;
using System.IO;


namespace System
{
    public static class JsonExtension
    {
        public static string ToJson<T>(this IEnumerable<T> structs, bool addBracket = true) where T : NFinal.IJson
        {
            StringWriter sw = new StringWriter();
            bool isFirst = true;
            if (addBracket)
            {
                sw.Write('[');
            }
            foreach (var str in structs)
            {
                if (isFirst)
                {
                    isFirst = true;
                }
                else
                {
                    sw.Write(',');
                }

                str.WriteJson(sw);
            }
            if (addBracket)
            {
                sw.Write(']');
            }
            return sw.ToString();
        }
        public static string ToJson<T>(this IEnumerable<dynamic> structs, bool addBracket = true)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(structs);
        }
        public static string ToJson<T>(this T t)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(t);
        }
    }
}
