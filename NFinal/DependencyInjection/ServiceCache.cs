using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace NFinal.DependencyInjection
{
    /// <summary>
    /// 服务数据
    /// </summary>
    public class ServiceCache: TypeHandler
    {
        /// <summary>
        /// 获取服务的函数代理
        /// </summary>
        public Delegate GetServiceDelegate;
    }
}
