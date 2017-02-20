using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Middleware
{
    public class ActionKey
    {
        /// <summary>
        /// 计算actionUrl
        /// </summary>
        /// <param name="requestedPath"></param>
        /// <returns></returns>
        public unsafe static string GetActionKey(string subDomain, string method, string requestedPath, NFinal.Middleware.UrlRouteRule urlRouteRule)
        {
            if (urlRouteRule == UrlRouteRule.AreaControllerCustomActionUrl)
            {
                string actionKeyString;
                //找到最后一个.的位置
                int len = requestedPath.Length;
                //
                char ch;
                char ch1;
                int pos = len;
                int count = 0;
                bool hasExtension = false;
                int shortActionKeyLength = 0;

                while (pos > 3)
                {
                    pos--;
                    ch = requestedPath[pos];
                    count++;
                    //"/a.html"
                    if (count < 6 && ch == '.')
                    {
                        hasExtension = true;
                        //"/a23.h"
                        if (pos > 3)
                        {
                            pos--;
                            ch = requestedPath[pos];
                            pos--;
                            ch1 = requestedPath[pos];
                            if ((!(ch < '0' || ch > '9'))
                              && (!(ch1 < '0' || ch1 > '9')))
                            {
                                shortActionKeyLength = (ch1 - '0') * 10 + ch - '0';
                            }
                            else
                            {
                                shortActionKeyLength = requestedPath.Length;
                            }
                        }
                        break;
                    }
                }
                if (!hasExtension && len > 2)
                {
                    pos = len;
                    pos--;
                    ch = requestedPath[pos];
                    pos--;
                    ch1 = requestedPath[pos];
                    if ((!(ch < '0' || ch > '9'))
                      && (!(ch1 < '0' || ch1 > '9')))
                    {
                        shortActionKeyLength = (ch1 - '0') * 10 + ch - '0';
                    }
                    else
                    {
                        shortActionKeyLength = requestedPath.Length;
                    }
                }
                int actionKeyLength = shortActionKeyLength + subDomain.Length + method.Length + 2;
                char[] actionKey = new char[actionKeyLength];
                int actionKeyPos = 0;
                if (subDomain != null)
                {
                    len = subDomain.Length;
                    pos = 0;
                    while (pos < len)
                    {
                        actionKey[actionKeyPos] = subDomain[pos];
                        pos++;
                        actionKeyPos++;
                    }
                }
                actionKey[actionKeyPos] = ':';
                actionKeyPos++;
                if (method != null)
                {
                    len = method.Length;
                    pos = 0;
                    while (pos < len)
                    {
                        actionKey[actionKeyPos] = method[pos];
                        pos++;
                        actionKeyPos++;
                    }
                }
                actionKey[actionKeyPos] = ':';
                actionKeyPos++;
                if (requestedPath != null)
                {
                    len = shortActionKeyLength;
                    pos = 0;
                    while (pos < len)
                    {
                        actionKey[actionKeyPos] = requestedPath[pos];
                        pos++;
                        actionKeyPos++;
                    }
                }
                return actionKeyString = new string(actionKey);
            }
            else
            {
                return string.Concat(subDomain,":",method,":",requestedPath);
            }
        }
        public static string GetUrlDelegate(string urlString,string path, System.Reflection.MethodInfo methodInfo)
        {
            return string.Empty;
        }
    }
}
