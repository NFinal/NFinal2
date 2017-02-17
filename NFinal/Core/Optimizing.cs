using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal
{
    public enum Optimizing
    {
        Cache_Server_NoCache=1,
        Cache_Server_FileDependency=2,
        Cache_Server_AbsoluteExpiration = 4,
        Cache_Server_SlidingExpiration = 8,
        Cache_Server_Cached=14,
        Cache_Browser_NoStore = 16,
        Cache_Browse_NotModify = 32,
        Cache_Browser_Expires = 64,
        Cache_Browser_NoExpires = 128,
        Cache_Brower_Cached=224,
        CacheFile = 34,
        CacheNormal=72,
        CompressZip=512,
        CompressDeflate=1024,
        Comresssed=1536
    }
}
