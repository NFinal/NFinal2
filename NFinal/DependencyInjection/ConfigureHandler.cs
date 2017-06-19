using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;

namespace NFinal.DependencyInjection
{
    public class ConfigureHandler : ITypeHandler
    {
        public RuntimeTypeHandle ImplementationTypeHandle { get; set; }
        public bool allowConfigaure { get; set; }
        public ConfigureHandler()
        {
            this.allowConfigaure = true;
        }
        public ITypeHandler SetType(Type type)
        {
            this.ImplementationTypeHandle = type.TypeHandle;
            return this;
        }
        /// <summary>
        /// 服务配置
        /// </summary>
        /// <param name="options">配置参数</param>
        public void Configure(params object[] options)
        {
            if (allowConfigaure)
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
}
