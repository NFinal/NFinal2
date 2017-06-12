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
        public static bool ParamaterFilter(IParameterFilter[] filters, NameValueCollection parameters)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!filter.ParameterFilter(parameters))
                    {
                        return false;
                    }
                }
            }
            return true;
        }

        public static bool AuthorizationFilter<TContext, TRequest>(IAuthorizationFilter[] filters, NFinal.Action.AbstractAction<TContext, TRequest> action)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!filter.AuthorizationFilter(action))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool BeforeActionFilter<TContext, TRequest>(IBeforeActionFilter[] filters, NFinal.Action.AbstractAction<TContext, TRequest> action)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!filter.ActionFilter(action))
                    {
                        return false;
                    }
                }
            }
            return true;
        }
        public static bool AfterActionFilter<TContext, TRequest>(IAfterActionFilter[] filters, NFinal.Action.AbstractAction<TContext, TRequest> action)
        {
            if (filters != null)
            {
                foreach (var filter in filters)
                {
                    if (!filter.ActionFilter(action))
                    {
                        return false;
                    }
                }
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
            return true;
        }
    }
}
