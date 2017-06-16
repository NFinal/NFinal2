using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace NFinal.DependencyInjection
{
    public class TypeHandler : ITypeHandler
    {
        public RuntimeTypeHandle ImplementationTypeHandle { get; set; }

        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="options">配置参数</param>
        public void Configaure(params object[] options)
        {
            Type ImplementationType = Type.GetTypeFromHandle(ImplementationTypeHandle);
            Type[] types = Type.EmptyTypes;
            if (options.Length > 0)
            {
                types = new Type[options.Length];
            }
            for (int i = 0; i < options.Length; i++)
            {
                types[i] = options[i].GetType();
            }
            MethodInfo configureMethodInfo = ImplementationType.GetMethod("Configure", types);
            if (configureMethodInfo != null)
            {
                configureMethodInfo.Invoke(null, options);
            }
        }
    }
}
