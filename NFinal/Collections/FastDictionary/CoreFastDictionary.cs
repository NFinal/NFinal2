#if NETCORE
using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Threading.Tasks;

namespace NFinal.Collections
{
    public class FastDictionary<TValue>
    {
        private IDictionary<string, TValue> _dictionary = null;
        public FastDictionary(IEnumerable<KeyValuePair<string, TValue>> kvList, int count)
        {
            this._dictionary = new Dictionary<string, TValue>(count);
            foreach (var keyValue in kvList)
            {
                this._dictionary.Add(keyValue.Key, keyValue.Value);
            }
        }
        public bool TryGetValue(string key, out TValue value)
        {
            return this._dictionary.TryGetValue(key, out value);
        }
        public TValue this[string key]
        {
            get
            {
                if (this._dictionary.ContainsKey(key))
                {
                    return this._dictionary[key];
                }
                else
                {
                    return default(TValue);
                }
            }
            set
            {
                if (this._dictionary.ContainsKey(key))
                {
                    this._dictionary[key] = value;
                }
            }
        }
    }
}
#endif