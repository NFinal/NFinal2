using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Filter
{
    public interface IResponseFilter
    {
        /// <summary>
        /// 返回过滤器
        /// </summary>
        /// <param name="response">响应数据</param>
        /// <returns>是否中断输出</returns>
        bool ResponseFilter(NFinal.Owin.Response response);
    }
}
