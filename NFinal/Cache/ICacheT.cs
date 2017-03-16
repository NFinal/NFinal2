using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NFinal.Cache
{
    public interface ICacheT<TKey,TValue>
    {
        void Set(TKey key, TValue value);
        bool TryGetValue(TKey key,out TValue value);
        void Remove(TKey key);
    }
}
