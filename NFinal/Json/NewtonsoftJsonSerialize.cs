//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : NewtonsoftJsonSerialize.cs
//        Description :基于Newtonsoft的Json序列化类
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

namespace NFinal.Json
{
    /// <summary>
    /// 基于Newtonsoft的Json序列化类
    /// </summary>
    public struct NewtonsoftJsonSerialize : IJsonSerialize
    {
        /// <summary>
        /// 把Json字符串转为object
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public object DeserializeObject(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject(json);
        }
        /// <summary>
        /// 把Json字符串转为自定义类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public T DeserializeObject<T>(string json)
        {
            return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(json);
        }
        /// <summary>
        /// 把object类型转为json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public string SerializeObject(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
