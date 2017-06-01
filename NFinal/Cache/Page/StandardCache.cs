//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : StandardCache.cs
//        Description :标准缓存类型
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
    /// 标准缓存类型
    /// </summary>
    public enum StandardCache
    {
        /// <summary>
        /// 文件缓存
        /// </summary>
        File,
        /// <summary>
        /// 内存缓存
        /// </summary>
        Normal
    }
}