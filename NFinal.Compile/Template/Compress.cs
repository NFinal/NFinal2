using System;
using System.Collections.Generic;
using System.Web;
using System.IO;
using System.IO.Compression;
using System.Text;

namespace NFinal.Compile.Template
{
    /// <summary>
    /// 文本压缩类
    /// </summary>
    public class Compress
    {
        /// <summary>
        /// 返回压缩后的16进制
        /// </summary>
        /// <param name="html">html</param>
        /// <returns></returns>
        public static string GetHexGz(string html)
        {
            StringBuilder  sbHexGz=new StringBuilder();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(html);
            MemoryStream ms = new MemoryStream();
            GZipStream gz = new GZipStream(ms, CompressionMode.Compress);
            gz.Write(buffer, 0, buffer.Length);
            gz.Close();
            buffer = ms.ToArray();
            ms.Close();
            for (int i = 0; i < buffer.Length; i++)
            {
                sbHexGz.Append( buffer[i].ToString("X2"));
            }
            return sbHexGz.ToString();
        }
        /// <summary>
        /// 获取压缩后的字节流
        /// </summary>
        /// <param name="html">html</param>
        /// <returns></returns>
        public static byte[] GetBytesGz(string html)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(html);
            MemoryStream ms = new MemoryStream();
            GZipStream gz = new GZipStream(ms, CompressionMode.Compress);
            gz.Write(buffer, 0, buffer.Length);
            gz.Close();
            buffer=ms.ToArray();
            ms.Close();
            return buffer;
        }
        /// <summary>
        /// 去掉前两个字节
        /// </summary>
        /// <param name="buffer">字节流</param>
        /// <returns></returns>
        public static byte[] RemoveHeader(byte[] buffer)
        {
            byte[] result=new byte[buffer.Length -2];
            for(int i=2;i<buffer.Length;i++)
            {
                result[i-2]=buffer[i];
            }
            return result;
        }
        /// <summary>
        /// 返回deflate后的十六进制字符串
        /// </summary>
        /// <param name="html">html</param>
        /// <returns></returns>
        public static string GetHexDef(string html)
        {
            StringBuilder sbHexGz = new StringBuilder();
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(html);
            MemoryStream ms = new MemoryStream();
            DeflateStream gz = new DeflateStream(ms, CompressionMode.Compress);
            gz.Write(buffer, 0, buffer.Length);
            gz.Close();
            buffer = ms.ToArray();
            ms.Close();
            for (int i = 0; i < buffer.Length; i++)
            {
                sbHexGz.Append(buffer[i].ToString("X2"));
            }
            return sbHexGz.ToString();
        }
    }
}