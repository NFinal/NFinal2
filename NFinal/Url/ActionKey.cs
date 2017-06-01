//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : ActionKey.cs
//        Description :计算搜索控制器行为所用的key
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace NFinal.Url
{
    /// <summary>
    /// 计算搜索控制器行为所用的key
    /// </summary>
    public class ActionKey
    {
        /// <summary>
        /// 计算actionUrl
        /// </summary>
        /// <param name="method">Http请求方法</param>
        /// <param name="requestedPath">Http请求路径</param>
        /// <param name="shortActionKeyLength">actionKey的长度</param>
        /// <returns></returns>
        public unsafe static string GetActionKey(string method, string requestedPath,out int shortActionKeyLength)
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
            shortActionKeyLength = 0;

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
            int actionKeyLength = shortActionKeyLength + method.Length + 1;
            char[] actionKey = new char[actionKeyLength];
            int actionKeyPos = 0;
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
    }
}
