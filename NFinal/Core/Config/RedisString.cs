using System;

namespace NFinal.Config
{
    public struct RedisString
    {
        public string redisConfigration;
        public RedisString(JsonObject data)
        {
            this.redisConfigration = data["redisConfigration"].ToString();
        }
    }
}
