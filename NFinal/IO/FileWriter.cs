//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : FileWriter.cs
//        Description :文件输出类
//
//        created by Lucas at  2015-5-31
//     
//        WebSite:http://www.nfinal.com
//
//======================================================================
using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace NFinal.IO
{
    /// <summary>
    /// 文件输出类
    /// </summary>
    public class FileWriter : Writer,IDisposable
    {
        /// <summary>
        /// 输出流
        /// </summary>
        public System.IO.Stream stream = null;
        /// <summary>
        /// 初始化
        /// </summary>
        /// <param name="path">文件路径</param>
        public FileWriter(string path)
        {
            this.stream = new FileStream(path,FileMode.Create);
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
        /// 写字符串
        /// </summary>
        /// <param name="value"></param>
        public override void Write(string value)
        {
            if (value == null) return;
            byte[] buffer = System.Text.Encoding.UTF8.GetBytes(value);
            this.stream.Write(buffer, 0, buffer.Length);
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
    }
}
