//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : IRequestFilter.cs
//        Description :Http请求信息过滤器接口
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

namespace NFinal.Filter
{
    /// <summary>
    /// Http请求信息过滤器接口
    /// </summary>
    /// <typeparam name="TRequest"></typeparam>
    public interface IRequestFilter<TRequest>
    {
        /// <summary>
        /// Request过滤器
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <returns>是否中断输出</returns>
        bool RequestFilter(TRequest request);
    }
}
