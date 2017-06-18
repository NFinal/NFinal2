//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Cache.cs
//        Description :缓存抽象类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.IO;

namespace NFinal.Cache
{
    /// <summary>
    /// 缓存
    /// </summary>
    public abstract class Cache<TKey> : ICache<TKey>
    {
        /// <summary>
        /// 缓存时间
        /// </summary>
        protected static int minutes = 20;
        /// <summary>
        /// 序列化
        /// </summary>
        NFinal.Serialize.ISerializable serialize = null;
        /// <summary>
        /// 缓存类型
        /// </summary>
        public CacheType cacheType;
        /// <summary>
        /// 初始化
        /// </summary>
        public Cache(NFinal.Serialize.ISerializable serialize)
        {
            this.cacheType = CacheType.SlidingExpiration;
            this.serialize = serialize;
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="cacheType">缓存类型</param>
        public Cache(NFinal.Serialize.ISerializable serialize,CacheType cacheType)
        {
            this.cacheType = cacheType;
            this.serialize = serialize;
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract byte[] Get(TKey key);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minutes"></param>
        public abstract void Set(TKey key, byte[] value, int minutes);
        /// <summary>
        /// 是否有该缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public abstract bool HasKey(TKey key);
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public abstract void Remove(TKey key);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">缓存内存块</param>
        public void Set(TKey key, byte[] value)
        {
            this.Set(key, value, Cache<TKey>.minutes);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T">缓存类型</typeparam>
        /// <param name="key">key</param>
        /// <param name="t">缓存内容</param>
        /// <param name="minutes">缓存时间</param>
        public void Set<T>(TKey key, T t, int minutes)
        {
            Set(key, serialize.Serialize<T>(t), minutes);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(TKey key)
        {
            return serialize.Deserialize<T>(this.Get(key));
        }
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string GetString(TKey key)
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
        public void SetString(TKey key, string value)
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
        public void Set<T>(TKey key, T value)
        {
            this.Set(key, serialize.Serialize<T>(value), Cache<TKey>.minutes);
        }
    }
}
