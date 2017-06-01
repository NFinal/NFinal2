//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ICookie.cs
//        Description :Cookie接口
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
    /// Cookie接口
    /// </summary>
    public interface ICookie
    {
        /// <summary>
        /// SessionId
        /// </summary>
        string SessionId
        {
            get;
        }
        /// <summary>
        /// 输出Cookie
        /// </summary>
        IDictionary<string, string> ResponseCookies { get; }
        /// <summary>
        /// 获取请求Cookie.设置输出Cookie
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string this[string key]
        {
            get;
            set;
        }
    }
}
