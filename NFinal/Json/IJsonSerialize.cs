//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : IJsonSerialize.cs
//        Description :Json序列化接口
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
    /// Json序列化接口
    /// </summary>
    public interface IJsonSerialize
    {
        /// <summary>
        /// 把object类型转为Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        string SerializeObject(object obj);
        /// <summary>
        /// 把Json字符串转为自定义类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        T DeserializeObject<T>(string json);
        /// <summary>
        /// 把Json字符串转为object类型
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        object DeserializeObject(string json);
    }
}
