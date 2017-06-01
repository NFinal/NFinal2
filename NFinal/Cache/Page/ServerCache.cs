//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ServerCache.cs
//        Description :服务器缓存类型
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;

namespace NFinal.Cache.Page
{
    /// <summary>
    /// 服务器端缓存类型
    /// </summary>
    public enum ServerCache
    {
        /// <summary>
        /// 不缓存
        /// </summary>
        NoCache = 1,
        /// <summary>
        /// 文件依赖
        /// </summary>
        FileDependency = 2,
        /// <summary>
        /// 绝对超时
        /// </summary>
        AbsoluteExpiration = 4,
        /// <summary>
        /// 滑动超时
        /// </summary>
        SlidingExpiration = 8
    }
}