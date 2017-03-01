using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal.Owin;

namespace NFinalCoreServer.Code
{
    /// <summary>
    /// 用户自定义过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class UserCheckAttribute : Attribute, NFinal.Filter.IRequestFilter<NFinal.Owin.Request>
    {
        public bool RequestFilter(Request request)
        {
            return true;
        }
    }
}
