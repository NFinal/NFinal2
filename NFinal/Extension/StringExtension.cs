//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : StringExtension.cs
//        Description :字符串扩展类
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

namespace NFinal
{
    /// <summary>
    /// 字符串扩展类
    /// </summary>
    public static class StringExtension
    {
        /// <summary>
        /// UrlEncode
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlEncode(this string url)
        {
            return Uri.EscapeDataString(url);
        }
        /// <summary>
        /// UrlDecode
        /// </summary>
        /// <param name="url"></param>
        /// <returns></returns>
        public static string UrlDecode(this string url)
        {
            return Uri.UnescapeDataString(url);
        }
        /// <summary>
        /// HtmlEncode
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlEncode(this string html)
        {
            return System.Net.WebUtility.HtmlEncode(html);
        }
        /// <summary>
        /// HtmlDecode
        /// </summary>
        /// <param name="html"></param>
        /// <returns></returns>
        public static string HtmlDecode(this string html)
        {
            return System.Net.WebUtility.HtmlDecode(html);
        }
        /// <summary>
        /// 字符串转Json字节流
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        public static byte[] JsonEncodeBytes(this string json)
        {
            if (json == null)
            {
                return new byte[] { (byte)'n', (byte)'u', (byte)'l', (byte)'l' };            }
            else if (json == string.Empty)
            {
                return new byte[] { (byte)'"', (byte)'"' };
            }
            byte[] buffer = NFinal.Constant.encoding.GetBytes(json);
            byte[] result = GetJsonString(buffer, 0, buffer.Length);
            return result;
        }
       /// <summary>
       /// 字符串转Json字符串
       /// </summary>
       /// <param name="json"></param>
       /// <returns></returns>
        public static string JsonEncodeString(this string json)
        {
            return NFinal.Constant.encoding.GetString(JsonEncodeBytes(json));
        }
        /// <summary>
        /// 获取hashCode.此函数是为了各平台的hashCode能保持统一。
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetHashCodeEx(this string obj)
        {
            unsafe
            {
                fixed (char* src = obj)
                {
#if WIN32
                    int hash1 = (5381<<16) + 5381;
#else
                    int hash1 = 5381;
#endif
                    int hash2 = hash1;

#if WIN32
                    // 32 bit machines.
                    int* pint = (int *)src;
                    int len = this.Length;
                    while (len > 2)
                    {
                        hash1 = ((hash1 << 5) + hash1 + (hash1 >> 27)) ^ pint[0];
                        hash2 = ((hash2 << 5) + hash2 + (hash2 >> 27)) ^ pint[1];
                        pint += 2;
                        len  -= 4;
                    }

                    if (len > 0)
                    {
                        hash1 = ((hash1 << 5) + hash1 + (hash1 >> 27)) ^ pint[0];
                    }
#else
                    int c;
                    char* s = src;
                    while ((c = s[0]) != 0)
                    {
                        hash1 = ((hash1 << 5) + hash1) ^ c;
                        c = s[1];
                        if (c == 0)
                            break;
                        hash2 = ((hash2 << 5) + hash2) ^ c;
                        s += 2;
                    }
#endif
                    return hash1 + (hash2 * 1566083941);
                }
            }
        }
        /// <summary>
        /// 判断该字节流是否为合法的字符串
        /// </summary>
        /// <param name="bytes"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        /// <returns></returns>
        private static bool ValidateParameters(byte[] bytes, int offset, int count)
        {
            if (bytes == null && count == 0)
                return false;
            if (bytes == null)
            {
                throw new ArgumentNullException("bytes");
            }
            if (offset < 0 || offset > bytes.Length)
            {
                throw new ArgumentOutOfRangeException("offset");
            }
            if (count < 0 || offset + count > bytes.Length)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            return true;
        }
        /// <summary>
        /// 把字符串转换为json中的字符串格式
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <param name="offset">开始位置</param>
        /// <param name="count">字节数</param>
        /// <returns></returns>
        private static byte[] GetJsonString(byte[] bytes, int offset, int count)
        {
            if (!ValidateParameters(bytes, offset, count))
            {
                return null;
            }
            int cWrapped = 0;
            // count them first
            for (int i = 0; i < count; i++)
            {
                char ch = (char)bytes[offset + i];
                switch (ch)
                {
                    case '\"': cWrapped++; break;
                    case '\\': cWrapped++; break;
                    case '/': cWrapped++; break;
                    case '\b': cWrapped++; break;
                    case '\f': cWrapped++; break;
                    case '\n': cWrapped++; break;
                    case '\r': cWrapped++; break;
                    case '\t': cWrapped++; break;
                }
            }
            if (cWrapped == 0)
            {
                return bytes;
            }
            byte[] expandedBytes = new byte[count + cWrapped + 2];
            int pos = 0;
            expandedBytes[pos++] = (byte)'\"';
            for (int i = 0; i < count; i++)
            {
                byte b = bytes[offset + i];
                char ch = (char)b;

                switch (ch)
                {
                    case '\"':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'\"'; break;
                    case '\\':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'\\'; break;
                    case '/':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'/'; break;
                    case '\b':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'b'; break;
                    case '\f':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'f'; break;
                    case '\n':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'n'; break;
                    case '\r':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'r'; break;
                    case '\t':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'t'; break;
                    default:
                        expandedBytes[pos++] = (byte)ch; break;
                }
            }
            expandedBytes[pos++] = (byte)'\"';
            return expandedBytes;
        }
        /// <summary>
        /// 把字符串转换为json中的字符串格式
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>json中的字符串格式</returns>
        public static string GetJsonString(this string text)
        {
            if (text == null)
            {
                return "null";
            }
            else if (text == string.Empty)
            {
                return "\"\"";
            }
            byte[] buffer = NFinal.Constant.encoding.GetBytes(text);
            byte[] result = GetJsonString(buffer, 0, buffer.Length);
            return NFinal.Constant.encoding.GetString(result);
        }
        /// <summary>
        /// 把字符串转换为代码中的字符串格式
        /// </summary>
        /// <param name="bytes">字节流</param>
        /// <param name="offset">开始位置</param>
        /// <param name="count">字节数</param>
        /// <returns>代码中的字符串</returns>
        public static byte[] GetCSharpString(byte[] bytes, int offset, int count)
        {
            if (!ValidateParameters(bytes, offset, count))
            {
                return null;
            }
            int cWrapped = 0;
            // count them first
            for (int i = 0; i < count; i++)
            {
                char ch = (char)bytes[offset + i];
                switch (ch)
                {
                    case '\'': cWrapped++; break;
                    case '\"': cWrapped++; break;
                    case '\\': cWrapped++; break;
                    case '\0': cWrapped++; break;
                    case '\a': cWrapped++; break;
                    case '\b': cWrapped++; break;
                    case '\f': cWrapped++; break;
                    case '\n': cWrapped++; break;
                    case '\r': cWrapped++; break;
                    case '\t': cWrapped++; break;
                    case '\v': cWrapped++; break;
                }
            }
            if (cWrapped == 0)
            {
                return bytes;
            }
            byte[] expandedBytes = new byte[count + cWrapped + 2];
            int pos = 0;
            expandedBytes[pos++] = (byte)'\"';
            for (int i = 0; i < count; i++)
            {
                byte b = bytes[offset + i];
                char ch = (char)b;

                switch (ch)
                {
                    case '\'':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'\''; break;
                    case '\"':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'\"'; break;
                    case '\\':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'\\'; break;
                    case '\0':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'0'; break;
                    case '\a':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'a'; break;
                    case '\b':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'b'; break;
                    case '\f':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'f'; break;
                    case '\n':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'n'; break;
                    case '\r':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'r'; break;
                    case '\t':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'t'; break;
                    case '\v':
                        expandedBytes[pos++] = (byte)'\\';
                        expandedBytes[pos++] = (byte)'v'; break;
                    default:
                        expandedBytes[pos++] = (byte)ch; break;
                }
            }
            expandedBytes[pos++] = (byte)'\"';
            return expandedBytes;
        }
        /// <summary>
        /// 把字符串转换为代码中的字符串格式
        /// </summary>
        /// <param name="text">字符串</param>
        /// <returns>代码中的字符串</returns>
        public static string GetCSharpString(this string text)
        {
            byte[] buffer = NFinal.Constant.encoding.GetBytes(text);
            byte[] result = GetCSharpString(buffer, 0, buffer.Length);
            return NFinal.Constant.encoding.GetString(result);
        }
    }
}
