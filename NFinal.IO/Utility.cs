using System;
using System.IO;
using System.Linq;

namespace NFinal
{
    /// <summary>
    /// 工具类，字符串处理
    /// </summary>
    public static class Utility
    {
        /// <summary>
        /// 站点根目录
        /// </summary>
        public static string rootPath = (AppDomain.CurrentDomain.GetData(".appPath") as string) ?? Environment.CurrentDirectory;
        /// <summary>
        /// 获得当前绝对路径，同时兼容windows和linux（系统自带的都不兼容）。
        /// </summary>
        /// <param name="strPath">指定的路径，支持/|./|../分割</param>
        /// <returns>绝对路径，不带/后缀</returns>
        public static string MapPath(string strPath)
        {
            if (strPath == null)
            {
                return rootPath;
            }
            else
            {
                System.Collections.Generic.List<string> prePath = rootPath.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                System.Collections.Generic.List<string> srcPath = strPath.Split(new char[] { '/', '\\' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                ComputePath(prePath, srcPath);
                if (prePath.Count > 0 && prePath[0].Contains(":"))//windows
                {
                    if (prePath.Count == 1)
                    {
                        return prePath[0] + "/";
                    }
                    else
                    {
                        return String.Join("/", prePath);
                    }
                }
                else//linux
                {
                    return "/" + String.Join("/", prePath);
                }
            }
        }
        private static void ComputePath(System.Collections.Generic.List<string> prePath, System.Collections.Generic.List<string> srcPath)
        {
            var precount = prePath.Count;
            foreach (string src in srcPath)
            {
                if (src == "..")
                {
                    if (precount > 1 || (precount == 1 && !prePath[0].Contains(":")))
                    {
                        prePath.RemoveAt(--precount);
                    }
                }
                else if (src != ".")
                {
                    prePath.Add(src);
                    precount++;
                }
            }
        }
        /// <summary>
        /// url编码
        /// </summary>
        /// <param name="url">原始url</param>
        /// <returns>编码后的url</returns>
        public static string UrlEncode(string url)
        {
            //byte[] buffer = encoding.GetBytes(url);
            //byte[] result= UrlEncode(buffer, 0, buffer.Length);
            return Uri.EscapeDataString(url);
        }
        /// <summary>
        /// url解码
        /// </summary>
        /// <param name="url">编码后的url</param>
        /// <returns>原始url</returns>
        public static string UrlDecode(string url)
        {
            //byte[] buffer = encoding.GetBytes(url);
            //byte[] result = UrlDecode(buffer, 0, buffer.Length);
            //return encoding.GetString(result);
            return Uri.UnescapeDataString(url);
        }
        /// <summary>
        /// html编码
        /// </summary>
        /// <param name="html">原始的html</param>
        /// <returns>编码后的html</returns>
        public static string HtmlEncode(string html)
        {
            return System.Net.WebUtility.HtmlEncode(html);
        }
        /// <summary>
        /// html解码
        /// </summary>
        /// <param name="html">编码后的html</param>
        /// <returns>原始的html</returns>
        public static string HtmlDecode(string html)
        {
            return System.Net.WebUtility.HtmlDecode(html);
        }
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
        public static byte[] GetJsonString(byte[] bytes, int offset, int count)
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
            byte[] expandedBytes = new byte[count + cWrapped];
            int pos = 0;
            //expandedBytes[pos++] = (byte)'\"';
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
            //expandedBytes[pos++] = (byte)'\"';
            return expandedBytes;
        }
    }
}
