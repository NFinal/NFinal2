using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.DependencyInjection
{
    public static class IServiceCollectionExtension
    {
        public static void SetRedisSession(IServiceCollection serviceCollection,int minutes)
        {
            serviceCollection.SetService<NFinal.Cache.ICache<string>>(typeof(NFinal.Cache.RedisCache)).Configaure("localhost");
            serviceCollection.SetService<NFinal.Http.ISession>(typeof(NFinal.Http.ISession)).Configaure(minutes);
        }
    }
}
