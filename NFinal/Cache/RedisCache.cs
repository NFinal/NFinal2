//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : RedisCache.cs
//        Description :基于Redis的缓存类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using StackExchange.Redis;

namespace NFinal.Cache
{
    
    /// <summary>
    /// Redis缓存类
    /// </summary>
    public class RedisCache : Cache<string>
    {
        /// <summary>
        /// 当前redis服务器
        /// </summary>
       public IDatabase database = null;

       private static ConnectionMultiplexer redis = null;
        /// <summary>
        /// reids缓存初始化
        /// </summary>
        /// <param name="configuration">redis配置参数</param>
        /// <param name="minutes">滑动缓存时间</param>
        public RedisCache(ConnectionMultiplexer redis, int minutes):base(CacheType.SlidingExpiration,minutes)
        {
            this.database = redis.GetDatabase();
        }
        /// <summary>
        /// redis缓存初始化
        /// </summary>
        /// <param name="configuration">redis配置参数</param>
        /// <param name="cacheType">缓存类型</param>
        /// <param name="minutes">缓存时间</param>
        public RedisCache(string configuration, CacheType cacheType,int minutes) : base(cacheType,minutes)
        {
            this.database = redis.GetDatabase();
        }
        /// <summary>
        /// 是否拥有该缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public override bool HasKey(string key)
        {
            return database.KeyExists(key);
        }
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        public override void Remove(string key)
        {
            database.KeyDelete(key);
        }
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <returns></returns>
        public override byte[] Get(string key)
        {
            if (cacheType == CacheType.SlidingExpiration)
            {
                database.KeyExpire(key, TimeSpan.FromMinutes(this.minutes));
            }
            return database.StringGet(key);
        }
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        /// <param name="minutes">缓存时间</param>
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
        /// <summary>
        /// 释放连接
        /// </summary>
        //public void Dispose()
        //{
        //    this.redis.Dispose();
        //}
    }
}
