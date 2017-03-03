using System;
using System.Collections.Generic;
#if (NETCORE || CORE)
using Microsoft.Extensions.Caching.Memory;
#endif
namespace NFinal.Cache
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class MemoryCache : Cache
    {
        //private System.Threading.Timer timer = null;
        //private System.Timers.Timer timer = null;
        //public static IDictionary<string, MemoryCacheValue> cacheStore = null;
        public int minutes;
#if !(NETCORE || CORE)
        System.Runtime.Caching.MemoryCache _memoryCache = null;
        System.Runtime.Caching.CacheItemPolicy _cacheItemPolicy = null;
#else
        Microsoft.Extensions.Caching.Memory.MemoryCache _memoryCache = null;
#endif
        public MemoryCache(int minutes) : base(minutes)
        {
            this.minutes = minutes;
            if (_memoryCache == null)
            {
#if !(NETCORE || CORE)
                _memoryCache = System.Runtime.Caching.MemoryCache.Default;
                _cacheItemPolicy = new System.Runtime.Caching.CacheItemPolicy();
                _cacheItemPolicy.SlidingExpiration = TimeSpan.FromMinutes(this.minutes);
#else
                _memoryCache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new Microsoft.Extensions.Caching.Memory.MemoryCacheOptions()
                {
                });
#endif
            }
        }
        public override bool HasKey(string key)
        {
#if !(NETCORE || CORE)
            return _memoryCache.Contains(key);
#else
            object obj;
            return _memoryCache.TryGetValue(key, out obj);
#endif
        }
        public override void Remove(string key)
        {
#if !(NETCORE || CORE)
            _memoryCache.Remove(key);
#else
            _memoryCache.Remove(key);
#endif
        }
        public override byte[] Get(string key)
        {
#if !(NETCORE || CORE)
            object obj =_memoryCache.Get(key);
            if (obj != null)
            {
                return (byte[])obj;
            }
            else
            {
                return null;
            }
#else
            object obj;
            bool success= _memoryCache.TryGetValue(key, out obj);
            if (success && obj != null)
            {
                return (byte[])obj;
            }
            else
            {
                return null;
            }
#endif
        }
        public override void Set(string key, byte[] value, int minutes)
        {
#if !(NETCORE || CORE)
            _memoryCache.Set(key, value, _cacheItemPolicy);
#else
            Microsoft.Extensions.Caching.Memory.ICacheEntry cacheEntry= _memoryCache.CreateEntry(key);
            cacheEntry.SlidingExpiration = TimeSpan.FromMinutes(this.minutes);
            cacheEntry.Value = value;
#endif
        }
    }
}
