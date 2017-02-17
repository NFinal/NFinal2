using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Config
{
    /// <summary>
    /// redis缓存配置实体类
    /// </summary>
    public class RedisData
    {
        public bool redisConfigAutoStart = false;
        public int redisConfigMaxReadPoolSize = 60;
        public int redisConfigMaxWritePoolSize = 60;
        public string[] redisReadWriteHosts = new string[] { "127.0.0.1:6379" };
        public string[] redisReadOnlyHosts = new string[] { "127.0.0.1:6379" };
    }
}
