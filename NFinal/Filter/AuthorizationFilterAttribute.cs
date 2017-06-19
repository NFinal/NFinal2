using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Filter
{ 
    /// <summary>
    /// 用户验证过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method|AttributeTargets.Class)]
    public abstract class AuthorizationFilterAttribute:System.Attribute,NFinal.Filter.IAuthorizationFilter
    {
        /// <summary>
        /// 用户验证
        /// </summary>
        /// <typeparam name="TContext"></typeparam>
        /// <typeparam name="TRequest"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract bool AuthorizationFilter<TContext, TRequest>(NFinal.Action.AbstractAction<TContext, TRequest> action);
    }
}
