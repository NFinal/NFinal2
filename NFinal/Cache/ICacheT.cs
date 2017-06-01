//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ICacheT.cs
//        Description :缓存泛型接口类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFinal.Cache
{
    /// <summary>
    /// 缓存泛型接口类
    /// </summary>
    /// <typeparam name="TKey"></typeparam>
    /// <typeparam name="TValue"></typeparam>
    public interface ICacheT<TKey,TValue>
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(TKey key, TValue value);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns>获取是否成功</returns>
        bool TryGetValue(TKey key,out TValue value);
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(TKey key);
    }
}
