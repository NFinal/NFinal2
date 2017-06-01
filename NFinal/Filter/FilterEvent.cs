//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : FilterEvent.cs
//        Description :Owin过滤器代理
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
    /// Owin中间件过滤器代理
    /// </summary>
    /// <param name="environment"></param>
    /// <returns></returns>
    public delegate bool EnvironmentFilter(IDictionary < string, object > environment);
    /// <summary>
    /// Owin请求信息过滤器代理
    /// </summary>
    /// <param name="request"></param>
    /// <returns></returns>
    public delegate bool RequestFilter(NFinal.Owin.Request request);
    /// <summary>
    /// Owin响应信处过滤器代理
    /// </summary>
    /// <param name="response"></param>
    /// <returns></returns>
    public delegate string ResponseFilter(NFinal.Owin.Response response);
}
