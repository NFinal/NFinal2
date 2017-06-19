using System;
using System.Collections.Generic;
using System.Text;
using NFinal.Action;

namespace NFinal.Filter
{
    /// <summary>
    /// 控制器行为过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Method)]
    public abstract class BeforeActionFilterAttribute :System.Attribute,IBeforeActionFilter
    {
        /// <summary>
        /// 行为过滤
        /// </summary>
        /// <typeparam name="TRequest"></typeparam>
        /// <typeparam name="TContext"></typeparam>
        /// <param name="action"></param>
        /// <returns></returns>
        public abstract bool ActionFilter<TContext, TRequest>(AbstractAction<TContext, TRequest> action);
    }

}
