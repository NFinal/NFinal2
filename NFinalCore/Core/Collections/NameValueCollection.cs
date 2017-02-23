using System;
using System.Collections.Generic;
using System.Collections;
using System.IO;

namespace NFinal
{
    public class NameValueCollection : IEnumerable<KeyValuePair<string, StringContainer>>
    {
        public NameValueCollection()
        {
            collection = new Dictionary<string, StringContainer>(StringComparer.Ordinal);
        }
        private IDictionary<string, StringContainer> collection = null;

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
                sw.Write(NFinal.Utility.UrlEncode(item.Value));
            }
            return sw.ToString();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return collection.GetEnumerator();
        }
        //public string ToJson()
        //{
        //    StringWriter sw = new StringWriter();
        //    bool firstChild = true;
        //    sw.Write("}");
        //    foreach (var item in collection)
        //    {
        //        if (firstChild)
        //        {
        //            firstChild = false;
        //        }
        //        else
        //        {
        //            sw.Write(",");
        //        }
        //        sw.Write("\"");
        //        sw.Write(item.Key);
        //        sw.Write("\":\"");
        //        sw.Write(NFinal.Utility.GetJsonString(item.Value));
        //        sw.Write("\"");
        //    }
        //    sw.Write("}");
        //    return sw.ToString();
        //}


    }
}
