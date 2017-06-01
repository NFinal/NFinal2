//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : UniqueKeyDictionayExtension.cs
//        Description :Dictionary覆盖添加扩展
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    /// <summary>
    /// 唯一Key添加字典
    /// </summary>
    public static class UniqueKeyDictionayExtension
    {
        /// <summary>
        /// 添加并覆盖KeyValue
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public static void AddValue<TValue>(this IDictionary<string, TValue> dic,string key, TValue value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }
        /// <summary>
        /// 把dictionary转换为QueryString格式
        /// </summary>
        /// <typeparam name="TValue"></typeparam>
        /// <param name="dic"></param>
        /// <returns></returns>
        public static string ToQueryString<TValue>(this IDictionary<string, TValue> dic)
        {
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;
            sb.Append('?');
            foreach (var obj in dic)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sb.Append('&');
                }
                sb.Append(Uri.EscapeUriString(obj.Key));
                sb.Append('=');
                sb.Append(Uri.EscapeUriString(obj.Value.ToString()));
            }
            return sb.ToString();
        }
    }
}
