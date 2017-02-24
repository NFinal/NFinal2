using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace NFinal.Cache
{
    
    /// <summary>
    /// Redis缓存类
    /// </summary>
    public class RedisCache : Cache
    {
        private string configuration = null;
        public static Dictionary<string, IDatabase> databasePool = new Dictionary<string, IDatabase>(StringComparer.Ordinal);
        public IDatabase database = null;
        public RedisCache(string configuration, int minutes):base(CacheType.SlidingExpiration,minutes)
        {
            this.configuration = configuration;
            if (databasePool.ContainsKey(configuration))
            {
                this.database = databasePool[configuration];
            }
            else
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration);
                this.database = redis.GetDatabase();
                databasePool.Add(configuration, this.database);
            }
        }
        public RedisCache(string configuration, CacheType cacheType,int minutes) : base(cacheType,minutes)
        {
            this.configuration = configuration;
            if (databasePool.ContainsKey(configuration))
            {
                this.database = databasePool[configuration];
            }
            else
            {
                ConnectionMultiplexer redis = ConnectionMultiplexer.Connect(configuration);
                this.database = redis.GetDatabase();
                databasePool.Add(configuration, this.database);
            }
        }
        public override bool HasKey(string key)
        {
            return database.KeyExists(key);
        }
        public override void Remove(string key)
        {
            database.KeyDelete(key);
        }
        public override byte[] Get(string key)
        {
            if (cacheType == CacheType.SlidingExpiration)
            {
                database.KeyExpire(key, TimeSpan.FromMinutes(this.minutes));
            }
            return database.StringGet(key);
        }
        public override void Set(string key, byte[] value, int minutes)
        {
            if (cacheType == CacheType.NoExpiration)
            {
                database.StringSet(key, value);
            }
            else
            {
                database.StringSet(key, value, TimeSpan.FromMinutes(minutes));
            }
        }
    }
}
