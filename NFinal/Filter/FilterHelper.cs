//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : FilterHelper.cs
//        Description :过滤器帮助类,用于执行设置的过滤器
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
    /// 过滤器帮助类,用于执行设置的过滤器
    /// </summary>
    public class FilterHelper
    {
        /// <summary>
        /// 基础Http上下文过滤器执行函数
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="filters"></param>
        /// <param name="context"></param>
        /// <returns></returns>
        public static bool BaseFilter<TContext>(IBaseFilter<TContext>[] filters,TContext context)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!filter.BaseFilter(context))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
            return true;
        }
        /// <summary>
        /// Http请求过滤器执行函数
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="filters"></param>
        /// <param name="context"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        public static bool RequestFilter<TContext,TRequest>(IRequestFilter<TRequest>[] filters,TContext context,TRequest request)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!filter.RequestFilter(request))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
            return true;
        }
        /// <summary>
        /// Http响应过滤器执行函数
        /// </summary>
        /// <param name="filters"></param>
        /// <param name="response"></param>
        /// <returns></returns>
        public static bool ResponseFilter(IResponseFilter[] filters, NFinal.Owin.Response response)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!filter.ResponseFilter(response))
                    {
                        return false;
                    }
                }
            }
            else
            {
                return true;
            }
            return true;
        }
    }
}
