//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : SimpleCacheT.cs
//        Description :基于当前内存实现的简单的泛型缓存功能
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
    /// 内存缓存类
    /// </summary>
    public struct SimpleCacheValueT<TValue>
    {
        /// <summary>
        /// 过期时间点
        /// </summary>
        public DateTimeOffset expires;
        /// <summary>
        /// 缓存内容
        /// </summary>
        public TValue value;
    }
    /// <summary>
    /// 基于当前内存实现的简单的泛型缓存功能
    /// </summary>
    public class SimpleCacheT<TValue> : NFinal.Cache.ICacheT<string, TValue>
    {
        private System.Threading.Timer timer = null;
        //private System.Timers.Timer timer = null;
        /// <summary>
        /// 缓存全局字典对象
        /// </summary>
        public static IDictionary<string, SimpleCacheValueT<TValue>> cacheStore = null;
        private int minutes = 0;
        /// <summary>
        /// 缓存初始化
        /// </summary>
        /// <param name="minutes">缓存时间</param>
        public SimpleCacheT(int minutes)
        {
            this.minutes = minutes;
            cacheStore = new System.Collections.Concurrent.ConcurrentDictionary<string, SimpleCacheValueT<TValue>>(StringComparer.Ordinal);
            timer = new System.Threading.Timer(Timer_Elapsed, this, 5000, 0);
        }
        /// <summary>
        /// 缓存定期处理函数
        /// </summary>
        /// <param name="sender"></param>
        private void Timer_Elapsed(object sender)
        {
            foreach (var cacheItem in cacheStore)
            {
                if (cacheItem.Value.expires < DateTimeOffset.Now)
                {
                    cacheStore.Remove(cacheItem);
                }
            }
        }
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">key</param>
        public void Remove(string key)
        {
            if (cacheStore.ContainsKey(key))
            {
                cacheStore.Remove(key);
            }
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
        public bool TryGetValue(string key, out TValue value)
        {
            SimpleCacheValueT<TValue> simpleCacheValue;
            if (cacheStore.TryGetValue(key, out simpleCacheValue))
            {
                if (cacheStore[key].expires >= DateTimeOffset.Now)
                {
                    value = cacheStore[key].value;
                }
                else
                {
                    cacheStore.Remove(key);
                }
                value = simpleCacheValue.value;
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Set(string key, TValue value)
        {
            SimpleCacheValueT<TValue> CacheValue;
            CacheValue.expires = DateTimeOffset.Now.AddMinutes(this.minutes);
            if (cacheStore.ContainsKey(key) && value != null)
            {
                CacheValue.value = value;
                cacheStore[key] = CacheValue;
            }
            else
            {
                CacheValue.value = value;
                cacheStore.Add(key, CacheValue);
            }
        }


    }
}
