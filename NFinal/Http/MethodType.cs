//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : MethodType.cs
//        Description :Http请求方法类型
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
    /// Http请求类型
    /// </summary>
    public enum MethodType
    {
        /// <summary>
        /// 未知
        /// </summary>
        NONE = 0,
        /// <summary>
        /// Post方法
        /// </summary>
        POST = 1,
        /// <summary>
        /// Get方法
        /// </summary>
        GET = 2,
        /// <summary>
        /// Put方法
        /// </summary>
        PUT = 3,
        /// <summary>
        /// Delte方法
        /// </summary>
        DELETE = 4,
        /// <summary>
        /// Ajax方法
        /// </summary>
        AJAX = 5
    }
}
