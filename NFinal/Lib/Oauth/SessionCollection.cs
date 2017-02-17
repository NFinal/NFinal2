using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NFinal.Lib.Oauth
{
    public class SessionCollection
    {
        private static Dictionary<string, string> session = null;
        private string domain = string.Empty;
        public SessionCollection(string sessionId)
        {
            if (string.IsNullOrEmpty(sessionId))
            {
                domain = sessionId + ":";
            }
            if (session == null)
            {
                session = new Dictionary<string, string>();
            }
        }
        public string this[string key]
        {
            get {
                if (session.ContainsKey(domain + key))
                {
                    return session[domain + key];
                }
                else
                {
                    return null;
                }
            }
            set {
                if (session.ContainsKey(domain + key))
                {
                    session[domain + key] = value;
                }
                else
                {
                    session.Add(domain + key, value);
                }
            }
        }
    }
}