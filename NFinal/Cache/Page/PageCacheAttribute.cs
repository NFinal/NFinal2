//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : PageCacheAttribute.cs
//        Description :页面缓存设置特性
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
    /// 页面缓存设置特性
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class PageCacheAttribute: System.Attribute
    {
        /// <summary>
        /// 页面缓存
        /// </summary>
        /// <param name="server">服务缓存类型</param>
        /// <param name="browser">浏览器缓存类型</param>
        /// <param name="minutes">缓存时间</param>
        public PageCacheAttribute(ServerCache server,BrowserCache browser,int minutes)
        { }
        /// <summary>
        /// 页面缓存
        /// </summary>
        /// <param name="standard">标准缓存类型</param>
        /// <param name="minutes">缓存时间</param>
        public PageCacheAttribute(StandardCache standard,int minutes)
        { }
        /// <summary>
        /// 页面缓存
        /// </summary>
        /// <param name="standard">标准缓存类型</param>
        public PageCacheAttribute(StandardCache standard)
        { }
    }
    /// <summary>
    /// 静态文件缓存
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheFileAttribute : System.Attribute
    {
        /// <summary>
        /// 静态文件缓存
        /// </summary>
        /// <param name="minutes">缓存时间</param>
        public CacheFileAttribute(int minutes)
        { }
    }
    /// <summary>
    /// 内存缓存
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheNormalAttribute : System.Attribute
    {
        /// <summary>
        /// 内存缓存
        /// </summary>
        /// <param name="minutes">缓存时间</param>
        public CacheNormalAttribute(int minutes)
        { }
    }
}