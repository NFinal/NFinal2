using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFinal.Owin;
using Microsoft.AspNetCore.Http;
using NFinal.Action;

namespace NFinalCorePlug.Code
{
    /// <summary>
    /// 用户自定义过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class UserCheckAttribute : Attribute, NFinal.Filter.IBeforeActionFilter
    {
        public bool ActionFilter<TContext, TRequest>(AbstractAction<TContext, TRequest> action)
        {
            return true;
        }
    }
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public class UserAuthroizationAttribute : Attribute, NFinal.Filter.IAuthorizationFilter
    {
        public bool AuthorizationFilter<TContext, TRequest>(AbstractAction<TContext, TRequest> action)
        {
            return true;
        }
    }
    [AttributeUsage(AttributeTargets.Method | AttributeTargets.Class)]
    public class AfterActionAttribute : Attribute, NFinal.Filter.IAfterActionFilter
    {
        public bool ActionFilter<TContext, TRequest>(AbstractAction<TContext, TRequest> action)
        {
            return true;
        }
    }
}
