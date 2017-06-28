using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Core.Filter
{
    /// <summary>
    /// Http响应信息过滤器
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public abstract class ResponseFilterAttribute : System.Attribute, NFinal.Filter.IResponseFilter
    {
        /// <summary>
        /// 需要重写的Owin向应信息过滤函数
        /// </summary>
        /// <param name="response">Owin向应信息</param>
        /// <returns></returns>
        public abstract bool ResponseFilter(NFinal.Owin.Response response);
    }
}
