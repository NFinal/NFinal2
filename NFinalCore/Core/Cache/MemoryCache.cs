using System;
using System.Collections.Generic;
using Microsoft.Extensions.Caching.Memory;
namespace NFinal.Cache
{
    /// <summary>
    /// 内存缓存
    /// </summary>
    public class MemoryCache : Cache
    {
        private static Microsoft.Extensions.Caching.Memory.IMemoryCache _cache = null;
        private static Microsoft.Extensions.Caching.Memory.MemoryCacheEntryOptions _cacheEntryOptions;
        public MemoryCache(int minutes) : base(minutes)
        {
            if (_cache == null)
            {
                _cache = new Microsoft.Extensions.Caching.Memory.MemoryCache(new MemoryCacheOptions());
            }
            if (_cacheEntryOptions == null)
            {
                _cacheEntryOptions = new MemoryCacheEntryOptions()
                // Keep in cache for this time, reset time if accessed.
                .SetSlidingExpiration(TimeSpan.FromSeconds(60 * 40));
            }
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