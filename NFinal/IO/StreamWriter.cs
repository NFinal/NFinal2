//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : StreamWriter.cs
//        Description :输出流
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;

namespace NFinal.IO
{
    /// <summary>
    /// 输出流
    /// </summary>
    public class StreamWriter:Writer,IDisposable
    {
        /// <summary>
        /// 输出流
        /// </summary>
        public System.IO.Stream stream = null;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="stream"></param>
        public StreamWriter(System.IO.Stream stream)
        {
            this.stream = stream;
        }
        /// <summary>
        /// 释放流资源
        /// </summary>
        public void Dispose()
        {
            if (this.stream != null)
            {
                this.stream.Dispose();
            }
        }
        /// <summary>
        /// 写字节流
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        public override void Write(byte[] buffer, int offset, int count)
        {
            this.stream.Write(buffer, offset, count);
        }
        /// <summary>
        /// 写字符串
        /// </summary>
        /// <param name="value"></param>
        public override void Write(string value)
        {
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(value);
            this.stream.Write(buffer, 0, buffer.Length);
        }
    }
}
