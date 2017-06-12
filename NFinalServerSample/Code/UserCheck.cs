using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal.Action;
using NFinal.Owin;

namespace NFinalServerSample.Code
{
    /// <summary>
    /// 用户自定义过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class UserCheckAttribute : Attribute, NFinal.Filter.IAuthorizationFilter
    {
        public bool AuthorizationFilter<TContext, TRequest>(AbstractAction<TContext, TRequest> action)
        {
            return true;
        }
    }
}
