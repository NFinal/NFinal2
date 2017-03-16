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
    /// 内存缓存
    /// </summary>
    public class SimpleCacheT<TValue> : NFinal.Cache.ICacheT<string, TValue>
    {
        private System.Threading.Timer timer = null;
        //private System.Timers.Timer timer = null;
        public static IDictionary<string, SimpleCacheValueT<TValue>> cacheStore = null;
        private int minutes = 0;
        public SimpleCacheT(int minutes)
        {
            this.minutes = minutes;
            cacheStore = new System.Collections.Concurrent.ConcurrentDictionary<string, SimpleCacheValueT<TValue>>(StringComparer.Ordinal);
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
        public void Remove(string key)
        {
            if (cacheStore.ContainsKey(key))
            {
                cacheStore.Remove(key);
            }
        }
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
