namespace NFinal.Cache
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">object</param>
        void Set<T>(string key, T t);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="minutes"></param>
        void Set<T>(string key, T t, int minutes);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minutes"></param>
        void Set(string key, byte[] value, int minutes);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(string key, byte[] value);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Get(string key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(string key);
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(string key);
        /// <summary>
        /// 设置字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetString(string key, string value);
        /// <summary>
        /// 是否有该缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool HasKey(string key);
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(string key);
    }
}
