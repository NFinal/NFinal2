using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Collections.FastSearch
{
    public class KVNode<TValue>
    {
        public KV<TValue> kvLeft;
        public long compareNumber;
        public KV<TValue> kvRight;
    }
}
