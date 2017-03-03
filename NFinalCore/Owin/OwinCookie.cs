using System;
using System.Collections.Generic;

namespace NFinal
{
    /// <summary>
    /// Cookie设置
    /// </summary>
    public class OwinCookie : NFinal.ICookie<IDictionary<string,string>>
    {
        private IDictionary<string, string> requestCookies = null;
        public IDictionary<string, string> responseCookies  = null;
        private string session_id = null;
        private bool isStatic = false;
        /// <summary>
        /// action
        /// </summary>
        public NFinal.IAction<IDictionary<string,string>,Owin.Request> action = null;
        public OwinCookie()
        {
            this.isStatic = true;
            this.requestCookies = null;
            this.responseCookies = null;
        }
        /// <summary>
        /// 初始化函数
        /// </summary>
        /// <param name="cookies"></param>
        public OwinCookie(IDictionary<string, string> requestCookies)
        {
            this.isStatic = true;
            this.requestCookies = requestCookies;
            responseCookies = new Dictionary<string, string>(StringComparer.Ordinal);
        }
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="action"></param>
        public OwinCookie(NFinal.IAction<IDictionary<string,string>,Owin.Request> action)
        {
            this.isStatic = false;
            this.action = action;
            string CookieHeader = this.action.GetRequestHeader("Cookie");
            if (!string.IsNullOrEmpty(CookieHeader))
            {
                requestCookies = new Dictionary<string, string>(StringComparer.Ordinal);
                string[] tempArray = CookieHeader.Split('&', '=');
                if ((tempArray.Length & 1) == 0)
                {
                    int len = tempArray.Length >> 1;
                    int i = 0;
                    while (i < len)
                    {
                        requestCookies.Add(tempArray[i << 1], Uri.UnescapeDataString(tempArray[(i << 1) + 1]));
                        i++;
                    }
                }
            }
            this.responseCookies = new Dictionary<string, string>(StringComparer.Ordinal);
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
            if (!isStatic)
            {
                string setCookieString = Uri.EscapeDataString(key) + "=" + Uri.EscapeDataString(value) + "; path=/";
                responseCookies.AddValue(key, setCookieString);
            }
        }
        /// <summary>
        /// Sets an expired cookie
        /// </summary>
        /// <param name="key"></param>
        public void SetExpiredCookie(string key)
        {
            if (!isStatic)
            {
                string deleteCookieString = Uri.EscapeDataString(key) + "=; expires=Thu, 01-Jan-1970 00:00:00 GMT";
                responseCookies.AddValue(key, deleteCookieString);
            }
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
