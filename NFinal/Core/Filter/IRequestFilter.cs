using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Filter
{
    public interface IRequestFilter<TRequest>
    {
        /// <summary>
        /// Request过滤器
        /// </summary>
        /// <param name="request">请求信息</param>
        /// <returns>是否中断输出</returns>
        bool RequestFilter(TRequest request);
    }
}
