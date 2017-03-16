using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using NFinal.Http;

namespace NFinal.Owin
{
    /// <summary>
    /// 输出Html类
    /// </summary>
    public struct HtmlWriter
    {
        public Response response;
        /// <summary>
        /// Set-Cookie的值存储
        /// </summary>
        public IDictionary<string, string> setCookies;
        Stream writeStream;
        CompressMode compressMode;
        /// <summary>
        /// Html类初始化
        /// </summary>
        /// <param name="environment">压缩方式</param>
        public HtmlWriter(Stream stream,CompressMode compressMode)
        {
            this.compressMode = compressMode;
            this.response = new Response();
            this.response.statusCode = 200;
            this.response.headers = new Dictionary<string, string[]>(StringComparer.Ordinal);
            this.setCookies = new Dictionary<string, string>(StringComparer.Ordinal);
            this.response.stream = stream;
            if (compressMode== CompressMode.GZip)
            {
                this.writeStream = new System.IO.Compression.GZipStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
                this.response.headers.Add(NFinal.Constant.HeaderContentEncoding, NFinal.Constant.HeaderContentEncodingGzip);
            }
            else if(compressMode==CompressMode.Deflate)
            {
                this.writeStream = new System.IO.Compression.DeflateStream(this.response.stream, System.IO.Compression.CompressionMode.Compress, true);
                this.response.headers.Add(NFinal.Constant.HeaderContentEncoding, NFinal.Constant.HeaderContentEncodingDeflate);
            }
            else
            {
                this.writeStream = this.response.stream;
            }
        }
        /// <summary>
        /// 写入状态码
        /// </summary>
        /// <param name="statusCode"></param>
        public void WriteStatusCode(int statusCode)
        {
            this.response.statusCode = statusCode;
        }
        /// <summary>
        /// 写入html头
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="value">value</param>
        public void WriteHeader(string key, string value)
        {
            if (this.response.headers.ContainsKey(key))
            {
                this.response.headers[key] = new string[] { value };
            }
            else
            {
                this.response.headers.Add(key, new string[] { value });
            }
        }
        /// <summary>
        /// 写入html头
        /// </summary>
        /// <param name="key">key</param>
        /// <param name="values">values</param>
        public void WriteHeader(string key,string[] values)
        {
            if (this.response.headers.ContainsKey(key))
            {
                this.response.headers[key] = values;
            }
            else
            {
                this.response.headers.Add(key, values);
            }
        }
        /// <summary>
        /// Add a new cookie and value
        /// </summary>
        /// <param name="key"></param>
        /// <param name="value"></param>
        public void SetCookie(string key, string value)
        {
            string setCookieString = Uri.EscapeDataString(key) + Constant.HeaderSetCookieEqual + Uri.EscapeDataString(value) + Constant.HeaderSetCookiePath;
            if (setCookies.ContainsKey(key))
            {
                setCookies[key] = setCookieString;

            }
            else
            {
                setCookies.Add(key, setCookieString);
            }
        }
        /// <summary>
        /// Sets an expired cookie
        /// </summary>
        /// <param name="key"></param>
        public void SetExpiredCookie(string key)
        {
            string deleteCookieString =  Uri.EscapeDataString(key) + Constant.HeaderSetCookieExpire ;
            if (setCookies.ContainsKey(key))
            {
                setCookies[key] = deleteCookieString;

            }
            else
            {
                setCookies.Add(key, deleteCookieString);
            }

            WriteHeader(Constant.HeaderSetCookie, deleteCookieString);
        }
        /// <summary>
        /// 写入重定向头
        /// </summary>
        /// <param name="url"></param>
        public void WriteRedirectHeader(string url)
        {
            WriteHeader(Constant.HeaderLocation, url);
        }
        /// <summary>
        /// 输出
        /// </summary>
        public void Flush()
        {
            if (compressMode == CompressMode.Deflate)
            {
                writeStream.Flush();
                writeStream.Dispose();
                response.stream.Flush();
            }
            else if (compressMode == CompressMode.GZip)
            {
                writeStream.Flush();
                writeStream.Dispose();
                response.stream.Flush();
            }
            else
            {
                response.stream.Flush();
            }
        }
        /// <summary>
        /// 关闭输出流并输出
        /// </summary>
        public void Close()
        {
            if (compressMode == CompressMode.Deflate)
            {
                writeStream.Flush();
                writeStream.Dispose();
            }
            else if (compressMode == CompressMode.GZip)
            {
                writeStream.Flush();
                writeStream.Dispose();
            }
        }
        /// <summary>
        /// 输出流
        /// </summary>
        /// <param name="buffer">字节数组</param>
        public void Write(byte[] buffer)
        {
            writeStream.Write(buffer, 0, buffer.Length);
        }
        /// <summary>
        /// 输出流
        /// </summary>
        /// <param name="buffer">字节数组</param>
        /// <param name="offset">起始位置</param>
        /// <param name="count">长度</param>
        public void Write(byte[] buffer,int offset,int count)
        {
            writeStream.Write(buffer, offset, count);
        }
        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <param name="value"></param>
        public void Write(string value)
        {
            byte[] buffer= Constant.encoding.GetBytes(value);
            writeStream.Write(buffer, 0, buffer.Length);
        }
       
        public NFinal.Owin.Response GetResponse()
        {
            //WriteHeader("Content-Length", response.stream.Length.ToString());
            response.stream.Seek(0, SeekOrigin.Begin);
            if (compressMode == CompressMode.Deflate)
            {
                if (writeStream != null)
                {
                    writeStream.Dispose();
                }
            }
            if (compressMode == CompressMode.GZip)
            {
                if (writeStream != null)
                {
                    writeStream.Dispose();
                }
            }
            return response;
        }
    }
}
