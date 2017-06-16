using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace NFinal.DependencyInjection
{
    /// <summary>
    /// 服务数据
    /// </summary>
    public class ServiceCache
    {
        public Delegate GetServiceDelegate;
        public RuntimeTypeHandle ImplementationTypeHandle;
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="options">配置参数</param>
        public void Configaure(params object[] options)
        {
            Type ImplementationType = Type.GetTypeFromHandle(ImplementationTypeHandle);
            MethodInfo configureMethodInfo = ImplementationType.GetMethod("Configure", new Type[] { typeof(object[]) });
            configureMethodInfo.Invoke(null, options);
        }
    }
}
