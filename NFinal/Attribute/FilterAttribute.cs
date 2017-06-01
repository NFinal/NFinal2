//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : FilterAttribute.cs
//        Description :过滤器基类信息
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
using NFinal.Filter;
using NFinal.Owin;

namespace NFinal
{
    /// <summary>
    /// Environment过滤器设置
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,Inherited =true)]
    public abstract class EnvironmentFilterAttribute : System.Attribute, IEnvironmentFilter
    {
        /// <summary>
        /// 需要重写的过滤器函数
        /// </summary>
        /// <param name="environment"></param>
        /// <returns></returns>
        public abstract bool BaseFilter(IDictionary<string, object> environment);
    }
    /// <summary>
    /// Owin请求信息过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,Inherited =true)]
    public abstract class OwinRequestFilterAttribute : System.Attribute, NFinal.Filter.IRequestFilter<NFinal.Owin.Request>
    {
        /// <summary>
        /// 需要重写的Owin请求信息过滤函数
        /// </summary>
        /// <param name="request">Owin请求信息</param>
        /// <returns></returns>
        public abstract bool RequestFilter(Request request);
    }
    /// <summary>
    /// Http响应信息过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true,Inherited =true)]
    public abstract class ResponseFilterAttribute : System.Attribute, NFinal.Filter.IResponseFilter
    {
        /// <summary>
        /// 需要重写的Owin向应信息过滤函数
        /// </summary>
        /// <param name="response">Owin向应信息</param>
        /// <returns></returns>
        public abstract bool ResponseFilter(Response response);
    }
}
