using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
namespace NFinal.Cache
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class MemoryCacheA : Cache
    {
        private IMemoryCache _cache = null;
        private MemoryCacheEntryOptions _cacheEntryOptions;
        public MemoryCacheA(int minutes) : base(minutes)
        {
            _cache = new MemoryCache(new MemoryCacheOptions());
            _cacheEntryOptions = new MemoryCacheEntryOptions()
            // Keep in cache for this time, reset time if accessed.
            .SetSlidingExpiration(TimeSpan.FromSeconds(60*40));
        }

        public override bool HasKey(string key)
        {
            object obj = null;
            return _cache.TryGetValue(key, out obj);
        }
        public override void Remove(string key)
        {
            _cache.Remove(key);
        }
        public override byte[] Get(string key)
        {
            return _cache.Get<byte[]>(key);
        }
        public override void Set(string key, byte[] value, int minutes)
        {
            _cache.Set(key, minutes, _cacheEntryOptions);
        }
    }
}
