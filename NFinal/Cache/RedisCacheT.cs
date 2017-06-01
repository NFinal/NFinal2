//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : RedisCacheT.cs
//        Description :基于Redis的泛型缓存类
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
using StackExchange.Redis;

namespace NFinal.Cache
{
    /// <summary>
    /// 基于Redis的泛型缓存类
    /// </summary>
    public class RedisCacheT<TValue> : ICacheT<string, TValue>
    {
        private int minutes;
        private string configuration = null;
        /// <summary>
        /// redis服务器缓存
        /// </summary>
        public static Dictionary<string, IDatabase> databasePool = new Dictionary<string, IDatabase>(StringComparer.Ordinal);
        /// <summary>
        /// 当前redis服务器
        /// </summary>
        public IDatabase database = null;
        /// <summary>
        /// 序列化对象
        /// </summary>
        public NFinal.ISerializable serialize;
        /// <summary>
        /// redis缓存
        /// </summary>
        /// <param name="configuration">redis缓存配置</param>
        /// <param name="minutes">缓存时间</param>
        public RedisCacheT(string configuration, int minutes)
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
        /// <summary>
        /// 移除缓存
        /// </summary>
        /// <param name="key">key</param>
        public void Remove(string key)
        {
            this.database.KeyDelete(key);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void Set(string key, TValue value)
        {
            byte[] buffer = null;
            if (value != null)
            {
                buffer = serialize.Serialize(value);
            }
            this.database.StringSet(key, buffer);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <returns></returns>
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
