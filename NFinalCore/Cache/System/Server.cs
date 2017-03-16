using System;
using System.Collections.Generic;

namespace NFinal.Cache
{
    /// <summary>
    /// 服务器端缓存类型
    /// </summary>
    public enum Server
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