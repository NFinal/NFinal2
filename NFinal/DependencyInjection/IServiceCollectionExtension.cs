using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        /// <summary>
        /// 使用Redis作为Session
        /// </summary>
        /// <param name="serviceCollection"></param>
        /// <param name="minutes"></param>
        public static void SetRedisSession(IServiceCollection serviceCollection,int minutes)
        {
            serviceCollection.SetService<NFinal.Cache.ICache<string>>(typeof(NFinal.Cache.RedisCache)).Configaure("localhost");
            serviceCollection.SetService<NFinal.Http.ISession>(typeof(NFinal.Http.ISession)).Configaure(minutes);
        }
        public static void SetSimpleSession(IServiceCollection serviceCollection, int minutes)
        {
            serviceCollection.SetService<NFinal.Cache.ICache<string>>(typeof(NFinal.Cache.SimpleCache)).Configaure();
            serviceCollection.SetService<NFinal.Http.ISession>(typeof(NFinal.Http.ISession)).Configaure(minutes);
        }
    }
}
