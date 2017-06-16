using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.DependencyInjection
{
    /// <summary>
    /// 依赖注入接口
    /// </summary>
    public interface IServiceCollection
    {
        /// <summary>
        /// 获取服务对象
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <returns>接口对象</returns>
        TInterface GetService<TInterface>();
        /// <summary>
        /// 获取服务对象
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <param name="t1">参数1</param>
        /// <returns>接口对象</returns>
        TInterface GetService<TInterface, T1>(T1 t1);
        /// <summary>
        /// 获取服务对象
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <typeparam name="T2">参数2类型</typeparam>
        /// <param name="t1">参数1</param>
        /// <param name="t2">参数2</param>
        /// <returns>接口对象</returns>
        TInterface GetService<TInterface, T1, T2>(T1 t1, T2 t2);
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
        TInterface GetService<TInterface, T1, T2, T3>(T1 t1, T2 t2, T3 t3);
        /// <summary>
        /// 设置服务
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <param name="ImplementationType">实现接口的类型</param>
        /// <returns>返回服务数据</returns>
        ServiceCache SetService<TInterface>(Type ImplementationType);
        /// <summary>
        /// 设置服务
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <param name="ImplementationType">实现接口的类型</param>
        /// <returns>返回服务数据</returns>
        ServiceCache SetService<TInterface, T1>(Type ImplementationType);
        /// <summary>
        /// 设置服务
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <typeparam name="T2">参数2类型</typeparam>
        /// <param name="ImplementationType">实现接口的类型</param>
        /// <returns>返回服务数据</returns>
        ServiceCache SetService<TInterface, T1, T2>(Type ImplementationType);
        /// <summary>
        /// 设置服务
        /// </summary>
        /// <typeparam name="TInterface">接口</typeparam>
        /// <typeparam name="T1">参数1类型</typeparam>
        /// <typeparam name="T2">参数2类型</typeparam>
        /// <typeparam name="T3">参数3类型</typeparam>
        /// <param name="ImplementationType">实现接口的类型</param>
        /// <returns>返回服务数据</returns>
        ServiceCache SetService<TInterface, T1, T2, T3>(Type ImplementationType);
    }
}
