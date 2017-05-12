using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Collections.FastSearch
{
    public struct KV<TValue>
    {
        public KV(string key, TValue value,int index)
        {
            this.key = key;
            this.length = key.Length;
            this.value = value;
            this.index = index;
        }
        public string key;
        public int length;
        public int index;
        public TValue value;
    }
}
