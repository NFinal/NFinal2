using System;
using System.Collections.Generic;

namespace NFinal.Http
{
    /// <summary>
    /// Cookie设置
    /// </summary>
    public class Cookie : NFinal.Http.ICookie
    {
        private IDictionary<string, string> requestCookies = null;
        private IDictionary<string, string> responseCookies  = null;
        public IDictionary<string, string> ResponseCookies { get { return responseCookies; } }
        private string session_id = null;
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="cookies"></param>
        public Cookie(IDictionary<string, string> requestCookies)
        {
            this.requestCookies = requestCookies;
            responseCookies = new Dictionary<string, string>(StringComparer.Ordinal);
        }
        /// <summary>
        /// sessionId
        /// </summary>
        public string SessionId
        {
            get
            {
                if (session_id == null)
                {
                    if (requestCookies != null && requestCookies.ContainsKey(Constant.sessionKey))
                    {
                        session_id = requestCookies[Constant.sessionKey];
                    }
                    else
                    {
                        session_id = Guid.NewGuid().ToString("N");
                        SetCookie(Constant.sessionKey, session_id);
                    }
                }
                return session_id;
            }
        }
        
        /// <summary>
        /// Add a new cookie and value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCookie(string key, string value)
        {
            string setCookieString = Uri.EscapeDataString(key) + "=" + Uri.EscapeDataString(value) + "; path=/";
            responseCookies.AddValue(key, setCookieString);
        }
        /// <summary>
        /// Sets an expired cookie
        /// </summary>
        /// <param name="key"></param>
        public void SetExpiredCookie(string key)
        {
            string deleteCookieString = Uri.EscapeDataString(key) + "=; expires=Thu, 01-Jan-1970 00:00:00 GMT";
            responseCookies.AddValue(key, deleteCookieString);
        }
        /// <summary>
        /// Cookie访问器
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public string this[string key]
        {
            get
            {
                if (requestCookies != null)
                {
                    if (requestCookies.ContainsKey(key))
                    {
                        return requestCookies[key];
                    }
                }
                return null;
            }
            set
            {
                if (value != null)
                {
                    SetCookie(key, value);
                }
                else
                {
                    SetExpiredCookie(key);
                }
            }
        }
    }
}
