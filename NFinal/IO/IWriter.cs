//======================================================================
//
//        Copyright : Zhengzhou Strawberry Computer Technology Co.,LTD.
//        All rights reserved
//        
//        Application:NFinal MVC framework
//        Filename : IWriter.cs
//        Description :输出流接口
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
    /// 输出流接口
    /// </summary>
    public interface IWriter
    {
        /// <summary>
        /// 输出字节流
        /// </summary>
        /// <param name="buffer"></param>
        /// <param name="offset"></param>
        /// <param name="count"></param>
        void Write(byte[] buffer, int offset, int count);
        /// <summary>
        /// 输出字符串
        /// </summary>
        /// <param name="value">字符串</param>
        void Write(string value);
    }
}
