namespace NFinal.Http
{
    /// <summary>
    /// Session
    /// </summary>
    public class Session : ISession
    {
        /// <summary>
        /// Session ID
        /// </summary>
        protected string sessionId = null;
        /// <summary>
        /// 缓存类
        /// </summary>
        private Cache.ICache<string> cache = null;
        public Session(string sessionId, Cache.ICache<string> cache)
        {
            this.sessionId = sessionId;
            this.cache = cache;
        }
        public bool HasKey(string key)
        {
            return cache.HasKey(string.Concat(Constant.SessionChannel , sessionId , key));
        }
        public byte[] Get(string key)
        {
            return cache.Get(string.Concat(Constant.SessionChannel, sessionId, key));
        }
        public T Get<T>(string key)
        {
            return cache.Get<T>(string.Concat(Constant.SessionChannel, sessionId, key));
        }
        public void Set(string key, object value)
        {
            cache.Set(string.Concat(Constant.SessionChannel, sessionId, key), value);
        }
        public void Set<T>(string key, T t)
        {
            cache.Set<T>(string.Concat(Constant.SessionChannel, sessionId, key), t);
        }
        public void Set(string key, string value)
        {
            cache.SetString(string.Concat(Constant.SessionChannel, sessionId, key), value);
        }
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
