using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using StackExchange.Redis;

namespace NFinal.Cache
{
    /// <summary>
    /// Redis缓存类
    /// </summary>
    public class RedisCache<TValue> : ICacheT<string, TValue>
    {
        private int minutes;
        private string configuration = null;
        public static Dictionary<string, IDatabase> databasePool = new Dictionary<string, IDatabase>(StringComparer.Ordinal);
        public IDatabase database = null;
        public NFinal.ISerializable serialize;
        public RedisCache(string configuration, int minutes)
        {
            this.serialize = new NFinal.ProtobufSerialize();
            this.configuration = configuration;
            this.minutes = minutes;
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
        public void Remove(string key)
        {
            this.database.KeyDelete(key);
        }

        public void Set(string key, TValue value)
        {
            byte[] buffer = null;
            if (value != null)
            {
                buffer = serialize.Serialize(value);
            }
            this.database.StringSet(key, buffer);
        }

        public bool TryGetValue(string key, out TValue value)
        {
            byte[] buffer= this.database.StringGet(key);
            if (buffer != null)
            {
                value = this.serialize.Deserialize<TValue>(buffer);
                return true;
            }
            else
            {
                value = default(TValue);
                return false;
            }
        }
    }
}
