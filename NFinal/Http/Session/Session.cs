//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : Session.cs
//        Description :Session类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;

namespace NFinal.Http
{
    /// <summary>
    /// Session类
    /// </summary>
    public class Session : ISession
    {
        /// <summary>
        /// Session ID
        /// </summary>
        private string sessionId = null;
        private static string userKey;
        /// <summary>
        /// 缓存类
        /// </summary>
        public Cache.ICache<string> cache = null;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="sessionId"></param>
        /// <param name="userKey">存储用户的key</param>
        /// <param name="cache"></param>
        public Session(string sessionId, Cache.ICache<string> cache)
        {
            this.sessionId = sessionId;
            this.cache = cache;
        }
        public static void Configaure(string userKey)
        {
            Session.userKey = userKey;
        }
        /// <summary>
        /// 获取用户
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <returns></returns>
        public TUser GetUser<TUser>() where TUser : class
        {
            return Get<TUser>(userKey);
        }
        /// <summary>
        /// 设置用户
        /// </summary>
        /// <typeparam name="TUser"></typeparam>
        /// <param name="user"></param>
        public void SetUser<TUser>(TUser user) where TUser : class
        {
            Set<TUser>(userKey, user);
        }
        /// <summary>
        /// 判断Session是否存在
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public bool HasKey(string key)
        {
            return cache.HasKey(string.Concat(Constant.SessionChannel , sessionId , key));
        }
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public byte[] Get(string key)
        {
            return cache.Get(string.Concat(Constant.SessionChannel, sessionId, key));
        }
        /// <summary>
        /// 获取Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        public T Get<T>(string key)
        {
            return cache.Get<T>(string.Concat(Constant.SessionChannel, sessionId, key));
        }
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, object value)
        {
            cache.Set(string.Concat(Constant.SessionChannel, sessionId, key), value);
        }
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        public void Set<T>(string key, T t)
        {
            cache.Set<T>(string.Concat(Constant.SessionChannel, sessionId, key), t);
        }
        /// <summary>
        /// 设置Session
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void Set(string key, string value)
        {
            cache.SetString(string.Concat(Constant.SessionChannel, sessionId, key), value);
        }


        /// <summary>
        /// Session属性
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public object this[string key]
        {
            get
            {
                if (HasKey(key))
                {
                    return Get<object>(key);
                }
                return null;
            }
            set
            {
                Set<object>(key, value);
            }
        }
    }
}
