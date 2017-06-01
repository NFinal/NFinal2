//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : IBaseFilter.cs
//        Description :Http上下文过滤器接口
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
    /// Http上下文过滤器接口
    /// </summary>
    /// <typeparam name="TContext"></typeparam>
    public interface IBaseFilter<TContext>
    {
        /// <summary>
        /// Http上下文过滤器接口
        /// </summary>
        /// <param name="context">Http上下文</param>
        /// <returns></returns>
        bool BaseFilter(TContext context);
    }
}
