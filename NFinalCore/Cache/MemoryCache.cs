using System;
using System.Collections.Generic;

namespace NFinal.Cache
{
    /// <summary>
    /// 内存缓存类
    /// </summary>
    public struct MemoryCacheValue
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
    public class MemoryCache : Cache
    {
        private System.Threading.Timer timer = null;
        //private System.Timers.Timer timer = null;
        public static IDictionary<string, MemoryCacheValue> cacheStore = null;
        public MemoryCache(int minutes) : base(minutes)
        {
            cacheStore = new System.Collections.Concurrent.ConcurrentDictionary<string, MemoryCacheValue>(StringComparer.Ordinal);
            timer = new System.Threading.Timer(Timer_Elapsed, this, 5000, 0); 
        }

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
        public override bool HasKey(string key)
        {
            return cacheStore.ContainsKey(key);
        }
        public override void Remove(string key)
        {
            if (cacheStore.ContainsKey(key))
            {
                cacheStore.Remove(key);
            }
        }
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
                    cacheStore.Remove(key);
                }
            }
            return null;
        }
        public override void Set(string key, byte[] value, int minutes)
        {
            MemoryCacheValue CacheValue;
            CacheValue.expires = DateTimeOffset.Now.AddMinutes(minutes);
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
