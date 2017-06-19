using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Filter
{
    /// <summary>
    /// 控制器行为过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class AfterActionFilterAttribute : System.Attribute, IAfterActionFilter
    {
        /// <summary>
        /// 行为过滤
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract bool ActionFilter<TContext, TRequest>(NFinal.Action.AbstractAction<TContext, TRequest> action);
    }
}
