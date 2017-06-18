using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.DependencyInjection
{
    /// <summary>
    /// 服务注册扩展类
    /// </summary>
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// 使用Redis作为Session
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="minutes">缓存时间</param>
        public static void SetRedisSession(this IServiceCollection serviceCollection,int minutes=30,string configration="localhost",string userSessionKey="User")
        {
            serviceCollection.SetService<NFinal.Cache.ICache<string>, NFinal.Serialize.ISerializable>(typeof(NFinal.Cache.RedisCache)).Configaure(configration,minutes);
            serviceCollection.SetService<NFinal.Http.ISession,string,NFinal.Cache.ICache<string>>
                (typeof(NFinal.Http.Session)).Configaure(userSessionKey);
        }
        /// <summary>
        /// 使用内存作为Session
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="minutes">缓存时间</param>
        public static void SetSimpleSession(this IServiceCollection serviceCollection, int minutes=30,string userSessionKey="User")
        {
            serviceCollection.SetService<NFinal.Cache.ICache<string>, NFinal.Serialize.ISerializable>(typeof(NFinal.Cache.SimpleCache)).Configaure(minutes);
            serviceCollection.SetService<NFinal.Http.ISession, string, NFinal.Cache.ICache<string>>
                (typeof(NFinal.Http.Session)).Configaure(userSessionKey);
        }
        /// <summary>
        /// 设置二进制序列化
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void SetBinarySerialize(this IServiceCollection serviceCollection)
        {
#if (NET40 || NET451 || NET461)
            serviceCollection.SetService<NFinal.Serialize.ISerializable>(typeof(NFinal.Serialize.BinarySerialize));
#endif
        }
        /// <summary>
        /// 设置ProtobufSerialize
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void SetProtobufSerialize(this IServiceCollection serviceCollection)
        {
            serviceCollection.SetService<NFinal.Serialize.ISerializable>(typeof(NFinal.Serialize.ProtobufSerialize));
        }
        /// <summary>
        /// 设置NFinal框架的默认服务
        /// </summary>
        /// <param name="serviceCollection"></param>
        public static void SetDefault(this IServiceCollection serviceCollection)
        {
            serviceCollection.SetService<NFinal.Cache.ICache<string>, NFinal.Serialize.ISerializable>(typeof(NFinal.Cache.SimpleCache),false).Configaure(30);
            serviceCollection.SetService<NFinal.Http.ISession, string, NFinal.Cache.ICache<string>>
                (typeof(NFinal.Http.Session)).Configaure("User",false);
            serviceCollection.SetService<NFinal.Serialize.ISerializable>(typeof(NFinal.Serialize.ProtobufSerialize),false);
        }
    }
}
