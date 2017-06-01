//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : CacheType.cs
//        Description :缓存类型枚举
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

namespace NFinal.Cache
{
    /// <summary>
    /// 缓存类型枚举
    /// </summary>
    public enum CacheType
    {
        /// <summary>
        /// 绝对超时
        /// </summary>
        AbsoluteExpiration,
        /// <summary>
        /// 滑动超时
        /// </summary>
        SlidingExpiration,
        /// <summary>
        /// 不超时
        /// </summary>
        NoExpiration
    }
}
