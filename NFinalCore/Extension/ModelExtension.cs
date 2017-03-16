using System;
using System.Collections.Generic;
using System.IO;
using NFinal;

namespace NFinal
{
    public static class ModelExtension
    {
        public static string ToJson<T>(this IEnumerable<T> modelList, NFinal.Json.DateTimeFormat format = Json.DateTimeFormat.LocalTimeNumber)
        {
            if (modelList == null)
            {
                return "null";
            }
            else
            {
                NFinal.IO.StringWriter sw = new NFinal.IO.StringWriter();
                NFinal.Json.GetJsonDelegate<T> dele = null;
                sw.Write("[");
                foreach (T model in modelList)
                {
                    if (dele == null)
                    {
                        dele = (NFinal.Json.GetJsonDelegate<T>)NFinal.Json.JsonHelper.GetDelegate(model, format);
                    }
                    else
                    {
                        sw.Write(",");
                    }
                    dele(model, sw, format);
                }
                sw.Write("]");
                return sw.ToString();
            }
        }
        
        public static string ToJson<T>(this T model,NFinal.Json.DateTimeFormat format=Json.DateTimeFormat.LocalTimeNumber)
        {
            NFinal.IO.StringWriter sw = new NFinal.IO.StringWriter();
            NFinal.Json.GetJsonDelegate<T> dele=(NFinal.Json.GetJsonDelegate<T>)NFinal.Json.JsonHelper.GetDelegate(model, format);
            dele(model, sw, format);
            return sw.ToString();
        }
        public static void WriteJson<T>(T model, NFinal.IO.IWriter sw, NFinal.Json.DateTimeFormat format = Json.DateTimeFormat.LocalTimeNumber)
        {
            NFinal.Json.GetJsonDelegate<T> dele = (NFinal.Json.GetJsonDelegate<T>)NFinal.Json.JsonHelper.GetDelegate(model, format);
            dele(model, sw, format);
        }
        public static void WriteJson<T>(IEnumerable<T> modelList, NFinal.IO.IWriter sw, NFinal.Json.DateTimeFormat format = Json.DateTimeFormat.LocalTimeNumber)
        {
            if (modelList == null)
            {
                sw.Write(Constant.nullString);
            }
            else
            {
                NFinal.Json.GetJsonDelegate<T> dele = null;
                sw.Write("[");
                foreach (T model in modelList)
                {
                    if (dele == null)
                    {
                        dele = (NFinal.Json.GetJsonDelegate<T>)NFinal.Json.JsonHelper.GetDelegate(model, format);
                    }
                    else
                    {
                        sw.Write(",");
                    }
                    dele(model, sw, format);
                }
                sw.Write("]");
            }
        }
    }
}
