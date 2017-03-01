using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace System
{
    public static class UniqueKeyDictionayExtension
    {
        public static void AddValue<TValue>(this IDictionary<string, TValue> dic,string key, TValue value)
        {
            if (dic.ContainsKey(key))
            {
                dic[key] = value;
            }
            else
            {
                dic.Add(key, value);
            }
        }
        public static string ToUrlString<TValue>(this IDictionary<string, TValue> dic)
        {
            StringBuilder sb = new StringBuilder();
            bool isFirst = true;
            sb.Append('?');
            foreach (var obj in dic)
            {
                if (isFirst)
                {
                    isFirst = false;
                }
                else
                {
                    sb.Append('&');
                }
                sb.Append(Uri.EscapeUriString(obj.Key));
                sb.Append('=');
                sb.Append(Uri.EscapeUriString(obj.Value.ToString()));
            }
            return sb.ToString();
        }
    }
}
