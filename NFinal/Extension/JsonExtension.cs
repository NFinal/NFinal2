//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : JsonExtension.cs
//        Description :Json扩展类，用于IJson数组或集合的转换
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using Newtonsoft.Json;
using NFinal.IO;

namespace NFinal
{
    /// <summary>
    /// Json扩展类，用于数组转换
    /// </summary>
    public static class JsonExtension
    {
        /// <summary>
        /// 把IJson集合转为Json字符串
        /// </summary>
        /// <typeparam name="T">泛型类型，继承IJSON接口</typeparam>
        /// <param name="structs">实体类</param>
        /// <param name="addBracket">添加括号</param>
        /// <returns></returns>
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
        /// <summary>
        /// 把任意实体类集合转换为Json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="structs"></param>
        /// <param name="addBracket"></param>
        /// <returns></returns>
        public static string ToJson<T>(this IEnumerable<dynamic> structs, bool addBracket = true)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(structs);
        }
        /// <summary>
        /// 把任意实体类转换为Json字符串
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="t"></param>
        /// <returns></returns>
        public static string ToJson<T>(this T t)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(t,new Newtonsoft.Json.Converters.JavaScriptDateTimeConverter());
        }
    }
}
