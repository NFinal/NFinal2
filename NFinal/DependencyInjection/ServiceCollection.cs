using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Reflection.Emit;

namespace NFinal.DependencyInjection
{
    /// <summary>
    /// 依赖注入服务
    /// </summary>
    public class ServiceCollection : IServiceCollection
    {
        /// <summary>
        /// 注入初始化数据缓存
        /// </summary>
        private static NFinal.Collections.FastDictionary<RuntimeTypeHandle, ServiceCache> serviceCacheDictionary = null;
        /// <summary>
        /// 服务集合
        /// </summary>
        public ServiceCollection()
        {
            serviceCacheDictionary = new NFinal.Collections.FastDictionary<RuntimeTypeHandle, ServiceCache>();
        }
        /// <summary>
        /// 获取服务对象
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <returns>接口对象</returns>
        public TInterface GetService<TInterface>()
        {
            ServiceCache serviceCache;
            if (serviceCacheDictionary.TryGetValue(typeof(TInterface).TypeHandle, out serviceCache))
            {
                return ((Func<TInterface>)serviceCache.GetServiceDelegate)();
            }
            else
            {
                return default(TInterface);
            }
        }
        /// <summary>
        /// 获取服务对象
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <param name="t1">参数1</param>
        /// <returns>接口对象</returns>
        public TInterface GetService<TInterface, T1>(T1 t1)
        {
            ServiceCache serviceCache;
            if (serviceCacheDictionary.TryGetValue(typeof(TInterface).TypeHandle, out serviceCache))
            {
                return ((Func<T1, TInterface>)serviceCache.GetServiceDelegate)(t1);
            }
            else
            {
                return default(TInterface);
            }
        }
        /// <summary>
        /// 获取服务对象
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <typeparam name="T2">参数2类型</typeparam>
        /// <param name="t1">参数1</param>
        /// <param name="t2">参数2</param>
        /// <returns>接口对象</returns>
        public TInterface GetService<TInterface, T1,T2>(T1 t1,T2 t2)
        {
            ServiceCache serviceCache;
            if (serviceCacheDictionary.TryGetValue(typeof(TInterface).TypeHandle, out serviceCache))
            {
                return ((Func<T1,T2, TInterface>)serviceCache.GetServiceDelegate)(t1,t2);
            }
            else
            {
                return default(TInterface);
            }
        }
        /// <summary>
        /// 获取服务对象
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <typeparam name="T2">参数2类型</typeparam>
        /// <typeparam name="T3">参数3类型</typeparam>
        /// <param name="t1">参数1</param>
        /// <param name="t2">参数2</param>
        /// <param name="t3">参数3</param>
        /// <returns>接口对象</returns>
        public TInterface GetService<TInterface, T1,T2,T3>(T1 t1,T2 t2,T3 t3)
        {
            ServiceCache serviceCache;
            if (serviceCacheDictionary.TryGetValue(typeof(TInterface).TypeHandle, out serviceCache))
            {
                return ((Func<T1,T2,T3, TInterface>)serviceCache.GetServiceDelegate)(t1,t2,t3);
            }
            else
            {
                return default(TInterface);
            }
        }
        
        /// <summary>
        /// 获取动态方法
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <param name="ImplementationType">实现接口的类型</param>
        /// <param name="types">参数类型</param>
        /// <returns></returns>
        private DynamicMethod GetServiceMethod<TInterface>(Type ImplementationType,Type[] types)
        {
           
            if (typeof(TInterface).IsAssignableFrom(ImplementationType))
            {
                ConstructorInfo constructorInfo = ImplementationType.GetConstructor(types);
                DynamicMethod method = new DynamicMethod(Guid.NewGuid().ToString(), typeof(TInterface), types);
                ILGenerator methodIL = method.GetILGenerator();
                for (int i = 0; i < types.Length; i++)
                {
                    methodIL.Emit(OpCodes.Ldarg, i);
                }
                methodIL.Emit(OpCodes.Newobj, constructorInfo);
                methodIL.Emit(OpCodes.Ret);
                return method;
            }
            return null;
        }
        /// <summary>
        /// 设置服务
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <param name="rewrite">如果已经注册服务，是否需要重写</param>
        /// <param name="ImplementationType">实现接口的类型</param>
        /// <returns>返回服务数据</returns>
        public ITypeHandler SetService<TInterface>(Type ImplementationType, bool rewrite = true)
        {
            DynamicMethod method = GetServiceMethod<TInterface>(ImplementationType, Type.EmptyTypes);
            Delegate createInstanceDelegate = method.CreateDelegate(typeof(Func<TInterface>));
            ServiceCache serviceCache = new ServiceCache();
            serviceCache.GetServiceDelegate = createInstanceDelegate;
            serviceCache.ImplementationTypeHandle = ImplementationType.TypeHandle;
            RuntimeTypeHandle key = typeof(TInterface).TypeHandle;
            ServiceCache serviceCache1 = null;
            if (serviceCacheDictionary.TryGetValue(key, out serviceCache1))
            {
                if (rewrite || serviceCache1 != null)
                {
                    serviceCacheDictionary[key] = serviceCache;
                    serviceCache.allowConfigaure = true;
                }
                else
                {
                    serviceCache.allowConfigaure = false;
                }
            }
            else
            {
                serviceCacheDictionary.Add(key, serviceCache);
                serviceCache.allowConfigaure = true;
            }
            return serviceCache;
        }
        /// <summary>
        /// 设置服务
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <param name="rewrite">如果已经注册服务，是否需要重写</param>
        /// <param name="ImplementationType">实现接口的类型</param>
        /// <returns>返回服务数据</returns>
        public ITypeHandler SetService<TInterface, T1>(Type ImplementationType, bool rewrite = true)
        {
            DynamicMethod method = GetServiceMethod<TInterface>(ImplementationType, new Type[] { typeof(T1)});
            Delegate createInstanceDelegate = method.CreateDelegate(typeof(Func<T1,TInterface>));
            ServiceCache serviceCache = new ServiceCache();
            serviceCache.GetServiceDelegate = createInstanceDelegate;
            serviceCache.ImplementationTypeHandle = ImplementationType.TypeHandle;
            RuntimeTypeHandle key = typeof(TInterface).TypeHandle;
            ServiceCache serviceCache1;
            
            if (serviceCacheDictionary.TryGetValue(key, out serviceCache1))
            {
                if (rewrite || serviceCache1 != null)
                {
                    serviceCacheDictionary[key] = serviceCache;
                    serviceCache.allowConfigaure = true;
                }
                else
                {
                    serviceCache.allowConfigaure = false;
                }
            }
            else
            {
                serviceCacheDictionary.Add(key, serviceCache);
                serviceCache.allowConfigaure = true;
            }
            return serviceCache;
        }
        /// <summary>
        /// 设置服务
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <typeparam name="T2">参数2类型</typeparam>
        /// <param name="rewrite">如果已经注册服务，是否需要重写</param>
        /// <param name="ImplementationType">实现接口的类型</param>
        /// <returns>返回服务数据</returns>
        public ITypeHandler SetService<TInterface, T1,T2>(Type ImplementationType, bool rewrite = true)
        {
            DynamicMethod method = GetServiceMethod<TInterface>(ImplementationType, new Type[] { typeof(T1),typeof(T2) });
            Delegate createInstanceDelegate = method.CreateDelegate(typeof(Func<T1,T2, TInterface>));
            ServiceCache serviceCache = new ServiceCache();
            serviceCache.GetServiceDelegate = createInstanceDelegate;
            serviceCache.ImplementationTypeHandle = ImplementationType.TypeHandle;
            RuntimeTypeHandle key = typeof(TInterface).TypeHandle;
            ServiceCache serviceCache1;
            if (serviceCacheDictionary.TryGetValue(key, out serviceCache1))
            {
                if (rewrite || serviceCache1 != null)
                {
                    serviceCacheDictionary[key] = serviceCache;
                    serviceCache.allowConfigaure = true;
                }
                else
                {
                    serviceCache.allowConfigaure = false;
                }
            }
            else
            {
                serviceCacheDictionary.Add(key, serviceCache);
                serviceCache.allowConfigaure = true;
            }
            return serviceCache;
        }
        /// <summary>
        /// 设置服务
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <typeparam name="T2">参数2类型</typeparam>
        /// <typeparam name="T3">参数3类型</typeparam>
        /// <param name="rewrite">如果已经注册服务，是否需要重写</param>
        /// <param name="ImplementationType">实现接口的类型</param>
        /// <returns>返回服务数据</returns>
        public ITypeHandler SetService<TInterface, T1, T2,T3>(Type ImplementationType, bool rewrite = true)
        {
            DynamicMethod method = GetServiceMethod<TInterface>(ImplementationType, new Type[] { typeof(T1), typeof(T2),typeof(T3) });
            Delegate createInstanceDelegate = method.CreateDelegate(typeof(Func<T1, T2,T3, TInterface>));
            ServiceCache serviceCache = new ServiceCache();
            serviceCache.GetServiceDelegate = createInstanceDelegate;
            serviceCache.ImplementationTypeHandle = ImplementationType.TypeHandle;
            RuntimeTypeHandle key = typeof(TInterface).TypeHandle;
            ServiceCache serviceCache1;
            if (serviceCacheDictionary.TryGetValue(key, out serviceCache1))
            {
                if (rewrite || serviceCache1 != null)
                {
                    serviceCacheDictionary[key] = serviceCache;
                    serviceCache.allowConfigaure = true;
                }
                else
                {
                    serviceCache.allowConfigaure = false;
                }
            }
            else
            {
                serviceCacheDictionary.Add(key, serviceCache);
                serviceCache.allowConfigaure = true;
            }
            return serviceCache;
        }
    }
}
