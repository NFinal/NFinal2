using System;
using System.Collections.Generic;

namespace NFinal.Cache
{
    /// <summary>
    /// 浏览器缓存类型
    /// </summary>
    public enum Browser
    {
        /// <summary>
        /// 不缓存
        /// </summary>
        NoStore = 16,
        /// <summary>
        /// 文件的修改日期不更改,则一直缓存,刷新后返回304.
        /// </summary>
        NotModify = 32,
        /// <summary>
        /// 一定时间内访问缓存,超时可重新获取页面
        /// </summary>
        Expires = 64,
        /// <summary>
        /// 缓存一直存在不过期,但可刷新页面重新获取
        /// </summary>
        NoExpires = 128
    }
}