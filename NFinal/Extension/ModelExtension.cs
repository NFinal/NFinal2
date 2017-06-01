//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ModelExtension.cs
//        Description :Model转换为Json字符串
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.IO;
using NFinal;

namespace NFinal
{
    /// <summary>
    /// Model转换为Json字符串
    /// </summary>
    public static class ModelExtension
    {
        /// <summary>
        /// Model集合转为Json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelList"></param>
        /// <param name="format"></param>
        /// <returns></returns>
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
        /// <summary>
        /// Model转为Json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="format"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T model,NFinal.Json.DateTimeFormat format=Json.DateTimeFormat.LocalTimeNumber)
        {
            NFinal.IO.StringWriter sw = new NFinal.IO.StringWriter();
            NFinal.Json.GetJsonDelegate<T> dele=(NFinal.Json.GetJsonDelegate<T>)NFinal.Json.JsonHelper.GetDelegate(model, format);
            dele(model, sw, format);
            return sw.ToString();
        }
        /// <summary>
        /// 写Json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="model"></param>
        /// <param name="sw"></param>
        /// <param name="format"></param>
        public static void WriteJson<T>(T model, NFinal.IO.IWriter sw, NFinal.Json.DateTimeFormat format = Json.DateTimeFormat.LocalTimeNumber)
        {
            NFinal.Json.GetJsonDelegate<T> dele = (NFinal.Json.GetJsonDelegate<T>)NFinal.Json.JsonHelper.GetDelegate(model, format);
            dele(model, sw, format);
        }
        /// <summary>
        /// 写Json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="modelList"></param>
        /// <param name="sw"></param>
        /// <param name="format"></param>
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
