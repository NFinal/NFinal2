using System;
using System.Collections.Generic;

namespace NFinal.Cache
{
    /// <summary>
    /// 页面缓存
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute: System.Attribute
    {
        /// <summary>
        /// 页面缓存
        /// </summary>
        /// <param name="server">服务缓存类型</param>
        /// <param name="browser">浏览器缓存类型</param>
        /// <param name="minutes">缓存时间</param>
        public CacheAttribute(Server server,Browser browser,int minutes)
        { }
        /// <summary>
        /// 页面缓存
        /// </summary>
        /// <param name="standard">标准缓存类型</param>
        /// <param name="minutes">缓存时间</param>
        public CacheAttribute(Standard standard,int minutes)
        { }
        /// <summary>
        /// 页面缓存
        /// </summary>
        /// <param name="standard">标准缓存类型</param>
        public CacheAttribute(Standard standard)
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