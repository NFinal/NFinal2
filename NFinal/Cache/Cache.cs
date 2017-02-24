using System;
using System.Collections.Generic;
using System.IO;


namespace NFinal.Cache
{
    
    /// <summary>
    /// 缓存
    /// </summary>
    public abstract class Cache : ICache
    {
        /// <summary>
        /// 缓存时间
        /// </summary>
        protected int minutes = 20;
        /// <summary>
        /// 序列化
        /// </summary>
        ISerializable serialize = null;
        public CacheType cacheType;
        /// <summary>
        /// 初始化
        /// </summary>
        public Cache()
        {
            this.cacheType = CacheType.SlidingExpiration;
            this.minutes = 20;
            serialize = new NFinal.ProtobufSerialize();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="">CacheType</param>
        public Cache(CacheType cacheType)
        {
            this.cacheType = cacheType;
            this.minutes = 20;
            this.serialize = new NFinal.ProtobufSerialize();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="minutes"></param>
        public Cache(CacheType cacheType, int minutes)
        {
            this.cacheType = cacheType;
            this.minutes = minutes;
            this.serialize = new NFinal.ProtobufSerialize();
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="minutes"></param>
        public Cache(int minutes)
        {
            this.cacheType = CacheType.SlidingExpiration;
            this.minutes = minutes;
            this.serialize = new NFinal.ProtobufSerialize();
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract byte[] Get(string key);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minutes"></param>
        public abstract void Set(string key, byte[] value, int minutes);
        /// <summary>
        /// 是否有该缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract bool HasKey(string key);
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public abstract void Remove(string key);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, byte[] value)
        {
            this.Set(key, value, this.minutes);
        }
        public void Set<T>(string key, T t, int minutes)
        {
            Set(key, serialize.Serialize<T>(t), minutes);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return serialize.Deserialize<T>(this.Get(key));
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(string key)
        {
            byte[] buffer= Get(key);
            if (buffer != null)
            {
                return Constant.encoding.GetString(buffer);
            }
            return null;
        }
        /// <summary>
        /// 设置字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetString(string key, string value)
        {
            if (value != null)
            {
                Set(key, Constant.encoding.GetBytes(value));
            }
            else
            {
                Set(key, null);
            }
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set<T>(string key, T value)
        {
            this.Set(key, serialize.Serialize<T>(value),this.minutes);
        }
    }
}
