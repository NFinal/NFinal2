using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;
using NFinal;

namespace NFinal
{
    public class NameValueCollection : IEnumerable<KeyValuePair<string, StringContainer>>
    {
        public NameValueCollection()
        {
            collection = new NFinal.Collections.FastDictionary<string, StringContainer>(StringComparer.Ordinal);
        }
        private NFinal.Collections.FastDictionary<string, StringContainer> collection = null;

        public StringContainer this[string key]
        {
            get {
                if (collection.ContainsKey(key))
                {
                    return collection[key];
                }
                else
                {
                    return StringContainer.Empty;
                }
            }
            set {
                if (value.value==null)
                {
                    if (collection.ContainsKey(key))
                    {
                        collection.Remove(key);
                    }
                }
                else
                {
                    if (collection.ContainsKey(key))
                    {
                        collection[key] = value;
                    }
                    else
                    {
                        collection.Add(key, value);
                    }
                }
            }
        }
        public void Add(string key, string value)
        {
            this[key]=value;
        }

        public IEnumerator<KeyValuePair<string, StringContainer>> GetEnumerator()
        {
            return collection.GetEnumerator();
        }

        public override string ToString()
        {
            StringWriter sw = new StringWriter();
            bool firstChild = true;
            foreach (var item in collection)
            {
                if (firstChild)
                {
                    firstChild = false;
                }
                else
                {
                    sw.Write("&");
                }
                sw.Write(item.Key);
                sw.Write("=");
                sw.Write(item.Value.value.UrlEncode());
            }
            return sw.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }
    }
}
