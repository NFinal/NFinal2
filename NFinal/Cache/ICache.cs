//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ICache.cs
//        Description :缓存接口类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
namespace NFinal.Cache
{
    /// <summary>
    /// 缓存接口
    /// </summary>
    public interface ICache<TKey>
    {
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="t">object</param>
        void Set<T>(TKey key, T t);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <param name="t"></param>
        /// <param name="minutes"></param>
        void Set<T>(TKey key, T t, int minutes);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        /// <param name="minutes"></param>
        void Set(TKey key, byte[] value, int minutes);
        /// <summary>
        /// 设置缓存
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void Set(TKey key, byte[] value);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        byte[] Get(TKey key);
        /// <summary>
        /// 获取缓存
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="key"></param>
        /// <returns></returns>
        T Get<T>(TKey key);
        /// <summary>
        /// 获取字符串
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        string GetString(TKey key);
        /// <summary>
        /// 设置字符串
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        void SetString(TKey key, string value);
        /// <summary>
        /// 是否有该缓存
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        bool HasKey(TKey key);
        /// <summary>
        /// 删除缓存
        /// </summary>
        /// <param name="key"></param>
        void Remove(TKey key);
    }
}
