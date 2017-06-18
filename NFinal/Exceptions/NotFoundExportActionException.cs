using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.Exceptions
{
    /// <summary>
    /// action导出错误
    /// </summary>
    public class NotFoundExportActionException:System.Exception
    {
        /// <summary>
        /// 视图数据类型未生成异常，通常是没有安装NFinalCompiler插件
        /// </summary>
        /// <param name="controllerName">控制器名称</param>
        /// <param name="methodName">方法名称</param>
        public NotFoundExportActionException(string controllerName,string methodName)
            :base($"在控制器{controllerName}中找不到{methodName}方法")
        {
        }
    }
}
