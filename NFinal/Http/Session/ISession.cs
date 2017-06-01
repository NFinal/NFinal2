//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ISession.cs
//        Description :Session接口
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

namespace NFinal.Http
{
    /// <summary>
    /// Session接口
    /// </summary>
    public interface ISession
    {
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        void Set<T>(string key, T t);
        /// <summary>
        /// 当前Session属性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        object this[string key]
        {
            get;
            set;
        }
    }
}
