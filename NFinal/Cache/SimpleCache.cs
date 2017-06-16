//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : SimpleCache.cs
//        Description :基于当前内存实现的简单缓存功能
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;

namespace NFinal.Cache
{
    /// <summary>
    /// 基于当前内存实现的简单缓存功能
    /// </summary>
    public struct SimpleCacheValue
    {
        /// <summary>
        /// 过期时间点
        /// </summary>
        public DateTimeOffset expires;
        /// <summary>
        /// 缓存内容
        /// </summary>
        public byte[] value;
    }
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class SimpleCache : Cache<string>
    {
        private static System.Threading.Timer timer = null;
        //private System.Timers.Timer timer = null;
        /// <summary>
        /// 缓存全局字典对象
        /// </summary>
        public static System.Collections.Concurrent.ConcurrentDictionary<string, SimpleCacheValue> cacheStore = null;
        /// <summary>
        /// 配置
        /// </summary>
        public static void Configaure()
        {
            cacheStore = new System.Collections.Concurrent.ConcurrentDictionary<string, SimpleCacheValue>();
            timer = new System.Threading.Timer(Timer_Elapsed, cacheStore, 5000, 0);
        }
        /// <summary>
        /// 缓存初始化
        /// </summary>
        /// <param name="minutes">滑动缓存时间</param>
        public SimpleCache(int minutes) : base(minutes)
        {
        }
        /// <summary>
        /// 缓存定期处理函数
        /// </summary>
        /// <param name="sender"></param>
        public static void Timer_Elapsed(object sender)
        {
            foreach (var cacheItem in cacheStore)
            {
                if (cacheItem.Value.expires < DateTimeOffset.Now)
                {
                    SimpleCacheValue simpleCacheValue;
                    cacheStore.TryRemove(cacheItem.Key,out simpleCacheValue);
                }
            }
        }
        /// <summary>
        /// 是否拥有某缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public override bool HasKey(string key)
        {
            return cacheStore.ContainsKey(key);
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key">key</param>
        public override void Remove(string key)
        {
            if (cacheStore.ContainsKey(key))
            {
                SimpleCacheValue simpleCacheValue;
                cacheStore.TryRemove(key,out simpleCacheValue);
            }
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public override byte[] Get(string key)
        {
            if (cacheStore.ContainsKey(key) && cacheStore[key].expires >= DateTimeOffset.Now)
            {
                if (cacheStore[key].expires >= DateTimeOffset.Now)
                {
                    return cacheStore[key].value;
                }
                else
                {
                    SimpleCacheValue simpleCacheValue;
                    cacheStore.TryRemove(key, out simpleCacheValue);
                }
            }
            return null;
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="minutes">缓存时间</param>
        public override void Set(string key, byte[] value, int minutes)
        {
            SimpleCacheValue CacheValue;
            CacheValue.expires = DateTimeOffset.Now.AddMinutes(minutes);
            if (cacheStore.ContainsKey(key) && value != null)
            {
                CacheValue.value = value;
                cacheStore[key] = CacheValue;
            }
            else
            {
                CacheValue.value = value;
                cacheStore.TryAdd(key, CacheValue);
            }
        }
    }
}
