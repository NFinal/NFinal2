using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal.Owin;
using Microsoft.AspNetCore.Http;

namespace NFinalCoreServer.Code
{
    /// <summary>
    /// 用户自定义过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class UserCheckAttribute : Attribute, NFinal.Filter.IRequestFilter<HttpRequest>
    {
        public bool RequestFilter(HttpRequest request)
        {
            return true;
        }
    }
}
