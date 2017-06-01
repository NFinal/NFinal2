//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : NameValueCollection.cs
//        Description :代替微软官方的NameValueCollection类型，具有无损自动转换功能。
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using NFinal;

namespace NFinal
{
    /// <summary>
    /// 代替微软官方的NameValueCollection类型，具有无损自动转换功能。
    /// </summary>
    public class NameValueCollection : IEnumerable<KeyValuePair<string, StringContainer>>
    {
        /// <summary>
        /// 初始化
        /// </summary>
        public NameValueCollection()
        {
            collection = new NFinal.Collections.FastDictionary<string, StringContainer>(StringComparer.Ordinal);
        }
        private NFinal.Collections.FastDictionary<string, StringContainer> collection = null;
        /// <summary>
        /// 索引属性
        /// </summary>
        /// <param name="key">key</param>
        /// <returns>value</returns>
        public StringContainer this[string key]
        {
            get {
                if (collection.ContainsKey(key))
                {
                    return collection[key];
                }
                else
                {
                    return StringContainer.Empty;
                }
            }
            set {
                if (value.value==null)
                {
                    if (collection.ContainsKey(key))
                    {
                        collection.Remove(key);
                    }
                }
                else
                {
                    if (collection.ContainsKey(key))
                    {
                        collection[key] = value;
                    }
                    else
                    {
                        collection.Add(key, value);
                    }
                }
            }
        }
        /// <summary>
        /// 添加KeyValue
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Add(string key, string value)
        {
            this[key]=value;
        }
        /// <summary>
        /// 获取枚举类型
        /// </summary>
        /// <returns></returns>
        public IEnumerator<KeyValuePair<string, StringContainer>> GetEnumerator()
        {
            return collection.GetEnumerator();
        }
        /// <summary>
        /// 把keyValue转为QueryString格式
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            StringWriter sw = new StringWriter();
            bool firstChild = true;
            foreach (var item in collection)
            {
                if (firstChild)
                {
                    firstChild = false;
                }
                else
                {
                    sw.Write("&");
                }
                sw.Write(item.Key);
                sw.Write("=");
                sw.Write(item.Value.value.UrlEncode());
            }
            return sw.ToString();
        }
        /// <summary>
        /// 获取枚举类型
        /// </summary>
        /// <returns></returns>
        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }
    }
}
